using System;
using Android.Net.Wifi;
using EvidencijaAndroidClient.Resources.models;
using System.Net;
using System.IO;

namespace EvidencijaAndroidClient.Resources.repo
{
    public class ConnectionService
    {
        public ConnectionSettings Settings { get; set; }

        public ConnectionService(ConnectionSettings settings)
        {
            Settings = settings;
        }
        internal async System.Threading.Tasks.Task<bool> CheckNetwork(WifiManager wifiManager)
        {
            if (wifiManager.ConnectionInfo.SSID == string.Format("\"{0}\"",Settings.NetworkSSID))
            {
                string Uri = string.Format("{0}:{1}{2}/check", Settings.ServerIP, Settings.ServerPort, Settings.WebServiceLocation);
                string Result = "";
                HttpStatusCode Status = HttpStatusCode.NotFound;
                try
                {
                    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(Uri));
                    request.Timeout = 500;
                    request.Method = "GET";

                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        using (Stream stream = response.GetResponseStream())
                        {
                            StreamReader reader = new StreamReader(stream);

                            Result = await reader.ReadToEndAsync();
                            Status = response.StatusCode;
                        }
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
                return (Result == "OK") && Status == HttpStatusCode.OK;

            }
            else return false;
        }
    }
}