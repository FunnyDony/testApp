using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testApp.Models.DbData;

namespace testApp.Services
{
    public interface IFileReader
    {

        List<GeoPosition> FindIp(string ip);
        IntervalIp FindLocation(string cyti);

         Header Header { get;}
         List<GeoPosition> GeoPositions { get;}
         List<IntervalIp> IntervalIps { get;}
         List<uint> Location_index { get; }
    }
}
