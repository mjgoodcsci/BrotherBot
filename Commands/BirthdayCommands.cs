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
                DateTime dateTime = new DateTime(2001, month, day);
                dayOfTheYear = dateTime.DayOfYear;
            }
            catch 
            {
                await ctx.Channel.SendMessageAsync(monthDay + " is not in the correct formate, please do MM/DD");
                return;
            }

            List<string> lines = File.ReadAllLines(filePath).ToList();
            bool notThere = true;
            foreach (string line in lines)
            {
                List<string> splitLine = line.Split(':').ToList();
                if (splitLine[0] == ctx.User.Id.ToString())
                {
                    notThere = false;
                    lines.Remove(line);
                    lines.Add(ctx.User.Id.ToString() + ":" + dayOfTheYear);
                    break;
                }
            }
            if (notThere) 
            {
                lines.Add(ctx.User.Id.ToString() + ":" + dayOfTheYear);
            }

            File.WriteAllLines(filePath, lines.ToArray());
            
            await ctx.Channel.SendMessageAsync("Birthday set");
        }


        public async Task<List<ulong>> GetBirthdaysAsync()
        {
            var day = DateTime.Now.DayOfYear;
            List<ulong> ids = new List<ulong>();
            List<string> lines = File.ReadAllLines(filePath).ToList();
            foreach(string line in lines)
            {
                List<string> nameDate = line.Split(':').ToList();
                if (day == Int32.Parse(line.Split(':').ToList()[1])) 
                {
                    ids.Add(Convert.ToUInt64(nameDate[0]));
                }
            }
            ids.Sort();
            return ids;
        }
    }
}
