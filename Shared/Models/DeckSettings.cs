using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Memorize.Shared.Models
{
    public class DeckSettings
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int EasyRememberFactor { get; set; }
        public int GoodRememberFactor { get; set; }
        public int HardRememberFactor { get; set; }
        public int DontRememberFactor { get; set; }
        public virtual User User { get; set; }
        public virtual List<Deck> Decks { get; set; }

    }

}