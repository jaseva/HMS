using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hms.Models.TelemetryServer
{
  [Table("simulationstates")]
  public partial class Simulationstate
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
    public int isRunning
    {
      get;
      set;
    }
    public int isPaused
    {
      get;
      set;
    }
    public string timer
    {
      get;
      set;
    }
    public string started_at
    {
      get;
      set;
    }
    public string t_battery
    {
      get;
      set;
    }
    public string t_oxygen
    {
      get;
      set;
    }
    public string t_water
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
