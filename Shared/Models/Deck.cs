using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Memorize.Shared.Models
{

    public class Deck
    {
        [Key]
        public int ID { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsPublic { get; set; } = true;
        public double EasyRememberFactor { get; set; } = 3;
        public double GoodRememberFactor { get; set; } = 2;
        public double HardRememberFactor { get; set; } = 1;
        public double DontRememberFactor { get; set; } = 0;
        public int UserID { get; set; }
        public int? FolderID { get; set; }
        public virtual Folder? Folder { get; set; }
        public virtual User? User { get; set; }
        public virtual List<Card>? Cards { get; set; }

    }
}