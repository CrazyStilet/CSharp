using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loger.Class
{
    class Player
    {
        List<Cards> listCards = new List<Cards>();
        internal List<Cards> ListCards
        {
            get
            {
                return listCards;
            }
            set
            {
                listCards = value;
            }
        }
        public void ShowPlayerCards()
        {
            int count = 0;
            foreach(Cards card in listCards)
            {
                if(++count%4!=0)
                {
                    Console.Write("{0} {1}\t", card.typesCard, card.typesMast);
                }
                else
                {
                    Console.WriteLine("{0} {1}\t", card.typesCard, card.typesMast);
                }
            }
        }
        public int CountPlayerCards
        {
            get
            {
                return listCards.Count;
            }
        }
    }
}
