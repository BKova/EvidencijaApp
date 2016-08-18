using System;

namespace EvidencijaAndroidClient.Resources.models
{
    class ConnectionState
    {
        public bool IsConnected { get; set; }

        public DateTime ConnectionBegin { get; set; }

        public bool UserAuthorized { get; set; }
    }
}