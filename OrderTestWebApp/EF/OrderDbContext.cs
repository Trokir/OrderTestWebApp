﻿using Microsoft.EntityFrameworkCore;

using OrderTestWebApp.Models;

namespace OrderTestWebApp.EF
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) { }
        public DbSet<Order> Orders { get; set; }
    }
}
