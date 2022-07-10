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
    public partial class AddLocationComponent : ComponentBase
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

        Hms.Models.TelemetryServer.Location _location;
        protected Hms.Models.TelemetryServer.Location location
        {
            get
            {
                return _location;
            }
            set
            {
                if (!object.Equals(_location, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "location", NewValue = value, OldValue = _location };
                    _location = value;
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
            location = new Hms.Models.TelemetryServer.Location(){};
        }

        protected async System.Threading.Tasks.Task Form0Submit(Hms.Models.TelemetryServer.Location args)
        {
            try
            {
                var telemetryServerCreateLocationResult = await TelemetryServer.CreateLocation(location:location);
                DialogService.Close(location);
            }
            catch (System.Exception telemetryServerCreateLocationException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to create new Location!" });
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
