using System.Reactive.Disposables;
using System.Reactive.Linq;
using Omnicasa.Mobile.Zetes.Standard;

namespace Omnicasa.Mobile.Zetes.iOS;

/// <summary>ZetesService.</summary>
public class ZetesService : AbstractService, IZetesService, IZetesCallback
{
    private Reader eidReader;
    private ZetesReaderDelegate zetesReaderDelegate;

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
                    // Use empty string "" to support both BLE and wired readers (mixed types)
                    // Options: "BLE" (BLE only), "IR301_AND_BR301" (wired only), or "" (both types)
                    // Note: Using "" may have unpredictable behavior - it will connect to BT3 if powered,
                    // otherwise scan for BLE, and after disconnect will scan for both types
                    eidReader = new Reader("FT_ANY", string.Empty, string.Empty);
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
        return ObsEvents;
    }

    /// <inheritdoc/>
    public IObservable<bool> IsSupported()
    {
        return Observable.Return(true);
    }

    /// <inheritdoc/>
    public IObservable<string> Logs()
    {
        return ObsLogs;
    }

    /// <inheritdoc/>
    public void CardDidChange(bool attached)
    {
        try
        {
            ObsLogs.OnNext("Card changes detected.");
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

            ObsEvents.OnNext(new ZetesEvent()
            {
                CardInfo = eidReader.Parse(),
                State = ZetesState.CardDetected,
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e.GetType().ToString());
            Console.WriteLine(e.StackTrace);
            ObsEvents.OnNext(new ZetesEvent()
            {
                Exception = e,
                State = ZetesState.Error,
            });
        }
    }

    /// <inheritdoc/>
    public void ReaderDidChange(bool attached)
    {
        ObsLogs.OnNext("Card reader is now ready or unavailable");
        ObsEvents.OnNext(new ZetesEvent()
        {
            State = ZetesState.CardReaderUnavailable,
        });
    }

    /// <inheritdoc/>
    public void DidDetectReader(string reader)
    {
        ObsLogs.OnNext("Card reader detected and ready to use");
        ObsEvents.OnNext(new ZetesEvent()
        {
            State = ZetesState.CardReaderReady,
        });
    }
}