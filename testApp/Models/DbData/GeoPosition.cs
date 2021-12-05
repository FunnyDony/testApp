using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testApp.Models.DbData
{
    [Serializable()]
    public class GeoPosition
    {
        public string Country{ get; set; }               // название страны 8 байт (случайная строка с префиксом "cou_")
        public string Region2 { get; set; }              // название области 12 байт (случайная строка с префиксом "reg_")
        public string Postal2 { get; set; }              // почтовый индекс 12 байт (случайная строка с префиксом "pos_")
        public string City { get; set; }                 // название города 24 байта (случайная строка с префиксом "cit_")
        public string Organization { get; set; }        // название организации 32 байта (случайная строка с префиксом "org_")
        public float  Latitude { get; set; }            // широта
        public float  Longitude { get; set; }           // долгота
        public uint LocationMemoryIndex { get; set; }    // Индекс начала записи о Геопозиции в памяти файла базы данных


        public static readonly int lenghtHeaderRecordsGeoPosition = 96;
     
        public GeoPosition(string country, string region2, string postal2, string city, string organization, float latitude, float longitude, uint locationMemoryIndex)
        {
            Country = country;
            Region2 = region2;
            Postal2 = postal2;
            City = city;
            Organization = organization;
            Latitude = latitude;
            Longitude = longitude;
            LocationMemoryIndex = locationMemoryIndex;
        }
    }
}
