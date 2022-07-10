
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
using Hms.Models.TelemetryServer;

namespace Hms
{
    public partial class TelemetryServerService
    {
        private readonly HttpClient httpClient;
        private readonly Uri baseUri;
        private readonly NavigationManager navigationManager;
        public TelemetryServerService(NavigationManager navigationManager, HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;

            this.navigationManager = navigationManager;
            this.baseUri = new Uri($"{navigationManager.BaseUri}odata/TelemetryServer/");
        }

        public async System.Threading.Tasks.Task ExportLocationsToExcel(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/telemetryserver/locations/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/telemetryserver/locations/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportLocationsToCSV(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/telemetryserver/locations/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/telemetryserver/locations/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }
        partial void OnGetLocations(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Radzen.ODataServiceResult<Hms.Models.TelemetryServer.Location>> GetLocations(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Locations");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetLocations(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<Hms.Models.TelemetryServer.Location>>(response);
        }
        partial void OnCreateLocation(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Hms.Models.TelemetryServer.Location> CreateLocation(Hms.Models.TelemetryServer.Location location = default(Hms.Models.TelemetryServer.Location))
        {
            var uri = new Uri(baseUri, $"Locations");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(location), Encoding.UTF8, "application/json");

            OnCreateLocation(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Hms.Models.TelemetryServer.Location>(response);
        }

        public async System.Threading.Tasks.Task ExportLsarsToExcel(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/telemetryserver/lsars/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/telemetryserver/lsars/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportLsarsToCSV(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/telemetryserver/lsars/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/telemetryserver/lsars/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }
        partial void OnGetLsars(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Radzen.ODataServiceResult<Hms.Models.TelemetryServer.Lsar>> GetLsars(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Lsars");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetLsars(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<Hms.Models.TelemetryServer.Lsar>>(response);
        }
        partial void OnCreateLsar(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Hms.Models.TelemetryServer.Lsar> CreateLsar(Hms.Models.TelemetryServer.Lsar lsar = default(Hms.Models.TelemetryServer.Lsar))
        {
            var uri = new Uri(baseUri, $"Lsars");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(lsar), Encoding.UTF8, "application/json");

            OnCreateLsar(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Hms.Models.TelemetryServer.Lsar>(response);
        }

        public async System.Threading.Tasks.Task ExportRolesToExcel(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/telemetryserver/roles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/telemetryserver/roles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportRolesToCSV(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/telemetryserver/roles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/telemetryserver/roles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }
        partial void OnGetRoles(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Radzen.ODataServiceResult<Hms.Models.TelemetryServer.Role>> GetRoles(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Roles");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetRoles(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<Hms.Models.TelemetryServer.Role>>(response);
        }
        partial void OnCreateRole(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Hms.Models.TelemetryServer.Role> CreateRole(Hms.Models.TelemetryServer.Role role = default(Hms.Models.TelemetryServer.Role))
        {
            var uri = new Uri(baseUri, $"Roles");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(role), Encoding.UTF8, "application/json");

            OnCreateRole(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Hms.Models.TelemetryServer.Role>(response);
        }

        public async System.Threading.Tasks.Task ExportRoomsToExcel(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/telemetryserver/rooms/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/telemetryserver/rooms/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportRoomsToCSV(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/telemetryserver/rooms/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/telemetryserver/rooms/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }
        partial void OnGetRooms(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Radzen.ODataServiceResult<Hms.Models.TelemetryServer.Room>> GetRooms(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Rooms");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetRooms(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<Hms.Models.TelemetryServer.Room>>(response);
        }
        partial void OnCreateRoom(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Hms.Models.TelemetryServer.Room> CreateRoom(Hms.Models.TelemetryServer.Room room = default(Hms.Models.TelemetryServer.Room))
        {
            var uri = new Uri(baseUri, $"Rooms");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(room), Encoding.UTF8, "application/json");

            OnCreateRoom(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Hms.Models.TelemetryServer.Room>(response);
        }

        public async System.Threading.Tasks.Task ExportSimulationcontrolsToExcel(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/telemetryserver/simulationcontrols/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/telemetryserver/simulationcontrols/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportSimulationcontrolsToCSV(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/telemetryserver/simulationcontrols/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/telemetryserver/simulationcontrols/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }
        partial void OnGetSimulationcontrols(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Radzen.ODataServiceResult<Hms.Models.TelemetryServer.Simulationcontrol>> GetSimulationcontrols(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Simulationcontrols");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSimulationcontrols(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<Hms.Models.TelemetryServer.Simulationcontrol>>(response);
        }
        partial void OnCreateSimulationcontrol(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Hms.Models.TelemetryServer.Simulationcontrol> CreateSimulationcontrol(Hms.Models.TelemetryServer.Simulationcontrol simulationcontrol = default(Hms.Models.TelemetryServer.Simulationcontrol))
        {
            var uri = new Uri(baseUri, $"Simulationcontrols");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(simulationcontrol), Encoding.UTF8, "application/json");

            OnCreateSimulationcontrol(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Hms.Models.TelemetryServer.Simulationcontrol>(response);
        }

        public async System.Threading.Tasks.Task ExportSimulationfailuresToExcel(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/telemetryserver/simulationfailures/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/telemetryserver/simulationfailures/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportSimulationfailuresToCSV(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/telemetryserver/simulationfailures/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/telemetryserver/simulationfailures/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }
        partial void OnGetSimulationfailures(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Radzen.ODataServiceResult<Hms.Models.TelemetryServer.Simulationfailure>> GetSimulationfailures(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Simulationfailures");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSimulationfailures(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<Hms.Models.TelemetryServer.Simulationfailure>>(response);
        }
        partial void OnCreateSimulationfailure(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Hms.Models.TelemetryServer.Simulationfailure> CreateSimulationfailure(Hms.Models.TelemetryServer.Simulationfailure simulationfailure = default(Hms.Models.TelemetryServer.Simulationfailure))
        {
            var uri = new Uri(baseUri, $"Simulationfailures");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(simulationfailure), Encoding.UTF8, "application/json");

            OnCreateSimulationfailure(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Hms.Models.TelemetryServer.Simulationfailure>(response);
        }

        public async System.Threading.Tasks.Task ExportSimulationstatesToExcel(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/telemetryserver/simulationstates/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/telemetryserver/simulationstates/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportSimulationstatesToCSV(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/telemetryserver/simulationstates/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/telemetryserver/simulationstates/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }
        partial void OnGetSimulationstates(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Radzen.ODataServiceResult<Hms.Models.TelemetryServer.Simulationstate>> GetSimulationstates(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Simulationstates");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSimulationstates(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<Hms.Models.TelemetryServer.Simulationstate>>(response);
        }
        partial void OnCreateSimulationstate(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Hms.Models.TelemetryServer.Simulationstate> CreateSimulationstate(Hms.Models.TelemetryServer.Simulationstate simulationstate = default(Hms.Models.TelemetryServer.Simulationstate))
        {
            var uri = new Uri(baseUri, $"Simulationstates");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(simulationstate), Encoding.UTF8, "application/json");

            OnCreateSimulationstate(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Hms.Models.TelemetryServer.Simulationstate>(response);
        }

        public async System.Threading.Tasks.Task ExportSimulationstateuiaToExcel(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/telemetryserver/simulationstateuia/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/telemetryserver/simulationstateuia/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportSimulationstateuiaToCSV(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/telemetryserver/simulationstateuia/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/telemetryserver/simulationstateuia/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }
        partial void OnGetSimulationstateuia(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Radzen.ODataServiceResult<Hms.Models.TelemetryServer.Simulationstateuium>> GetSimulationstateuia(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Simulationstateuia");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSimulationstateuia(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<Hms.Models.TelemetryServer.Simulationstateuium>>(response);
        }
        partial void OnCreateSimulationstateuium(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Hms.Models.TelemetryServer.Simulationstateuium> CreateSimulationstateuium(Hms.Models.TelemetryServer.Simulationstateuium simulationstateuium = default(Hms.Models.TelemetryServer.Simulationstateuium))
        {
            var uri = new Uri(baseUri, $"Simulationstateuia");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(simulationstateuium), Encoding.UTF8, "application/json");

            OnCreateSimulationstateuium(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Hms.Models.TelemetryServer.Simulationstateuium>(response);
        }

        public async System.Threading.Tasks.Task ExportSimulationuiaToExcel(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/telemetryserver/simulationuia/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/telemetryserver/simulationuia/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportSimulationuiaToCSV(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/telemetryserver/simulationuia/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/telemetryserver/simulationuia/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }
        partial void OnGetSimulationuia(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Radzen.ODataServiceResult<Hms.Models.TelemetryServer.Simulationuium>> GetSimulationuia(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Simulationuia");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSimulationuia(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<Hms.Models.TelemetryServer.Simulationuium>>(response);
        }
        partial void OnCreateSimulationuium(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Hms.Models.TelemetryServer.Simulationuium> CreateSimulationuium(Hms.Models.TelemetryServer.Simulationuium simulationuium = default(Hms.Models.TelemetryServer.Simulationuium))
        {
            var uri = new Uri(baseUri, $"Simulationuia");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(simulationuium), Encoding.UTF8, "application/json");

            OnCreateSimulationuium(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Hms.Models.TelemetryServer.Simulationuium>(response);
        }

        public async System.Threading.Tasks.Task ExportUsersToExcel(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/telemetryserver/users/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/telemetryserver/users/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportUsersToCSV(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/telemetryserver/users/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/telemetryserver/users/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }
        partial void OnGetUsers(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Radzen.ODataServiceResult<Hms.Models.TelemetryServer.User>> GetUsers(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Users");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetUsers(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<Hms.Models.TelemetryServer.User>>(response);
        }
        partial void OnCreateUser(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Hms.Models.TelemetryServer.User> CreateUser(Hms.Models.TelemetryServer.User user = default(Hms.Models.TelemetryServer.User))
        {
            var uri = new Uri(baseUri, $"Users");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(user), Encoding.UTF8, "application/json");

            OnCreateUser(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Hms.Models.TelemetryServer.User>(response);
        }
        partial void OnDeleteLocation(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> DeleteLocation(int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"Locations({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteLocation(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnGetLocationByid(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Hms.Models.TelemetryServer.Location> GetLocationByid(string expand = default(string), int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"Locations({id})");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetLocationByid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Hms.Models.TelemetryServer.Location>(response);
        }
        partial void OnUpdateLocation(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> UpdateLocation(int? id = default(int?), Hms.Models.TelemetryServer.Location location = default(Hms.Models.TelemetryServer.Location))
        {
            var uri = new Uri(baseUri, $"Locations({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(location), Encoding.UTF8, "application/json");

            OnUpdateLocation(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnDeleteLsar(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> DeleteLsar(int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"Lsars({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteLsar(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnGetLsarByid(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Hms.Models.TelemetryServer.Lsar> GetLsarByid(string expand = default(string), int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"Lsars({id})");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetLsarByid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Hms.Models.TelemetryServer.Lsar>(response);
        }
        partial void OnUpdateLsar(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> UpdateLsar(int? id = default(int?), Hms.Models.TelemetryServer.Lsar lsar = default(Hms.Models.TelemetryServer.Lsar))
        {
            var uri = new Uri(baseUri, $"Lsars({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(lsar), Encoding.UTF8, "application/json");

            OnUpdateLsar(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnDeleteRole(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> DeleteRole(int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"Roles({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteRole(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnGetRoleByid(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Hms.Models.TelemetryServer.Role> GetRoleByid(string expand = default(string), int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"Roles({id})");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetRoleByid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Hms.Models.TelemetryServer.Role>(response);
        }
        partial void OnUpdateRole(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> UpdateRole(int? id = default(int?), Hms.Models.TelemetryServer.Role role = default(Hms.Models.TelemetryServer.Role))
        {
            var uri = new Uri(baseUri, $"Roles({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(role), Encoding.UTF8, "application/json");

            OnUpdateRole(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnDeleteRoom(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> DeleteRoom(int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"Rooms({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteRoom(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnGetRoomByid(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Hms.Models.TelemetryServer.Room> GetRoomByid(string expand = default(string), int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"Rooms({id})");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetRoomByid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Hms.Models.TelemetryServer.Room>(response);
        }
        partial void OnUpdateRoom(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> UpdateRoom(int? id = default(int?), Hms.Models.TelemetryServer.Room room = default(Hms.Models.TelemetryServer.Room))
        {
            var uri = new Uri(baseUri, $"Rooms({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(room), Encoding.UTF8, "application/json");

            OnUpdateRoom(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnDeleteSimulationcontrol(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> DeleteSimulationcontrol(int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"Simulationcontrols({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteSimulationcontrol(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnGetSimulationcontrolByid(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Hms.Models.TelemetryServer.Simulationcontrol> GetSimulationcontrolByid(string expand = default(string), int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"Simulationcontrols({id})");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSimulationcontrolByid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Hms.Models.TelemetryServer.Simulationcontrol>(response);
        }
        partial void OnUpdateSimulationcontrol(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> UpdateSimulationcontrol(int? id = default(int?), Hms.Models.TelemetryServer.Simulationcontrol simulationcontrol = default(Hms.Models.TelemetryServer.Simulationcontrol))
        {
            var uri = new Uri(baseUri, $"Simulationcontrols({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(simulationcontrol), Encoding.UTF8, "application/json");

            OnUpdateSimulationcontrol(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnDeleteSimulationfailure(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> DeleteSimulationfailure(int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"Simulationfailures({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteSimulationfailure(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnGetSimulationfailureByid(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Hms.Models.TelemetryServer.Simulationfailure> GetSimulationfailureByid(string expand = default(string), int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"Simulationfailures({id})");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSimulationfailureByid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Hms.Models.TelemetryServer.Simulationfailure>(response);
        }
        partial void OnUpdateSimulationfailure(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> UpdateSimulationfailure(int? id = default(int?), Hms.Models.TelemetryServer.Simulationfailure simulationfailure = default(Hms.Models.TelemetryServer.Simulationfailure))
        {
            var uri = new Uri(baseUri, $"Simulationfailures({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(simulationfailure), Encoding.UTF8, "application/json");

            OnUpdateSimulationfailure(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnDeleteSimulationstate(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> DeleteSimulationstate(int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"Simulationstates({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteSimulationstate(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnGetSimulationstateByid(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Hms.Models.TelemetryServer.Simulationstate> GetSimulationstateByid(string expand = default(string), int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"Simulationstates({id})");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSimulationstateByid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Hms.Models.TelemetryServer.Simulationstate>(response);
        }
        partial void OnUpdateSimulationstate(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> UpdateSimulationstate(int? id = default(int?), Hms.Models.TelemetryServer.Simulationstate simulationstate = default(Hms.Models.TelemetryServer.Simulationstate))
        {
            var uri = new Uri(baseUri, $"Simulationstates({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(simulationstate), Encoding.UTF8, "application/json");

            OnUpdateSimulationstate(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnDeleteSimulationstateuium(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> DeleteSimulationstateuium(int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"Simulationstateuia({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteSimulationstateuium(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnGetSimulationstateuiumByid(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Hms.Models.TelemetryServer.Simulationstateuium> GetSimulationstateuiumByid(string expand = default(string), int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"Simulationstateuia({id})");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSimulationstateuiumByid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Hms.Models.TelemetryServer.Simulationstateuium>(response);
        }
        partial void OnUpdateSimulationstateuium(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> UpdateSimulationstateuium(int? id = default(int?), Hms.Models.TelemetryServer.Simulationstateuium simulationstateuium = default(Hms.Models.TelemetryServer.Simulationstateuium))
        {
            var uri = new Uri(baseUri, $"Simulationstateuia({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(simulationstateuium), Encoding.UTF8, "application/json");

            OnUpdateSimulationstateuium(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnDeleteSimulationuium(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> DeleteSimulationuium(int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"Simulationuia({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteSimulationuium(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnGetSimulationuiumByid(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Hms.Models.TelemetryServer.Simulationuium> GetSimulationuiumByid(string expand = default(string), int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"Simulationuia({id})");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSimulationuiumByid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Hms.Models.TelemetryServer.Simulationuium>(response);
        }
        partial void OnUpdateSimulationuium(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> UpdateSimulationuium(int? id = default(int?), Hms.Models.TelemetryServer.Simulationuium simulationuium = default(Hms.Models.TelemetryServer.Simulationuium))
        {
            var uri = new Uri(baseUri, $"Simulationuia({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(simulationuium), Encoding.UTF8, "application/json");

            OnUpdateSimulationuium(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnDeleteUser(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> DeleteUser(int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"Users({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteUser(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnGetUserByid(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Hms.Models.TelemetryServer.User> GetUserByid(string expand = default(string), int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"Users({id})");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetUserByid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Hms.Models.TelemetryServer.User>(response);
        }
        partial void OnUpdateUser(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> UpdateUser(int? id = default(int?), Hms.Models.TelemetryServer.User user = default(Hms.Models.TelemetryServer.User))
        {
            var uri = new Uri(baseUri, $"Users({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(user), Encoding.UTF8, "application/json");

            OnUpdateUser(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
    }
}
