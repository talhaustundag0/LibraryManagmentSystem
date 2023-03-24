using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagment.Data.Model
{
    public class GivenBooks: BaseEntity
    {
        [Required]
        public int BookID { get; set; }
        [Required]
        public int MemberID { get; set; }
        [Required]
        public DateTime Receiving { get; set; }
        [Required]
        public DateTime Delivery { get; set; }
        public DateTime? Delivered { get; set; }

        public virtual Members Member { get; set; }

        public virtual Books Book { get; set; }
    }
}
