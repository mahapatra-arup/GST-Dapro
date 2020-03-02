using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Windows.Forms;

namespace DAPRO
{
    class CheckInternetconnection
    {
        public static bool CheckForInternetConnection()
        {
            //***************simple Web Request Test*************
            bool interConn = WebRequestTest();
            if (!interConn)
            {
                //***************if it false then check Ping Test*************
                interConn = PingTest();
            }
           return interConn;
        }

        public static bool WebRequestTest()
        {
            string url = "http://www.google.com";
            try
            {
                System.Net.WebRequest myRequest = System.Net.WebRequest.Create(url);
                System.Net.WebResponse myResponse = myRequest.GetResponse();
            }
            catch (System.Net.WebException)
            {
                return false;
            }
            return true;
        }
        public static bool TcpSocketTest()
        {
            try
            {
                System.Net.Sockets.TcpClient client =
                    new System.Net.Sockets.TcpClient("www.google.com", 80);
                client.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }
        public static bool PingTest()
        {
            try
            {
                System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();

                System.Net.NetworkInformation.PingReply pingStatus =
                    ping.Send(IPAddress.Parse("208.69.34.231"), 1000);//208.69.34.231 means google .com

                if (pingStatus.Status == System.Net.NetworkInformation.IPStatus.Success)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool DnsTest()
        {
            try
            {
                System.Net.IPHostEntry ipHe =
                    System.Net.Dns.GetHostByName("www.google.com");
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
