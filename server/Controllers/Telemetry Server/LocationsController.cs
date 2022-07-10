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

  [Route("odata/TelemetryServer/Locations")]
  public partial class LocationsController : ODataController
  {
    private Data.TelemetryServerContext context;

    public LocationsController(Data.TelemetryServerContext context)
    {
      this.context = context;
    }
    // GET /odata/TelemetryServer/Locations
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.TelemetryServer.Location> GetLocations()
    {
      var items = this.context.Locations.AsQueryable<Models.TelemetryServer.Location>();
      this.OnLocationsRead(ref items);

      return items;
    }

    partial void OnLocationsRead(ref IQueryable<Models.TelemetryServer.Location> items);

    partial void OnLocationGet(ref SingleResult<Models.TelemetryServer.Location> item);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("/odata/TelemetryServer/Locations(id={id})")]
    public SingleResult<Location> GetLocation(int? key)
    {
        var items = this.context.Locations.Where(i=>i.id == key);
        var result = SingleResult.Create(items);

        OnLocationGet(ref result);

        return result;
    }
    partial void OnLocationDeleted(Models.TelemetryServer.Location item);
    partial void OnAfterLocationDeleted(Models.TelemetryServer.Location item);

    [HttpDelete("/odata/TelemetryServer/Locations(id={id})")]
    public IActionResult DeleteLocation(int? key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var item = this.context.Locations
                .Where(i => i.id == key)
                .FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }

            this.OnLocationDeleted(item);
            this.context.Locations.Remove(item);
            this.context.SaveChanges();
            this.OnAfterLocationDeleted(item);

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnLocationUpdated(Models.TelemetryServer.Location item);
    partial void OnAfterLocationUpdated(Models.TelemetryServer.Location item);

    [HttpPut("/odata/TelemetryServer/Locations(id={id})")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutLocation(int? key, [FromBody]Models.TelemetryServer.Location newItem)
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

            this.OnLocationUpdated(newItem);
            this.context.Locations.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.Locations.Where(i => i.id == key);
            this.OnAfterLocationUpdated(newItem);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    [HttpPatch("/odata/TelemetryServer/Locations(id={id})")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PatchLocation(int? key, [FromBody]Delta<Models.TelemetryServer.Location> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = this.context.Locations.Where(i => i.id == key).FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }

            patch.Patch(item);

            this.OnLocationUpdated(item);
            this.context.Locations.Update(item);
            this.context.SaveChanges();

            var itemToReturn = this.context.Locations.Where(i => i.id == key);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnLocationCreated(Models.TelemetryServer.Location item);
    partial void OnAfterLocationCreated(Models.TelemetryServer.Location item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.TelemetryServer.Location item)
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

            this.OnLocationCreated(item);
            this.context.Locations.Add(item);
            this.context.SaveChanges();

            return Created($"odata/TelemetryServer/Locations/{item.id}", item);
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }
  }
}
