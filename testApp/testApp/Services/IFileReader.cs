using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testApp.Models.DbData;

namespace testApp.Services
{
    /// <summary>
    /// Интрефейс для работы с файлом .dat.
    /// </summary>
    public interface IFileReader
    {

        /// <summary>
        /// Поиск по Ip-адресу
        /// </summary>
        /// <param name="ip"> Ip для поиска списка городов</param>
        /// <returns> список городов </returns>
        List<GeoPosition> FindIp(string ip);

        /// <summary>
        /// Поиск по названию города
        /// </summary>
        /// <param name="cyti"> название города</param>
        /// <returns> интревал Ip-адресов </returns>
        IntervalIp FindLocation(string cyti);

         Header Header { get;}
         List<GeoPosition> GeoPositions { get;}
         List<IntervalIp> IntervalIps { get;}
         List<uint> Location_index { get; }
    }
}
