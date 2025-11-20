namespace Omnicasa.Mobile.Zetes;

/// <summary>IZetesCallback.</summary>
internal interface IZetesCallback
{
    /// <summary>
    /// CardDidChange.
    /// </summary>
    /// <param name="attached">bool.</param>
    void CardDidChange(bool attached);

    /// <summary>
    /// ReaderDidChange.
    /// </summary>
    /// <param name="attached">bool.</param>
    void ReaderDidChange(bool attached);

    /// <summary>
    /// ReaderDidChange.
    /// </summary>
    /// <param name="reader">string.</param>
    void DidDetectReader(string reader);
}
