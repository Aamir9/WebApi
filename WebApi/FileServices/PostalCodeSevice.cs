using System;
using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using WebApi.Entities;
using WebApi.Mappers;
using System.Threading;

namespace WebApi.FileServices
{
    public class PostalCodeSevice
    {
        public IEnumerable<PostalCode> ReadCSVFile(string location , int id)
        {
            try
            {
                //Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US", false);
                using (var reader = new StreamReader(location))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {

                    csv.Configuration.RegisterClassMap<PostalCodeMap>();
                    var records = csv.GetRecords<PostalCode>().ToList();
                    foreach (var item in records)
                    {
                        item.CityId = id;
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
