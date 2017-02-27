using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using MyAmbu.Dto;
using Microsoft.AspNet.SignalR.Hubs;

namespace MyAmbu
{
    [HubName("hubAlarma")]
    public class HubAlarma : Hub
    {
        private static Dictionary<string, string> ListadoAmbulancias = new Dictionary<string, string>();
        //public void Hello()
        //{
        //    Clients.All.hello();
        //}

        public void registerConId(String userID)
        {
            //bool alreadyExists = false;
            if (ListadoAmbulancias.ContainsKey(userID))                  
            {
                ListadoAmbulancias.Remove(userID);
            }
            ListadoAmbulancias.Add(userID, Context.ConnectionId);
        }
        public void LogoutId(String userID)
        {
            //bool alreadyExists = false;
            if (ListadoAmbulancias.ContainsKey(userID))
            {
                ListadoAmbulancias.Remove(userID);
            }
            
        }

        public void Send(string Pedido, string Ambu)
        {
            // Call the broadcastMessage method to update clients.


            String Ambulancia;
            if (ListadoAmbulancias.TryGetValue(Ambu, out Ambulancia))
            {
                
                var context = GlobalHost.ConnectionManager.GetHubContext<HubAlarma>();
                
                context.Clients.Client(Ambulancia).broadcastMessage(Pedido);

            }
        }
        public Dictionary<string, string> Listado() {
            return ListadoAmbulancias;
        }
    }
}