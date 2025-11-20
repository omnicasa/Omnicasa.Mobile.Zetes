using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Omnicasa.Mobile.Zetes.Standard;

namespace Omnicasa.Mobile.Zetes.iOS;

/// <summary>ZetesService.</summary>
public class ZetesService : IZetesService, IZetesCallback
{
    private Reader eidReader;
    private ZetesReaderDelegate zetesReaderDelegate;
    private BehaviorSubject<string> logs = new BehaviorSubject<string>("Initializing..");
    private BehaviorSubject<ZetesEvent> events = new BehaviorSubject<ZetesEvent>(null);

    /// <inheritdoc/>
    public void Dispose()
    {
        StopScan().Subscribe();
    }

    /// <inheritdoc/>
    public IObservable<ZetesEvent> StartScan()
    {
        return Observable.Create<ZetesEvent>(o =>
        {
            try
            {
                if (eidReader == null)
                {
                    // Match demo approach: use "FT_ANY" to scan for any reader, with BLE type
                    // Options: "BLE", "IR301_AND_BR301", or "" for undefined
                    eidReader = new Reader("FT_ANY", string.Empty, "BLE");
                }

                if (zetesReaderDelegate == null)
                {
                    zetesReaderDelegate = new ZetesReaderDelegate(this);
                }

                eidReader.Delegate = zetesReaderDelegate;
                eidReader.StartScan();

                o.OnNext(new ZetesEvent()
                {
                    State = ZetesState.Starting,
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            o.OnCompleted();

            return Disposable.Empty;
        }).Catch((Exception ex) => Observable.Return(new ZetesEvent()
        {
            Exception = ex,
            State = ZetesState.Error,
        }));
    }

    /// <inheritdoc/>
    public IObservable<ZetesEvent> StopScan()
    {
        return Observable.Create<ZetesEvent>(o =>
        {
            try
            {
                if (eidReader != null)
                {
                    eidReader.Delegate = null;
                    eidReader.StopScan();
                }

                o.OnNext(new ZetesEvent()
                {
                    State = ZetesState.Stopped,
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            o.OnCompleted();

            return Disposable.Empty;
        }).Catch((Exception ex) => Observable.Return(new ZetesEvent()
        {
            Exception = ex,
            State = ZetesState.Error,
        }));
    }

    /// <inheritdoc/>
    public IObservable<ZetesEvent> Scanning()
    {
        return events;
    }

    /// <inheritdoc/>
    public IObservable<bool> IsSupported()
    {
        return Observable.Return(true);
    }

    /// <inheritdoc/>
    public IObservable<string> Logs()
    {
        return logs;
    }

    /// <inheritdoc/>
    public void CardDidChange(bool attached)
    {
        try
        {
            logs.OnNext("Card changes detected.");
            var codeId = eidReader.Open;
            if (codeId == -1)
            {
                throw new EIdAdapterNotPresentException();
            }

            if (codeId == -2)
            {
                throw new EIdCardNotPresentException();
            }

            if (codeId == -3)
            {
                throw new EIdCardNotSupportException();
            }

            if (codeId == -4)
            {
                throw new EIdCardReadErrorException();
            }

            if (codeId == -5)
            {
                throw new EIdAdapterNotSupportException();
            }

            events.OnNext(new ZetesEvent()
            {
                CardInfo = eidReader.Parse(),
                State = ZetesState.CardDetected,
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e.GetType().ToString());
            Console.WriteLine(e.StackTrace);
            events.OnNext(new ZetesEvent()
            {
                Exception = e,
                State = ZetesState.Error,
            });
        }
    }

    /// <inheritdoc/>
    public void ReaderDidChange(bool attached)
    {
        logs.OnNext("Card reader is now ready or unavailable");
    }

    /// <inheritdoc/>
    public void DidDetectReader(string reader)
    {
        logs.OnNext("Card reader detected and ready to use");
    }
}