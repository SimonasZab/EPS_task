using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace ServerSide {
	public class Program {
		public static void Main(string[] args) {
			/*if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "LocalDB.sqlite"))) {
				SQLiteConnection.CreateFile("LocalDB.sqlite");
			}*/

			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder => {
					webBuilder.UseStartup<Startup>();
				});
	}
}
