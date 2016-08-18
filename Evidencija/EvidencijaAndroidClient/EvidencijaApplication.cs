using System;
using Android.App;
using Android.Runtime;
using EvidencijaAndroidClient.Resources.models;
using EvidencijaAndroidClient.Resources.repo;
using Android.Content;
using Android.Net;

namespace EvidencijaAndroidClient
{
    [Application]
    class EvidencijaApplication : Application
    {
        public UserInfo UserInfo { get; set; }

        public ConnectionState ConnectionState { get; set; }

        public ConnectionSettings ConnectionSettings { get; set; }

        public UserInfo UserInfoChanged { get; set; }

        public ConnectionSettings ConnectionSettingsChanged { get; set; }

        public ConnectionService ConnectionService { get; set; }

        public NetworkStatusListener Reciver { get; set; }

        public SignalRService SignalRService { get; set; }

        public bool IsActivated { get; set; }

        public bool IsActivatedChange { get; set; }

        public EvidencijaApplication(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {}

        public override void OnCreate()
        {
            UserInfo = DataStorageService.LoadData<UserInfo>("UserInfo.json",Context);

            if (UserInfo == null)
            {
                UserInfo = new UserInfo();
                DataStorageService.StoreData(UserInfo, "UserInfo.json",Context);
            }

            ConnectionSettings = DataStorageService.LoadData<ConnectionSettings>("ConnectionSettings.json",Context);
            if (ConnectionSettings == null)
            {
                ConnectionSettings = new ConnectionSettings();
                DataStorageService.StoreData(ConnectionSettings,"ConnectionSettings.json",Context);
            }

            IsActivated = DataStorageService.LoadData<bool>("IsActivated.json",Context);
            if (IsActivated == default(bool))
            {
                IsActivated = false;
                DataStorageService.StoreData(IsActivated, "IsActivated.json",Context);
            }
            SignalRService = new SignalRService(ConnectionSettings, UserInfo);

            ConnectionService = new ConnectionService(ConnectionSettings);

            UserInfoChanged = UserInfo;

            ConnectionSettingsChanged = ConnectionSettings;

            Reciver = new NetworkStatusListener();

            Reciver.CheckNetwork = ConnectionService.CheckNetwork;

            Reciver.RegisterOnNetwork = SignalRService.StartConnection;

            RegisterReceiver(Reciver, new IntentFilter(ConnectivityManager.ConnectivityAction));

            base.OnCreate();
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

            if(CurrentConnectionStatus) SignalRService.StartConnection();

            ConnectionService.Settings = ConnectionSettings;

            DataStorageService.StoreData(UserInfo, "UserInfo.json",Context);
            DataStorageService.StoreData(ConnectionSettings, "ConnectionSettings.json",Context);
            DataStorageService.StoreData(IsActivated, "IsActivated.json",Context);
        }
    }
}