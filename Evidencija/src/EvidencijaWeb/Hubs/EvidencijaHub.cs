using Evidencija.Database.Models;
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
        private UserCollection CurrentUsers { get; set; }

        private IDbContextBinder _binder;

        public EvidencijaHub(IDbContextBinder Binder, UserCollection UserCollection) : base()
        {
            CurrentUsers = UserCollection;
            _binder = Binder;
        }
        
        public void CheckIn(string UserName, int Key)
        {
            CurrentUsers.Users.Add(new User(){UserName=UserName, Key = Key, ConnectionId = Context.ConnectionId, TimeRegistered = DateTime.Now});
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            TimeStamp Stamp = new TimeStamp();

            var connectionUser = CurrentUsers.Users.Where(User => User.ConnectionId == Context.ConnectionId).SingleOrDefault();

            Stamp.User = _binder.GetUser(connectionUser.UserName, connectionUser.Key);

            if(Stamp.User != default(Database.Models.User))
            {
                Stamp.Start = connectionUser.TimeRegistered;
                Stamp.End = DateTime.Now;
                Stamp.Duration = Stamp.End - Stamp.Start;
                Stamp.Closed = true;
                _binder.CreateTimeStamp(Stamp);
            }

                CurrentUsers.Users.Remove(CurrentUsers.Users.Where(User => User.ConnectionId == Context.ConnectionId).SingleOrDefault());
            return base.OnDisconnected(stopCalled);
        }
    }
    public class UserCollection
    {
        public UserCollection()
        {
            Users = new List<User>();
        }
        public IList<User> Users { get; set; }
    }

    public class User
    {
        public string ConnectionId { get; set; }

        public string UserName { get; set; }

        public int Key { get; set; }

        public DateTime TimeRegistered { get; set; }


    }
}
