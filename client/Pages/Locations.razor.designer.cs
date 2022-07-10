using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using Hms.Models.DataSource;
using Hms.Models.TelemetryServer;
using Microsoft.AspNetCore.Identity;
using Hms.Models;
using Hms.Client.Pages;

namespace Hms.Pages
{
    public partial class LocationsComponent : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, dynamic> Attributes { get; set; }

        public void Reload()
        {
            InvokeAsync(StateHasChanged);
        }

        public void OnPropertyChanged(PropertyChangedEventArgs args)
        {
        }

        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        protected ContextMenuService ContextMenuService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        protected SecurityService Security { get; set; }

        [Inject]
        protected AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        protected DataSourceService DataSource { get; set; }

        [Inject]
        protected TelemetryServerService TelemetryServer { get; set; }
        protected RadzenDataGrid<Hms.Models.TelemetryServer.Location> grid0;

        string _search;
        protected string search
        {
            get
            {
                return _search;
            }
            set
            {
                if (!object.Equals(_search, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "search", NewValue = value, OldValue = _search };
                    _search = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<Hms.Models.TelemetryServer.Location> _getLocationsResult;
        protected IEnumerable<Hms.Models.TelemetryServer.Location> getLocationsResult
        {
            get
            {
                return _getLocationsResult;
            }
            set
            {
                if (!object.Equals(_getLocationsResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getLocationsResult", NewValue = value, OldValue = _getLocationsResult };
                    _getLocationsResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        int _getLocationsCount;
        protected int getLocationsCount
        {
            get
            {
                return _getLocationsCount;
            }
            set
            {
                if (!object.Equals(_getLocationsCount, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getLocationsCount", NewValue = value, OldValue = _getLocationsCount };
                    _getLocationsCount = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            await Security.InitializeAsync(AuthenticationStateProvider);
            if (!Security.IsAuthenticated())
            {
                UriHelper.NavigateTo("Login", true);
            }
            else
            {
                await Load();
            }
        }
        protected async System.Threading.Tasks.Task Load()
        {
            if (string.IsNullOrEmpty(search)) {
                search = "";
            }
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<AddLocation>("Add Location", null);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Splitbutton0Click(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await TelemetryServer.ExportLocationsToCSV(new Query() { Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "", Select = "id,user,room,createdAt,updatedAt" }, $"Locations");

            }

            if (args == null || args.Value == "xlsx")
            {
                await TelemetryServer.ExportLocationsToExcel(new Query() { Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "", Select = "id,user,room,createdAt,updatedAt" }, $"Locations");

            }
        }

        protected async System.Threading.Tasks.Task Grid0LoadData(LoadDataArgs args)
        {
            try
            {
                var telemetryServerGetLocationsResult = await TelemetryServer.GetLocations(filter:$@"{(string.IsNullOrEmpty(args.Filter)? "true" : args.Filter)}", orderby:$"{args.OrderBy}", top:args.Top, skip:args.Skip, count:args.Top != null && args.Skip != null);
                getLocationsResult = telemetryServerGetLocationsResult.Value.AsODataEnumerable();

                getLocationsCount = telemetryServerGetLocationsResult.Count;
            }
            catch (System.Exception telemetryServerGetLocationsException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to load Locations" });
            }
        }

        protected async System.Threading.Tasks.Task Grid0RowDoubleClick(DataGridRowMouseEventArgs<Hms.Models.TelemetryServer.Location> args)
        {
            var dialogResult = await DialogService.OpenAsync<EditLocation>("Edit Location", new Dictionary<string, object>() { {"id", args.Data.id} });
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var telemetryServerDeleteLocationResult = await TelemetryServer.DeleteLocation(id:data.id);
                    if (telemetryServerDeleteLocationResult != null && telemetryServerDeleteLocationResult.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        await grid0.Reload();
                    }

                    if (telemetryServerDeleteLocationResult != null && telemetryServerDeleteLocationResult.StatusCode != System.Net.HttpStatusCode.NoContent)
                    {
                        NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete Location" });
                    }
                }
            }
            catch (System.Exception telemetryServerDeleteLocationException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete Location" });
            }
        }
    }
}
