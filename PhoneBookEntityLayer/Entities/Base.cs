using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookEntityLayer.Entities
{
    public abstract class Base <T>
    {
        [Column(Order =1)]
        [Key] // Primary Key olmasını sağlayacak
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //idendity(1,1)
        public T Id { get; set; }
        [Column(Order = 2)]

        public DateTime CreatedDate { get; set; }
        public bool IsRemoved { get; set; }

    }
}
