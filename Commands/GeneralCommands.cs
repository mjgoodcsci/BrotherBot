using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Threading.Tasks;

namespace BrotherBot.Commands
{
    public class GeneralCommands : BaseCommandModule
    {
        [Command("brotherhelp")]
        public async Task brotherHelpCommand(CommandContext ctx)
        {
            var embedHelp = new DiscordEmbedBuilder()
            {
                Title = "List of Commands I can do:",
                Description = "!brotherhelp -- list of commands \n" +
                              "!whatis <text> -- tells you what <text> is (Only for PKF Role) \n" +
                              "!james -- sends a great emoji here \n" +
                              "!add <number> <number> -- adds two numbers together and sends the result \n" +
                              "!bing -- try it out :) \n" +
                              "!higherlower -- game where you react with an emoji whether the next card will be higher or lower or the same (Only available to PKF role and only one instance possible at a time) \n" +
                              "!setbirthday <month/day> -- adds your birthday to the bot (WIP) (Only for PKF Role) \n",
                Color = DiscordColor.Teal,
            };

            await ctx.Channel.SendMessageAsync(embedHelp);
        }

        // !whatis takes in a name and returns "name is a bitch"
        [Command("whatis")]
        [RequireRoles(RoleCheckMode.MatchNames, "PKF")]
        public async Task isBitch(CommandContext ctx, string name)
        {
            string answer = name + " is a bitch";
            await ctx.Channel.SendMessageAsync(answer);
        }
        // !james returns the JamesJoker emoji
        [Command("james")]
        
        public async Task jamesCommand(CommandContext ctx)
        {
            var jamesEmoji = DiscordEmoji.FromName(ctx.Client, ":JamesJoker:", true);
            string jamesMessage = jamesEmoji.ToString();
            await ctx.Channel.SendMessageAsync(jamesMessage);
        }
        // !add
        [Command("add")]
        public async Task addCommand(CommandContext ctx, int num1, int num2)
        {
            int answer = num1 + num2;
            await ctx.Channel.SendMessageAsync(answer.ToString());
        }
        // !bing now returns embedde bong with a link to video
        [Command("bing")]
        public async Task embeddedBingBong(CommandContext ctx)
        {
            var embedBing = new DiscordEmbedBuilder()
            {
                Title = "BONG!",
                Description = "Click the word above to find out more",
                Color = DiscordColor.Orange,
                Url = "https://www.youtube.com/watch?v=4BMqms4h_O4"
            };

            await ctx.Channel.SendMessageAsync(embed: embedBing);
        }
    }
}
