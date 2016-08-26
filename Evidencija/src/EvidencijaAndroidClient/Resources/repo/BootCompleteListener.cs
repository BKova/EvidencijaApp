///Created by: Bartul Kovaèiæ
///Github: https:github.com/BKova
using Android.App;
using Android.Content;
using Java.Lang;
using System.Linq;

namespace EvidencijaAndroidClient.Resources.repo
{
    [BroadcastReceiver]
    [IntentFilter(new[] { Intent.ActionBootCompleted })]
    class BootCompleteListener : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            try
            {
                var manager = (ActivityManager)context.GetSystemService(Context.ActivityService);
                var services = manager.GetRunningServices(int.MaxValue).Select(service => service.Service.ClassName).ToList();
                if (!services.Contains("com.xamarin.BackgroundServiceEvidencije")) context.StartService(new Intent("com.xamarin.BackgroundServiceEvidencije"));
            }
            catch(Exception ex)
            { }
        }
    }
}