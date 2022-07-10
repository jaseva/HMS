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

  [Route("odata/TelemetryServer/Users")]
  public partial class UsersController : ODataController
  {
    private Data.TelemetryServerContext context;

    public UsersController(Data.TelemetryServerContext context)
    {
      this.context = context;
    }
    // GET /odata/TelemetryServer/Users
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.TelemetryServer.User> GetUsers()
    {
      var items = this.context.Users.AsQueryable<Models.TelemetryServer.User>();
      this.OnUsersRead(ref items);

      return items;
    }

    partial void OnUsersRead(ref IQueryable<Models.TelemetryServer.User> items);

    partial void OnUserGet(ref SingleResult<Models.TelemetryServer.User> item);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("/odata/TelemetryServer/Users(id={id})")]
    public SingleResult<User> GetUser(int? key)
    {
        var items = this.context.Users.Where(i=>i.id == key);
        var result = SingleResult.Create(items);

        OnUserGet(ref result);

        return result;
    }
    partial void OnUserDeleted(Models.TelemetryServer.User item);
    partial void OnAfterUserDeleted(Models.TelemetryServer.User item);

    [HttpDelete("/odata/TelemetryServer/Users(id={id})")]
    public IActionResult DeleteUser(int? key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var item = this.context.Users
                .Where(i => i.id == key)
                .FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }

            this.OnUserDeleted(item);
            this.context.Users.Remove(item);
            this.context.SaveChanges();
            this.OnAfterUserDeleted(item);

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnUserUpdated(Models.TelemetryServer.User item);
    partial void OnAfterUserUpdated(Models.TelemetryServer.User item);

    [HttpPut("/odata/TelemetryServer/Users(id={id})")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutUser(int? key, [FromBody]Models.TelemetryServer.User newItem)
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

            this.OnUserUpdated(newItem);
            this.context.Users.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.Users.Where(i => i.id == key);
            this.OnAfterUserUpdated(newItem);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    [HttpPatch("/odata/TelemetryServer/Users(id={id})")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PatchUser(int? key, [FromBody]Delta<Models.TelemetryServer.User> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = this.context.Users.Where(i => i.id == key).FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }

            patch.Patch(item);

            this.OnUserUpdated(item);
            this.context.Users.Update(item);
            this.context.SaveChanges();

            var itemToReturn = this.context.Users.Where(i => i.id == key);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnUserCreated(Models.TelemetryServer.User item);
    partial void OnAfterUserCreated(Models.TelemetryServer.User item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.TelemetryServer.User item)
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

            this.OnUserCreated(item);
            this.context.Users.Add(item);
            this.context.SaveChanges();

            return Created($"odata/TelemetryServer/Users/{item.id}", item);
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }
  }
}
