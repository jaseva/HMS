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

  [Route("odata/TelemetryServer/Simulationstates")]
  public partial class SimulationstatesController : ODataController
  {
    private Data.TelemetryServerContext context;

    public SimulationstatesController(Data.TelemetryServerContext context)
    {
      this.context = context;
    }
    // GET /odata/TelemetryServer/Simulationstates
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.TelemetryServer.Simulationstate> GetSimulationstates()
    {
      var items = this.context.Simulationstates.AsQueryable<Models.TelemetryServer.Simulationstate>();
      this.OnSimulationstatesRead(ref items);

      return items;
    }

    partial void OnSimulationstatesRead(ref IQueryable<Models.TelemetryServer.Simulationstate> items);

    partial void OnSimulationstateGet(ref SingleResult<Models.TelemetryServer.Simulationstate> item);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("/odata/TelemetryServer/Simulationstates(id={id})")]
    public SingleResult<Simulationstate> GetSimulationstate(int? key)
    {
        var items = this.context.Simulationstates.Where(i=>i.id == key);
        var result = SingleResult.Create(items);

        OnSimulationstateGet(ref result);

        return result;
    }
    partial void OnSimulationstateDeleted(Models.TelemetryServer.Simulationstate item);
    partial void OnAfterSimulationstateDeleted(Models.TelemetryServer.Simulationstate item);

    [HttpDelete("/odata/TelemetryServer/Simulationstates(id={id})")]
    public IActionResult DeleteSimulationstate(int? key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var item = this.context.Simulationstates
                .Where(i => i.id == key)
                .FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }

            this.OnSimulationstateDeleted(item);
            this.context.Simulationstates.Remove(item);
            this.context.SaveChanges();
            this.OnAfterSimulationstateDeleted(item);

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnSimulationstateUpdated(Models.TelemetryServer.Simulationstate item);
    partial void OnAfterSimulationstateUpdated(Models.TelemetryServer.Simulationstate item);

    [HttpPut("/odata/TelemetryServer/Simulationstates(id={id})")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutSimulationstate(int? key, [FromBody]Models.TelemetryServer.Simulationstate newItem)
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

            this.OnSimulationstateUpdated(newItem);
            this.context.Simulationstates.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.Simulationstates.Where(i => i.id == key);
            this.OnAfterSimulationstateUpdated(newItem);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    [HttpPatch("/odata/TelemetryServer/Simulationstates(id={id})")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PatchSimulationstate(int? key, [FromBody]Delta<Models.TelemetryServer.Simulationstate> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = this.context.Simulationstates.Where(i => i.id == key).FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }

            patch.Patch(item);

            this.OnSimulationstateUpdated(item);
            this.context.Simulationstates.Update(item);
            this.context.SaveChanges();

            var itemToReturn = this.context.Simulationstates.Where(i => i.id == key);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnSimulationstateCreated(Models.TelemetryServer.Simulationstate item);
    partial void OnAfterSimulationstateCreated(Models.TelemetryServer.Simulationstate item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.TelemetryServer.Simulationstate item)
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

            this.OnSimulationstateCreated(item);
            this.context.Simulationstates.Add(item);
            this.context.SaveChanges();

            return Created($"odata/TelemetryServer/Simulationstates/{item.id}", item);
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }
  }
}
