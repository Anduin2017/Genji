using System;
using System.IO;
using Genji.Library;
using Genji.Models;
using Genji.Models.Middlewares;
using Genji.Service;
using GenjiExample.Data;
using GenjiExample.Controllers;
using Microsoft.EntityFrameworkCore;

namespace GenjiExample
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Starting Genji server...");

            var services = StartUp.ConfigServices();
            var server = StartUp.ConfigureServer(services);
            server.Start().Wait();
        }
    }
}