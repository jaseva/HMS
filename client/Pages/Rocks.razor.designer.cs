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
    public partial class RocksComponent : ComponentBase
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
        protected RadzenDataGrid<Hms.Models.DataSource.Rock> grid0;

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

        IEnumerable<Hms.Models.DataSource.Rock> _getRocksResult;
        protected IEnumerable<Hms.Models.DataSource.Rock> getRocksResult
        {
            get
            {
                return _getRocksResult;
            }
            set
            {
                if (!object.Equals(_getRocksResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getRocksResult", NewValue = value, OldValue = _getRocksResult };
                    _getRocksResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        int _getRocksCount;
        protected int getRocksCount
        {
            get
            {
                return _getRocksCount;
            }
            set
            {
                if (!object.Equals(_getRocksCount, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getRocksCount", NewValue = value, OldValue = _getRocksCount };
                    _getRocksCount = value;
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
                await DataSource.ExportRocksToCSV(new Query() { Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "", Select = "ID,Title,Description" }, $"Rocks");

            }

            if (args == null || args.Value == "xlsx")
            {
                await DataSource.ExportRocksToExcel(new Query() { Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "", Select = "ID,Title,Description" }, $"Rocks");

            }
        }

        protected async System.Threading.Tasks.Task Grid0LoadData(LoadDataArgs args)
        {
            try
            {
                var dataSourceGetRocksResult = await DataSource.GetRocks(filter:$@"(contains(Title,""{search}"") or contains(Description,""{search}"")) and {(string.IsNullOrEmpty(args.Filter)? "true" : args.Filter)}", orderby:$"{args.OrderBy}", top:args.Top, skip:args.Skip, count:args.Top != null && args.Skip != null);
                getRocksResult = dataSourceGetRocksResult.Value.AsODataEnumerable();

                getRocksCount = dataSourceGetRocksResult.Count;
            }
            catch (System.Exception dataSourceGetRocksException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to load Rocks" });
            }
        }
    }
}
