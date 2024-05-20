using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Memorize.Shared.Models
{
    public class Card
    {
        [Key]
        public int ID { get; set; } = 0;
        public string FrontSide { get; set; } = string.Empty;
        public string BackSide { get; set; } = string.Empty;
        public byte[]? Image { get; set; } = new byte[0];
        public DateTime? NextAppear { get; set; } = DateTime.Today;
        public int DayCounter { get; set; } = 0;
        public int DeckID { get; set; }
        public virtual Deck? Deck { get; set; } 
    }
}