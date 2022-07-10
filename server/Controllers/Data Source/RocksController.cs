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

  [Route("odata/DataSource/Rocks")]
  public partial class RocksController : ODataController
  {
    private Data.DataSourceContext context;

    public RocksController(Data.DataSourceContext context)
    {
      this.context = context;
    }
    // GET /odata/DataSource/Rocks
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.DataSource.Rock> GetRocks()
    {
      var items = this.context.Rocks.AsNoTracking().AsQueryable<Models.DataSource.Rock>();
      this.OnRocksRead(ref items);

      return items;
    }

    partial void OnRocksRead(ref IQueryable<Models.DataSource.Rock> items);

    partial void OnRockGet(ref SingleResult<Models.DataSource.Rock> item);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("/odata/DataSource/Rocks(ID={ID})")]
    public SingleResult<Rock> GetRock(int? key)
    {
        var items = this.context.Rocks.AsNoTracking().Where(i=>i.ID == key);
        var result = SingleResult.Create(items);

        OnRockGet(ref result);

        return result;
    }
  }
}
