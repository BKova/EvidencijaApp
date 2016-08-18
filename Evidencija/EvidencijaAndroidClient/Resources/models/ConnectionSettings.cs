namespace EvidencijaAndroidClient.Resources.models
{
    class ConnectionSettings
    {
        public ConnectionSettings()
        {
            NetworkSSID = "";
            ServerIP = "";
            WebServiceLocation = "";
        }
        public string NetworkSSID { get; set; }

        public string ServerIP { get; set; }

        public string ServerPort { get; set; }
        public string WebServiceLocation { get; set; }
    }
}