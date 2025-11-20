namespace Omnicasa.Mobile.Zetes;

/// <summary>IDroidZetesCallback.</summary>
public interface IDroidZetesCallback
{
    /// <summary>OnCardInserted.</summary>
    void OnCardInserted();

    /// <summary>OnCardRemoved.</summary>
    void OnCardRemoved();

    /// <summary>OnReaderConnected.</summary>
    void OnReaderConnected(string p0);

    /// <summary>OnReaderDisconnected.</summary>
    void OnReaderDisconnected(string p0);
}