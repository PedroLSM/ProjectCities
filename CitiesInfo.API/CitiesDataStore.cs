using System.Collections.Generic;
using CitiesInfo.API.Models;

namespace CitiesInfo.API
{
    public class CitiesDataStore
    {
        public static CitiesDataStore Current { get; } = new CitiesDataStore();
        public List<CityDTO> Cities {get; set;}
        public CitiesDataStore()
        {
            Cities = new List<CityDTO>(){
                new CityDTO(){
                    Id = 1,
                    Name = "Fortaleza",
                    Description = "Fortaleza é um município brasileiro, situado na região Nordeste do país.",
                    PointsOfInterest = new List<PointOfInterestDTO>(){
                        new PointOfInterestDTO(){
                            Id = 1,
                            Name = "Praia do Iracema",
                            Description = "Uma praia de bairro homônimo localizados no município de Fortaleza."
                        },
                        new PointOfInterestDTO(){
                            Id = 2,
                            Name = "Beack Park",
                            Description = "É um complexo turístico do litoral do Nordeste do Brasil a 26 quilômetros de Fortaleza."
                        }
                    }
                },
                new CityDTO(){
                    Id = 2,
                    Name = "Rio de Janeiro",
                    Description = "Rio de Janeiro é um município brasileiro, situado na região Sudeste do país.",
                    PointsOfInterest = new List<PointOfInterestDTO>(){
                        new PointOfInterestDTO(){
                            Id = 1,
                            Name = "Praia Copacabana",
                            Description = "É considerado um dos bairros mais famosos e prestigiados do Brasil e um dos mais conhecidos do mundo."
                        },
                        new PointOfInterestDTO(){
                            Id = 2,
                            Name = "Cristo Redentor",
                            Description = "Em 2007 foi eleito informalmente como uma das sete maravilhas do mundo moderno."
                        }
                    }
                },
                new CityDTO(){
                    Id = 3,
                    Name = "Caucaia",
                    Description = "Caucaia é um município limítrofes brasileiro, situado na região Nordeste do país.",
                    PointsOfInterest = new List<PointOfInterestDTO>(){
                        new PointOfInterestDTO(){
                            Id = 1,
                            Name = "Praia do Cumbuco",
                            Description = "Não é lá essas coisas mais da pra curti."
                        },
                        new PointOfInterestDTO(){
                            Id = 2,
                            Name = "Parque Botânico do Ceará",
                            Description = "possui uma área de 190 hectares, rica em oxigênio, bom lugar para apreciar a natureza."
                        }
                    }
                }
            };
        }
    }
}