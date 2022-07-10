using System;
using System.Net;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;




namespace Hms.Controllers.DataSource
{
  using Models;
  using Data;
  using Models.DataSource;

  [Route("odata/DataSource/Anomalies")]
  public partial class AnomaliesController : ODataController
  {
    private Data.DataSourceContext context;

    public AnomaliesController(Data.DataSourceContext context)
    {
      this.context = context;
    }
    // GET /odata/DataSource/Anomalies
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.DataSource.Anomaly> GetAnomalies()
    {
      var items = this.context.Anomalies.AsNoTracking().AsQueryable<Models.DataSource.Anomaly>();
      this.OnAnomaliesRead(ref items);

      return items;
    }

    partial void OnAnomaliesRead(ref IQueryable<Models.DataSource.Anomaly> items);

    partial void OnAnomalyGet(ref SingleResult<Models.DataSource.Anomaly> item);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("/odata/DataSource/Anomalies(ID={ID})")]
    public SingleResult<Anomaly> GetAnomaly(int? key)
    {
        var items = this.context.Anomalies.AsNoTracking().Where(i=>i.ID == key);
        var result = SingleResult.Create(items);

        OnAnomalyGet(ref result);

        return result;
    }
  }
}
