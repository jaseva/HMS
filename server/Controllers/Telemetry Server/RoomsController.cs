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

  [Route("odata/TelemetryServer/Rooms")]
  public partial class RoomsController : ODataController
  {
    private Data.TelemetryServerContext context;

    public RoomsController(Data.TelemetryServerContext context)
    {
      this.context = context;
    }
    // GET /odata/TelemetryServer/Rooms
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.TelemetryServer.Room> GetRooms()
    {
      var items = this.context.Rooms.AsQueryable<Models.TelemetryServer.Room>();
      this.OnRoomsRead(ref items);

      return items;
    }

    partial void OnRoomsRead(ref IQueryable<Models.TelemetryServer.Room> items);

    partial void OnRoomGet(ref SingleResult<Models.TelemetryServer.Room> item);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("/odata/TelemetryServer/Rooms(id={id})")]
    public SingleResult<Room> GetRoom(int? key)
    {
        var items = this.context.Rooms.Where(i=>i.id == key);
        var result = SingleResult.Create(items);

        OnRoomGet(ref result);

        return result;
    }
    partial void OnRoomDeleted(Models.TelemetryServer.Room item);
    partial void OnAfterRoomDeleted(Models.TelemetryServer.Room item);

    [HttpDelete("/odata/TelemetryServer/Rooms(id={id})")]
    public IActionResult DeleteRoom(int? key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var item = this.context.Rooms
                .Where(i => i.id == key)
                .FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }

            this.OnRoomDeleted(item);
            this.context.Rooms.Remove(item);
            this.context.SaveChanges();
            this.OnAfterRoomDeleted(item);

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnRoomUpdated(Models.TelemetryServer.Room item);
    partial void OnAfterRoomUpdated(Models.TelemetryServer.Room item);

    [HttpPut("/odata/TelemetryServer/Rooms(id={id})")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutRoom(int? key, [FromBody]Models.TelemetryServer.Room newItem)
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

            this.OnRoomUpdated(newItem);
            this.context.Rooms.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.Rooms.Where(i => i.id == key);
            this.OnAfterRoomUpdated(newItem);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    [HttpPatch("/odata/TelemetryServer/Rooms(id={id})")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PatchRoom(int? key, [FromBody]Delta<Models.TelemetryServer.Room> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = this.context.Rooms.Where(i => i.id == key).FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }

            patch.Patch(item);

            this.OnRoomUpdated(item);
            this.context.Rooms.Update(item);
            this.context.SaveChanges();

            var itemToReturn = this.context.Rooms.Where(i => i.id == key);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnRoomCreated(Models.TelemetryServer.Room item);
    partial void OnAfterRoomCreated(Models.TelemetryServer.Room item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.TelemetryServer.Room item)
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

            this.OnRoomCreated(item);
            this.context.Rooms.Add(item);
            this.context.SaveChanges();

            return Created($"odata/TelemetryServer/Rooms/{item.id}", item);
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }
  }
}
