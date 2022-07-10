using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hms.Models.TelemetryServer
{
  [Table("simulationcontrols")]
  public partial class Simulationcontrol
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
    public int suit_power
    {
      get;
      set;
    }
    public int o2_switch
    {
      get;
      set;
    }
    public int aux
    {
      get;
      set;
    }
    public int rca
    {
      get;
      set;
    }
    public int pump
    {
      get;
      set;
    }
    public int fan_switch
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
