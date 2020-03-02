using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace DAPRO
{
    public static class OnlineServerDateTimeTools
    {
        #region This  Method Not use thistime

        public static string sNISTDateTimeFull; //the string holding the full date time output from NIST in ASCII format
        public static string subStringNISTDateTimeShort; //the string holding the full date time output without reference to NIST in ASCII format
        public static DateTime dtNISTDateTime; //the DateTime value of the NIST date and time used for calculating the span between NIST and the systme clock

        //Server addresses and information from //http://tf.nist.gov/tf-cgi/servers.cgi
        public const string server = "time.nist.gov"; //URL to connect to the server
        public const Int32 port = 13; //port to get date time data from the server


        //**************************************************************************
        //NAME:      SetSystemDate
        //PURPOSE:   change  System date From Online Server Date
        //**

        //**************************************************************************
        //NAME:      RunGetDateReport
        //PURPOSE:   Display the information from NIST and state if the system clock
        //and the date and time from NIST are within 24 hours of eachother.
        //**************************************************************************
        public static string RunGetDateReport()
        {
            GetNISTDateTime();
            string subStringNISTDateTimeLong = sNISTDateTimeFull.Substring(7, 40);
            dtNISTDateTime = DateTime.Parse("20" + subStringNISTDateTimeShort);
            long lNISTDateTime = dtNISTDateTime.Ticks;
            //This is Main full date
            return dtNISTDateTime.ToString();
        }

        //**************************************************************************
        //NAME:      GetNISTDateTime
        //PURPOSE:   Crates a TCP client to connect to the NIST server, get the 
        //data from the server, and parse that data into a string that can be used 
        //for display or calculation.
        //**************************************************************************
        public static void GetNISTDateTime()
        {
            bool bGoodConnection = false;

            //Create and instance of a TCP client that will connect to the server 
            //and get the information it offers
            System.Net.Sockets.TcpClient tcpClientConnection = new System.Net.Sockets.TcpClient();

            //Attempt to connect to the NIST server. If it succeeds, the flag is set 
            //to collect the information from the server If it fails, try again
            try
            {
                tcpClientConnection.Connect(server, port);
                bGoodConnection = true;
            }
            catch
            {
                //btnGetServerDateAndTime.PerformClick();
            }

            //Don't continue if you haven't got a good connection
            if (bGoodConnection == true)
            {
                //Attempt to get the data streaming from the NIST server
                try
                {
                    NetworkStream netStream = tcpClientConnection.GetStream();

                    //Check the flag the states if you can read the stream or not
                    if (netStream.CanRead)
                    {
                        //Get the size of the buffer
                        byte[] bytes = new byte[tcpClientConnection.ReceiveBufferSize];

                        //Read in the stream to the length of the buffer
                        netStream.Read(bytes, 0, tcpClientConnection.ReceiveBufferSize);

                        //Read the Bytes as ASCII values and build the stream of charaters that are the date and time from NIST. 
                        sNISTDateTimeFull = Encoding.ASCII.GetString(bytes).Substring(0, 50);

                        //Convert the string to a date time value
                        try
                        {
                            subStringNISTDateTimeShort = sNISTDateTimeFull.Substring(7, 17);
                            dtNISTDateTime = DateTime.Parse("20" + subStringNISTDateTimeShort);
                        }
                        catch
                        {
                            //btnGetServerDateAndTime.PerformClick(); //try running the sub again if you didn't get anything worth while
                        }
                    }
                    else //If the data stream was unreadable, do the following
                    {
                        MessageBox.Show("You cannot read data from this stream."); //Advise the user of the situation
                        tcpClientConnection.Close(); //close the client stream
                        netStream.Close(); //close the network stream
                    }

                    tcpClientConnection.Close(); //Uses the Close public method to close the network stream and socket.
                }
                //Provide the user feedback if the TCP client couldn't even get the stream of data from the server.
                catch (ArgumentNullException e)
                {
                    MessageBox.Show("ArgumentNullException: {0}", e.ToString()); // show error messages when appropriate
                }
                catch (SocketException e)
                {
                    MessageBox.Show("SocketException: {0}", e.ToString()); // show error messages when appropriate
                }
            }
        }
        #endregion

        public static void SetSystemDate(this DateTime? date)
        {
            if (date!=null)
            {
                var proc = new System.Diagnostics.ProcessStartInfo();
                proc.UseShellExecute = true;
                proc.WorkingDirectory = @"C:\Windows\System32";
                proc.CreateNoWindow = true;
                proc.FileName = @"C:\Windows\System32\cmd.exe";
                proc.Verb = "runas";
                proc.Arguments = "/c date " + date.Value.Date.ToString("dd-MM-yy");
                try
                {
                    System.Diagnostics.Process.Start(proc);
                }
                catch
                {
                } 
            }
        }

        public static DateTime? GetOnlineServerDate()
        {
            DateTime? dt = null;
            string Qry = "select Getdate() ";
            object o = AzureSQLHelper.GetInstance().ExcuteScalar(Qry);
            if (o != null)
            {
                try
                {
                    dt = DateTime.Parse(o.ToString());
                }
                catch (Exception e)
                {
                    dt = null;
                }
            }
            return dt;
        }
    }
}
