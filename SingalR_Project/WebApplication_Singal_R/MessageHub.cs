using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WebApplication_Singal_R
{
    public class MessageHub : Hub
    {
        private static Dictionary<string, string> users = new Dictionary<string, string>();


        public void AddMe(string name)
        {
            users.Add(Context.ConnectionId, name);
            Clients.All.updateUsers(users.Values.ToArray());
        }

        public void Send(string name, string msg)
        {
            Clients.All.broadCast(name, msg);
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            users.Remove(Context.ConnectionId);
            Clients.All.updateUsers(users.Values.ToArray());

            return base.OnDisconnected(stopCalled);

        }
    }
}