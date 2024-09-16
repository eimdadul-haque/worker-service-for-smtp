using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using worker_service_for_smtp.Interfaces;

namespace worker_service_for_smtp
{
    public class SmtpWorker : BackgroundService
    {
        private readonly ILogger<SmtpWorker> _logger;
        private readonly ISmtpService _smtpService;
        public SmtpWorker(
            ISmtpService smtpService, 
            ILogger<SmtpWorker> logger)
        {
            _smtpService = smtpService;
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int count = 1;
            while (!stoppingToken.IsCancellationRequested)
            {
                string subject = $"Subject No: {count}";
                string body = "This is a test mail.";
                string toEmail = "eimdadulhaque@gmail.com";

                await _smtpService.SendEmailAsync(toEmail, subject, body);

                if (_logger.IsEnabled(LogLevel.Information)) 
                {
                    _logger.LogInformation($"Email No: {count}");
                }

                count += 1;
                await Task.Delay(3000, stoppingToken);
            }
        }
    }
}
