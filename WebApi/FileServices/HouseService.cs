using System;
using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using WebApi.Entities;
using WebApi.Mappers;

namespace WebApi.FileServices
{
    public class HouseService
    {
   
            public List<House> ReadCSVFile(string location, int id)
            {
                try
                {
                    using (var reader = new StreamReader(location, Encoding.Default))
                    using (var csv = new CsvReader(reader, CultureInfo.CurrentCulture))
                    {

                        csv.Configuration.RegisterClassMap<HouseMap>();
                        var records = csv.GetRecords<House>().ToList();
                        foreach (var item in records)
                        {
                            item.StreetId = id;
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
