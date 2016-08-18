using Android.App;
using Android.Widget;
using Android.OS;
using static Android.Widget.CompoundButton;
using Android.Graphics;
using EvidencijaAndroidClient.Activities;
using System;
using Android.Content;

namespace EvidencijaAndroidClient
{
    [Activity(Label = "EvidencijaAndroidClient",Theme = "@android:style/Theme.Material.NoActionBar", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private TextView _statusText;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it

            Button connectionSettings = FindViewById<Button>(Resource.Id.ConnectionSettingsButton);

            connectionSettings.Click += ((object sender, EventArgs args) => {
                var intent = new Intent(this, typeof(ConnectionSettingsActivity));
                StartActivity(intent);
            });

            Button userSettings = FindViewById<Button>(Resource.Id.UserSettingsButton);

            userSettings.Click += ((object sender, EventArgs args) => {
                var intent = new Intent(this, typeof(UserInfoActivity));
                StartActivity(intent);
            });

            Button apply = FindViewById<Button>(Resource.Id.ApplyButton);

            apply.Click += ((EvidencijaApplication)Application).ChangeSettings;

            ToggleButton onOff = FindViewById<ToggleButton>(Resource.Id.OnOffButton);

            onOff.Checked = ((EvidencijaApplication)Application).IsActivated;

            onOff.CheckedChange += ((object sender, CheckedChangeEventArgs args) => {
                ((EvidencijaApplication)Application).IsActivatedChange = args.IsChecked;
            });

            _statusText = FindViewById<TextView>(Resource.Id.StatusText);

            UpdateStatus(((EvidencijaApplication)Application).SignalRService.IsConnected);

            ((EvidencijaApplication)Application).SignalRService.UpdateStatusColor = UpdateStatus;
        }

        public void UpdateStatus(bool status)
        {
            if (status)
            {
                _statusText.Text = "Online";

                _statusText.SetTextColor(Color.Green);
            }
            else
            {
                _statusText.Text = "Offline";

                _statusText.SetTextColor(Color.Red);
            }
        }
    }
}