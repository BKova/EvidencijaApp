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

            if(!services.Contains("com.xamarin.BackgroundServiceEvidencije")) StartService(new Intent("com.xamarin.BackgroundServiceEvidencije"));

            var backgroundServiceIntent = new Intent("com.xamarin.BackgroundServiceEvidencije");
            ServiceConnection = new BackgroundServiceConnection(this);
            BindService(backgroundServiceIntent, ServiceConnection, Bind.AutoCreate);

            base.OnCreate();
        }

        public override void OnTerminate()
        {
            UnbindService(ServiceConnection);
            base.OnTerminate();
        }
    }
}