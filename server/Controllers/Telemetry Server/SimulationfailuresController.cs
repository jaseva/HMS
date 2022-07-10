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

  [Route("odata/TelemetryServer/Simulationfailures")]
  public partial class SimulationfailuresController : ODataController
  {
    private Data.TelemetryServerContext context;

    public SimulationfailuresController(Data.TelemetryServerContext context)
    {
      this.context = context;
    }
    // GET /odata/TelemetryServer/Simulationfailures
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.TelemetryServer.Simulationfailure> GetSimulationfailures()
    {
      var items = this.context.Simulationfailures.AsQueryable<Models.TelemetryServer.Simulationfailure>();
      this.OnSimulationfailuresRead(ref items);

      return items;
    }

    partial void OnSimulationfailuresRead(ref IQueryable<Models.TelemetryServer.Simulationfailure> items);

    partial void OnSimulationfailureGet(ref SingleResult<Models.TelemetryServer.Simulationfailure> item);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("/odata/TelemetryServer/Simulationfailures(id={id})")]
    public SingleResult<Simulationfailure> GetSimulationfailure(int? key)
    {
        var items = this.context.Simulationfailures.Where(i=>i.id == key);
        var result = SingleResult.Create(items);

        OnSimulationfailureGet(ref result);

        return result;
    }
    partial void OnSimulationfailureDeleted(Models.TelemetryServer.Simulationfailure item);
    partial void OnAfterSimulationfailureDeleted(Models.TelemetryServer.Simulationfailure item);

    [HttpDelete("/odata/TelemetryServer/Simulationfailures(id={id})")]
    public IActionResult DeleteSimulationfailure(int? key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var item = this.context.Simulationfailures
                .Where(i => i.id == key)
                .FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }

            this.OnSimulationfailureDeleted(item);
            this.context.Simulationfailures.Remove(item);
            this.context.SaveChanges();
            this.OnAfterSimulationfailureDeleted(item);

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnSimulationfailureUpdated(Models.TelemetryServer.Simulationfailure item);
    partial void OnAfterSimulationfailureUpdated(Models.TelemetryServer.Simulationfailure item);

    [HttpPut("/odata/TelemetryServer/Simulationfailures(id={id})")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutSimulationfailure(int? key, [FromBody]Models.TelemetryServer.Simulationfailure newItem)
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

            this.OnSimulationfailureUpdated(newItem);
            this.context.Simulationfailures.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.Simulationfailures.Where(i => i.id == key);
            this.OnAfterSimulationfailureUpdated(newItem);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    [HttpPatch("/odata/TelemetryServer/Simulationfailures(id={id})")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PatchSimulationfailure(int? key, [FromBody]Delta<Models.TelemetryServer.Simulationfailure> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = this.context.Simulationfailures.Where(i => i.id == key).FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }

            patch.Patch(item);

            this.OnSimulationfailureUpdated(item);
            this.context.Simulationfailures.Update(item);
            this.context.SaveChanges();

            var itemToReturn = this.context.Simulationfailures.Where(i => i.id == key);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnSimulationfailureCreated(Models.TelemetryServer.Simulationfailure item);
    partial void OnAfterSimulationfailureCreated(Models.TelemetryServer.Simulationfailure item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.TelemetryServer.Simulationfailure item)
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

            this.OnSimulationfailureCreated(item);
            this.context.Simulationfailures.Add(item);
            this.context.SaveChanges();

            return Created($"odata/TelemetryServer/Simulationfailures/{item.id}", item);
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }
  }
}
