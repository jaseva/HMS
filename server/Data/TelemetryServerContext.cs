using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;

using Hms.Models.TelemetryServer;

namespace Hms.Data
{
  public partial class TelemetryServerContext : Microsoft.EntityFrameworkCore.DbContext
  {
    public TelemetryServerContext(DbContextOptions<TelemetryServerContext> options):base(options)
    {
    }

    public TelemetryServerContext()
    {
    }

    partial void OnModelBuilding(ModelBuilder builder);

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);


        builder.Entity<Hms.Models.TelemetryServer.Room>()
              .Property(p => p.users)
              .HasDefaultValueSql("0").ValueGeneratedNever();

        builder.Entity<Hms.Models.TelemetryServer.Simulationcontrol>()
              .Property(p => p.suit_power)
              .HasDefaultValueSql("0").ValueGeneratedNever();

        builder.Entity<Hms.Models.TelemetryServer.Simulationcontrol>()
              .Property(p => p.o2_switch)
              .HasDefaultValueSql("0").ValueGeneratedNever();

        builder.Entity<Hms.Models.TelemetryServer.Simulationcontrol>()
              .Property(p => p.aux)
              .HasDefaultValueSql("0").ValueGeneratedNever();

        builder.Entity<Hms.Models.TelemetryServer.Simulationcontrol>()
              .Property(p => p.rca)
              .HasDefaultValueSql("0").ValueGeneratedNever();

        builder.Entity<Hms.Models.TelemetryServer.Simulationcontrol>()
              .Property(p => p.pump)
              .HasDefaultValueSql("0").ValueGeneratedNever();

        builder.Entity<Hms.Models.TelemetryServer.Simulationcontrol>()
              .Property(p => p.fan_switch)
              .HasDefaultValueSql("0").ValueGeneratedNever();

        builder.Entity<Hms.Models.TelemetryServer.Simulationfailure>()
              .Property(p => p.o2_error)
              .HasDefaultValueSql("0").ValueGeneratedNever();

        builder.Entity<Hms.Models.TelemetryServer.Simulationfailure>()
              .Property(p => p.pump_error)
              .HasDefaultValueSql("0").ValueGeneratedNever();

        builder.Entity<Hms.Models.TelemetryServer.Simulationfailure>()
              .Property(p => p.power_error)
              .HasDefaultValueSql("0").ValueGeneratedNever();

        builder.Entity<Hms.Models.TelemetryServer.Simulationfailure>()
              .Property(p => p.fan_error)
              .HasDefaultValueSql("0").ValueGeneratedNever();

        builder.Entity<Hms.Models.TelemetryServer.Simulationstate>()
              .Property(p => p.isRunning)
              .HasDefaultValueSql("0").ValueGeneratedNever();

        builder.Entity<Hms.Models.TelemetryServer.Simulationstate>()
              .Property(p => p.isPaused)
              .HasDefaultValueSql("0").ValueGeneratedNever();

        builder.Entity<Hms.Models.TelemetryServer.Simulationstate>()
              .Property(p => p.timer)
              .HasDefaultValueSql("'00:00:00'");

        builder.Entity<Hms.Models.TelemetryServer.Simulationstate>()
              .Property(p => p.started_at)
              .HasDefaultValueSql("'00:00:00'");

        builder.Entity<Hms.Models.TelemetryServer.Simulationstate>()
              .Property(p => p.t_battery)
              .HasDefaultValueSql("'00:00:00'");

        builder.Entity<Hms.Models.TelemetryServer.Simulationstate>()
              .Property(p => p.t_oxygen)
              .HasDefaultValueSql("'00:00:00'");

        builder.Entity<Hms.Models.TelemetryServer.Simulationstate>()
              .Property(p => p.t_water)
              .HasDefaultValueSql("'00:00:00'");

        builder.Entity<Hms.Models.TelemetryServer.Simulationstateuium>()
              .Property(p => p.emu1)
              .HasDefaultValueSql("'OFF'");

        builder.Entity<Hms.Models.TelemetryServer.Simulationstateuium>()
              .Property(p => p.emu2)
              .HasDefaultValueSql("'OFF'");

        builder.Entity<Hms.Models.TelemetryServer.Simulationstateuium>()
              .Property(p => p.ev1_supply)
              .HasDefaultValueSql("'CLOSE'");

