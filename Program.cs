using Microsoft.Extensions.DependencyInjection;
using worker_service_for_smtp;
using worker_service_for_smtp.Interfaces;
using worker_service_for_smtp.services;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddSingleton<ISmtpService, SmtpService>();
builder.Services.AddHostedService<SmtpWorker>();

var host = builder.Build();
host.Run();
