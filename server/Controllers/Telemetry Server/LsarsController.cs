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




namespace Hms.Controllers.TelemetryServer
{
  using Models;
  using Data;
  using Models.TelemetryServer;

  [Route("odata/TelemetryServer/Lsars")]
  public partial class LsarsController : ODataController
  {
    private Data.TelemetryServerContext context;

    public LsarsController(Data.TelemetryServerContext context)
    {
      this.context = context;
    }
    // GET /odata/TelemetryServer/Lsars
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.TelemetryServer.Lsar> GetLsars()
    {
      var items = this.context.Lsars.AsQueryable<Models.TelemetryServer.Lsar>();
      this.OnLsarsRead(ref items);

      return items;
    }

    partial void OnLsarsRead(ref IQueryable<Models.TelemetryServer.Lsar> items);

    partial void OnLsarGet(ref SingleResult<Models.TelemetryServer.Lsar> item);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("/odata/TelemetryServer/Lsars(id={id})")]
    public SingleResult<Lsar> GetLsar(int? key)
    {
        var items = this.context.Lsars.Where(i=>i.id == key);
        var result = SingleResult.Create(items);

        OnLsarGet(ref result);

        return result;
    }
    partial void OnLsarDeleted(Models.TelemetryServer.Lsar item);
    partial void OnAfterLsarDeleted(Models.TelemetryServer.Lsar item);

    [HttpDelete("/odata/TelemetryServer/Lsars(id={id})")]
    public IActionResult DeleteLsar(int? key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var item = this.context.Lsars
                .Where(i => i.id == key)
                .FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }

            this.OnLsarDeleted(item);
            this.context.Lsars.Remove(item);
            this.context.SaveChanges();
            this.OnAfterLsarDeleted(item);

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnLsarUpdated(Models.TelemetryServer.Lsar item);
    partial void OnAfterLsarUpdated(Models.TelemetryServer.Lsar item);

    [HttpPut("/odata/TelemetryServer/Lsars(id={id})")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutLsar(int? key, [FromBody]Models.TelemetryServer.Lsar newItem)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (newItem == null || (newItem.id != key))
            {
                return BadRequest();
            }

            this.OnLsarUpdated(newItem);
            this.context.Lsars.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.Lsars.Where(i => i.id == key);
            this.OnAfterLsarUpdated(newItem);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    [HttpPatch("/odata/TelemetryServer/Lsars(id={id})")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PatchLsar(int? key, [FromBody]Delta<Models.TelemetryServer.Lsar> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = this.context.Lsars.Where(i => i.id == key).FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }

            patch.Patch(item);

            this.OnLsarUpdated(item);
            this.context.Lsars.Update(item);
            this.context.SaveChanges();

            var itemToReturn = this.context.Lsars.Where(i => i.id == key);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnLsarCreated(Models.TelemetryServer.Lsar item);
    partial void OnAfterLsarCreated(Models.TelemetryServer.Lsar item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.TelemetryServer.Lsar item)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (item == null)
            {
                return BadRequest();
            }

            this.OnLsarCreated(item);
            this.context.Lsars.Add(item);
            this.context.SaveChanges();

            return Created($"odata/TelemetryServer/Lsars/{item.id}", item);
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }
  }
}
