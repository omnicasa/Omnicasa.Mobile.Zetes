using System.Reactive.Subjects;
using Omnicasa.Mobile.Zetes.Standard;

namespace Omnicasa.Mobile.Zetes;

/// <summary>AbstractService</summary>
public class AbstractService
{
    protected BehaviorSubject<string> ObsLogs = new BehaviorSubject<string>("Initializing..");
    protected BehaviorSubject<ZetesEvent> ObsEvents = new BehaviorSubject<ZetesEvent>(new ZetesEvent()
    {
        State = ZetesState.Unknown,
    });
}