using Microsoft.Extensions.Logging;
using System.ComponentModel;
using Xamarin.Forms;

namespace XamarinFormsSampleApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public MainPage(ILogger<MainPage> logger) : this()
        {
            logger.LogInformation("Hello from MainPage!");
        }
    }
}
