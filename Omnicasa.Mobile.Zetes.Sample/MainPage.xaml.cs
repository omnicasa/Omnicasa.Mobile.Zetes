using System;
using System.Reactive.Linq;
using Microsoft.Maui.Controls;
using ReactiveUI;

namespace Omnicasa.Mobile.Zetes.Sample;

public partial class MainPage : ContentPage
{
    private IZetesService zetesService;

    /// <summary>
    /// Initializes a new instance of the <see cref="MainPage"/> class.
    /// </summary>
    public MainPage()
    {
        InitializeComponent();
        #if __IOS__
        zetesService = new Omnicasa.Mobile.Zetes.iOS.ZetesService();
        #else
        zetesService = new Omnicasa.Mobile.Zetes.Droid.ZetesService();
        #endif

        zetesService.Logs()
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe(mes =>
            {
                Console.WriteLine($"Zetes logging: {mes}");
                Label.Text = mes;
            });

        zetesService.Scanning()
            .Skip(1)
            .Subscribe(mes =>
            {
                Console.WriteLine($"{mes.State}: {mes.Exception?.GetType()?.ToString()}");
            });
    }

    private void Button_OnClicked(object? sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("Button_OnClicked");
        Console.WriteLine("Button_OnClicked");
        zetesService.StartScan().Subscribe();
    }
}