using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using CsvHelper;
using ExcelDataReader;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WebApi.DAL;
using WebApi.Entities;
using WebApi.FileServices;



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly MyContext _context;
      

        public UploadController(MyContext context)
        {
            _context = context;
            
        }

        // GET: api/<UploadController>
        [HttpPost]
        [Route("Country")]
        [Consumes("multipart/form-data")]
        public void Country(IFormFile file)
        {  
            if (file.Length > 0)
            {
                var filePath = Path.GetTempFileName();
                using (var stream = System.IO.File.Create(filePath))
                {
                    file.CopyToAsync(stream);
                }
                var _countryService = new CountryService();
                List<Country> resultData = _countryService.ReadCSVFile(filePath);
                var countryList = _context.countrty.ToList();
               
                foreach (var item in resultData)
                {
                    
                   var  value = _context.countrty.Where(a => a.Name == item.Name).FirstOrDefault();
                    if (value==null)
                    {
                        _context.countrty.Add(item);

                    }
                    value = null;

                }
              
              
                _context.SaveChanges();
           
            }

  }


        [HttpPost]
        [Route("States/{id}")]
        [Consumes("multipart/form-data")]
        public void States(IFormFile file , int id)
        {
            if (file.Length > 0)
            {
                var filePath = Path.GetTempFileName();
                using (var stream = System.IO.File.Create(filePath))
                {
                    file.CopyToAsync(stream);
                }
                var _stateService = new StateService();
                List<State> resultData = _stateService.ReadCSVFile(filePath,id);

                foreach (var item in resultData)
                {

                    var value = _context.state.Where(a => a.Name == item.Name).FirstOrDefault();
                    if (value == null)
                    {
                        _context.state.Add(item);

                    }
                    value = null;

                }

                _context.SaveChanges();
            
            }
         


        }


        [HttpPost]
        [Route("Cities/{id}")]
        [Consumes("multipart/form-data")]
        public void Cities(IFormFile file, int id)
        {
            if (file.Length > 0)
            {
                var filePath = Path.GetTempFileName();
                using (var stream = System.IO.File.Create(filePath))
                {
                    file.CopyToAsync(stream);
                }
                var _cityService = new CityService();
                List<City> resultData = _cityService.ReadCSVFile(filePath, id);


                foreach (var item in resultData)
                {

                    var value = _context.city.Where(a => a.Name == item.Name).FirstOrDefault();
                    if (value == null)
                    {
                        _context.city.Add(item);

                    }
                    value = null;

                }
                _context.SaveChanges();
         
            }
           

        }


        [HttpPost]
        [Route("PostalCodes/{id}")]
        [Consumes("multipart/form-data")]
        public void PostalCodes(IFormFile file, int id)
        {
            if (file.Length > 0)
            {
                var filePath = Path.GetTempFileName();
                using (var stream = System.IO.File.Create(filePath))
                {
                    file.CopyToAsync(stream);
                }
                var _Service = new PostalCodeSevice();
                var resultData = _Service.ReadCSVFile(filePath, id);


                foreach (var item in resultData)
                {

                    var value = _context.PostalCode.Where(a => a.Name == item.Name).FirstOrDefault();
                    if (value == null)
                    {
                        _context.PostalCode.Add(item);

                    }
                    value = null;

                }
                _context.SaveChanges();

            }


        }



        [HttpPost]
        [Route("Streets/{id}")]
        [Consumes("multipart/form-data")]
        public void Streets(IFormFile file, int id)
        {
            if (file.Length > 0)
            {
                var filePath = Path.GetTempFileName();
                using (var stream = System.IO.File.Create(filePath))
                {
                    file.CopyToAsync(stream);
                }
                var _Service = new StreetService();
                var resultData = _Service.ReadCSVFile(filePath, id);


                foreach (var item in resultData)
                {

                    var value = _context.street.Where(a => a.Name == item.Name).FirstOrDefault();
                    if (value == null)
                    {
                        _context.street.Add(item);

                    }
                    value = null;

                }
                _context.SaveChanges();

            }


        }


        [HttpPost]
        [Route("Houses/{id}")]
        [Consumes("multipart/form-data")]
        public void Houses(IFormFile file, int id)
        {
            if (file.Length > 0)
            {
                var filePath = Path.GetTempFileName();
                using (var stream = System.IO.File.Create(filePath))
                {
                    file.CopyToAsync(stream);
                }
                var _Service = new HouseService();
                var resultData = _Service.ReadCSVFile(filePath, id);
                foreach (var item in resultData)
                {
                var value = _context.house.Where(a => a.Name == item.Name).FirstOrDefault();
                    if (value == null)
                    {
                        _context.house.Add(item);

                    }
                    value = null;

                }
                _context.SaveChanges();

            }


        }
    }
}
