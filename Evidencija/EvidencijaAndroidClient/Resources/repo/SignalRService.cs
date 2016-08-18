using EvidencijaAndroidClient.Resources.models;
using Microsoft.AspNet.SignalR.Client;

namespace EvidencijaAndroidClient.Resources.repo
{
    public delegate void UpdateStatusColorDelegate(bool status);
    class SignalRService
    {
        private HubConnection Connection { get; set; }

        private IHubProxy Hub { get; set; }

        public ConnectionSettings Settings { get; set; }

        public UserInfo UserInfo { get; set; }

        private bool isconnected;

        public bool IsConnected { get { return isconnected; } set { isconnected = value; UpdateStatusColor(value); } }

        public UpdateStatusColorDelegate UpdateStatusColor { get; set; }
        public SignalRService(ConnectionSettings settings, UserInfo userInfo)
        {
            Settings = settings;

            UserInfo = userInfo;

            UpdateStatusColor = (val) => { };

            IsConnected = false;
        }

        public void StartConnection()
        {
            var connectionUrl = string.Format("{0}:{1}{2}/signalr", Settings.ServerIP, Settings.ServerPort, Settings.WebServiceLocation);
            Connection = new HubConnection(connectionUrl);

            Hub = Connection.CreateHubProxy("EvidencijaHub");

            Hub.Invoke("CheckIn", UserInfo.UserName, UserInfo.CertificationCode);

            Connection.Closed += (() => {
                IsConnected = false;
                Connection.Dispose();
            });

            IsConnected = true;
        }
        public void CloseConnection()
        {
            IsConnected = false;

            Connection.Dispose();
        }
    }
}