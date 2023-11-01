using System;
using System.Collections.Generic;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer;

public partial class SswdatabaseContext : DbContext
{
	public SswdatabaseContext()
	{
	}

	public SswdatabaseContext(DbContextOptions<SswdatabaseContext> options)
		: base(options)
	{
	}

	public virtual DbSet<Account> Accounts { get; set; }

	public virtual DbSet<AccountDetail> AccountDetails { get; set; }

	public virtual DbSet<FeedBack> FeedBacks { get; set; }

	public virtual DbSet<Image> Images { get; set; }

	public virtual DbSet<OrderProduct> OderProducts { get; set; }

	public virtual DbSet<Order> Orders { get; set; }

	public virtual DbSet<OrderService> OrderServices { get; set; }

	public virtual DbSet<Payment> Payments { get; set; }

	public virtual DbSet<Product> Products { get; set; }

	public virtual DbSet<Service> Services { get; set; }


	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Account>().HasOne(a => a.AccountDetail).WithOne(a => a.Account)
			.HasConstraintName("FK_Account_AccountDetail");

		modelBuilder.Entity<FeedBack>().HasKey(f => f.Id);
		modelBuilder.Entity<FeedBack>().HasOne(f => f.Order).WithOne(o => o.FeedBack)
			.HasConstraintName("FK_Order_Feedback");

		modelBuilder.Entity<Image>().HasKey(i => i.Id);
		modelBuilder.Entity<Image>().HasOne(i => i.Account).WithMany(a => a.Images)
			.HasConstraintName("FK_Account_Image");

		modelBuilder.Entity<Order>().HasKey(o => o.Id);
		modelBuilder.Entity<Order>().HasOne(o => o.Account).WithMany(a => a.Orders)
			.HasConstraintName("FK_Account_Order");

		modelBuilder.Entity<Product>().HasKey(p => p.Id);
		modelBuilder.Entity<Service>().HasKey(s => s.Id);

		modelBuilder.Entity<OrderProduct>().HasKey(op => new { op.OrderId, op.ProductId });
		modelBuilder.Entity<OrderProduct>().HasOne(op => op.Product).WithMany(p => p.Orders)
			.HasConstraintName("FK_Product_OrderProduct");
		modelBuilder.Entity<OrderProduct>().HasOne(op => op.Order).WithMany(o => o.Products)
			.HasConstraintName("FK_Order_OrderProduct");

		modelBuilder.Entity<OrderService>().HasKey(os => new { os.OrderId, os.ServiceId });
		modelBuilder.Entity<OrderService>().HasOne(os => os.Service).WithMany(s => s.Orders)
			.HasConstraintName("FK_Service_OrderService");
		modelBuilder.Entity<OrderService>().HasOne(os => os.Order).WithMany(o => o.Services)
			.HasConstraintName("FK_Order_OrderService");

		modelBuilder.Entity<Payment>().HasKey(p => p.OrderId);
		modelBuilder.Entity<Payment>().HasOne(p => p.Order).WithOne(o => o.Payment)
			.HasConstraintName("FK_Order_Payment");

		OnModelCreatingPartial(modelBuilder);
	}

	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
