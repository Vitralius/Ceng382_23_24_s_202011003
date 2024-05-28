using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Models;

public partial class RoomsAndReservationsDatabaseContext : DbContext
{
    public RoomsAndReservationsDatabaseContext()
    {
    }

    public RoomsAndReservationsDatabaseContext(DbContextOptions<RoomsAndReservationsDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var builder = WebApplication.CreateBuilder();
        var connectionString = builder.Configuration.GetConnectionString ("MyConnection");
        optionsBuilder.UseSqlServer(connectionString);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.ToTable("reservations");

            entity.Property(e => e.ReservationId).HasColumnName("reservationId");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.IsCanceled).HasColumnName("isCanceled");
            entity.Property(e => e.IsConfirmed).HasColumnName("isConfirmed");
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
            entity.Property(e => e.ReserverId).HasColumnName("reserverId");
            entity.Property(e => e.RoomId).HasColumnName("roomId");

            entity.HasOne(d => d.Room).WithMany(p => p.Reservations).HasForeignKey(d => d.RoomId);
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.ToTable("rooms");

            entity.Property(e => e.RoomId).HasColumnName("roomId");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
            entity.Property(e => e.IsReservable).HasColumnName("isReservable");
            entity.Property(e => e.OwnerId).HasColumnName("ownerId");
            entity.Property(e => e.RoomName).HasColumnName("roomName");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
