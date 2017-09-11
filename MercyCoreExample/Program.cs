using System;
using System.IO;
using Mercy.Library;
using Mercy.Models;
using Mercy.Models.Middlewares;
using Mercy.Service;
using MercyCoreExample.Data;
using MercyCoreExample.Controllers;
using Microsoft.EntityFrameworkCore;

namespace MercyCoreExample
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Starting Mercy server...");

            var services = StartUp.ConfigServices();

            var server = StartUp.ConfigureServer(services);

            server.Start().Wait();
        }
    }
}