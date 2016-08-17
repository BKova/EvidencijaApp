using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using EvidencijaAndroidClient.Resources.repo;

namespace EvidencijaAndroidClient
{
    [Activity(Label = "EvidencijaAndroidClient",Theme = "@android:style/Theme.Material.NoActionBar", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            var Req = new ConnectionTest(@"http://192.168.31.58:500");

            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);

            button.Click += Req.SendRequest;
        }
    }
}

