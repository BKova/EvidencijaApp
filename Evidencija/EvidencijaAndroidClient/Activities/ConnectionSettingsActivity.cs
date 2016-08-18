using Android.App;
using Android.OS;
using Android.Widget;
using System;

namespace EvidencijaAndroidClient.Activities
{
    [Activity(Label = "ConnectionSettingsActivity", Theme = "@android:style/Theme.Material.NoActionBar")]
    public class ConnectionSettingsActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ConnectionSettings);

            TextView ssid = FindViewById<TextView>(Resource.Id.SSID);

            ssid.Text = ((EvidencijaApplication)Application).ConnectionSettingsChanged.NetworkSSID;

            TextView iPAddress = FindViewById<TextView>(Resource.Id.IPAddress);

            iPAddress.Text = ((EvidencijaApplication)Application).ConnectionSettingsChanged.ServerIP;

            TextView portNumber = FindViewById<TextView>(Resource.Id.PortNumber);

            portNumber.Text = ((EvidencijaApplication)Application).ConnectionSettingsChanged.ServerPort;

            TextView webServiceLocation = FindViewById<TextView>(Resource.Id.WebServiceLocation);

            webServiceLocation.Text = ((EvidencijaApplication)Application).ConnectionSettingsChanged.WebServiceLocation;

            Button close = FindViewById<Button>(Resource.Id.CloseButton1);

            close.Click += ((object sender, EventArgs args) =>
            {
                ((EvidencijaApplication)Application).ConnectionSettingsChanged.NetworkSSID = string.Format("\"{0}\"",ssid.Text);
                ((EvidencijaApplication)Application).ConnectionSettingsChanged.ServerIP = iPAddress.Text;
                ((EvidencijaApplication)Application).ConnectionSettingsChanged.ServerPort = portNumber.Text;
                ((EvidencijaApplication)Application).ConnectionSettingsChanged.WebServiceLocation = webServiceLocation.Text;
                Finish();
            });
        }
    }
}