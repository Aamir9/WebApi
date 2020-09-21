using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
  
    [Table("PostalCode")]
    public class PostalCode
    {
        [Key]
        public int PostalCodeId { get; set; }
        public string Name { get; set; }
        public int CityId { get; set; }
    }
}
