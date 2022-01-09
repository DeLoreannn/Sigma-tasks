using System.Collections.Generic;

namespace Shop_cousework
{
    class ClientBase
    {
        private static ClientBase instance;
        private List<Client> clients;

        public List<Client> Clients
        {
            get { return clients; }
        }

        private ClientBase()
        {
            clients = new List<Client>();
        }

        public static ClientBase GetIntsance()
        {
            if (instance == null)
            {
                instance = new ClientBase();
            }
            return instance;
        }

        public override string ToString()
        {
            string result = "";
            for (int i = 0; i < Clients.Count; ++i)
            {
                result += Clients[i].ToString() + "\n";
            }
            return result;
        }
    }
}
