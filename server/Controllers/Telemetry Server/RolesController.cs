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

  [Route("odata/TelemetryServer/Roles")]
  public partial class RolesController : ODataController
  {
    private Data.TelemetryServerContext context;

    public RolesController(Data.TelemetryServerContext context)
    {
      this.context = context;
    }
    // GET /odata/TelemetryServer/Roles
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.TelemetryServer.Role> GetRoles()
    {
      var items = this.context.Roles.AsQueryable<Models.TelemetryServer.Role>();
      this.OnRolesRead(ref items);

      return items;
    }

    partial void OnRolesRead(ref IQueryable<Models.TelemetryServer.Role> items);

    partial void OnRoleGet(ref SingleResult<Models.TelemetryServer.Role> item);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("/odata/TelemetryServer/Roles(id={id})")]
    public SingleResult<Role> GetRole(int? key)
    {
        var items = this.context.Roles.Where(i=>i.id == key);
        var result = SingleResult.Create(items);

        OnRoleGet(ref result);

        return result;
    }
    partial void OnRoleDeleted(Models.TelemetryServer.Role item);
    partial void OnAfterRoleDeleted(Models.TelemetryServer.Role item);

    [HttpDelete("/odata/TelemetryServer/Roles(id={id})")]
    public IActionResult DeleteRole(int? key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var item = this.context.Roles
                .Where(i => i.id == key)
                .FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }

            this.OnRoleDeleted(item);
            this.context.Roles.Remove(item);
            this.context.SaveChanges();
            this.OnAfterRoleDeleted(item);

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnRoleUpdated(Models.TelemetryServer.Role item);
    partial void OnAfterRoleUpdated(Models.TelemetryServer.Role item);

    [HttpPut("/odata/TelemetryServer/Roles(id={id})")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutRole(int? key, [FromBody]Models.TelemetryServer.Role newItem)
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

            this.OnRoleUpdated(newItem);
            this.context.Roles.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.Roles.Where(i => i.id == key);
            this.OnAfterRoleUpdated(newItem);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    [HttpPatch("/odata/TelemetryServer/Roles(id={id})")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PatchRole(int? key, [FromBody]Delta<Models.TelemetryServer.Role> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = this.context.Roles.Where(i => i.id == key).FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }

            patch.Patch(item);

            this.OnRoleUpdated(item);
            this.context.Roles.Update(item);
            this.context.SaveChanges();

            var itemToReturn = this.context.Roles.Where(i => i.id == key);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnRoleCreated(Models.TelemetryServer.Role item);
    partial void OnAfterRoleCreated(Models.TelemetryServer.Role item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.TelemetryServer.Role item)
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

            this.OnRoleCreated(item);
            this.context.Roles.Add(item);
            this.context.SaveChanges();

            return Created($"odata/TelemetryServer/Roles/{item.id}", item);
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }
  }
}
