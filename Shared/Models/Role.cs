using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memorize.Shared.Models
{
    public class Role
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public virtual List<User>? Users { get; set; }   
    }
}
