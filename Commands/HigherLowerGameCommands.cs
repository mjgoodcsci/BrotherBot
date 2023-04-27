using BrotherBot.ExternalClasses;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrotherBot.Commands
{
    public class HigherLowerGameCommands : BaseCommandModule
    {
        // !higherlower game where user reacts to an arrow emoji to guess whether the next card will
        // be higher or lower than previous card
        [Command("higherlower")]
        [RequireRoles(RoleCheckMode.MatchNames, "PKF")]
        [Cooldown(1, 16, CooldownBucketType.Global)]
        public async Task HigherLowerCardGame(CommandContext ctx)
        {
            var interactivity = ctx.Client.GetInteractivity();
            TimeSpan timer = TimeSpan.FromSeconds(15);

            DiscordEmoji[] optionsEmojis =
            {
                DiscordEmoji.FromName(ctx.Client, ":arrow_up:", false),
                DiscordEmoji.FromName(ctx.Client, ":arrow_down:", false),
                DiscordEmoji.FromName(ctx.Client, ":left_right_arrow:", false)
            };


            var firstCard = new CardBuilder();

            var firstCardMessage = new DiscordEmbedBuilder()
            {
                Title = "Higher or Lower?",
                Description = "This card is a(n): " + firstCard.selectedCard,
                Color = DiscordColor.DarkGreen
            };

            
            var putReactOn = await ctx.Channel.SendMessageAsync(firstCardMessage);

            foreach (var emoji in optionsEmojis)
            {
                await putReactOn.CreateReactionAsync(emoji);
            }
            var secondCard = new CardBuilder();

            var secondCardMessage = new DiscordEmbedBuilder()
            {
                Title = "The card you're betting against",
                Description = "This card is a(n): " + secondCard.selectedCard,
                Color = DiscordColor.DarkGreen
            };


            var collectedanswers = await interactivity.CollectReactionsAsync(putReactOn, timer);

            int countHigher = 0;
            int countLower = 0;
            int countSame = 0;


            foreach (var emoji in collectedanswers)
            {
                if (emoji.Emoji == optionsEmojis[0])
                {
                    countHigher++;
                }
                if (emoji.Emoji == optionsEmojis[1])
                {
                    countLower++;
                }
                if (emoji.Emoji == optionsEmojis[2])
                {
                    countSame++;
                }
            }
            int countWinner = 0;
            int countLoser = 0;

            await ctx.Channel.SendMessageAsync(secondCardMessage);

            if (firstCard.selectedNum < secondCard.selectedNum)
            {
                countWinner = countHigher;
                countLoser = countLower + countSame;
                var higherMessage = new DiscordEmbedBuilder()
                {
                    Title = "Higher Wins!",
                    Description = "Congratulations to the " + countWinner + " winner(s) \n" +
                    "better luck next time to the other " + countLoser + " people.",
                    Color = DiscordColor.Blue
                };
                await ctx.Channel.SendMessageAsync(higherMessage);
            }
            else if (firstCard.selectedNum > secondCard.selectedNum)
            {
                countWinner = countLower;
                countLoser = countHigher + countSame;
                var lowerMessage = new DiscordEmbedBuilder()
                {
                    Title = "Lower Wins!",
                    Description = "Congratulations to the " + countWinner + " winner(s)! \n" +
                    "better luck next time to the other " + countLoser + " people.",
                    Color = DiscordColor.Red
                };
                await ctx.Channel.SendMessageAsync(lowerMessage);
            }
            else
            {
                countWinner = countSame;
                countLoser = countHigher + countLower;
                var drawMessage = new DiscordEmbedBuilder()
                {
                    Title = "It's the Same Number!!",
                    Description = "Congratulations to the " + countWinner + " winner(s)! \n" +
                    "better luck next time to the other " + countLoser + " people.",
                    Color = DiscordColor.Yellow
                };
                await ctx.Channel.SendMessageAsync(drawMessage);
            }

        }
    }
}
