using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CitiesInfo.API.Models;
using CitiesInfo.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CitiesInfo.API.Controllers{
    [Route("api/cities")]    
    public class CitiesController: Controller
    {
        private ICityInfoRepository _cityInfoRepository;

        public CitiesController(ICityInfoRepository cityInfoRepository)
        {
            _cityInfoRepository = cityInfoRepository;
        }

        [HttpGet()]
        public IActionResult GetCities(bool includePointsOfInterest = false)
        {
            var cityEntities = _cityInfoRepository.GetCities(includePointsOfInterest);

            if(includePointsOfInterest){
                var results = Mapper.Map<IEnumerable<CityDTO>>(cityEntities);
                return Ok(results);
            }else{
                var results = Mapper.Map<IEnumerable<CityWithoutPointsOfInterestDTO>>(cityEntities);
                return Ok(results);
            }


            // var results = new List<CityWithoutPointsOfInterestDTO>();
            
            // foreach (var cityEntity in cityEntities)
            // {
            //     results.Add(
            //         new CityWithoutPointsOfInterestDTO(){
            //             Id = cityEntity.Id,
            //             Name = cityEntity.Name,
            //             Description = cityEntity.Description
            //         }
            //     );
            // }

            // return Ok(CitiesDataStore.Current.Cities);
        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id, bool includePointsOfInterest = false)
        {

            if(!_cityInfoRepository.CityExists(id)){
                return NotFound();
            }

            var city = _cityInfoRepository.GetCity(id, includePointsOfInterest);

            if(includePointsOfInterest)
            {

                var cityResult = Mapper.Map<CityDTO>(city);
                
                return Ok(cityResult);

                // var cityResult = new CityDTO()
                // {
                //     Id = city.Id,
                //     Name = city.Name,
                //     Description = city.Description
                // };

                // foreach (var poi in city.PointsOfInterest)
                // {
                //     cityResult.PointsOfInterest.Add(
                //         new PointOfInterestDTO(){
                //             Id = poi.Id,
                //             Name = poi.Name,
                //             Description = poi.Description
                //         }
                //     );
                // }

                // return Ok(cityResult);
            }

            var cityWithoutPointsOfInterestResult = Mapper.Map<CityWithoutPointsOfInterestDTO>(city);

            return Ok(cityWithoutPointsOfInterestResult);

            // var cityWithoutPointsOfInterestResult = 
            //     new CityWithoutPointsOfInterestDTO()
            //     {
            //         Id = city.Id,
            //         Name = city.Name,
            //         Description = city.Description
            //     };
            
            // return Ok(cityWithoutPointsOfInterestResult);
            
            // var cityToReturn = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id);

            // if(cityToReturn == null)
            // {
            //     return NotFound();
            // }

            // return Ok(cityToReturn);
        }

    } 

}
