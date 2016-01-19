using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Class
{
    class Cards
    {
        public enum Types
        {
            //6,7,8,9,10,11,12,13,14
            Six=6,Seven,Eight,Nine,Ten,Valet,Dama,Korol,Tuz
        }
        public enum Masts
        {
            //'♥'=1,'♦','♣','♠'
            Chirva=1,Buba,Kresta,Pika
        }
        public Types typesCard;
        public Masts typesMast;
        List<Cards> cards = new List<Cards>();

        public List<Cards> CreateKoloda()
        {
            string[] masTypes = Enum.GetNames(typeof(Types));
            string[] masMasts = Enum.GetNames(typeof(Masts));

            for (int i = 0; i < masTypes.Length; i++)
			{
                for (int j = 0; j < masMasts.Length; j++)
			{
                    Cards card = new Cards();
                    object temp = Enum.Parse(typeof(Types), masTypes[i]);
                    card.typesCard = (Types)temp;
                    object tempM = Enum.Parse(typeof(Masts), masMasts[j]);
                    card.typesMast = (Masts)tempM;
                    cards.Add(card);
			}
			}

            return cards;
        }
        public void ShowCards()
        {
            int count=0;
            foreach(Cards card in cards)
            {
                if (++count%4!=0)
	            {
                    Console.Write("{0} {1}\t", card.typesCard, card.typesMast);
	            }
                else
                {
                    Console.WriteLine("{0} {1}\t", card.typesCard, card.typesMast);
                }
            }
        }
        public List<Cards> Sort()
        {
            for(int i = 0; i < cards.Count; i++)
            {
                Random rand = new Random();
                int rnd = rand.Next(0, cards.Count);
                Cards temp = new Cards();
                temp = cards[i];
                cards[i] = cards[rnd];
                cards[rnd] = temp;
            }
            return cards;
        }
        public int CountCards
        {
            get
            {
                return cards.Count;
            }
            
        }
        public List<Cards> Razdacha(int countPlayers)
        {
            List<Cards>chastCard=new List<Cards>();
            for(int i = 0; i < 36 / countPlayers; i++)
            {
                chastCard.Add(cards[0]);
                cards.RemoveAt(0);
            }
            return chastCard;
        }
    }
}
