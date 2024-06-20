using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memorize.Shared.Models
{
    public class Report
    {
        [Key]
        public int ID { get; set; }

        [MaxLength(255)]
        public string Message { get; set; } = string.Empty;
        public DateTime? Date { get; set; } = DateTime.Now;
        public bool IsHandled { get; set; }
        public int DeckID { get; set; }
        public virtual Deck? Deck { get; set; }
    }
}
