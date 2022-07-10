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
    public partial class SimulationuiaComponent : ComponentBase
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
        protected RadzenDataGrid<Hms.Models.TelemetryServer.Simulationuium> grid0;

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

        IEnumerable<Hms.Models.TelemetryServer.Simulationuium> _getSimulationuiaResult;
        protected IEnumerable<Hms.Models.TelemetryServer.Simulationuium> getSimulationuiaResult
        {
            get
            {
                return _getSimulationuiaResult;
            }
            set
            {
                if (!object.Equals(_getSimulationuiaResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getSimulationuiaResult", NewValue = value, OldValue = _getSimulationuiaResult };
                    _getSimulationuiaResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        int _getSimulationuiaCount;
        protected int getSimulationuiaCount
        {
            get
            {
                return _getSimulationuiaCount;
            }
            set
            {
                if (!object.Equals(_getSimulationuiaCount, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getSimulationuiaCount", NewValue = value, OldValue = _getSimulationuiaCount };
                    _getSimulationuiaCount = value;
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
            var dialogResult = await DialogService.OpenAsync<AddSimulationuium>("Add Simulationuium", null);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Splitbutton0Click(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await TelemetryServer.ExportSimulationuiaToCSV(new Query() { Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "", Select = "id,room,started_at,emu1,ev1_supply,ev1_waste,emu1_O2,emu2,ev2_supply,ev2_waste,emu2_O2,O2_vent,depress_pump,createdAt,updatedAt" }, $"Simulationuia");

            }

            if (args == null || args.Value == "xlsx")
            {
                await TelemetryServer.ExportSimulationuiaToExcel(new Query() { Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "", Select = "id,room,started_at,emu1,ev1_supply,ev1_waste,emu1_O2,emu2,ev2_supply,ev2_waste,emu2_O2,O2_vent,depress_pump,createdAt,updatedAt" }, $"Simulationuia");

            }
        }

        protected async System.Threading.Tasks.Task Grid0LoadData(LoadDataArgs args)
        {
            try
            {
                var telemetryServerGetSimulationuiaResult = await TelemetryServer.GetSimulationuia(filter:$@"(contains(started_at,""{search}"")) and {(string.IsNullOrEmpty(args.Filter)? "true" : args.Filter)}", orderby:$"{args.OrderBy}", top:args.Top, skip:args.Skip, count:args.Top != null && args.Skip != null);
                getSimulationuiaResult = telemetryServerGetSimulationuiaResult.Value.AsODataEnumerable();

                getSimulationuiaCount = telemetryServerGetSimulationuiaResult.Count;
            }
            catch (System.Exception telemetryServerGetSimulationuiaException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to load Simulationuia" });
            }
        }

        protected async System.Threading.Tasks.Task Grid0RowDoubleClick(DataGridRowMouseEventArgs<Hms.Models.TelemetryServer.Simulationuium> args)
        {
            var dialogResult = await DialogService.OpenAsync<EditSimulationuium>("Edit Simulationuium", new Dictionary<string, object>() { {"id", args.Data.id} });
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var telemetryServerDeleteSimulationuiumResult = await TelemetryServer.DeleteSimulationuium(id:data.id);
                    if (telemetryServerDeleteSimulationuiumResult != null && telemetryServerDeleteSimulationuiumResult.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        await grid0.Reload();
                    }

                    if (telemetryServerDeleteSimulationuiumResult != null && telemetryServerDeleteSimulationuiumResult.StatusCode != System.Net.HttpStatusCode.NoContent)
                    {
                        NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete Simulationuium" });
                    }
                }
            }
            catch (System.Exception telemetryServerDeleteSimulationuiumException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete Simulationuium" });
            }
        }
    }
}
