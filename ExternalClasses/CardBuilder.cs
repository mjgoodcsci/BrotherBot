using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrotherBot.ExternalClasses
{
    internal class CardBuilder
    {
        public int[] cardNums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };
        public string[] cardSuits = { "Clubs", "Spades", "Diamonds", "Hearts" };

        public int selectedNum { get; internal set; }
        public string selectedCard { get; internal set; }

        public CardBuilder()
        {
            var Random = new Random();
            int indexNums = Random.Next(0, this.cardNums.Length -1);
            int indexSuit = Random.Next(0, this.cardSuits.Length - 1);

            this.selectedNum = this.cardNums.ElementAt(indexNums);
            this.selectedCard = this.cardNums.ElementAt(indexNums) + " of " + this.cardSuits.ElementAt(indexSuit);


        }
    }
}
