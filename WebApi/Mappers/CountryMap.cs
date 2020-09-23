﻿using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Mappers
{
    public class CountryMap : ClassMap<Country>
    {
        public CountryMap()
        {
            Map(a => a.Name).Name("Country");
        }
    }
}
