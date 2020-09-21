using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    [Table("House")]
    public class House
    {
        [Key]
        public int HouseId { get; set; }
        public string Name { get; set; }

        public int StreetId { get; set; }
    }
}
