using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using DGTween = DG.Tweening.Tween;

namespace Omnio.Tween
{

	[System.Serializable]
	public class SequenceDOer : DOer
	{

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
		public List<SequenceTweenData> Tweens;

		public override DGTween GetTween()
		{
			Sequence sequence = DOTween.Sequence();
			foreach (SequenceTweenData tweenData in Tweens)
			{
				switch (tweenData.TweenType)
				{
					case TweenType.Append:
						sequence.Append(tweenData.Tween.GetTween(tweenData.Target));
						break;
					case TweenType.Insert:
						sequence.Insert(tweenData.InsertTime, tweenData.Tween.GetTween(tweenData.Target));
						break;
					case TweenType.Join:
						sequence.Join(tweenData.Tween.GetTween(tweenData.Target));
						break;
					case TweenType.AppendCallback:
						sequence.AppendCallback(() => tweenData.Callback?.Invoke());
						break;
					case TweenType.InsertCallback:
						sequence.InsertCallback(tweenData.InsertTime, () => tweenData.Callback?.Invoke());
						break;
					case TweenType.AppendInverval:
						sequence.AppendInterval(tweenData.IntervalTime);
						break;
					case TweenType.PrependInterval:
						sequence.PrependInterval(tweenData.IntervalTime);
						break;
				}
			}
			return sequence.SetAs(new TweenParams()
					.SetEase(Ease)
					.SetLoops(LoopCount, LoopType)
					.SetDelay(Delay)
					.SetUpdate(UpdateType.Normal, IgnoreUnityTime))
				.OnStart(() => OnStart?.Invoke())
				.OnComplete(() => OnComplete?.Invoke());
		}

	}

	[System.Serializable]
	public class SequenceTweenData
	{

		[HideLabel, EnumToggleButtons]
		public TweenType TweenType;
		[ShowIf("IsTweenOperation")]
		public Component Target;
		[ShowIf("IsTweenOperation"), InlineEditor]
		public Tween Tween;
		[ShowIf("IsInsert")]
		public float InsertTime;
		[ShowIf("IsInterval")]
		public float IntervalTime;
		[ShowIf("IsCallback")]
		public UnityEvent Callback;

#if UNITY_EDITOR
		private bool IsTweenOperation => TweenType == TweenType.Append || TweenType == TweenType.Insert || TweenType == TweenType.Join;
		private bool IsInsert => TweenType == TweenType.Insert || TweenType == TweenType.InsertCallback;
		private bool IsCallback => TweenType == TweenType.AppendCallback || TweenType == TweenType.InsertCallback;
		private bool IsInterval => TweenType == TweenType.PrependInterval || TweenType == TweenType.AppendInverval;
#endif
	}

}
