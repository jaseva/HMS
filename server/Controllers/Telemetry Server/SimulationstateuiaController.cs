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

  [Route("odata/TelemetryServer/Simulationstateuia")]
  public partial class SimulationstateuiaController : ODataController
  {
    private Data.TelemetryServerContext context;

    public SimulationstateuiaController(Data.TelemetryServerContext context)
    {
      this.context = context;
    }
    // GET /odata/TelemetryServer/Simulationstateuia
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.TelemetryServer.Simulationstateuium> GetSimulationstateuia()
    {
      var items = this.context.Simulationstateuia.AsQueryable<Models.TelemetryServer.Simulationstateuium>();
      this.OnSimulationstateuiaRead(ref items);

      return items;
    }

    partial void OnSimulationstateuiaRead(ref IQueryable<Models.TelemetryServer.Simulationstateuium> items);

    partial void OnSimulationstateuiumGet(ref SingleResult<Models.TelemetryServer.Simulationstateuium> item);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("/odata/TelemetryServer/Simulationstateuia(id={id})")]
    public SingleResult<Simulationstateuium> GetSimulationstateuium(int? key)
    {
        var items = this.context.Simulationstateuia.Where(i=>i.id == key);
        var result = SingleResult.Create(items);

        OnSimulationstateuiumGet(ref result);

        return result;
    }
    partial void OnSimulationstateuiumDeleted(Models.TelemetryServer.Simulationstateuium item);
    partial void OnAfterSimulationstateuiumDeleted(Models.TelemetryServer.Simulationstateuium item);

    [HttpDelete("/odata/TelemetryServer/Simulationstateuia(id={id})")]
    public IActionResult DeleteSimulationstateuium(int? key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var item = this.context.Simulationstateuia
                .Where(i => i.id == key)
                .FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }

            this.OnSimulationstateuiumDeleted(item);
            this.context.Simulationstateuia.Remove(item);
            this.context.SaveChanges();
            this.OnAfterSimulationstateuiumDeleted(item);

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnSimulationstateuiumUpdated(Models.TelemetryServer.Simulationstateuium item);
    partial void OnAfterSimulationstateuiumUpdated(Models.TelemetryServer.Simulationstateuium item);

    [HttpPut("/odata/TelemetryServer/Simulationstateuia(id={id})")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutSimulationstateuium(int? key, [FromBody]Models.TelemetryServer.Simulationstateuium newItem)
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

            this.OnSimulationstateuiumUpdated(newItem);
            this.context.Simulationstateuia.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.Simulationstateuia.Where(i => i.id == key);
            this.OnAfterSimulationstateuiumUpdated(newItem);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    [HttpPatch("/odata/TelemetryServer/Simulationstateuia(id={id})")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PatchSimulationstateuium(int? key, [FromBody]Delta<Models.TelemetryServer.Simulationstateuium> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = this.context.Simulationstateuia.Where(i => i.id == key).FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }

            patch.Patch(item);

            this.OnSimulationstateuiumUpdated(item);
            this.context.Simulationstateuia.Update(item);
            this.context.SaveChanges();

            var itemToReturn = this.context.Simulationstateuia.Where(i => i.id == key);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnSimulationstateuiumCreated(Models.TelemetryServer.Simulationstateuium item);
    partial void OnAfterSimulationstateuiumCreated(Models.TelemetryServer.Simulationstateuium item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.TelemetryServer.Simulationstateuium item)
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

            this.OnSimulationstateuiumCreated(item);
            this.context.Simulationstateuia.Add(item);
            this.context.SaveChanges();

            return Created($"odata/TelemetryServer/Simulationstateuia/{item.id}", item);
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }
  }
}
