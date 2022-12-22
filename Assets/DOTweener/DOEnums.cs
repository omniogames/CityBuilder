
using System;

namespace Omnio.Tween
{

    public enum DOType
    {
        Default,
        Punch,
        Shake
    }

    public enum PlayOptions
    {
        DontPlay,
        PlayOnEnabled,
        PlayOnAwake,
        PlayOnStart
    }

    public enum KillOptions
    {
        Default,
        KillDontComplete,
        KillComplete,
        CompleteWithoutCallbacks,
        CompleteWithCallbacks
    }

    public enum TweenType
    {
        Append,
        Join,
        Insert,
        AppendCallback,
        InsertCallback,
        AppendInverval,
        PrependInterval
    }

}
