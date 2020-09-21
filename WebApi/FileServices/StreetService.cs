using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Mappers;

namespace WebApi.FileServices
{
    public class StreetService
    {
        public List<Street> ReadCSVFile(string location, int id)
        {
            try
            {
                using (var reader = new StreamReader(location, Encoding.Default))
                using (var csv = new CsvReader(reader, CultureInfo.CurrentCulture))
                {

                    csv.Configuration.RegisterClassMap<StreetMap>();
                    var records = csv.GetRecords<Street>().ToList();
                    foreach (var item in records)
                    {
                        item.PostalCodeId = id;
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