        builder.Entity<Hms.Models.TelemetryServer.Simulationstateuium>()
              .Property(p => p.ev2_supply)
              .HasDefaultValueSql("'CLOSE'");

        builder.Entity<Hms.Models.TelemetryServer.Simulationstateuium>()
              .Property(p => p.ev1_waste)
              .HasDefaultValueSql("'CLOSE'");

        builder.Entity<Hms.Models.TelemetryServer.Simulationstateuium>()
              .Property(p => p.ev2_waste)
              .HasDefaultValueSql("'CLOSE'");

        builder.Entity<Hms.Models.TelemetryServer.Simulationstateuium>()
              .Property(p => p.emu1_O2)
              .HasDefaultValueSql("'CLOSE'");

        builder.Entity<Hms.Models.TelemetryServer.Simulationstateuium>()
              .Property(p => p.emu2_O2)
              .HasDefaultValueSql("'CLOSE'");

        builder.Entity<Hms.Models.TelemetryServer.Simulationstateuium>()
              .Property(p => p.O2_vent)
              .HasDefaultValueSql("'CLOSE'");

        builder.Entity<Hms.Models.TelemetryServer.Simulationstateuium>()
              .Property(p => p.depress_pump)
              .HasDefaultValueSql("'FAULT'");

        builder.Entity<Hms.Models.TelemetryServer.Simulationuium>()
              .Property(p => p.emu1)
              .HasDefaultValueSql("0").ValueGeneratedNever();

        builder.Entity<Hms.Models.TelemetryServer.Simulationuium>()
              .Property(p => p.ev1_supply)
              .HasDefaultValueSql("0").ValueGeneratedNever();

        builder.Entity<Hms.Models.TelemetryServer.Simulationuium>()
              .Property(p => p.ev1_waste)
              .HasDefaultValueSql("0").ValueGeneratedNever();

        builder.Entity<Hms.Models.TelemetryServer.Simulationuium>()
              .Property(p => p.emu1_O2)
              .HasDefaultValueSql("0").ValueGeneratedNever();

        builder.Entity<Hms.Models.TelemetryServer.Simulationuium>()
              .Property(p => p.emu2)
              .HasDefaultValueSql("0").ValueGeneratedNever();

        builder.Entity<Hms.Models.TelemetryServer.Simulationuium>()
              .Property(p => p.ev2_supply)
              .HasDefaultValueSql("0").ValueGeneratedNever();

        builder.Entity<Hms.Models.TelemetryServer.Simulationuium>()
              .Property(p => p.ev2_waste)
              .HasDefaultValueSql("0").ValueGeneratedNever();

        builder.Entity<Hms.Models.TelemetryServer.Simulationuium>()
              .Property(p => p.emu2_O2)
              .HasDefaultValueSql("0").ValueGeneratedNever();

        builder.Entity<Hms.Models.TelemetryServer.Simulationuium>()
              .Property(p => p.O2_vent)
              .HasDefaultValueSql("0").ValueGeneratedNever();

        builder.Entity<Hms.Models.TelemetryServer.Simulationuium>()
              .Property(p => p.depress_pump)
              .HasDefaultValueSql("0").ValueGeneratedNever();

        this.OnModelBuilding(builder);
    }


    public DbSet<Hms.Models.TelemetryServer.Location> Locations
    {
      get;
      set;
    }

    public DbSet<Hms.Models.TelemetryServer.Lsar> Lsars
    {
      get;
      set;
    }

    public DbSet<Hms.Models.TelemetryServer.Role> Roles
    {
      get;
      set;
    }

    public DbSet<Hms.Models.TelemetryServer.Room> Rooms
    {
      get;
      set;
    }

    public DbSet<Hms.Models.TelemetryServer.Simulationcontrol> Simulationcontrols
    {
      get;
      set;
    }

    public DbSet<Hms.Models.TelemetryServer.Simulationfailure> Simulationfailures
    {
      get;
      set;
    }

    public DbSet<Hms.Models.TelemetryServer.Simulationstate> Simulationstates
    {
      get;
      set;
    }

    public DbSet<Hms.Models.TelemetryServer.Simulationstateuium> Simulationstateuia
    {
      get;
      set;
    }

    public DbSet<Hms.Models.TelemetryServer.Simulationuium> Simulationuia
    {
      get;
      set;
    }

    public DbSet<Hms.Models.TelemetryServer.User> Users
    {
      get;
      set;
    }
  }
}
