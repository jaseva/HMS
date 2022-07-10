using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hms.Data;

namespace Hms
{
    public partial class ExportTelemetryServerController : ExportController
    {
        private readonly TelemetryServerContext context;
        public ExportTelemetryServerController(TelemetryServerContext context)
        {
            this.context = context;
        }

        [HttpGet("/export/TelemetryServer/locations/csv")]
        [HttpGet("/export/TelemetryServer/locations/csv(fileName='{fileName}')")]
        public FileStreamResult ExportLocationsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Locations, Request.Query), fileName);
        }

        [HttpGet("/export/TelemetryServer/locations/excel")]
        [HttpGet("/export/TelemetryServer/locations/excel(fileName='{fileName}')")]
        public FileStreamResult ExportLocationsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Locations, Request.Query), fileName);
        }
        [HttpGet("/export/TelemetryServer/lsars/csv")]
        [HttpGet("/export/TelemetryServer/lsars/csv(fileName='{fileName}')")]
        public FileStreamResult ExportLsarsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Lsars, Request.Query), fileName);
        }

        [HttpGet("/export/TelemetryServer/lsars/excel")]
        [HttpGet("/export/TelemetryServer/lsars/excel(fileName='{fileName}')")]
        public FileStreamResult ExportLsarsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Lsars, Request.Query), fileName);
        }
        [HttpGet("/export/TelemetryServer/roles/csv")]
        [HttpGet("/export/TelemetryServer/roles/csv(fileName='{fileName}')")]
        public FileStreamResult ExportRolesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Roles, Request.Query), fileName);
        }

        [HttpGet("/export/TelemetryServer/roles/excel")]
        [HttpGet("/export/TelemetryServer/roles/excel(fileName='{fileName}')")]
        public FileStreamResult ExportRolesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Roles, Request.Query), fileName);
        }
        [HttpGet("/export/TelemetryServer/rooms/csv")]
        [HttpGet("/export/TelemetryServer/rooms/csv(fileName='{fileName}')")]
        public FileStreamResult ExportRoomsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Rooms, Request.Query), fileName);
        }

        [HttpGet("/export/TelemetryServer/rooms/excel")]
        [HttpGet("/export/TelemetryServer/rooms/excel(fileName='{fileName}')")]
        public FileStreamResult ExportRoomsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Rooms, Request.Query), fileName);
        }
        [HttpGet("/export/TelemetryServer/simulationcontrols/csv")]
        [HttpGet("/export/TelemetryServer/simulationcontrols/csv(fileName='{fileName}')")]
        public FileStreamResult ExportSimulationcontrolsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Simulationcontrols, Request.Query), fileName);
        }

        [HttpGet("/export/TelemetryServer/simulationcontrols/excel")]
        [HttpGet("/export/TelemetryServer/simulationcontrols/excel(fileName='{fileName}')")]
        public FileStreamResult ExportSimulationcontrolsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Simulationcontrols, Request.Query), fileName);
        }
        [HttpGet("/export/TelemetryServer/simulationfailures/csv")]
        [HttpGet("/export/TelemetryServer/simulationfailures/csv(fileName='{fileName}')")]
        public FileStreamResult ExportSimulationfailuresToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Simulationfailures, Request.Query), fileName);
        }

        [HttpGet("/export/TelemetryServer/simulationfailures/excel")]
        [HttpGet("/export/TelemetryServer/simulationfailures/excel(fileName='{fileName}')")]
        public FileStreamResult ExportSimulationfailuresToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Simulationfailures, Request.Query), fileName);
        }
        [HttpGet("/export/TelemetryServer/simulationstates/csv")]
        [HttpGet("/export/TelemetryServer/simulationstates/csv(fileName='{fileName}')")]
        public FileStreamResult ExportSimulationstatesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Simulationstates, Request.Query), fileName);
        }

        [HttpGet("/export/TelemetryServer/simulationstates/excel")]
        [HttpGet("/export/TelemetryServer/simulationstates/excel(fileName='{fileName}')")]
        public FileStreamResult ExportSimulationstatesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Simulationstates, Request.Query), fileName);
        }
        [HttpGet("/export/TelemetryServer/simulationstateuia/csv")]
        [HttpGet("/export/TelemetryServer/simulationstateuia/csv(fileName='{fileName}')")]
        public FileStreamResult ExportSimulationstateuiaToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Simulationstateuia, Request.Query), fileName);
        }

        [HttpGet("/export/TelemetryServer/simulationstateuia/excel")]
        [HttpGet("/export/TelemetryServer/simulationstateuia/excel(fileName='{fileName}')")]
        public FileStreamResult ExportSimulationstateuiaToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Simulationstateuia, Request.Query), fileName);
        }
        [HttpGet("/export/TelemetryServer/simulationuia/csv")]
        [HttpGet("/export/TelemetryServer/simulationuia/csv(fileName='{fileName}')")]
        public FileStreamResult ExportSimulationuiaToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Simulationuia, Request.Query), fileName);
        }

        [HttpGet("/export/TelemetryServer/simulationuia/excel")]
        [HttpGet("/export/TelemetryServer/simulationuia/excel(fileName='{fileName}')")]
        public FileStreamResult ExportSimulationuiaToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Simulationuia, Request.Query), fileName);
        }
        [HttpGet("/export/TelemetryServer/users/csv")]
        [HttpGet("/export/TelemetryServer/users/csv(fileName='{fileName}')")]
        public FileStreamResult ExportUsersToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Users, Request.Query), fileName);
        }

        [HttpGet("/export/TelemetryServer/users/excel")]
        [HttpGet("/export/TelemetryServer/users/excel(fileName='{fileName}')")]
        public FileStreamResult ExportUsersToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Users, Request.Query), fileName);
        }
    }
}
