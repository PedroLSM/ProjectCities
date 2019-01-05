using System.Collections.Generic;
using CitiesInfo.API.Entities;

namespace CitiesInfo.API.Services
{
    public interface ICityInfoRepository
    {
        bool CityExists(int cityId);
        IEnumerable<City> GetCities(bool includePointsOfInterest);
        City GetCity(int cityId, bool includePointsOfInterest);
        IEnumerable<PointOfInterest> GetPointsOfInterestForCity(int cityId);
        PointOfInterest GetPointOfInterestForCity(int cityId, int pointOfInterestId);
        void AddPointOfInterestForCity(int cityId, PointOfInterest pointOfInterest);
        void DeletePointOfInterestForCity(PointOfInterest pointOfInterest);
        bool Save();
    }
}