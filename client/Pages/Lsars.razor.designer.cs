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
    public partial class LsarsComponent : ComponentBase
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
        protected RadzenDataGrid<Hms.Models.TelemetryServer.Lsar> grid0;

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

        IEnumerable<Hms.Models.TelemetryServer.Lsar> _getLsarsResult;
        protected IEnumerable<Hms.Models.TelemetryServer.Lsar> getLsarsResult
        {
            get
            {
                return _getLsarsResult;
            }
            set
            {
                if (!object.Equals(_getLsarsResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getLsarsResult", NewValue = value, OldValue = _getLsarsResult };
                    _getLsarsResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        int _getLsarsCount;
        protected int getLsarsCount
        {
            get
            {
                return _getLsarsCount;
            }
            set
            {
                if (!object.Equals(_getLsarsCount, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getLsarsCount", NewValue = value, OldValue = _getLsarsCount };
                    _getLsarsCount = value;
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
            var dialogResult = await DialogService.OpenAsync<AddLsar>("Add Lsar", null);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Splitbutton0Click(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await TelemetryServer.ExportLsarsToCSV(new Query() { Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "", Select = "id,sender,room,time,priority_tag,encoded_lat,encoded_lon,pnt_source,condition_state,vmc_txt,tac_sn,cntry_code,homing_dvc_stat,ret_lnk_stat,test_proto,vessel_id,beacon_type,createdAt,updatedAt" }, $"Lsars");

            }

            if (args == null || args.Value == "xlsx")
            {
                await TelemetryServer.ExportLsarsToExcel(new Query() { Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "", Select = "id,sender,room,time,priority_tag,encoded_lat,encoded_lon,pnt_source,condition_state,vmc_txt,tac_sn,cntry_code,homing_dvc_stat,ret_lnk_stat,test_proto,vessel_id,beacon_type,createdAt,updatedAt" }, $"Lsars");

            }
        }

        protected async System.Threading.Tasks.Task Grid0LoadData(LoadDataArgs args)
        {
            try
            {
                var telemetryServerGetLsarsResult = await TelemetryServer.GetLsars(filter:$@"(contains(time,""{search}"") or contains(priority_tag,""{search}"") or contains(encoded_lat,""{search}"") or contains(encoded_lon,""{search}"") or contains(pnt_source,""{search}"") or contains(condition_state,""{search}"") or contains(vmc_txt,""{search}"") or contains(tac_sn,""{search}"") or contains(cntry_code,""{search}"") or contains(homing_dvc_stat,""{search}"") or contains(ret_lnk_stat,""{search}"") or contains(test_proto,""{search}"") or contains(vessel_id,""{search}"") or contains(beacon_type,""{search}"")) and {(string.IsNullOrEmpty(args.Filter)? "true" : args.Filter)}", orderby:$"{args.OrderBy}", top:args.Top, skip:args.Skip, count:args.Top != null && args.Skip != null);
                getLsarsResult = telemetryServerGetLsarsResult.Value.AsODataEnumerable();

                getLsarsCount = telemetryServerGetLsarsResult.Count;
            }
            catch (System.Exception telemetryServerGetLsarsException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to load Lsars" });
            }
        }

        protected async System.Threading.Tasks.Task Grid0RowDoubleClick(DataGridRowMouseEventArgs<Hms.Models.TelemetryServer.Lsar> args)
        {
            var dialogResult = await DialogService.OpenAsync<EditLsar>("Edit Lsar", new Dictionary<string, object>() { {"id", args.Data.id} });
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var telemetryServerDeleteLsarResult = await TelemetryServer.DeleteLsar(id:data.id);
                    if (telemetryServerDeleteLsarResult != null && telemetryServerDeleteLsarResult.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        await grid0.Reload();
                    }

                    if (telemetryServerDeleteLsarResult != null && telemetryServerDeleteLsarResult.StatusCode != System.Net.HttpStatusCode.NoContent)
                    {
                        NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete Lsar" });
                    }
                }
            }
            catch (System.Exception telemetryServerDeleteLsarException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete Lsar" });
            }
        }
    }
}
