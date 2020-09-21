using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Mappers
{
    public class StateMap : ClassMap<State>
    {
        public StateMap()
        {
            Map(a => a.Name).Name("Name");
         

        }
    }
}
