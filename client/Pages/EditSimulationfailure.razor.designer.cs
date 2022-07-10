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
    public partial class EditSimulationfailureComponent : ComponentBase
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

        [Parameter]
        public dynamic id { get; set; }

        Hms.Models.TelemetryServer.Simulationfailure _simulationfailure;
        protected Hms.Models.TelemetryServer.Simulationfailure simulationfailure
        {
            get
            {
                return _simulationfailure;
            }
            set
            {
                if (!object.Equals(_simulationfailure, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "simulationfailure", NewValue = value, OldValue = _simulationfailure };
                    _simulationfailure = value;
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
            var telemetryServerGetSimulationfailureByidResult = await TelemetryServer.GetSimulationfailureByid(id:id);
            simulationfailure = telemetryServerGetSimulationfailureByidResult;
        }

        protected async System.Threading.Tasks.Task Form0Submit(Hms.Models.TelemetryServer.Simulationfailure args)
        {
            try
            {
                var telemetryServerUpdateSimulationfailureResult = await TelemetryServer.UpdateSimulationfailure(id:id, simulationfailure:simulationfailure);
                DialogService.Close(simulationfailure);
            }
            catch (System.Exception telemetryServerUpdateSimulationfailureException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to update Simulationfailure" });
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
