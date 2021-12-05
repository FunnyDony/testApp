using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace testApp.Models.DbData
{
    [Serializable()]
    public class IntervalIp
    {
       // public uint Ip_from { get; set; }           // начало диапазона IP адресов
        public IPAddress Ip_from { get; set; }           // начало диапазона IP адресов
      //  public uint Ip_to { get; set; }               // конец диапазона IP адресов
        public IPAddress Ip_to { get; set; }               // конец диапазона IP адресов
        public uint Location_index { get; set; }   // индекс записи о местоположении

        
        public static readonly int lenghtHeaderRecordsIntervalIp = 12;
      
        public IntervalIp(IPAddress ip_from, IPAddress ip_to, uint location_index)
        {
            Ip_from = ip_from;
            Ip_to = ip_to;
            Location_index = location_index;
         }
    }
}
