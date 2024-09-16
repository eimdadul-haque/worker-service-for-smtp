using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace worker_service_for_smtp.Interfaces
{
    public interface ISmtpService
    {
        Task SendEmailAsync(string toEmail, string subject, string body);
    }
}
