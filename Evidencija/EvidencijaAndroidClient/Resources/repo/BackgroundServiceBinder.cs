using Android.OS;

namespace EvidencijaAndroidClient
{
    public class BackgroundServiceBinder : Binder
    {
        public BackgroundService BackgroundService { get; set; }

        public BackgroundServiceBinder(BackgroundService backgroundService)
        {
            BackgroundService = backgroundService;
        }
    }
}