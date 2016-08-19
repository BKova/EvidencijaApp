using Android.App;
using Android.Content;
using Android.Widget;

namespace EvidencijaAndroidClient.Resources.repo
{
    [BroadcastReceiver]
    [IntentFilter(new[] { Intent.ActionBootCompleted })]
    class BootCompleteListener : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            //Intent service = new Intent("com.xamarin.BackgroundServiceEvidencije");
            //service.AddFlags(ActivityFlags.NewTask);
            //service.AddFlags(ActivityFlags.FromBackground);
            //service.PutExtras(intent);
            //context.StartService(service);

            if (intent.Action == Intent.ActionBootCompleted)
            {
                Toast.MakeText
                (
                    context,
                    "The BootCompletedExample application catches the BootCompleted broadcast message",
                    ToastLength.Long
                ).Show();
            }
        }
    }
}