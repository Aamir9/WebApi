using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.DAL;
using WebApi.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {

        private readonly MyContext _context;

        public CountriesController(MyContext context)
        {
            _context = context;
        }
       

        /// <summary>
        /// Get all api request 
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [Route("api/Countries")]
        public async Task<IEnumerable<Country>> Get()
        {
            return await _context.countrty.ToListAsync();
        }


        [HttpGet]
        [Route("api/GetStates")]
        public async Task<IEnumerable<State>> GetStates()
        {
           return await _context.state.ToListAsync();
           
        }

        [HttpGet]
        [Route("api/GetCities")]
        public async Task<IEnumerable<City>> GetCities()
        {
            return await _context.city.ToListAsync();

        }

        [HttpGet]
        [Route("api/GetPostalCodes")]
        public async Task<IEnumerable<PostalCode>> GetPostalCodes()
        {
            return await _context.PostalCode.ToListAsync();

        }

        [HttpGet]
        [Route("api/GetStreets")]
        public async Task<IEnumerable<Street>> GetStreets()
        {
            return await _context.street.ToListAsync();

        }

        [HttpGet]
        [Route("api/GetHouses")]
        public async Task<IEnumerable<House>> GetHouses()
        {
            return await _context.house.ToListAsync();

        }






        /// <summary>
        /// Get all api request  by Id
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Country/States/{countryId}")]
        public async Task<IEnumerable<State>> States(int countryId)
        {
            List<State> states = new List<State>();
             if(countryId >0)
            {
                states= await _context.state.Where(a => a.CountryId == countryId).ToListAsync();
            }
            return states;
        }

        [HttpGet]
        [Route("api/Country/Cities/{Id}")]
        public async Task<IEnumerable<City>> Cities(int Id)
        {
           return await _context.city.Where(a => a.StateId == Id).ToListAsync();
          
        }


        [HttpGet]
        [Route("api/Country/PostalCodes/{Id}")]
        public async Task<IEnumerable<PostalCode>> PostalCodes(int Id)
        {
            return await _context.PostalCode.Where(a => a.CityId == Id).ToListAsync();

        }


        [HttpGet]
        [Route("api/Country/Streets/{Id}")]
        public async Task<IEnumerable<Street>> Streets(int Id)
        {
            return await _context.street.Where(a => a.PostalCodeId == Id).ToListAsync();
        }


        [HttpGet]
        [Route("api/Country/Houses/{Id}")]
        public async Task<IEnumerable<House>> Houses(int id)
        {
            return await _context.house.Where(a => a.StreetId == id).ToListAsync();

        }



        /// <summary>
        /// Get list of data country state etc according to ... 
        /// </summary>
        /// <param name="stateId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/StateCities")]
        public async Task<IEnumerable<City>> StateCities(int stateId)
        {
            List<City> cities=new List<City>();
            if (stateId >0)
            {
                 int id = await _context.state.Where(a => a.StateId == stateId).Select(s => s.StateId).SingleOrDefaultAsync();
                 cities =await  _context.city.Where(c => c.StateId == id).ToListAsync();
              }

            return cities;
        }


        [HttpPost]
        [Route("api/CountryCities")]
        public async Task<IEnumerable<City>> CountryCities(int countryId)
        {
            List<City> cities=new List<City>();
            if(countryId > 0) { 
            var states = await States(countryId);
            foreach (var item in states)
            {
                var values= await StateCities(item.StateId);
                cities.AddRange(values);
            }
            }
            return cities;
        }


        [HttpPost]
        [Route("api/CityPostalCode")]
        public async Task<IEnumerable<PostalCode>> CityPostalCode(int cityId)
        {
            List<PostalCode> postCodes = new List<PostalCode>();
            if (cityId > 0)
            {
                postCodes = await _context.PostalCode.Where(a => a.CityId == cityId).ToListAsync();
              
            }
          return postCodes;
        }

       
        [HttpPost]
        [Route("api/StatePostalCode")]
        public async Task<IEnumerable<PostalCode>> StatePostalCode(int stateId)
        {
            List<PostalCode> postalCode = new List<PostalCode>();
            if (stateId >0)
            {
                var cities = await StateCities(stateId);
                foreach (var item in cities)
                {
                    var values = await CityPostalCode(item.CityId);
                    postalCode.AddRange(values);
                }
            }
            return postalCode;
        }

        [HttpPost]
        [Route("api/CountryPostalCode")]
        public async Task<IEnumerable<PostalCode>> CountryPostalCode(int countryId)
        {
            List<PostalCode> postalCode = new List<PostalCode>();
            List<City> cityList = new List<City>();
            if (countryId >0)
            {
                var states = await States(countryId);
                foreach (var item in states)
                {
                  var values= await StateCities(item.StateId);
                  cityList.AddRange(values);

                }

                foreach (var city in cityList)
                {
                    var values = await CityPostalCode(city.CityId);
                    postalCode.AddRange(values);
                }

            }
            
            return postalCode;
        }


        [HttpPost]
        [Route("api/PostalCodeStreetNumbers")]
        public async Task<IEnumerable<Street>> PostalCodeStreetNumbers(int postcodeId)
        {
            List<Street> postcodeList = new List<Street>();
            if (postcodeId > 0)
            {
                postcodeList = await _context.street.Where(a => a.PostalCodeId == postcodeId).ToListAsync();
            }
            return postcodeList;
        }

        [HttpPost]
        [Route("api/CityStreetNumbers")]
        public async Task<IEnumerable<Street>> CityStreetNumbers(int cityId)
        {
            List<Street> streetList = new List<Street>();
            if (cityId  > 0)
            {
             var postcodesList= await CityPostalCode(cityId);
                foreach (var item in postcodesList)
                {
                 var values= await PostalCodeStreetNumbers(item.PostalCodeId);
                    streetList.AddRange(values);
                }
            }
            return streetList;
            
        }

        [HttpPost]
        [Route("api/StateStreetNumbers")]
        public async Task<IEnumerable<Street>> StateStreetNumbers(int stateId)
        {
            List<Street> streetList = new List<Street>();
            List<PostalCode> postCodeList = new List<PostalCode>();
            if (stateId >0)
            {
                var citiesList =await StateCities(stateId);
                foreach (var item in citiesList)
                {
                 var postcodesValues = await CityPostalCode(item.CityId);
                    postCodeList.AddRange(postcodesValues);
                }


                foreach (var postcodeItem in postCodeList)
                {
                    var values = await PostalCodeStreetNumbers(postcodeItem.PostalCodeId);
                    streetList.AddRange(values);
                }

            }

            return streetList;
        }


        [HttpPost]
        [Route("api/CountryStreetNumbers")]
        public async Task<IEnumerable<Street>> CountryStreetNumbers(int countryId)
        {
            List<Street> streetList = new List<Street>();
            List<PostalCode> postCodeList = new List<PostalCode>();
            List<City> cityList = new List<City>();

            if (countryId >0)
            {
                var stateList = await States(countryId);
                foreach (var stateItem in stateList)
                {
                  var  values = await StateCities(stateItem.StateId);
                   cityList.AddRange(values);
                }

                foreach (var item in cityList)
                {
                    var postcodesValues = await CityPostalCode(item.CityId);
                    postCodeList.AddRange(postcodesValues);
                }

                foreach (var postcodeItem in postCodeList)
                {
                    var values = await PostalCodeStreetNumbers(postcodeItem.PostalCodeId);
                    streetList.AddRange(values);
                }

            }

            return streetList;
        }

        [HttpPost]
        [Route("api/StreetHouseNumbers")]
        public async Task<IEnumerable<House>> StreetHouseNumbers(int streetId)
        {
            List<House> houseList = new List<House>();
            if (streetId > 0)
            {
                houseList = await _context.house.Where(a => a.StreetId == streetId).ToListAsync();
             }            
            return houseList;

        }
    }
}
