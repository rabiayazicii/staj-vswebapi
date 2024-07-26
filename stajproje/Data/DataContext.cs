using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using stajproje.Model;

namespace stajproje.com
{
	public class DataContext : DbContext
	{

		public DbSet<NYO_SosyalYardimModel> NYO_SosyalYardimModels { get; set; }

		public DataContext(DbContextOptions<DataContext> options) : base(options)
		{

		}


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<NYO_SosyalYardimModel>().HasData(new NYO_SosyalYardimModel
			{
				
			});


			base.OnModelCreating(modelBuilder);
		}

	
	}
}