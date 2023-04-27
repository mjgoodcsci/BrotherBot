using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.IO;

namespace BrotherBot.Commands
{
    public class BirthdayCommands : BaseCommandModule
    {
        string filePath = @"..\..\..\Birthdays.txt";
        // !setbirthday sets birthday
        [Command("setbirthday")]
        [RequireRoles(RoleCheckMode.MatchNames, "PKF")]
        public async Task setBirthdayCommand(CommandContext ctx, string monthDay)
        {
            int dayOfTheYear = 0;
            try
            {
                int month = Int32.Parse(monthDay.Split('/')[0]);
                int day = Int32.Parse(monthDay.Split('/')[1]);
                DateTime dateTime = new DateTime(2000, month, day);
                dayOfTheYear = dateTime.DayOfYear;
            }
            catch 
            {
                await ctx.Channel.SendMessageAsync(monthDay + " is not in the correct formate, please do MM/DD");
                return;
            }

            string nameDiscriminator = ctx.Member.Username + "#" + ctx.Member.Discriminator;
            List<string> lines = File.ReadAllLines(filePath).ToList();
            bool notThere = true;
            foreach (string line in lines)
            {
                List<string> splitLine = line.Split(':').ToList();
                if (splitLine[0] == nameDiscriminator)
                {
                    notThere = false;
                    lines.Remove(line);
                    lines.Add(nameDiscriminator + ":" + dayOfTheYear);
                    break;
                }
            }
            if (notThere) 
            {
                lines.Add(nameDiscriminator + ":" + dayOfTheYear);
            }

            File.WriteAllLines(filePath, lines.ToArray());
            
            await ctx.Channel.SendMessageAsync("Birthday set");
        }


        public List<string> GetBirthdays()
        {
            var day = DateTime.Now.DayOfYear;
            List<string> lines = File.ReadAllLines(filePath).ToList();

            foreach(string line in lines)
            {
                
            }

            return new List<string>();
        }
    }
}
