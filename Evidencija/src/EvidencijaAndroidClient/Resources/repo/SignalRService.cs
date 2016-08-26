///Created by: Bartul Kovaèiæ
///Github: https:github.com/BKova
using EvidencijaAndroidClient.Resources.models;
using Microsoft.AspNet.SignalR.Client;
using System;

namespace EvidencijaAndroidClient.Resources.repo
{
    public delegate void UpdateStatusColorDelegate(bool status);
    public class SignalRService
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

        public async void StartConnection()
        {
            var connectionUrl = string.Format("{0}:{1}{2}", Settings.ServerIP, Settings.ServerPort, Settings.WebServiceLocation);
            Connection = new HubConnection(connectionUrl);

            Connection.Closed += CloseConnection;

            Hub = Connection.CreateHubProxy("EvidencijaHub");

            await Connection.Start();

            try { await Hub.Invoke("CheckIn", UserInfo.UserName, UserInfo.CertificationCode); }
            catch (Exception ex)
            {
                this.CloseConnection();
            }

            IsConnected = true;
        }
        public void CloseConnection()
        {
            IsConnected = false;

            Connection.Stop();

            Connection.Dispose();
        }
    }
}