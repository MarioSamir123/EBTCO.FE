using EBTCO.FE.Contract;
using EBTCO.FE.Widgets.Employee;
using EBTCO.FE.Widgets.Owner;
using EBTCO.FE.Widgets.SalesOffice;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace EBTCO.FE
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    
                    fonts.AddFont("Brands-Regular-400.otf", "FBR");
                    fonts.AddFont("Free-Regular-400.otf", "FFR");
                    fonts.AddFont("Free-Solid-900", "FFS");

                });
            builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
#if DEBUG
            builder.Logging.AddDebug();
#endif
            builder.Services.AddScoped<IApiService, ApiService>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<EmployeesIndex>();
            builder.Services.AddSingleton<OwnersIndex>();
            builder.Services.AddSingleton<SalesOfficesIndex>();
            var app = builder.Build(); 
            return app;
        }
    }
}
