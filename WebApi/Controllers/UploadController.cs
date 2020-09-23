using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.DAL;
using WebApi.Entities;
using WebApi.FileServices;





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
        public int Country(IFormFile file)
        {
            int count = 0;
            if (file.Length > 0)
            {
                var filePath = Path.GetTempFileName();
                using (var stream = System.IO.File.Create(filePath))
                {
                    file.CopyToAsync(stream);
                }
                var _countryService = new CountryService();
                IEnumerable<Country> resultData = _countryService.ReadCSVFile(filePath);
                var countryList = _context.countrty.ToList();
               
                foreach (var item in resultData)
                {
                    
                   var  value = _context.countrty.Where(a => a.Name.ToLower() == item.Name.ToLower()).FirstOrDefault();
                    if (value==null)
                    {
                         count = count + 1;
                        _context.countrty.Add(item);

                    }
                    value = null;

                }
              
              
                _context.SaveChanges();

                return count;
           
            }

            return count;

        }


        [HttpPost]
        [Route("States/{id}")]
        [Consumes("multipart/form-data")]
        public int States(IFormFile file , int id)
        {
            int count = 0;
            if (file.Length > 0)
            {
                var filePath = Path.GetTempFileName();
                using (var stream = System.IO.File.Create(filePath))
                {
                    file.CopyToAsync(stream);
                }
                var _stateService = new StateService();
                IEnumerable<State> resultData = _stateService.ReadCSVFile(filePath,id);

                foreach (var item in resultData)
                {
                    
                    var value = _context.state.Where(a => a.Name.ToLower() == item.Name.ToLower() && a.CountryId==item.CountryId).FirstOrDefault();
                    if (value == null)
                    {
                        count = count + 1;
                        _context.state.Add(item);

                    }
                    value = null;

                }

                _context.SaveChanges();

                return count;
            
            }

            return count;

        }


        [HttpPost]
        [Route("Cities/{id}")]
        [Consumes("multipart/form-data")]
        public int Cities(IFormFile file, int id)
        {
            int count = 0;
            if (file.Length > 0)
            {
                var filePath = Path.GetTempFileName();
                using (var stream = System.IO.File.Create(filePath))
                {
                    file.CopyToAsync(stream);
                }
                var _cityService = new CityService();
                IEnumerable<City> resultData = _cityService.ReadCSVFile(filePath, id);


                foreach (var item in resultData)
                {

                    var value = _context.city.Where(a => a.Name.ToLower() == item.Name.ToLower() && a.StateId==item.StateId).FirstOrDefault();
                    if (value == null)
                    {
                        count = count + 1;
                        _context.city.Add(item);

                    }
                    value = null;

                }
                _context.SaveChanges();
                return count;
         
            }

            return count;

        }


        [HttpPost]
        [Route("PostalCodes/{id}")]
        [Consumes("multipart/form-data")]
        public int PostalCodes(IFormFile file, int id)
        {
            int count = 0;
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

                    var value = _context.PostalCode.Where(a => a.Name.ToLower() == item.Name.ToLower() && a.CityId==item.CityId).FirstOrDefault();
                    if (value == null)
                    {
                        count = count + 1;
                        _context.PostalCode.Add(item);

                    }
                    value = null;

                }
                _context.SaveChanges();

                return count;

            }

            return count;
        }



        [HttpPost]
        [Route("Streets/{id}")]
        [Consumes("multipart/form-data")]
        public int Streets(IFormFile file, int id)
        {
            int count = 0;
            if (file.Length > 0)
            {
                var filePath = Path.GetTempFileName();
                using (var stream = System.IO.File.Create(filePath))
                {
                    file.CopyToAsync(stream);
                }
                var _Service = new StreetService();
                IEnumerable<Street> resultData = _Service.ReadCSVFile(filePath, id);
                foreach (var item in resultData)
                {

                    var value = _context.street.Where(a => a.Name.ToLower() == item.Name.ToLower() && a.PostalCodeId==item.PostalCodeId).FirstOrDefault();
                    if (value == null)
                    {
                        count = count + 1;
                        _context.street.Add(item);

                    }
                    value = null;

                }
                _context.SaveChanges();
                return count;
            }

            return count;
        }


        [HttpPost]
        [Route("Houses/{id}")]
        [Consumes("multipart/form-data")]
        public int Houses(IFormFile file, int id)
        {
            int count = 0;
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
                    var value = _context.house.Where(a => a.Name.ToLower() == item.Name.ToLower() && a.StreetId == item.StreetId).FirstOrDefault();
                    if (value == null)
                    {
                        count = count + 1;
                        _context.house.Add(item);

                    }
                    value = null;
               


                }
                _context.SaveChanges();
                return count;
            }

            return count;
        }
    }
}
