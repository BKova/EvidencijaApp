using Android.Content;
using Android.OS;
using Android.Widget;
using EvidencijaAndroidClient.Resources.repo;

namespace EvidencijaAndroidClient
{
    public class BackgroundServiceConnection : Java.Lang.Object, IServiceConnection
    {
        public ToggleButton toggleButton { get; set; }

        public UpdateStatusColorDelegate UpdateStatusCollor { get; set; }

        public Button Apply { get; set; }

        public BackgroundServiceBinder Binder { get; set; }

        public bool isBoundToUi { get; set; }

        EvidencijaApplication application;

        public BackgroundServiceConnection(EvidencijaApplication Application)
        {
            application = Application;
        }
        public void OnServiceConnected(ComponentName name, IBinder service)
        {
            var backgroundServiceBinder = service as BackgroundServiceBinder;
            var binder = (BackgroundServiceBinder)service;
            application.Binder = binder;
            application.IsBound = true;
            this.Binder = (BackgroundServiceBinder)service;

            if (isBoundToUi == false || toggleButton == null) return;

            toggleButton.Checked = Binder.BackgroundService.IsActivated;

            toggleButton.CheckedChange += (s, args) => { Binder.BackgroundService.IsActivatedChange = args.IsChecked; };

            UpdateStatusCollor(Binder.BackgroundService.SignalRService.IsConnected);

            Apply.Click += (s, args) => { Binder.BackgroundService.ChangeSettings(s, args); };

            Binder.BackgroundService.SignalRService.UpdateStatusColor = UpdateStatusCollor;

        }

        public void OnServiceDisconnected(ComponentName name)
        {
            application.IsBound = false;
        }
    }
}