using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagment.Data.Model
{
    public class Books : BaseEntity
    {
        [Required]
        [Column(TypeName = "varchar")]
        [MaxLength(50)]
        public string name { get; set; }
        [Required]
        public int piece { get; set; }
        [Required]
        public DateTime DOAdd { get; set; }
        [Required]
        public int WriterID { get; set; }
        public virtual Writer Writer { get; set; }

        public virtual List<Category> Categories { get; set; }
    }
}
