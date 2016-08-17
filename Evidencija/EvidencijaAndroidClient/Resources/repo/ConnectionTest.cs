using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
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
            string Result;

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(_url));
            request.Method = "GET";

            using (WebResponse response = await request.GetResponseAsync())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream);

                    Result = await reader.ReadToEndAsync();

                    ((Button)sender).Text = Result;
                }
            }

        }
    }
}