using CitiesInfo.API.Entities;
using System.Collections.Generic;
using System.Linq;

namespace CitiesInfo.API
{
    public static class CityInfoContextExtensions
    {
        public static void EnsureSeedDataForContext(this CityInfoContext context){
            if(context.Cities.Any()){
                return;
            }

            var cities = new List<City>(){
                new City(){
                    Name = "Fortaleza",
                    Description = "Fortaleza é um município brasileiro, situado na região Nordeste do país.",
                    PointsOfInterest = new List<PointOfInterest>()
                    {
                        new PointOfInterest(){
                            Name = "Praia do Iracema",
                            Description = "Uma praia de bairro homônimo localizados no município de Fortaleza."
                        },
                        new PointOfInterest(){
                            Name = "Beack Park",
                            Description = "É um complexo turístico do litoral do Nordeste do Brasil."
                        }
                    }
                },
                new City(){
                    Name = "Rio de Janeiro",
                    Description = "Rio de Janeiro é um município brasileiro, situado na região Sudeste do país.",
                    PointsOfInterest = new List<PointOfInterest>()
                    {
                        new PointOfInterest(){
                            Name = "Praia Copacabana",
                            Description = "É considerado um dos bairros mais conhecidos do mundo."
                        },
                        new PointOfInterest(){
                            Name = "Cristo Redentor",
                            Description = "Em 2007 foi eleito informalmente como uma das sete maravilhas do mundo moderno."
                        }
                    }
                },
                new City(){
                    Name = "Caucaia",
                    Description = "Caucaia é um município limítrofes brasileiro, situado na região Nordeste do país.",
                    PointsOfInterest = new List<PointOfInterest>()
                    {
                        new PointOfInterest(){
                            Name = "Praia do Cumbuco",
                            Description = "Não é lá essas coisas mais da pra curti."
                        },
                        new PointOfInterest(){
                            Name = "Parque Botânico do Ceará",
                            Description = "possui uma área de 190 hectares, bom lugar para apreciar a natureza."
                        }
                    }
                }
            };

            context.Cities.AddRange(cities);
            context.SaveChanges();

        }
    }
}