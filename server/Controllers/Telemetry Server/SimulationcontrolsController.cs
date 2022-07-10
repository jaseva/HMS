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

  [Route("odata/TelemetryServer/Simulationcontrols")]
  public partial class SimulationcontrolsController : ODataController
  {
    private Data.TelemetryServerContext context;

    public SimulationcontrolsController(Data.TelemetryServerContext context)
    {
      this.context = context;
    }
    // GET /odata/TelemetryServer/Simulationcontrols
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.TelemetryServer.Simulationcontrol> GetSimulationcontrols()
    {
      var items = this.context.Simulationcontrols.AsQueryable<Models.TelemetryServer.Simulationcontrol>();
      this.OnSimulationcontrolsRead(ref items);

      return items;
    }

    partial void OnSimulationcontrolsRead(ref IQueryable<Models.TelemetryServer.Simulationcontrol> items);

    partial void OnSimulationcontrolGet(ref SingleResult<Models.TelemetryServer.Simulationcontrol> item);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("/odata/TelemetryServer/Simulationcontrols(id={id})")]
    public SingleResult<Simulationcontrol> GetSimulationcontrol(int? key)
    {
        var items = this.context.Simulationcontrols.Where(i=>i.id == key);
        var result = SingleResult.Create(items);

        OnSimulationcontrolGet(ref result);

        return result;
    }
    partial void OnSimulationcontrolDeleted(Models.TelemetryServer.Simulationcontrol item);
    partial void OnAfterSimulationcontrolDeleted(Models.TelemetryServer.Simulationcontrol item);

    [HttpDelete("/odata/TelemetryServer/Simulationcontrols(id={id})")]
    public IActionResult DeleteSimulationcontrol(int? key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var item = this.context.Simulationcontrols
                .Where(i => i.id == key)
                .FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }

            this.OnSimulationcontrolDeleted(item);
            this.context.Simulationcontrols.Remove(item);
            this.context.SaveChanges();
            this.OnAfterSimulationcontrolDeleted(item);

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnSimulationcontrolUpdated(Models.TelemetryServer.Simulationcontrol item);
    partial void OnAfterSimulationcontrolUpdated(Models.TelemetryServer.Simulationcontrol item);

    [HttpPut("/odata/TelemetryServer/Simulationcontrols(id={id})")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutSimulationcontrol(int? key, [FromBody]Models.TelemetryServer.Simulationcontrol newItem)
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

            this.OnSimulationcontrolUpdated(newItem);
            this.context.Simulationcontrols.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.Simulationcontrols.Where(i => i.id == key);
            this.OnAfterSimulationcontrolUpdated(newItem);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    [HttpPatch("/odata/TelemetryServer/Simulationcontrols(id={id})")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PatchSimulationcontrol(int? key, [FromBody]Delta<Models.TelemetryServer.Simulationcontrol> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = this.context.Simulationcontrols.Where(i => i.id == key).FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }

            patch.Patch(item);

            this.OnSimulationcontrolUpdated(item);
            this.context.Simulationcontrols.Update(item);
            this.context.SaveChanges();

            var itemToReturn = this.context.Simulationcontrols.Where(i => i.id == key);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnSimulationcontrolCreated(Models.TelemetryServer.Simulationcontrol item);
    partial void OnAfterSimulationcontrolCreated(Models.TelemetryServer.Simulationcontrol item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.TelemetryServer.Simulationcontrol item)
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

            this.OnSimulationcontrolCreated(item);
            this.context.Simulationcontrols.Add(item);
            this.context.SaveChanges();

            return Created($"odata/TelemetryServer/Simulationcontrols/{item.id}", item);
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }
  }
}
