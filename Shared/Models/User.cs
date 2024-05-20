using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Memorize.Shared.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; } = 0;

        [Required (ErrorMessage = "Обязательное поле")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Длина должна быть от {2} до {1} символов")]
        public string Login { get; set; } = string.Empty;

        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "Длина должна быть от {2} до {1} символов")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "Длина должна быть от {2} до {1} символов")]
        public string Password { get; set; } = string.Empty;
        public virtual List<Deck>? Decks { get; set; }
        public virtual List<Folder>? Folders { get; set; }

    }
}