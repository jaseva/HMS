
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Web;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Components;
using Hms.Models.DataSource;

namespace Hms
{
    public partial class DataSourceService
    {
        private readonly HttpClient httpClient;
        private readonly Uri baseUri;
        private readonly NavigationManager navigationManager;
        public DataSourceService(NavigationManager navigationManager, HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;

            this.navigationManager = navigationManager;
            this.baseUri = new Uri($"{navigationManager.BaseUri}odata/DataSource/");
        }

        public async System.Threading.Tasks.Task ExportAnomaliesToExcel(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/datasource/anomalies/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/datasource/anomalies/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportAnomaliesToCSV(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/datasource/anomalies/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/datasource/anomalies/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }
        partial void OnGetAnomalies(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Radzen.ODataServiceResult<Hms.Models.DataSource.Anomaly>> GetAnomalies(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Anomalies");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAnomalies(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<Hms.Models.DataSource.Anomaly>>(response);
        }

        public async System.Threading.Tasks.Task ExportRocksToExcel(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/datasource/rocks/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/datasource/rocks/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportRocksToCSV(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/datasource/rocks/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/datasource/rocks/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }
        partial void OnGetRocks(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Radzen.ODataServiceResult<Hms.Models.DataSource.Rock>> GetRocks(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Rocks");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetRocks(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<Hms.Models.DataSource.Rock>>(response);
        }
    }
}
