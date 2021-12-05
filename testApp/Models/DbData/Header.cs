using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testApp.Models.DbData
{
    [Serializable()]
    public class Header
    {
        public int               Version { get; set; }                    // версия база данных
        public string           Name { get; set; }                // название/префикс для базы данных 32 байта
        public DateTime     Timestamp { get; set; }     // 8 байт время создания базы данных
        public int               Records { get; set; }                 // общее количество записей
        public uint             Offset_ranges { get; set; }          // смещение относительно начала файла до начала списка записей с геоинформацией
        public uint             Offset_cities { get; set; }            // смещение относительно начала файла до начала индекса с сортировкой по названию городов
        public uint            Offset_locations { get; set; }    // смещение относительно начала файла до начала списка записей о местоположении  

        [NonSerialized]
        public static readonly int lenghtHeader = 60;
        [NonSerialized]
        public static readonly int lenghtName = 60;
        public Header(int version,string name, DateTime timestamp,int records, uint offset_ranges, uint offset_cities, uint offset_locations)
        {
           Version            = version;
           Name              = name;
           Timestamp       = timestamp;    
           Records           = records;
           Offset_ranges   = offset_ranges;
           Offset_cities     = offset_cities;
           Offset_locations= offset_locations;

        }

    //    var d = new Data();
    //    var sI32 = sizeof(Int32);
    //    d._int1 = BitConverter.ToInt32(bytes, 0);
    //d._int2 = BitConverter.ToInt32(bytes, sI32);
    //d._short1 = BitConverter.ToInt16(bytes, 2*sI32);
    }
}
