using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;

using Hms.Models.DataSource;

namespace Hms.Data
{
  public partial class DataSourceContext : Microsoft.EntityFrameworkCore.DbContext
  {
    public DataSourceContext(DbContextOptions<DataSourceContext> options):base(options)
    {
    }

    public DataSourceContext()
    {
    }

    partial void OnModelBuilding(ModelBuilder builder);

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Hms.Models.DataSource.Anomaly>().HasNoKey();
        builder.Entity<Hms.Models.DataSource.Rock>().HasNoKey();


        builder.Entity<Hms.Models.DataSource.Anomaly>()
              .Property(p => p.ID)
              .HasPrecision(10, 0);

        builder.Entity<Hms.Models.DataSource.Rock>()
              .Property(p => p.ID)
              .HasPrecision(10, 0);
        this.OnModelBuilding(builder);
    }


    public DbSet<Hms.Models.DataSource.Anomaly> Anomalies
    {
      get;
      set;
    }

    public DbSet<Hms.Models.DataSource.Rock> Rocks
    {
      get;
      set;
    }
  }
}
