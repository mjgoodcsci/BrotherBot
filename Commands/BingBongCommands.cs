using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;

namespace BrotherBot.Commands
{
    public class BingBongCommands : BaseCommandModule
    {
        // !bing returns bong
        [Command("bing")]
        public async Task BingBongCommand(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("Bong!");
        }
        // !whatis takes in a name and returns "name is a bitch"
        [Command("whatis")]
        public async Task isBitch(CommandContext ctx, string name)
        {
            string answer = name + " is a bitch";
            await ctx.Channel.SendMessageAsync(answer);
        }
        // !james returns the JamesJoker emoji
        [Command("james")]
        public async Task jamesCommand(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("<:JamesJoker:737870460259270726>");
        }
        // !add
        [Command("add")]
        public async Task addCommand(CommandContext ctx, int num1, int num2)
        {
            int answer = num1 + num2;
            await ctx.Channel.SendMessageAsync(answer.ToString());
        }
    }
}
