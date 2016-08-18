using Android.Content;
using Android.Net;
using Android.Net.Wifi;
using System.Threading.Tasks;

namespace EvidencijaAndroidClient.Resources.repo
{
    public delegate Task<bool> CheckNetworkDelegate(WifiManager wifiManager);

    public delegate void RegisterOnNetworkDelegate();

    [BroadcastReceiver]
    class NetworkStatusListener : BroadcastReceiver
    {
        public CheckNetworkDelegate CheckNetwork { get; set; }

        public RegisterOnNetworkDelegate RegisterOnNetwork { get; set; }

        public override async void OnReceive(Context context, Intent intent)
        {
            bool isActivated = ((EvidencijaApplication)context).IsActivated;

            if (!isActivated)
            {
                return;
            }

            ConnectivityManager connectivityManager = (ConnectivityManager)context.GetSystemService(Context.ConnectivityService);
            NetworkInfo activeConnection = connectivityManager.ActiveNetworkInfo;
            if (activeConnection == null)
            {
                return;
            }
            bool isConnectedOnWifi = activeConnection.Type == ConnectivityType.Wifi && activeConnection.IsConnected;
            if (isConnectedOnWifi)
            {
                WifiManager wifiManager = (WifiManager)context.GetSystemService(Context.WifiService);

                bool isCorrespondingNetwork = await CheckNetwork(wifiManager);

                if (isCorrespondingNetwork) RegisterOnNetwork();
            }
        }
    }
}