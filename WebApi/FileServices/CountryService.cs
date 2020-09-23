using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Mappers;

namespace WebApi.FileServices
{
    public class CountryService
    {
        public IEnumerable<Country> ReadCSVFile(string location)
        {
            try
            {
                
                using (var reader = new StreamReader(location))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                   
                    csv.Configuration.RegisterClassMap<CountryMap>();
                    var records = csv.GetRecords<Country>().ToList();
                    return records;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
