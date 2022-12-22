using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using DGTween = DG.Tweening.Tween;

namespace Omnio.Tween
{

    public abstract class Tween : ScriptableObject
    {

        [FoldoutGroup("General")]
        public float Duration;
        [FoldoutGroup("General")]
        public float Delay;
        [FoldoutGroup("General")]
        public Ease Ease;
        [FoldoutGroup("General")]
        public int LoopCount;
        [FoldoutGroup("General"), ShowIf("@this.LoopCount != 0")]
        public LoopType LoopType;

        [FoldoutGroup("General")]
        public bool IgnoreUnityTime;
        public abstract DGTween GetTween(Component component);

    }

}