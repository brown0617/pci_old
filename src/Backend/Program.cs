using System;
using System.Data.Entity;
using Backend.Authentication.Models;
using Backend.Domain;
using Microsoft.Owin.Hosting;

namespace Backend
{
	internal class Program
	{
		private static void Main()
		{
			// Set up and seed the database:
			Console.WriteLine("Initializing and seeding database...");
			Database.SetInitializer(new AppDbInitializer());
			Database.SetInitializer(new AuthDbInitializer());

			const string uri = "http://localhost:32150";

			using (WebApp.Start<Startup>(uri))
			{
				Console.WriteLine("Server started...");
				Console.ReadKey();
				Console.WriteLine("Server stopped!");
			}
		}
	}
}