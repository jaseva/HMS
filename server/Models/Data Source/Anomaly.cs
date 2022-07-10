using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hms.Models.DataSource
{
  [Table("Anomalies", Schema = "dbo")]
  public partial class Anomaly
  {
    [Key]
    public int? ID
    {
      get;
      set;
    }
    public string Title
    {
      get;
      set;
    }
    public string Description
    {
      get;
      set;
    }
  }
}
