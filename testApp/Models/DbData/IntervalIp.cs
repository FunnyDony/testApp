using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace testApp.Models.DbData
{
    /// <summary>
    /// Модель для хранения интервала IP адресов
    /// </summary>
    [Serializable()]
    public class IntervalIp
    {
        public string Ip_from { get; set; }           // начало диапазона IP адресов
        public string Ip_to { get; set; }               // конец диапазона IP адресов
        public uint Location_index { get; set; }   // номер индекса записи о местоположении


        public static readonly int lenghtHeaderRecordsIntervalIp = 12;
      
        public IntervalIp(string ip_from, string ip_to, uint location_index, uint indexVal)
        {
            Ip_from = ip_from;
            Ip_to = ip_to;
            Location_index = location_index;
         }
    }
}
