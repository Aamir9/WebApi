using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
   
    [Table("Street")]
    public class Street
    {
        [Key]
        public int StreetId { get; set; }
        public string Name { get; set; }

        public int PostalCodeId { get; set; }
    }
}
