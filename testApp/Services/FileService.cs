using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using testApp.Models.ComparerForBinarySearch;
using testApp.Models.DbData;

namespace testApp.Services
{
    public class FileService : IFileSevice
    {

        public Header header;
        public List<GeoPosition> geoPositions;
        public List<IntervalIp> IntervalIps;
        public List<UInt32> IndexCytisInMemory;

        public List<GeoPosition> GeoPositions => geoPositions ;

        public List<uint> Location_index => IndexCytisInMemory;

        Header IFileSevice.Header => header;

        List<IntervalIp> IFileSevice.IntervalIps => IntervalIps;

        public void ReadFile()
        {
            Init();
        }

        /// <summary>
        /// Загрузка файла базы данных. Обработка БД.
        /// </summary>
        public void Init()
        {
            using (BinaryReader reader = new BinaryReader(File.Open($"geobase.dat", FileMode.Open)))
            {
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


                reader.BaseStream.Position = header.Offset_cities;
                int countRecords = 0;
                IndexCytisInMemory = new List<UInt32>(header.Records);

                while (countRecords < header.Records)
                {
                    IndexCytisInMemory.Add(reader.ReadUInt32());
                    countRecords++;
                }


                countRecords = 0;
                geoPositions = new List<GeoPosition>(header.Records);

                reader.BaseStream.Position = header.Offset_locations;


                while (countRecords < header.Records)
                {
                    GeoPosition geoPosition = new GeoPosition
                    (
                            Encoding.ASCII.GetString(reader.ReadBytes(8)),
                            Encoding.ASCII.GetString(reader.ReadBytes(12)),
                            Encoding.ASCII.GetString(reader.ReadBytes(12)),
                            Encoding.ASCII.GetString(reader.ReadBytes(24)),
                            Encoding.ASCII.GetString(reader.ReadBytes(32)),
                            reader.ReadSingle(),
                            reader.ReadSingle(),
                            IndexCytisInMemory[countRecords]
                     );

                    geoPositions.Add(geoPosition);

                    countRecords++;
                }
               
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
                        iP1,
                        iP2,
                        reader.ReadUInt32()
                    );

                    IntervalIps.Add(intervalIp);

                    countRecords++;
                }

                
            }
        }

        public GeoPosition FindIp(string ip)
        {
            int memoryIndex, indexCyti;
            IPAddress iPAddress;
            IPAddress.TryParse(ip, out iPAddress);
            ComparerGeoPosition comparerGeo = new ComparerGeoPosition();
            ComparerIntervalIp comparerIntIp = new ComparerIntervalIp();


            memoryIndex = IntervalIps.BinarySearch(new IntervalIp(iPAddress, null, 0), comparerIntIp);
            indexCyti = GeoPositions.BinarySearch(new GeoPosition(null, null, null, null, null, 0, 0, (uint)memoryIndex), comparerGeo);
            return GeoPositions[indexCyti];
        }

        public  IntervalIp FindLocation(string cyti)
        {
            throw new NotImplementedException();
        }
    }
}
