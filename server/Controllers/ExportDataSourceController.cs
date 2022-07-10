using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hms.Data;

namespace Hms
{
    public partial class ExportDataSourceController : ExportController
    {
        private readonly DataSourceContext context;
        public ExportDataSourceController(DataSourceContext context)
        {
            this.context = context;
        }

        [HttpGet("/export/DataSource/anomalies/csv")]
        [HttpGet("/export/DataSource/anomalies/csv(fileName='{fileName}')")]
        public FileStreamResult ExportAnomaliesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Anomalies, Request.Query), fileName);
        }

        [HttpGet("/export/DataSource/anomalies/excel")]
        [HttpGet("/export/DataSource/anomalies/excel(fileName='{fileName}')")]
        public FileStreamResult ExportAnomaliesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Anomalies, Request.Query), fileName);
        }
        [HttpGet("/export/DataSource/rocks/csv")]
        [HttpGet("/export/DataSource/rocks/csv(fileName='{fileName}')")]
        public FileStreamResult ExportRocksToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Rocks, Request.Query), fileName);
        }

        [HttpGet("/export/DataSource/rocks/excel")]
        [HttpGet("/export/DataSource/rocks/excel(fileName='{fileName}')")]
        public FileStreamResult ExportRocksToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Rocks, Request.Query), fileName);
        }
    }
}
