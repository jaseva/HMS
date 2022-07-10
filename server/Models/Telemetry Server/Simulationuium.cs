using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hms.Models.TelemetryServer
{
  [Table("simulationuia")]
  public partial class Simulationuium
  {
    [Key]
    public int? id
    {
      get;
      set;
    }
    public int room
    {
      get;
      set;
    }
    public string started_at
    {
      get;
      set;
    }
    public int emu1
    {
      get;
      set;
    }
    public int ev1_supply
    {
      get;
      set;
    }
    public int ev1_waste
    {
      get;
      set;
    }
    public int emu1_O2
    {
      get;
      set;
    }
    public int emu2
    {
      get;
      set;
    }
    public int ev2_supply
    {
      get;
      set;
    }
    public int ev2_waste
    {
      get;
      set;
    }
    public int emu2_O2
    {
      get;
      set;
    }
    public int O2_vent
    {
      get;
      set;
    }
    public int depress_pump
    {
      get;
      set;
    }
    public DateTime createdAt
    {
      get;
      set;
    }
    public DateTime updatedAt
    {
      get;
      set;
    }
  }
}
