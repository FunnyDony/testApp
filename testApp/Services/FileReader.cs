using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using testApp.Models.DbData; 

namespace testApp.Services
{
   
    /// <summary>
    /// Реализация сервиса для работы с файлом .dat.
    /// Чтение и поиск информации
    /// </summary>
    public class FileReader : IFileReader
    {

        public Header header; 
        public List<GeoPosition> geoPositions;
        public List<IntervalIp> IntervalIps;
        public List<UInt32> IndexCytisInMemory;
        public List<GeoPosition> GeoPositions => geoPositions ;
        public List<uint> Location_index => IndexCytisInMemory;
        Header IFileReader.Header => header;
        List<IntervalIp> IFileReader.IntervalIps => IntervalIps;


        public  FileReader()
        {
            Init();
        }
        
        /// <summary>
        /// Загрузка файла базы данных. Обработка БД.
        /// </summary>
        public void Init()
        {
            using (BinaryReader reader = new BinaryReader(File.Open(@"geobase.dat", FileMode.Open))) 
            {
                #region считываем заголовок
                header = new Header
                (
                    reader.ReadInt32(),
                    Encoding.ASCII.GetString(reader.ReadBytes(32)),
                    DateTime.FromBinary(reader.ReadInt64()),
                    reader.ReadInt32(),
                    reader.ReadUInt32(),
                    reader.ReadUInt32(),
                    reader.ReadUInt32()
                );
                #endregion

                #region считываем индексы в памяти о положении городов в памяти
                reader.BaseStream.Position = header.Offset_cities;
                int countRecords = 0;
               
                IndexCytisInMemory = new List<UInt32>(header.Records);

                while (countRecords < header.Records)
                {
                    IndexCytisInMemory.Add(reader.ReadUInt32());
                    countRecords++;
                }
                #endregion

                #region Считываем информацию о Городах
                countRecords = 0;
                geoPositions = new List<GeoPosition>(header.Records);

                reader.BaseStream.Position = header.Offset_locations;

                while (countRecords < header.Records)
                {
                    GeoPosition geoPosition = new GeoPosition
                    (
                            Encoding.ASCII.GetString(reader.ReadBytes(8)).Replace("\0", ""),
                            Encoding.ASCII.GetString(reader.ReadBytes(12)).Replace("\0", ""),
                            Encoding.ASCII.GetString(reader.ReadBytes(12)).Replace("\0", ""),
                            Encoding.ASCII.GetString(reader.ReadBytes(24)).Replace("\0", ""),
                            Encoding.ASCII.GetString(reader.ReadBytes(32)).Replace("\0", ""),
                            reader.ReadSingle(),
                            reader.ReadSingle(),
                            //IndexCytisInMemory[countRecords]
                            (uint)countRecords
                     );

                    geoPositions.Add(geoPosition);

                    countRecords++;
                }
                #endregion

                #region Считываем интервалы IP адресов
                reader.BaseStream.Position = header.Offset_ranges;
                countRecords = 0;
                IntervalIps = new List<IntervalIp>(header.Records);

                while (countRecords < header.Records)
                {

                    IPAddress iP1 = null;
                    IPAddress iP2 = null;

                    byte[] val = BitConverter.GetBytes(reader.ReadUInt32());
                    iP1 = new IPAddress(val);

                    byte[] val2 = BitConverter.GetBytes(reader.ReadUInt32());
                    iP2 = new IPAddress(val2);


                    IntervalIp intervalIp = new IntervalIp
                    (
                        iP1.ToString(),
                        iP2.ToString(),
                        reader.ReadUInt32(),
                        IndexCytisInMemory[countRecords]
                    );

                    IntervalIps.Add(intervalIp);

                    countRecords++;
                }
                #endregion
            }
        }

        /// <summary>
        /// Поиск Городов в которых используется IP адрес
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public List<GeoPosition> FindIp(string ip)
        {
            IPAddress iPAddress;
            IPAddress.TryParse(ip, out iPAddress);
            List<GeoPosition> result = new List<GeoPosition>();

            for (int i = 0; i < IntervalIps.Count; i++)
            {
               if( IPAddressesRange(IPAddress.Parse(IntervalIps[i].Ip_from),IPAddress.Parse( IntervalIps[i].Ip_to), iPAddress))
                    result.Add(GeoPositions[i]);
            }
            return result;
        }

        /// <summary>
        /// Функция принадлежности Ip адреса к интервалу Адресов
        /// </summary>
        /// <param name="firstIPAddress">От Адреса</param>
        /// <param name="lastIPAddress">До адреса</param>
        /// <param name="findIPAddress"> Адрес </param>
        /// <returns> true если адрес принадлежит заданному интервалу адресов </returns>
        static bool IPAddressesRange(IPAddress firstIPAddress, IPAddress lastIPAddress, IPAddress findIPAddress)
        {
            
            var firstIPAddressAsBytesArray = firstIPAddress.GetAddressBytes();
            var lastIPAddressAsBytesArray = lastIPAddress.GetAddressBytes();
            var findIPAddressAsBytesArray = findIPAddress.GetAddressBytes();

            Array.Reverse(firstIPAddressAsBytesArray);
            Array.Reverse(lastIPAddressAsBytesArray);
            Array.Reverse(findIPAddressAsBytesArray);

            var firstIPAddressAsInt = BitConverter.ToUInt32(firstIPAddressAsBytesArray, 0);
            var lastIPAddressAsInt = BitConverter.ToUInt32(lastIPAddressAsBytesArray, 0);
            var findIPAddressAsInt = BitConverter.ToUInt32(findIPAddressAsBytesArray, 0);

            if (findIPAddressAsInt >= firstIPAddressAsInt && findIPAddressAsInt < lastIPAddressAsInt)
                return true;

            return false;
        }

        /// <summary>
        /// Поиск интервала адресов по названию города
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        public IntervalIp FindLocation(string city)
        {
            string st = city.Replace(" ", "");

            for (int i = 0; i < IntervalIps.Count; i++)
            {
                string str = geoPositions[i].City.Replace(" ", "");
                if (str.Equals(st)) 
                        return IntervalIps[i];
            }
            return null;
        }
    }
}
