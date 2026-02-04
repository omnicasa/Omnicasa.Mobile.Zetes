using System.Reactive.Disposables;
using System.Reactive.Linq;
using Android.OS;
using BE.Zetes.Zseidlib;
using BE.Zetes.Zseidlib.Domain;
using BE.Zetes.Zseidlib.Reader;
using Omnicasa.Mobile.Zetes.Standard;

namespace Omnicasa.Mobile.Zetes.Droid;

/// <summary>
/// ZetesService.
/// </summary>
public class ZetesService :
    AbstractService,
    IZetesService,
    IDroidZetesCallback
{
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
                        ObsLogs.OnNext("Please connect BlueTooth Reader (BLE)...");
                    }
                    else if (zsBleIdLib.CardStatus == CardStatus.CardAbsent)
                    {
                        ObsLogs.OnNext("Please insert eID..");
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
        return ObsEvents;
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
        return ObsLogs;
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
        ObsLogs.OnNext("Card inserted");
        try
        {
            ObsLogs.OnNext("Card changes detected.");
            var rc = zsBleIdLib.PowerOnCard();
            if (rc != ReturnCode.Ok)
            {
                throw new Exception(rc?.Name());
            }

            var identity = zsBleIdLib.Identity;
            if (identity == null)
            {
                throw new Exception("NOK");
            }

            ObsEvents.OnNext(new ZetesEvent()
            {
                CardInfo = zsBleIdLib.Parse(),
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
    public void OnCardRemoved()
    {
        ObsLogs.OnNext("Card removed");
        ObsEvents.OnNext(new ZetesEvent()
        {
            State = ZetesState.CardRemoved,
        });
    }

    /// <inheritdoc/>
    public void OnReaderConnected(string p0)
    {
        ObsLogs.OnNext("Card reader connected");
        ObsEvents.OnNext(new ZetesEvent()
        {
            State = ZetesState.CardReaderReady,
        });
    }

    /// <inheritdoc/>
    public void OnReaderDisconnected(string p0)
    {
        ObsLogs.OnNext("Card reader disconnected");
        ObsEvents.OnNext(new ZetesEvent()
        {
            State = ZetesState.CardReaderUnavailable,
        });
    }
}