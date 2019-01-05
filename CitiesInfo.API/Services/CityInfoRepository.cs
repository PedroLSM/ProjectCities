using System.Collections.Generic;
using CitiesInfo.API.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CitiesInfo.API.Services
{
    public class CityInfoRepository : ICityInfoRepository
    {
        CityInfoContext _context;

        public CityInfoRepository(CityInfoContext cityInfoContext)
        {
            _context = cityInfoContext;
        }

        public void AddPointOfInterestForCity(int cityId, PointOfInterest pointOfInterest)
        {
            var city = GetCity(cityId, false);
            city.PointsOfInterest.Add(pointOfInterest);
        }

        public bool CityExists(int cityId)
        {
            return _context.Cities.Any(c => c.Id == cityId);
        }

        public void DeletePointOfInterestForCity(PointOfInterest pointOfInterest)
        {
            _context.PointsOfInterest.Remove(pointOfInterest);
        }

        public IEnumerable<City> GetCities(bool includePointsOfInterest)
        {
            if(includePointsOfInterest){
                return _context.Cities
                        .Include(c => c.PointsOfInterest)
                        .ToList();
            }

            return _context.Cities
                        .OrderBy(c => c.Name)
                        .ToList();
        }

        public City GetCity(int cityId, bool includePointsOfInterest)
        {
            if(includePointsOfInterest){
                return _context.Cities
                        .Include(c => c.PointsOfInterest)
                        .Where(c => c.Id == cityId)
                        .FirstOrDefault();
            }

            return _context.Cities
                        .Where(c => c.Id == cityId)
                        .FirstOrDefault();
        }

        public PointOfInterest GetPointOfInterestForCity(int cityId, int pointOfInterestId)
        {
            return _context.PointsOfInterest
                    .Where(p => p.CityId == cityId && p.Id == pointOfInterestId)
                    .FirstOrDefault();
        }

        public IEnumerable<PointOfInterest> GetPointsOfInterestForCity(int cityId)
        {
            return _context.PointsOfInterest
                    .Where(p => p.CityId == cityId)
                    .ToList();
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}