using System;

namespace Omnicasa.Mobile.Zetes.Standard;

/// <summary>
/// ZetesEvent.
/// </summary>
public class ZetesEvent
{
    /// <summary>
    /// CardInfo.
    /// </summary>
    public EidCardInfo CardInfo { get; set; }

    /// <summary>
    /// Exception.
    /// </summary>
    public Exception Exception { get; set; }

    /// <summary>
    /// State.
    /// </summary>
    public ZetesState State { get; set; }
}
