using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestProject.Email
{
   public interface IEmailSender
    {
        void SendEmail(Message message);
    }
}
