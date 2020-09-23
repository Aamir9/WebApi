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
    public class StateService
    {
        public IEnumerable<State> ReadCSVFile(string location,int CountryId)
        {
            try
            {
                //CultureInfo.CurrentCulture
                //Encoding.Default
                //Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US", false);
                using (var reader = new StreamReader(location))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
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
