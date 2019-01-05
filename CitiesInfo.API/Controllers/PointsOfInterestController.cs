using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CitiesInfo.API.Models;
using CitiesInfo.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CitiesInfo.API.Controllers
{   
    [Route("api/cities")]
    public class PointsOfInterestController : Controller
    {   
        private ILogger<PointsOfInterestController> _log;
        private IMailService _mail;
        private ICityInfoRepository _cityInfoRepository;

        public PointsOfInterestController(ILogger<PointsOfInterestController> log, IMailService mail, 
            ICityInfoRepository cityInfoRepository)
        {
            _log = log; 
            _mail = mail;
            _cityInfoRepository = cityInfoRepository;
        }

        [HttpGet("{cityId}/pointsofinterest")]
        public IActionResult GetPointsOfInterest(int cityId)
        {

            if(!_cityInfoRepository.CityExists(cityId))
            {
                _log.LogInformation("Cidade não existe.");
                return NotFound();
            }

            var poiEntities = _cityInfoRepository.GetPointsOfInterestForCity(cityId);

            var results = Mapper.Map<IEnumerable<PointOfInterestDTO>>(poiEntities);

            return Ok(results);
        }

        [HttpGet("{cityId}/pointsofinterest/{id}", Name = "GetPointOfInterest")]
        public IActionResult GetPointOfInterest(int cityId, int id)
        {

            if(!_cityInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }

            var pointOfInterest = _cityInfoRepository.GetPointOfInterestForCity(cityId, id);

            if(pointOfInterest == null)
            {
                return NotFound();
            }
            
            var result = Mapper.Map<PointOfInterestDTO>(pointOfInterest);

            return Ok(result);
        }

        [HttpPost("{cityId}/pointsofinterest")]
        public IActionResult CreatePointOfInterest(int cityId, 
            [FromBody] PointOfInterestForCreateDTO pointOfInterest){
            
            if(pointOfInterest == null) return BadRequest();

            if(pointOfInterest.Description == pointOfInterest.Name) {
                ModelState.AddModelError("Description", "Descrição não pode ser igual ao nome.");
            }

            if(!ModelState.IsValid) return BadRequest(ModelState);
            
            if(!_cityInfoRepository.CityExists(cityId)) return NotFound();

            var finalPoint = Mapper.Map<Entities.PointOfInterest>(pointOfInterest);

            _cityInfoRepository.AddPointOfInterestForCity(cityId, finalPoint);

            if (!_cityInfoRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            var createdPointOfInterestToReturn = Mapper.Map<PointOfInterestDTO>(finalPoint);

            return CreatedAtAction("GetPointOfInterest", new {cityId = cityId, Id = createdPointOfInterestToReturn.Id, }, createdPointOfInterestToReturn);
        }

        [HttpPut("{cityId}/pointsofinterest/{id}")]
        public IActionResult UpdatePointOfInterest(int cityId, int id, 
            [FromBody] PointOfInterestForUpdateDTO pointOfInterest)
        {
            if(pointOfInterest == null) return BadRequest();

            if(pointOfInterest.Description == pointOfInterest.Name) {
                ModelState.AddModelError("Description", "Descrição não pode ser igual ao nome.");
            }

            if(!ModelState.IsValid) return BadRequest(ModelState);

            if(!_cityInfoRepository.CityExists(cityId)) return NotFound();

            var pointOfInterestEntity = _cityInfoRepository.GetPointOfInterestForCity(cityId, id);

            if(pointOfInterestEntity == null) return NotFound();

            Mapper.Map(pointOfInterest, pointOfInterestEntity);

            if (!_cityInfoRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();
        }

        [HttpPatch("{cityId}/pointsofinterest/{id}")]
        public IActionResult PartiallyUpdatePointOfInterest(int cityId, int id,
            [FromBody] JsonPatchDocument<PointOfInterestForUpdateDTO> patchDoc)
        {
            if(patchDoc == null) return BadRequest();

            if(!_cityInfoRepository.CityExists(cityId)) return NotFound();

            var pointOfInterestEntity = _cityInfoRepository.GetPointOfInterestForCity(cityId, id);
            if(pointOfInterestEntity == null) return NotFound();

            var pointOfInterestToPach = Mapper.Map<PointOfInterestForUpdateDTO>(pointOfInterestEntity);

            patchDoc.ApplyTo(pointOfInterestToPach, ModelState);

            if(!ModelState.IsValid) return BadRequest(ModelState);
            
            if(pointOfInterestToPach.Description == pointOfInterestToPach.Name) {
                ModelState.AddModelError("Description", "Descrição não pode ser igual ao nome.");
            }

            TryValidateModel(pointOfInterestToPach);

            if(!ModelState.IsValid) return BadRequest(ModelState);

            Mapper.Map(pointOfInterestToPach, pointOfInterestEntity);
            
            if (!_cityInfoRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();
        }

        [HttpDelete("{cityId}/pointsofinterest/{id}")]
        public IActionResult DeletePointOfInterest(int cityId, int id)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if(!_cityInfoRepository.CityExists(cityId)) return NotFound();

            var pointOfInterestEntity = _cityInfoRepository.GetPointOfInterestForCity(cityId, id);
            if(pointOfInterestEntity == null) return NotFound();

            _cityInfoRepository.DeletePointOfInterestForCity(pointOfInterestEntity);
            
            if (!_cityInfoRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }
            
            _mail.Send("Point of interest deleted.", 
                $"Point of interest { pointOfInterestEntity.Name } with id { pointOfInterestEntity.Id } was deleted.");

            return NoContent();
        }
    }
}