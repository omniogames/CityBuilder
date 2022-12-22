using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using DGTween = DG.Tweening.Tween;

namespace Omnio.Tween
{

	[System.Serializable]
	public class TweenDOer : DOer
	{

		public Component Target;
		[InlineEditor]
		public Tween Tween;

		public override DGTween GetTween()
		{
			return Tween.GetTween(Target)
				.SetAs(new TweenParams()
					.SetEase(Tween.Ease)
					.SetLoops(Tween.LoopCount, Tween.LoopType)
					.SetDelay(Tween.Delay)
					.SetUpdate(UpdateType.Normal, Tween.IgnoreUnityTime))
				.OnStart(() => OnStart?.Invoke())
				.OnComplete(() => OnComplete?.Invoke());
		}

	}

}