using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ChatUDP
{
    internal class Program
    {
        private static IPAddress RemoteIpAdress;
        private static int RemotePort;
        private static int LocalPort;
       

        [STAThread]
        static void Main(string[] args)
        {
            try
            {

                Console.WriteLine("Укажите локальный порт");
                LocalPort = Convert.ToInt16(Console.ReadLine());

                Console.WriteLine("Укажите удаленный порт");
                RemotePort = Convert.ToInt16(Console.ReadLine());

                Console.WriteLine("Укажите удаленный IP-адрес");
                RemoteIpAdress = IPAddress.Parse(Console.ReadLine());

                Thread thread = new Thread(new ThreadStart(Receive));
                thread.Start();
                while (true)
                {
                    Send(Console.ReadLine());
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Возникло исключение: " + ex.ToString() + "\n  " + ex.Message);

            }
        }
        public static void Send (string datagram)
        {
            UdpClient udpClient = new UdpClient();
            IPEndPoint iPEndPoint = new IPEndPoint(RemoteIpAdress,RemotePort);
            try
            {
                byte[] bytes=Encoding.UTF8.GetBytes(datagram);
                udpClient.Send(bytes,bytes.Length,iPEndPoint);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Возникло исключение: " + ex.ToString() + "\n  " + ex.Message);
            }
            finally
            {
                 udpClient.Close();
            }
        }
        public static void Receive()
        {
            UdpClient ReseiveUdpClient = new UdpClient(LocalPort);
            IPEndPoint iPEndPoint = null;
            try
            {
                Console.WriteLine(
                  "\n-----------*******Общий чат*******-----------");
                while (true)
                {
                    byte[] bytes = ReseiveUdpClient.Receive(ref iPEndPoint);
                    string returnData = Encoding.UTF8.GetString(bytes);
                    Console.WriteLine(" --> " + returnData.ToString());
                }
              

            }
            catch (Exception ex)
            {

                Console.WriteLine("Возникло исключение: " + ex.ToString() + "\n  " + ex.Message);
            }
            finally
            {
                ReseiveUdpClient.Close();
            }
        }
    }
}
