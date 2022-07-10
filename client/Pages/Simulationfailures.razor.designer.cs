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
    public partial class SimulationfailuresComponent : ComponentBase
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
        protected RadzenDataGrid<Hms.Models.TelemetryServer.Simulationfailure> grid0;

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

        IEnumerable<Hms.Models.TelemetryServer.Simulationfailure> _getSimulationfailuresResult;
        protected IEnumerable<Hms.Models.TelemetryServer.Simulationfailure> getSimulationfailuresResult
        {
            get
            {
                return _getSimulationfailuresResult;
            }
            set
            {
                if (!object.Equals(_getSimulationfailuresResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getSimulationfailuresResult", NewValue = value, OldValue = _getSimulationfailuresResult };
                    _getSimulationfailuresResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        int _getSimulationfailuresCount;
        protected int getSimulationfailuresCount
        {
            get
            {
                return _getSimulationfailuresCount;
            }
            set
            {
                if (!object.Equals(_getSimulationfailuresCount, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getSimulationfailuresCount", NewValue = value, OldValue = _getSimulationfailuresCount };
                    _getSimulationfailuresCount = value;
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
            var dialogResult = await DialogService.OpenAsync<AddSimulationfailure>("Add Simulationfailure", null);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Splitbutton0Click(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await TelemetryServer.ExportSimulationfailuresToCSV(new Query() { Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "", Select = "id,room,started_at,o2_error,pump_error,power_error,fan_error,createdAt,updatedAt" }, $"Simulationfailures");

            }

            if (args == null || args.Value == "xlsx")
            {
                await TelemetryServer.ExportSimulationfailuresToExcel(new Query() { Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "", Select = "id,room,started_at,o2_error,pump_error,power_error,fan_error,createdAt,updatedAt" }, $"Simulationfailures");

            }
        }

        protected async System.Threading.Tasks.Task Grid0LoadData(LoadDataArgs args)
        {
            try
            {
                var telemetryServerGetSimulationfailuresResult = await TelemetryServer.GetSimulationfailures(filter:$@"(contains(started_at,""{search}"")) and {(string.IsNullOrEmpty(args.Filter)? "true" : args.Filter)}", orderby:$"{args.OrderBy}", top:args.Top, skip:args.Skip, count:args.Top != null && args.Skip != null);
                getSimulationfailuresResult = telemetryServerGetSimulationfailuresResult.Value.AsODataEnumerable();

                getSimulationfailuresCount = telemetryServerGetSimulationfailuresResult.Count;
            }
            catch (System.Exception telemetryServerGetSimulationfailuresException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to load Simulationfailures" });
            }
        }

        protected async System.Threading.Tasks.Task Grid0RowDoubleClick(DataGridRowMouseEventArgs<Hms.Models.TelemetryServer.Simulationfailure> args)
        {
            var dialogResult = await DialogService.OpenAsync<EditSimulationfailure>("Edit Simulationfailure", new Dictionary<string, object>() { {"id", args.Data.id} });
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var telemetryServerDeleteSimulationfailureResult = await TelemetryServer.DeleteSimulationfailure(id:data.id);
                    if (telemetryServerDeleteSimulationfailureResult != null && telemetryServerDeleteSimulationfailureResult.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        await grid0.Reload();
                    }

                    if (telemetryServerDeleteSimulationfailureResult != null && telemetryServerDeleteSimulationfailureResult.StatusCode != System.Net.HttpStatusCode.NoContent)
                    {
                        NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete Simulationfailure" });
                    }
                }
            }
            catch (System.Exception telemetryServerDeleteSimulationfailureException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete Simulationfailure" });
            }
        }
    }
}
