using Android.App;
using Android.Content;

namespace Omnicasa.Mobile.Zetes;

/// <summary>ZetesInitializer.</summary>
public static class ZetesInitializer
{
    /// <summary>Context.</summary>
    public static Context Context { get; set; }

    /// <summary>Activity.</summary>
    public static Activity Activity { get; set; }

    /// <summary>
    /// Init.
    /// </summary>
    /// <param name="context">Context.</param>
    public static void Init(Context context, Activity activity)
    {
        Context = context;
        Activity = activity;
    }
}