using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hms.Models.TelemetryServer
{
  [Table("simulationfailures")]
  public partial class Simulationfailure
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
    public int o2_error
    {
      get;
      set;
    }
    public int pump_error
    {
      get;
      set;
    }
    public int power_error
    {
      get;
      set;
    }
    public int fan_error
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
