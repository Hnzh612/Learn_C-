using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SerialPortClass
{
    public class SocketSerialPort
    {
        private static SocketSerialPort s_instane;
        public static readonly object _obj = new object();
        private static IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
        public static SocketSerialPort CreateInstance() 
        { 
            if(null == s_instane)
            {
                lock(_obj)
                {
                    if(null == s_instane )
                    {
                        s_instane = new SocketSerialPort();
                    }
                }
            }
            return s_instane;
        }
        public SocketSerialPort InitUDPClient(int _port = 9527)
        {
            udp = new UdpClient(new IPEndPoint(IPAddress.Any, _port));
            return this;
        }
        public SocketSerialPort StartUDPClient()
        {
            Task.Run(() =>
            {
                List<byte> AllReceivedData = new List<byte>();
                while (true)
                {
                    Byte[] TEMP = udp.Receive(remoteEP: ref RemoteIpEndPoint);
                    if (TEMP != null && TEMP.Length >= 1)
                    {
                        AllReceivedData.AddRange(TEMP);
                    }
                    //FA 5A 44 59 FF=>FA Z D Y FF
                    for (int i = 0; i < AllReceivedData.Count; i++)
                    {
                        if (AllReceivedData.Count >= 5
                        && i + 4 < AllReceivedData.Count
                        && AllReceivedData[i] == 0xFA
                        && AllReceivedData[i + 1] == 0X5A
                        && AllReceivedData[i + 2] == 0X44
                        && AllReceivedData[i + 3] == 0X59
                        && AllReceivedData[i + 4] == 0XFF)
                        {

                        }
                    }
                }
            });
            return this;
        }
        public UdpClient udp { get; private set; }
        private SocketSerialPort()
        {

        }
    }
}
