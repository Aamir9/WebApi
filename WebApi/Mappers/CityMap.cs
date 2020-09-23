using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Mappers
{
    public class CityMap : ClassMap<City>
    {
        public CityMap()
        {
            //Map(a => a.Name).Name("City");
            Map(a =>a.Name).ConvertUsing(row => row.GetField<string>("City"));

        }
    }

}
