using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xamarin.Forms;

namespace XamarinFormsSampleApp
{
    public partial class App : Application
    {
        public App() => InitializeComponent();

        public App(IHost host) : this() => Host = host;

        public static IHost Host { get; private set; }

        public static IHostBuilder BuildHost() =>
            XamarinHost.CreateDefaultBuilder<App>()
            .ConfigureServices((context, services) =>
            {
                services.AddScoped<MainPage>();
            });

        protected override void OnStart()
        {
            Task.Run(async () => await Host.StartAsync());
            MainPage = Host.Services.GetRequiredService<MainPage>();
        }

        protected override void OnSleep()
        {
            Task.Run(async () => await Host.SleepAsync());
        }

        protected override void OnResume()
        {
            Task.Run(async () => await Host.ResumeAsync());
        }
    }
}
