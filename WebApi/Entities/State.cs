using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    [Table("State")]
    public class State
    {
        [Key]
        public int StateId { get; set; }
        public string Name { get; set; }
        public int CountryId{ get; set; }
    }
}

