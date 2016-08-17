using Android.Content;
using Android.Net;
using Android.Net.Wifi;

namespace EvidencijaAndroidClient.Resources.repo
{
    [BroadcastReceiver]
    class NetworkStatusListener : BroadcastReceiver
    {
        //private RequestService _requestServiceManager;

        //public NetworkStatusListener(RequestService requestService)
        //{
        //    _requestServiceManager = requestService;
        //}
        public override void OnReceive(Context context, Intent intent)
        {
            ConnectivityManager connectivityManager = (ConnectivityManager)context.GetSystemService(Context.ConnectivityService);
            NetworkInfo activeConnection = connectivityManager.ActiveNetworkInfo;
            bool isConnectedOnWifi = activeConnection.Type == ConnectivityType.Wifi || activeConnection.Type == ConnectivityType.Wimax && activeConnection.IsAvailable;
            if (isConnectedOnWifi)
            {
                WifiManager wifiManager = (WifiManager)context.GetSystemService(Context.WifiService);

                //bool isCorrespondingNetwork = _requestServiceManager.CheckNetwork(wifiManager);

                //if (isCorrespondingNetwork) _requestServiceManager.RegisterOnNetwork();
            }
        }
    }
}