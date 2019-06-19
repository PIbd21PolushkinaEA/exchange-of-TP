using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShopServiceDAL.Interfaces
{
    public interface IBackUpService
    {
        void ClientBackUpXML(int ClientId);

        void ClientBackUpJSON(int ClientId);
    }
}
