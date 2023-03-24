using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagment.Data.Model
{
    public class Category : BaseEntity
    {
        [Required]
        [Column(TypeName="varchar")]
        [MaxLength(50)]
        public string CategoryName { get; set; }

        public virtual List<Books> Books { get; set; }
    }
}
