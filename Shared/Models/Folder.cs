using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Memorize.Shared.Models
{
    public class Folder
    {
        [Key]
        public int ID { get; set; } = 0;

        [MaxLength(64)]
        public string Name { get; set; } = string.Empty;
        public int UserID { get; set; }
        public virtual List<Deck>? Decks { get; set; } 
        public virtual User? User { get; set; } 
    }
}