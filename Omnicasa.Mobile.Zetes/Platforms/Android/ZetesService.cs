using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Android.OS;
using BE.Zetes.Zseidlib;
using BE.Zetes.Zseidlib.Reader;

namespace Omnicasa.Mobile.Zetes.Droid;

/// <summary>
/// ZetesService.
/// </summary>
public class ZetesService :
    IZetesService,
    IDroidZetesCallback
{
    private BehaviorSubject<string> logs = new BehaviorSubject<string>("Initializing..");
    private BehaviorSubject<ZetesEvent> events = new BehaviorSubject<ZetesEvent>(null);
    private ZsEidLib zsBleIdLib;
    private bool isSupportDevice;

    /// <inheritdoc/>
    public IObservable<ZetesEvent> StartScan()
    {
        return Observable.Create<ZetesEvent>(o =>
        {
            try
            {
                isSupportDevice = GetSupportCPU();
                zsBleIdLib = new ZsEidLib(ZetesInitializer.Activity);
                if (isSupportDevice)
                {
                    zsBleIdLib.InitSDK();
                    zsBleIdLib.RegisterCardEventListener(new ZetesOnCardReaderEventListener(this));
                    zsBleIdLib.RegisterReaderEventListener(new ZetesOnCardReaderEventListener(this));

                    if (!zsBleIdLib.IsValidReader)
                    {
                        logs.OnNext("Please connect BlueTooth Reader (BLE)...");
                    }
                    else if (zsBleIdLib.CardStatus == CardStatus.CardAbsent)
                    {
                        logs.OnNext("Please insert eID..");
                    }

                    o.OnNext(new ZetesEvent()
                    {
                        State = ZetesState.Starting,
                    });
                }
                else
                {
                    o.OnNext(new ZetesEvent()
                    {
                        State = ZetesState.Stopped,
                        Exception = new EIdAdapterNotSupportException(),
                    });
                }
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
                if (isSupportDevice)
                {
                    zsBleIdLib.UnregisterAllEventListeners();
                    zsBleIdLib?.CloseSDK();
                    zsBleIdLib?.Dispose();
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
        return Observable.Create<bool>(o =>
        {
            try
            {
                o.OnNext(GetSupportCPU());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            o.OnCompleted();

            return Disposable.Empty;
        }).Catch((Exception ex) => Observable.Return(false));
    }

    /// <inheritdoc/>
    public IObservable<string> Logs()
    {
        return logs;
    }

    /// <inheritdoc/>
    public void Dispose()
    {
    }

    private bool GetSupportCPU()
    {
        var cpuSupported = Build.SupportedAbis;
        var supportedCPU = cpuSupported?.FirstOrDefault();
        if (supportedCPU?.Contains("arm64-v8a") == true || supportedCPU?.Contains("armeabi-v7a") == true)
        {
            isSupportDevice = true;
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <inheritdoc/>
    public void OnCardInserted()
    {
        logs.OnNext("Card inserted");
    }

    /// <inheritdoc/>
    public void OnCardRemoved()
    {
        logs.OnNext("Card removed");
    }

    /// <inheritdoc/>
    public void OnReaderConnected(string p0)
    {
        logs.OnNext("Card reader connected");
    }

    /// <inheritdoc/>
    public void OnReaderDisconnected(string p0)
    {
        logs.OnNext("Card reader disconnected");
    }
}