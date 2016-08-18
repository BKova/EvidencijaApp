using Android.App;
using Android.OS;
using Android.Widget;
using System;
using static Android.Widget.TextView;

namespace EvidencijaAndroidClient.Activities
{
    [Activity(Label = "ConnectionSettingsActivity", Theme = "@android:style/Theme.Material.NoActionBar", MainLauncher = true, Icon = "@drawable/icon")]
    public class ConnectionSettingsActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ConnectionSettings);

            TextView iPAddress = FindViewById<TextView>(Resource.Id.IPAddress);

            iPAddress.Text = ((EvidencijaApplication)Application).ConnectionSettingsChanged.ServerIP;

            TextView portNumber = FindViewById<TextView>(Resource.Id.PortNumber);

            portNumber.Text = ((EvidencijaApplication)Application).ConnectionSettingsChanged.ServerPort;

            TextView webServiceLocation = FindViewById<TextView>(Resource.Id.WebServiceLocation);

            webServiceLocation.Text = ((EvidencijaApplication)Application).ConnectionSettingsChanged.WebServiceLocation;

            Button close = FindViewById<Button>(Resource.Id.CloseButton1);

            close.Click += ((object sender, EventArgs args) =>
            {
                ((EvidencijaApplication)Application).ConnectionSettingsChanged.ServerIP = iPAddress.Text;
                ((EvidencijaApplication)Application).ConnectionSettingsChanged.ServerPort = portNumber.Text;
                ((EvidencijaApplication)Application).ConnectionSettingsChanged.WebServiceLocation = webServiceLocation.Text;
                Finish();
            });
        }
    }
}