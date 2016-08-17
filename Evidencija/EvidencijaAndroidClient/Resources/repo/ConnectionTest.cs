using System;
using Android.Widget;
using System.Net;
using System.IO;

namespace EvidencijaAndroidClient.Resources.repo
{
    class ConnectionTest
    {
        private string _url { get; set; }

        public ConnectionTest(string url)
        {
            _url = url;
        }

        public async void SendRequest(object sender, System.EventArgs args)
        {
            string Result = "Fail";

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(_url));
            request.Timeout = 500;
            request.Method = "GET";
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(stream);

                        Result = await reader.ReadToEndAsync();


                    }
                }
            }
            catch (Exception ex)
            { }
            ((Button)sender).Text = Result;

        }
    }
}