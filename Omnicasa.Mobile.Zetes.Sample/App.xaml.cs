using Microsoft.Maui.Controls;

namespace Omnicasa.Mobile.Zetes.Sample;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
    }
}