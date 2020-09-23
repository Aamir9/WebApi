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
    public class CityService
    {
        public IEnumerable<City> ReadCSVFile(string location, int id)
        {
            try
            {
                //Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US", false);
                using (var reader = new StreamReader(location))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {

                    csv.Configuration.RegisterClassMap<CityMap>();
                    var records = csv.GetRecords<City>().ToList();

                    foreach (var item in records)
                    {
                        item.StateId = id;
                    }

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
