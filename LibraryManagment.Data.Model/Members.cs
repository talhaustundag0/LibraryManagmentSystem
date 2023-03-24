using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagment.Data.Model
{
    public class Members: BaseEntity
    {
        [Required]
        [Column(TypeName = "varchar")]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "varchar")]
        [MaxLength(50)]
        public string Lastname { get; set; }
        [Required]
        [Column(TypeName = "char")]
        [MaxLength(11), MinLength(11)]
        public string TCKNO { get; set; }

        [Required]
        [Column(TypeName = "char")]
        [MaxLength(11), MinLength(11)]
        public string Phone { get; set; }
        [Column(TypeName = "nvarchar")]
        [MaxLength(100)]
        public string Mail { get; set; }
        public string Password { get; set; }
        public DateTime DORegistration { get; set; }
        public virtual List<GivenBooks> OduncKitaplar { get; set; }
    }
}
