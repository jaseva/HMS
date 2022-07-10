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
    public partial class AnomaliesComponent : ComponentBase
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
        protected RadzenDataGrid<Hms.Models.DataSource.Anomaly> grid0;

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

        IEnumerable<Hms.Models.DataSource.Anomaly> _getAnomaliesResult;
        protected IEnumerable<Hms.Models.DataSource.Anomaly> getAnomaliesResult
        {
            get
            {
                return _getAnomaliesResult;
            }
            set
            {
                if (!object.Equals(_getAnomaliesResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getAnomaliesResult", NewValue = value, OldValue = _getAnomaliesResult };
                    _getAnomaliesResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        int _getAnomaliesCount;
        protected int getAnomaliesCount
        {
            get
            {
                return _getAnomaliesCount;
            }
            set
            {
                if (!object.Equals(_getAnomaliesCount, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getAnomaliesCount", NewValue = value, OldValue = _getAnomaliesCount };
                    _getAnomaliesCount = value;
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

        protected async System.Threading.Tasks.Task Splitbutton0Click(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await DataSource.ExportAnomaliesToCSV(new Query() { Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "", Select = "ID,Title,Description" }, $"Anomalies");

            }

            if (args == null || args.Value == "xlsx")
            {
                await DataSource.ExportAnomaliesToExcel(new Query() { Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "", Select = "ID,Title,Description" }, $"Anomalies");

            }
        }

        protected async System.Threading.Tasks.Task Grid0LoadData(LoadDataArgs args)
        {
            try
            {
                var dataSourceGetAnomaliesResult = await DataSource.GetAnomalies(filter:$@"(contains(Title,""{search}"") or contains(Description,""{search}"")) and {(string.IsNullOrEmpty(args.Filter)? "true" : args.Filter)}", orderby:$"{args.OrderBy}", top:args.Top, skip:args.Skip, count:args.Top != null && args.Skip != null);
                getAnomaliesResult = dataSourceGetAnomaliesResult.Value.AsODataEnumerable();

                getAnomaliesCount = dataSourceGetAnomaliesResult.Count;
            }
            catch (System.Exception dataSourceGetAnomaliesException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to load Anomalies" });
            }
        }
    }
}
