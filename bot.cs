using BrotherBot.Commands;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext.Exceptions;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using Newtonsoft.Json;
using System.Text;
using System.Timers;

namespace BrotherBot
{
    public class bot
    {
        public DiscordClient Client { get; private set; }
        public InteractivityExtension Interactivity { get; private set; }
        public CommandsNextExtension Commands { get; private set; }

        private BirthdayCommands _birthdayCommands = new BirthdayCommands();

        public async Task RunAsync()
        {
            var json = string.Empty;
            using (var fs = File.OpenRead("config.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = await sr.ReadToEndAsync();

            var configJson = JsonConvert.DeserializeObject<ConfigJSON>(json);

            var config = new DiscordConfiguration()
            {
                Intents = DiscordIntents.All,
                Token = configJson.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
            };

            Client = new DiscordClient(config);
            Client.UseInteractivity(new InteractivityConfiguration()
            {
                Timeout = TimeSpan.FromMinutes(2)
            });

            var commandsConfig = new CommandsNextConfiguration()
            {
                StringPrefixes = new string[] { configJson.Prefix },
                EnableMentionPrefix = true,
                EnableDms = true,
                EnableDefaultHelp = false
            };

            Commands = Client.UseCommandsNext(commandsConfig);

            Commands.RegisterCommands<GeneralCommands>();
            Commands.RegisterCommands<BirthdayCommands>();
            Commands.RegisterCommands<HigherLowerGameCommands>();

            Commands.CommandErrored += onCommandError;

            await Client.ConnectAsync();

            await HourTimer();

            //keeps bot up
            await Task.Delay(-1);

        }

        //command error (so far only checks for cooldown error
        private async Task onCommandError(CommandsNextExtension sender, CommandErrorEventArgs args)
        {
            if(args.Exception is ChecksFailedException)
            {
                var castedException = (ChecksFailedException) args.Exception ;
                string cooldownTimer = string.Empty;

                foreach (var check in castedException.FailedChecks)
                {
                    var cooldown = (CooldownAttribute) check;
                    TimeSpan timeleft = cooldown.GetRemainingCooldown(args.Context);
                    cooldownTimer = timeleft.ToString(@"hh\:mm\:ss");
                }

                var cooldownMessage = new DiscordEmbedBuilder()
                {
                    Title = "Wait for the cooldown to end.",
                    Description = "Remaining time: " + cooldownTimer,
                    Color = DiscordColor.IndianRed
                };

                await args.Context.Channel.SendMessageAsync(cooldownMessage);
            }
        }

        private Task OnClientReady(ReadyEventArgs e)
        {
            return Task.CompletedTask;
        }

        private async Task HourTimer()
        {
            var timer = new System.Timers.Timer(3600000); //hour timer
            timer.Elapsed += async (sender, e) =>
            {
                var lastCheckedAt = DateTime.Now;
                if(lastCheckedAt.Hour == 4)
                {
                    DiscordChannel channel = await Client.GetChannelAsync(199035901622353920);
                    List<ulong> ids = await _birthdayCommands.GetBirthdaysAsync();
                    foreach (ulong id in ids)
                    {
                        DiscordUser user = await Client.GetUserAsync(id);

                        var birthdayMessage = new DiscordEmbedBuilder()
                        {
                            Description = "Happy Birthday " + user.Mention + "!",
                            Color = DiscordColor.Teal
                        };
                        await channel.SendMessageAsync(birthdayMessage);
                    }
                }
            };
            timer.Start();
        }
    }
}
