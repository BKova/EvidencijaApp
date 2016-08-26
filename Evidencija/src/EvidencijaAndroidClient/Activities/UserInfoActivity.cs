///Created by: Bartul Kovaèiæ
///Github: https:github.com/BKova
using Android.App;
using Android.OS;
using Android.Widget;
using System;

namespace EvidencijaAndroidClient.Activities
{
    [Activity(Label = "UserInfoActivity", Theme = "@android:style/Theme.Material.NoActionBar")]
    public class UserInfoActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.UserInfo);

            TextView userName = FindViewById<TextView>(Resource.Id.UserName);

            userName.Text = ((EvidencijaApplication)Application).ServiceConnection.Binder.BackgroundService.UserInfoChanged.UserName;

            TextView password = FindViewById<TextView>(Resource.Id.Password);

            password.Text = Convert.ToString(((EvidencijaApplication)Application).ServiceConnection.Binder.BackgroundService.UserInfoChanged.CertificationCode);

            if (password.Text.Length<3) password.Text = "";

             Button close = FindViewById<Button>(Resource.Id.CloseButton2);

            close.Click += ((object sender, EventArgs args) =>
            {
                ((EvidencijaApplication)Application).ServiceConnection.Binder.BackgroundService.UserInfoChanged.UserName = userName.Text;

                if (password.Text != "") ((EvidencijaApplication)Application).ServiceConnection.Binder.BackgroundService.UserInfoChanged.CertificationCode = Convert.ToInt32(password.Text);

                else ((EvidencijaApplication)Application).ServiceConnection.Binder.BackgroundService.UserInfoChanged.CertificationCode = -1;
                Finish();
            });
        }
    }
}