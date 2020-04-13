using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Services
{
    public interface IMailService
    {
        void SendMessage(string to, string subject, string body);
    }
}
