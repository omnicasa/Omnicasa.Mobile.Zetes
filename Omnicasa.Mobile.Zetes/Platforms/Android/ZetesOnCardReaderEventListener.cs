using BE.Zetes.Zseidlib;

namespace Omnicasa.Mobile.Zetes;

/// <summary>ZetesOnCardReaderEventListener.</summary>
public class ZetesOnCardReaderEventListener : Java.Lang.Object,
    ZsEidLib.IOnCardEventListener,
    ZsEidLib.IOnCardReaderEventListener
{
    private IDroidZetesCallback callback;

    /// <summary>
    /// Initializes a new instance of the <see cref="ZetesOnCardReaderEventListener"/> class.
    /// </summary>
    /// <param name="callback">IDroidZetesCallback.</param>
    public ZetesOnCardReaderEventListener(IDroidZetesCallback callback)
    {
        this.callback = callback;
    }

    /// <inheritdoc/>
    public void OnCardInserted()
    {
        callback.OnCardInserted();
    }

    /// <inheritdoc/>
    public void OnCardRemoved()
    {
        callback.OnCardRemoved();
    }

    /// <inheritdoc/>
    public void OnReaderConnected(string p0)
    {
        callback.OnReaderConnected(p0);
    }

    /// <inheritdoc/>
    public void OnReaderDisconnected(string p0)
    {
        callback.OnReaderDisconnected(p0);
    }
}
