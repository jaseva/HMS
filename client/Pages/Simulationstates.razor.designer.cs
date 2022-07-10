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
    public partial class SimulationstatesComponent : ComponentBase
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
        protected RadzenDataGrid<Hms.Models.TelemetryServer.Simulationstate> grid0;

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

        IEnumerable<Hms.Models.TelemetryServer.Simulationstate> _getSimulationstatesResult;
        protected IEnumerable<Hms.Models.TelemetryServer.Simulationstate> getSimulationstatesResult
        {
            get
            {
                return _getSimulationstatesResult;
            }
            set
            {
                if (!object.Equals(_getSimulationstatesResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getSimulationstatesResult", NewValue = value, OldValue = _getSimulationstatesResult };
                    _getSimulationstatesResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        int _getSimulationstatesCount;
        protected int getSimulationstatesCount
        {
            get
            {
                return _getSimulationstatesCount;
            }
            set
            {
                if (!object.Equals(_getSimulationstatesCount, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getSimulationstatesCount", NewValue = value, OldValue = _getSimulationstatesCount };
                    _getSimulationstatesCount = value;
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
            var dialogResult = await DialogService.OpenAsync<AddSimulationstate>("Add Simulationstate", null);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Splitbutton0Click(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await TelemetryServer.ExportSimulationstatesToCSV(new Query() { Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "", Select = "id,room,isRunning,isPaused,timer,started_at,t_battery,t_oxygen,t_water,createdAt,updatedAt" }, $"Simulationstates");

            }

            if (args == null || args.Value == "xlsx")
            {
                await TelemetryServer.ExportSimulationstatesToExcel(new Query() { Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "", Select = "id,room,isRunning,isPaused,timer,started_at,t_battery,t_oxygen,t_water,createdAt,updatedAt" }, $"Simulationstates");

            }
        }

        protected async System.Threading.Tasks.Task Grid0LoadData(LoadDataArgs args)
        {
            try
            {
                var telemetryServerGetSimulationstatesResult = await TelemetryServer.GetSimulationstates(filter:$@"(contains(timer,""{search}"") or contains(started_at,""{search}"") or contains(t_battery,""{search}"") or contains(t_oxygen,""{search}"") or contains(t_water,""{search}"")) and {(string.IsNullOrEmpty(args.Filter)? "true" : args.Filter)}", orderby:$"{args.OrderBy}", top:args.Top, skip:args.Skip, count:args.Top != null && args.Skip != null);
                getSimulationstatesResult = telemetryServerGetSimulationstatesResult.Value.AsODataEnumerable();

                getSimulationstatesCount = telemetryServerGetSimulationstatesResult.Count;
            }
            catch (System.Exception telemetryServerGetSimulationstatesException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to load Simulationstates" });
            }
        }

        protected async System.Threading.Tasks.Task Grid0RowDoubleClick(DataGridRowMouseEventArgs<Hms.Models.TelemetryServer.Simulationstate> args)
        {
            var dialogResult = await DialogService.OpenAsync<EditSimulationstate>("Edit Simulationstate", new Dictionary<string, object>() { {"id", args.Data.id} });
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var telemetryServerDeleteSimulationstateResult = await TelemetryServer.DeleteSimulationstate(id:data.id);
                    if (telemetryServerDeleteSimulationstateResult != null && telemetryServerDeleteSimulationstateResult.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        await grid0.Reload();
                    }

                    if (telemetryServerDeleteSimulationstateResult != null && telemetryServerDeleteSimulationstateResult.StatusCode != System.Net.HttpStatusCode.NoContent)
                    {
                        NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete Simulationstate" });
                    }
                }
            }
            catch (System.Exception telemetryServerDeleteSimulationstateException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete Simulationstate" });
            }
        }
    }
}
