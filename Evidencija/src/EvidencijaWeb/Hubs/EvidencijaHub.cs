using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evidencija.Hubs
{
    [HubName("EvidencijaHub")]
    public class EvidencijaHub : Hub
    {
        private IList<User> CurrentUsers { get; set; }

        public EvidencijaHub() : base()
        {
            CurrentUsers = new List<User>();
        }
        
        public void CheckIn(string UserName, int Key)
        {
            CurrentUsers.Add(new User(){UserName=UserName, Key = Key, ConnectionId = Context.ConnectionId, TimeRegistered = DateTime.Now});
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            CurrentUsers.Remove(CurrentUsers.Where(User => User.ConnectionId == Context.ConnectionId).SingleOrDefault());
            return base.OnDisconnected(stopCalled);
        }
    }

    internal class User
    {
        public string ConnectionId { get; set; }

        public string UserName { get; set; }

        public int Key { get; set; }

        public DateTime TimeRegistered { get; set; }


    }
}
