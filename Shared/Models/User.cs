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

        [Required (ErrorMessage = "������������ ����")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "����� ������ ���� �� {2} �� {1} ��������")]
        public string Login { get; set; } = string.Empty;

        [Required(ErrorMessage = "������������ ����")]
        [EmailAddress(ErrorMessage = "������������ �����")]
        [StringLength(30, MinimumLength = 0, ErrorMessage = "����� ������ ���� �� {2} �� {1} ��������")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "������������ ����")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "����� ������ ���� �� {2} �� {1} ��������")]
        public string Password { get; set; } = string.Empty;
        public byte[]? Image { get; set; } = null;
        public int RoleID { get; set; }
        public virtual List<Deck>? Decks { get; set; }
        public virtual List<Folder>? Folders { get; set; }
        public virtual Role? Roles { get; set; }

    }
}