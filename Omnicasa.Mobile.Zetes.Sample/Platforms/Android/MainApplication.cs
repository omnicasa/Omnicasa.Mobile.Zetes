using System;
using Android.App;
using Android.Runtime;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;

namespace Omnicasa.Mobile.Zetes.Sample;

[Application]
public class MainApplication : MauiApplication
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MainApplication"/> class.
    /// </summary>
    /// <param name="handle">IntPtr.</param>
    /// <param name="ownership">JniHandleOwnership.</param>
    public MainApplication(IntPtr handle, JniHandleOwnership ownership)
        : base(handle, ownership)
    {
    }

    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}