using System.Reactive.Subjects;
using Omnicasa.Mobile.Zetes.Standard;

namespace Omnicasa.Mobile.Zetes;

/// <summary>AbstractService.</summary>
public abstract class AbstractService
{
    /// <summary>ObsLogs.</summary>
#pragma warning disable SA1306, SA1401
    protected BehaviorSubject<string> ObsLogs = new BehaviorSubject<string>("Initializing..");

    /// <summary>ObsEvents.</summary>
    protected BehaviorSubject<ZetesEvent> ObsEvents = new BehaviorSubject<ZetesEvent>(new ZetesEvent()
    {
        State = ZetesState.Unknown,
    });
#pragma warning restore SA1306, SA1401
}