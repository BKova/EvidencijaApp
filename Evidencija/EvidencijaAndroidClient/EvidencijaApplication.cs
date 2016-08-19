using System;
using Android.App;
using Android.Runtime;
using Android.Content;
using System.Linq;

namespace EvidencijaAndroidClient
{
    [Application]
    public class EvidencijaApplication : Application
    {
        public BackgroundServiceBinder Binder { get; set; }

        public BackgroundServiceConnection ServiceConnection { get; set; }

        public bool IsBound { get; set; }

        public void ChangeSettings(object sender, EventArgs args)
        {
            Binder.BackgroundService.ChangeSettings(sender, args);
        }
        public EvidencijaApplication(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {}

        public override void OnCreate()
        {
            var manager = (ActivityManager)GetSystemService(ActivityService);
            var services = manager.GetRunningServices(int.MaxValue).Select(service => service.Service.ClassName).ToList();

            StartService(new Intent("com.xamarin.BackgroundServiceEvidencije"));

            var backgroundServiceIntent = new Intent("com.xamarin.BackgroundServiceEvidencije");
            var serviceConnection = new BackgroundServiceConnection(this);
            ServiceConnection = serviceConnection;
            var success = BindService(backgroundServiceIntent, serviceConnection, Bind.AutoCreate);

            base.OnCreate();
        }
    }
}