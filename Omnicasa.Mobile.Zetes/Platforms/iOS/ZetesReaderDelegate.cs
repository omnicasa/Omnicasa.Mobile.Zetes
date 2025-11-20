using Omnicasa.Mobile.Zetes.iOS;

namespace Omnicasa.Mobile.Zetes;

/// <summary>ZetesReaderDelegate.</summary>
internal class ZetesReaderDelegate : ReaderDelegate
{
    private IZetesCallback callback;

    /// <summary>
    /// Initializes a new instance of the <see cref="ZetesReaderDelegate"/> class.
    /// </summary>
    /// <param name="callback">IZetesCallback.</param>
    public ZetesReaderDelegate(IZetesCallback callback)
    {
        this.callback = callback;
    }

    /// <inheritdoc/>
    public override void ReaderDidChange(bool attached)
    {
        callback.ReaderDidChange(attached);
    }

    /// <inheritdoc/>
    public override void CardDidChange(bool attached)
    {
        callback.CardDidChange(attached);
    }

    /// <inheritdoc/>
    public override void DidDetectReader(string reader)
    {
        callback.DidDetectReader(reader);
    }
}
