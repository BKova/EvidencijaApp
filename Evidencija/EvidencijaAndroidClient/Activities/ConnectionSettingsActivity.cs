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

            ssid.Text = ((EvidencijaApplication)Application).ServiceConnection.Binder.BackgroundService.ConnectionSettingsChanged.NetworkSSID;

            TextView iPAddress = FindViewById<TextView>(Resource.Id.IPAddress);

            iPAddress.Text = ((EvidencijaApplication)Application).ServiceConnection.Binder.BackgroundService.ConnectionSettingsChanged.ServerIP;

            TextView portNumber = FindViewById<TextView>(Resource.Id.PortNumber);

            portNumber.Text = ((EvidencijaApplication)Application).ServiceConnection.Binder.BackgroundService.ConnectionSettingsChanged.ServerPort;

            TextView webServiceLocation = FindViewById<TextView>(Resource.Id.WebServiceLocation);

            webServiceLocation.Text = ((EvidencijaApplication)Application).ServiceConnection.Binder.BackgroundService.ConnectionSettingsChanged.WebServiceLocation;

            Button close = FindViewById<Button>(Resource.Id.CloseButton1);

            close.Click += ((object sender, EventArgs args) =>
            {
                ((EvidencijaApplication)Application).ServiceConnection.Binder.BackgroundService.ConnectionSettingsChanged.NetworkSSID = ssid.Text;

                ((EvidencijaApplication)Application).ServiceConnection.Binder.BackgroundService.ConnectionSettingsChanged.ServerIP = iPAddress.Text;

                ((EvidencijaApplication)Application).ServiceConnection.Binder.BackgroundService.ConnectionSettingsChanged.ServerPort = portNumber.Text;

                ((EvidencijaApplication)Application).ServiceConnection.Binder.BackgroundService.ConnectionSettingsChanged.WebServiceLocation = webServiceLocation.Text;

                Finish();
            });
        }
    }
}