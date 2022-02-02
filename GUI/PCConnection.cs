using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace GUI
{
    public class PCConnection : IDisposable
    {
        private IPEndPoint _groupEP;

        public PCConnection(UdpClient listen, IPEndPoint group)
        {
            listener = listen;
            _groupEP = group;
        }
        
        public void ReadPackets()
        {

            byte[] UDPpacket = listener.Receive(ref _groupEP);
            
            using (var stream = new MemoryStream(UDPpacket))
            {
                using (var binaryReader = new BinaryReader(stream))
                {
                    ReadBaseUDP(stream, binaryReader);
                    if (PacketType == 0)
                    {
                        ReadTelemetryData(stream, binaryReader);
                    }
                }
            }
        }


        public void ReadBaseUDP(Stream stream, BinaryReader binaryReader)
        {
            stream.Position = 10;
            //PacketNumber = binaryReader.ReadUInt32();
            //CategoryPacketNumber = binaryReader.ReadUInt32();
            //PartialPacketIndex = binaryReader.ReadByte();
            //PartialPacketNumber = binaryReader.ReadByte();
            PacketType = binaryReader.ReadByte();
            //PacketVersion = binaryReader.ReadByte();
        }

        public void ReadTelemetryData(Stream stream, BinaryReader binaryReader)
        {
            stream.Position = 40;
            Rpm = binaryReader.ReadUInt16();
        }

        public void Dispose()
        {
            listener.Close();
        }

        public UdpClient listener { get; set; }

        public IPEndPoint groupEP
        {
            get
            {
                return _groupEP;
            }
            set
            {
                _groupEP = value;
            }
        }

        public UInt32 PacketNumber { get; set; }

        public UInt32 CategoryPacketNumber { get; set; }

        public byte PartialPacketIndex { get; set; }

        public byte PartialPacketNumber { get; set; }

        public byte PacketType { get; set; }

        public byte PacketVersion { get; set; }

        public UInt16 Rpm { get; set; }
    }
}
