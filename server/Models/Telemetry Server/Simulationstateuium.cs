using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hms.Models.TelemetryServer
{
  [Table("simulationstateuia")]
  public partial class Simulationstateuium
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
    public string emu1
    {
      get;
      set;
    }
    public string emu2
    {
      get;
      set;
    }
    public string ev1_supply
    {
      get;
      set;
    }
    public string ev2_supply
    {
      get;
      set;
    }
    public string ev1_waste
    {
      get;
      set;
    }
    public string ev2_waste
    {
      get;
      set;
    }
    public string emu1_O2
    {
      get;
      set;
    }
    public string emu2_O2
    {
      get;
      set;
    }
    public string O2_vent
    {
      get;
      set;
    }
    public string depress_pump
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
