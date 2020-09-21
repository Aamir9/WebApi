using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Mappers
{
    public class PostalCodeMap : ClassMap<PostalCode>
    {
        public PostalCodeMap()
        {
            Map(a => a.Name).Name("Name");
        }
    }
}
