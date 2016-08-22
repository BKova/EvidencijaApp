using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using EvidencijaAndroidClient.Resources.models;
using EvidencijaAndroidClient.Resources.repo;
using Android.Net;
using Android.Widget;
using Android.Util;

namespace EvidencijaAndroidClient
{
    [Service]
    [IntentFilter(new String[] { "com.xamarin.BackgroundServiceEvidencije" })]
    public class BackgroundService : Service
    {
        BackgroundServiceBinder Binder { get; set; }

        public UserInfo UserInfo { get; set; }

        public ConnectionSettings ConnectionSettings { get; set; }

        public UserInfo UserInfoChanged { get; set; }

        public ConnectionSettings ConnectionSettingsChanged { get; set; }

        public ConnectionService ConnectionService { get; set; }

        public NetworkStatusListener Reciver { get; set; }

        public SignalRService SignalRService { get; set; }

        public bool IsActivated { get; set; }

        public bool IsActivatedChange { get; set; }

        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            UserInfo = DataStorageService.LoadData<UserInfo>("UserInfo.json", this);

            if (UserInfo == null)
            {
                UserInfo = new UserInfo();
                DataStorageService.StoreData(UserInfo, "UserInfo.json", this);
            }

            ConnectionSettings = DataStorageService.LoadData<ConnectionSettings>("ConnectionSettings.json", this);
            if (ConnectionSettings == null)
            {
                ConnectionSettings = new ConnectionSettings();
                DataStorageService.StoreData(ConnectionSettings, "ConnectionSettings.json", this);
            }

            IsActivated = DataStorageService.LoadData<bool>("IsActivated.json", this);
            if (IsActivated == default(bool))
            {
                IsActivated = false;
                DataStorageService.StoreData(IsActivated, "IsActivated.json", this);
            }
            SignalRService = new SignalRService(ConnectionSettings, UserInfo);

            ConnectionService = new ConnectionService(ConnectionSettings);

            UserInfoChanged = UserInfo;

            ConnectionSettingsChanged = ConnectionSettings;

            IsActivatedChange = IsActivated;

            Reciver = new NetworkStatusListener();

            Reciver.CheckNetwork = ConnectionService.CheckNetwork;

            Reciver.RegisterOnNetwork = SignalRService.StartConnection;

            RegisterReceiver(Reciver, new IntentFilter(ConnectivityManager.ConnectivityAction));

            return StartCommandResult.Sticky;
        }

        public void ChangeSettings(object sender, EventArgs args)
        {
            bool CurrentConnectionStatus = SignalRService.IsConnected;

            if (CurrentConnectionStatus) SignalRService.CloseConnection();

            ConnectionSettings = ConnectionSettingsChanged;

            UserInfo = UserInfoChanged;

            IsActivated = IsActivatedChange;

            SignalRService.Settings = ConnectionSettings;

            SignalRService.UserInfo = UserInfo;

            if (CurrentConnectionStatus) SignalRService.StartConnection();

            ConnectionService.Settings = ConnectionSettings;

            DataStorageService.StoreData(UserInfo, "UserInfo.json", this);
            DataStorageService.StoreData(ConnectionSettings, "ConnectionSettings.json", this);
            DataStorageService.StoreData(IsActivated, "IsActivated.json", this);
        }

        public override IBinder OnBind(Intent intent)
        {
            Binder = new BackgroundServiceBinder(this);
            return Binder;
        }
    }

    public class BackgroundServiceBinder : Binder
    {
        public BackgroundService BackgroundService { get; set; }

        public BackgroundServiceBinder(BackgroundService backgroundService)
        {
            BackgroundService = backgroundService;
        }
    }
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