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
    public partial class UsersComponent : ComponentBase
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
        protected RadzenDataGrid<Hms.Models.TelemetryServer.User> grid0;

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

        IEnumerable<Hms.Models.TelemetryServer.User> _getUsersResult;
        protected IEnumerable<Hms.Models.TelemetryServer.User> getUsersResult
        {
            get
            {
                return _getUsersResult;
            }
            set
            {
                if (!object.Equals(_getUsersResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getUsersResult", NewValue = value, OldValue = _getUsersResult };
                    _getUsersResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        int _getUsersCount;
        protected int getUsersCount
        {
            get
            {
                return _getUsersCount;
            }
            set
            {
                if (!object.Equals(_getUsersCount, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getUsersCount", NewValue = value, OldValue = _getUsersCount };
                    _getUsersCount = value;
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
            var dialogResult = await DialogService.OpenAsync<AddUser>("Add User", null);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Splitbutton0Click(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await TelemetryServer.ExportUsersToCSV(new Query() { Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "", Select = "id,username,room,createdAt,updatedAt" }, $"Users");

            }

            if (args == null || args.Value == "xlsx")
            {
                await TelemetryServer.ExportUsersToExcel(new Query() { Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "", Select = "id,username,room,createdAt,updatedAt" }, $"Users");

            }
        }

        protected async System.Threading.Tasks.Task Grid0LoadData(LoadDataArgs args)
        {
            try
            {
                var telemetryServerGetUsersResult = await TelemetryServer.GetUsers(filter:$@"(contains(username,""{search}"")) and {(string.IsNullOrEmpty(args.Filter)? "true" : args.Filter)}", orderby:$"{args.OrderBy}", top:args.Top, skip:args.Skip, count:args.Top != null && args.Skip != null);
                getUsersResult = telemetryServerGetUsersResult.Value.AsODataEnumerable();

                getUsersCount = telemetryServerGetUsersResult.Count;
            }
            catch (System.Exception telemetryServerGetUsersException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to load Users" });
            }
        }

        protected async System.Threading.Tasks.Task Grid0RowDoubleClick(DataGridRowMouseEventArgs<Hms.Models.TelemetryServer.User> args)
        {
            var dialogResult = await DialogService.OpenAsync<EditUser>("Edit User", new Dictionary<string, object>() { {"id", args.Data.id} });
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var telemetryServerDeleteUserResult = await TelemetryServer.DeleteUser(id:data.id);
                    if (telemetryServerDeleteUserResult != null && telemetryServerDeleteUserResult.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        await grid0.Reload();
                    }

                    if (telemetryServerDeleteUserResult != null && telemetryServerDeleteUserResult.StatusCode != System.Net.HttpStatusCode.NoContent)
                    {
                        NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete User" });
                    }
                }
            }
            catch (System.Exception telemetryServerDeleteUserException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete User" });
            }
        }
    }
}
