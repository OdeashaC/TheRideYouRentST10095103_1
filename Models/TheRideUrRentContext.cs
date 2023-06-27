using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TheRideYouRentST10095103_1.Models;

public partial class TheRideUrRentContext : DbContext
{
    public TheRideUrRentContext()
    {
    }

    public TheRideUrRentContext(DbContextOptions<TheRideUrRentContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Car> Cars { get; set; }

    public virtual DbSet<CarBodyType> CarBodyTypes { get; set; }

    public virtual DbSet<CarMake> CarMakes { get; set; }

    public virtual DbSet<Driver> Drivers { get; set; }

    public virtual DbSet<Inspector> Inspectors { get; set; }

    public virtual DbSet<Rental> Rentals { get; set; }

    public virtual DbSet<ReturnCar> ReturnCars { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder != null)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("MyConnectionStringAZURE"));
        }
    }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(e => e.CarNo).HasName("PK__Car__52362C6AE703E4E1");

            entity.ToTable("Car");

            entity.Property(e => e.CarNo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Car_No");
            entity.Property(e => e.Available)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CarBodyTypeId).HasColumnName("CarBodyTypeID");
            entity.Property(e => e.CarMakeId).HasColumnName("CarMakeID");
            entity.Property(e => e.CarModel)
                .HasMaxLength(250)
                .IsUnicode(false);

            entity.HasOne(d => d.CarBodyType).WithMany(p => p.Cars)
                .HasForeignKey(d => d.CarBodyTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Car__CarBodyType__403A8C7D");

            entity.HasOne(d => d.CarMake).WithMany(p => p.Cars)
                .HasForeignKey(d => d.CarMakeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Car__CarMakeID__3F466844");
        });

        modelBuilder.Entity<CarBodyType>(entity =>
        {
            entity.HasKey(e => e.CarBodyTypeId).HasName("PK__CarBodyT__2BA49AEBC687853F");

            entity.ToTable("CarBodyType");

            entity.Property(e => e.CarBodyTypeId).HasColumnName("CarBodyTypeID");
            entity.Property(e => e.CarBodyTypeDescription)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CarMake>(entity =>
        {
            entity.HasKey(e => e.CarMakeId).HasName("PK__CarMake__A125EE7C5138D4A0");

            entity.ToTable("CarMake");

            entity.Property(e => e.CarMakeId).HasColumnName("CarMakeID");
            entity.Property(e => e.CarMakeDescription)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Driver>(entity =>
        {
            entity.HasKey(e => e.DriverId).HasName("PK__Driver__F1B1CD246E2D936D");

            entity.ToTable("Driver");

            entity.Property(e => e.DriverId).HasColumnName("DriverID");
            entity.Property(e => e.DriverAddress)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Driver_Address");
            entity.Property(e => e.DriverEmail)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Driver_Email");
            entity.Property(e => e.DriverMobile)
                .HasMaxLength(20)
                .HasColumnName("Driver_Mobile");
            entity.Property(e => e.DriverName)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Driver_Name");
        });

        modelBuilder.Entity<Inspector>(entity =>
        {
            entity.HasKey(e => e.InspectorNo).HasName("PK__Inspecto__F49FBEAFAAC179A4");

            entity.ToTable("Inspector");

            entity.Property(e => e.InspectorNo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Inspector_no");
            entity.Property(e => e.InspectorEmail)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Inspector_Email");
            entity.Property(e => e.InspectorMobile)
                .HasMaxLength(20)
                .HasColumnName("Inspector_Mobile");
            entity.Property(e => e.InspectorName)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Inspector_Name");
        });

        modelBuilder.Entity<Rental>(entity =>
        {
            entity.HasKey(e => e.RentalNo).HasName("PK__Rental__9D238DBDD99C8D3B");

            entity.ToTable("Rental");

            entity.Property(e => e.RentalNo).HasColumnName("Rental_No");
            entity.Property(e => e.CarNo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Car_No");
            entity.Property(e => e.EndDate).HasColumnType("date");
            entity.Property(e => e.InspectorNo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Inspector_no");
            entity.Property(e => e.RentalFee).HasColumnName("Rental_Fee");
            entity.Property(e => e.StartDate).HasColumnType("date");

            entity.HasOne(d => d.CarNumber).WithMany(p => p.Rentals)
                .HasForeignKey(d => d.CarNo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Rental__Car_No__44FF419A");

            entity.HasOne(d => d.Driver).WithMany(p => p.Rentals)
                .HasForeignKey(d => d.Driverid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Rental__Driverid__4316F928");

            entity.HasOne(d => d.InspectorNumber).WithMany(p => p.Rentals)
                .HasForeignKey(d => d.InspectorNo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Rental__Inspecto__440B1D61");
        });

        modelBuilder.Entity<ReturnCar>(entity =>
        {
            entity.HasKey(e => e.ReturnNo).HasName("PK__ReturnCa__0F4F6B99A0E9E958");

            entity.ToTable("ReturnCar");

            entity.Property(e => e.ReturnNo).HasColumnName("Return_No");
            entity.Property(e => e.CarNo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Car_No");
            entity.Property(e => e.DriverId).HasColumnName("DriverID");
            entity.Property(e => e.ElapsedDate).HasColumnName("Elapsed_Date");
            entity.Property(e => e.InspectorNo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Inspector_no");
            entity.Property(e => e.RentalNo).HasColumnName("Rental_No");
            entity.Property(e => e.ReturnDate)
                .HasColumnType("date")
                .HasColumnName("Return_Date");

            entity.HasOne(d => d.CarNumber).WithMany(p => p.ReturnCars)
                .HasForeignKey(d => d.CarNo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ReturnCar__Car_N__49C3F6B7");

            entity.HasOne(d => d.Driver).WithMany(p => p.ReturnCars)
                .HasForeignKey(d => d.DriverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ReturnCar__Drive__47DBAE45");

            entity.HasOne(d => d.InspectorNumber).WithMany(p => p.ReturnCars)
                .HasForeignKey(d => d.InspectorNo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ReturnCar__Inspe__48CFD27E");

            entity.HasOne(d => d.RentalNumber).WithMany(p => p.ReturnCars)
                .HasForeignKey(d => d.RentalNo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ReturnCar__Renta__4AB81AF0");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
