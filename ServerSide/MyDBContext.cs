using Microsoft.EntityFrameworkCore;
using ServerSide.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ServerSide {
	public class MyDBContext : DbContext {
		public DbSet<PromoCode> PromoCodes { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder options)
			=> options.UseSqlite(@"Data Source=" + Path.Combine(Directory.GetCurrentDirectory(), "LocalDB.sqlite"));
	}
}
