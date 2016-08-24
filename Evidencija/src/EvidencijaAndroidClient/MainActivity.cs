using Android.App;
using Android.Widget;
using Android.OS;
using Android.Graphics;
using EvidencijaAndroidClient.Activities;
using System;
using Android.Content;

namespace EvidencijaAndroidClient
{
    [Activity(Label = "EvidencijaAndroidClient",Theme = "@android:style/Theme.Material.NoActionBar", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        public TextView statusText { get; set; }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            Button connectionSettings = FindViewById<Button>(Resource.Id.ConnectionSettingsButton);

            connectionSettings.Click += ((object sender, EventArgs args) =>
            {
                var intent = new Intent(this, typeof(ConnectionSettingsActivity));
                StartActivity(intent);
            });

            Button userSettings = FindViewById<Button>(Resource.Id.UserSettingsButton);

            userSettings.Click += ((object sender, EventArgs args) =>
            {
                var intent = new Intent(this, typeof(UserInfoActivity));
                StartActivity(intent);
            });

            Button apply = FindViewById<Button>(Resource.Id.ApplyButton);

            ((EvidencijaApplication)Application).ServiceConnection.Apply = apply;

            ToggleButton onOff = FindViewById<ToggleButton>(Resource.Id.OnOffButton);

            ((EvidencijaApplication)Application).ServiceConnection.toggleButton = onOff;

            statusText = FindViewById<TextView>(Resource.Id.StatusText);

            ((EvidencijaApplication)Application).ServiceConnection.UpdateStatusCollor = UpdateStatus;

            ((EvidencijaApplication)Application).ServiceConnection.isBoundToUi = true;
        }

        public void UpdateStatus(bool status)
        {
            if (status)
            {
                statusText.Text = "Online";

                statusText.SetTextColor(Color.Green);
            }
            else
            {
                statusText.Text = "Offline";

                statusText.SetTextColor(Color.Red);
            }
        }
    }
}