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

  [Route("odata/TelemetryServer/Simulationuia")]
  public partial class SimulationuiaController : ODataController
  {
    private Data.TelemetryServerContext context;

    public SimulationuiaController(Data.TelemetryServerContext context)
    {
      this.context = context;
    }
    // GET /odata/TelemetryServer/Simulationuia
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.TelemetryServer.Simulationuium> GetSimulationuia()
    {
      var items = this.context.Simulationuia.AsQueryable<Models.TelemetryServer.Simulationuium>();
      this.OnSimulationuiaRead(ref items);

      return items;
    }

    partial void OnSimulationuiaRead(ref IQueryable<Models.TelemetryServer.Simulationuium> items);

    partial void OnSimulationuiumGet(ref SingleResult<Models.TelemetryServer.Simulationuium> item);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("/odata/TelemetryServer/Simulationuia(id={id})")]
    public SingleResult<Simulationuium> GetSimulationuium(int? key)
    {
        var items = this.context.Simulationuia.Where(i=>i.id == key);
        var result = SingleResult.Create(items);

        OnSimulationuiumGet(ref result);

        return result;
    }
    partial void OnSimulationuiumDeleted(Models.TelemetryServer.Simulationuium item);
    partial void OnAfterSimulationuiumDeleted(Models.TelemetryServer.Simulationuium item);

    [HttpDelete("/odata/TelemetryServer/Simulationuia(id={id})")]
    public IActionResult DeleteSimulationuium(int? key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var item = this.context.Simulationuia
                .Where(i => i.id == key)
                .FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }

            this.OnSimulationuiumDeleted(item);
            this.context.Simulationuia.Remove(item);
            this.context.SaveChanges();
            this.OnAfterSimulationuiumDeleted(item);

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnSimulationuiumUpdated(Models.TelemetryServer.Simulationuium item);
    partial void OnAfterSimulationuiumUpdated(Models.TelemetryServer.Simulationuium item);

    [HttpPut("/odata/TelemetryServer/Simulationuia(id={id})")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutSimulationuium(int? key, [FromBody]Models.TelemetryServer.Simulationuium newItem)
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

            this.OnSimulationuiumUpdated(newItem);
            this.context.Simulationuia.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.Simulationuia.Where(i => i.id == key);
            this.OnAfterSimulationuiumUpdated(newItem);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    [HttpPatch("/odata/TelemetryServer/Simulationuia(id={id})")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PatchSimulationuium(int? key, [FromBody]Delta<Models.TelemetryServer.Simulationuium> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = this.context.Simulationuia.Where(i => i.id == key).FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }

            patch.Patch(item);

            this.OnSimulationuiumUpdated(item);
            this.context.Simulationuia.Update(item);
            this.context.SaveChanges();

            var itemToReturn = this.context.Simulationuia.Where(i => i.id == key);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnSimulationuiumCreated(Models.TelemetryServer.Simulationuium item);
    partial void OnAfterSimulationuiumCreated(Models.TelemetryServer.Simulationuium item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.TelemetryServer.Simulationuium item)
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

            this.OnSimulationuiumCreated(item);
            this.context.Simulationuia.Add(item);
            this.context.SaveChanges();

            return Created($"odata/TelemetryServer/Simulationuia/{item.id}", item);
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }
  }
}
