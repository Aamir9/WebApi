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
    public class StateService
    {
        public List<State> ReadCSVFile(string location,int CountryId)
        {
            try
            {
                using (var reader = new StreamReader(location, Encoding.Default))
                using (var csv = new CsvReader(reader, CultureInfo.CurrentCulture))
                {

                    csv.Configuration.RegisterClassMap<StateMap>();
                    var records = csv.GetRecords<State>().ToList();

                    foreach (var item in records)
                    {
                        item.CountryId = CountryId;
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
