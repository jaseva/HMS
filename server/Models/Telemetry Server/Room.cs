using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hms.Models.TelemetryServer
{
  [Table("rooms")]
  public partial class Room
  {
    [Key]
    public int? id
    {
      get;
      set;
    }
    public string name
    {
      get;
      set;
    }
    public int users
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
