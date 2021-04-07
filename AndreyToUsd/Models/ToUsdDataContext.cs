﻿using AndreyToUsd.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AndreyToUsd.Data
{

       public class ToUsdContext : DbContext
    {

    
        public ToUsdContext(DbContextOptions<ToUsdContext> options)
            : base(options)
        {
        }

    
        public DbSet<RateToUsd> Rates { get; set; }
          
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            _ = options.UseSqlite("Data Source=./Data/toUSd.sqlite");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RateToUsd>(entity =>
            {

                entity.ToTable("RateUsd", "toUSD");

                entity.HasKey(e => e.code)
                    .HasName("PK_CODE");

                entity.Property(e => e.name)
                    .HasColumnName("name")
                    .HasColumnType("varchar")
                    .HasMaxLength(30);


                entity.Property(e => e.rate)
                    .HasColumnName("rate");
                entity.Property(e => e.bid)
                     .HasColumnName("bid");
                entity.Property(e => e.ask)
                     .HasColumnName("ask");
                entity.Property(e => e.stored)
                     .HasColumnName("stored");
                entity.Property(e => e.lastRefreshed)
                     .HasColumnName("lastRefreshed");
            });

            //    modelBuilder.Entity<Emp>(entity =>
            //    {
            //        entity.HasKey(e => e.Empno)
            //            .HasName("PK_EMP");

            //        entity.ToTable("EMP", "DEMOBASE");

            //        entity.Property(e => e.Deptno).HasColumnName("DEPTNO");

            //        entity.Property(e => e.Ename)
            //            .HasColumnName("ENAME")
            //            .HasColumnType("varchar")
            //            .HasMaxLength(10);

            //        entity.Property(e => e.Hiredate)
            //            .HasColumnType("date");

            //        entity.Property(e => e.Job)
            //            .HasColumnType("varchar")
            //            .HasMaxLength(9);

            //        entity.HasOne(d => d.DeptnoNavigation)
            //             .WithMany(p => p.Emp)
            //             .HasForeignKey(d => d.Deptno)
            //    });
            //}
        }

}
}