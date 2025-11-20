using System;

namespace Omnicasa.Mobile.Zetes;

/// <summary>
/// IZetesService.
/// </summary>
public interface IZetesService : IDisposable
{
    /// <summary>
    /// StartScan.
    /// </summary>
    /// <returns>ZetesEvent.</returns>
    IObservable<ZetesEvent> StartScan();

    /// <summary>
    /// StopScan.
    /// </summary>
    /// <returns>ZetesEvent.</returns>
    IObservable<ZetesEvent> StopScan();

    /// <summary>
    /// Scanning.
    /// </summary>
    /// <returns>ZetesEvent.</returns>
    IObservable<ZetesEvent> Scanning();

    /// <summary>
    /// IsSupported.
    /// </summary>
    /// <returns>bool.</returns>
    IObservable<bool> IsSupported();

    /// <summary>
    /// Logs.
    /// </summary>
    /// <returns>string.</returns>
    IObservable<string> Logs();
}
