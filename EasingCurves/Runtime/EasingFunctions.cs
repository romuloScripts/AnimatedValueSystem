using System;
using System.Collections.Generic;
using UnityEngine;

namespace TweenSystem
{
	[Serializable]
	public class EasingCurve {
		public EasingTypes easingtype;
		public AnimationCurve curve = AnimationCurve.Linear(0,0,1,1);
		public float time;
		
		public EasingCurve(){
		}

		public EasingCurve(EasingTypes easing){
			easingtype = easing;
		}
		
		public EasingCurve(AnimationCurve animationCurve){
			easingtype = EasingTypes.custom;
			curve = animationCurve;
		}

		public float Evaluate(float t){
			time=t;
			return EasingFunctions.EasyFunction[easingtype].Invoke(this);
		}
	}

	public enum EasingTypes{
		linear,
		custom,
		easeInSine,
		easeOutSine,
		easeInOutSine,
		easeInQuad,
		easeOutQuad,
		easeInOutQuad,
		easeInCubic,
		easeOutCubic,
		easeInOutCubic,
		easeInQuart,
		easeOutQuart,
		easeInOutQuart,
		easeInQuint,
		easeOutQuint,
		easeInOutQuint,
		easeInExpo,
		easeOutExpo,
		easeInOutExpo,
		easeInCirc,
		easeOutCirc,
		easeInOutCirc,
		easeInBack,
		easeOutBack,
		easeInOutBack,
		easeInElastic,
		easeOutElastic,
		easeInOutElastic,
		easeInBounce,
		easeOutBounce,
		easeInOutBounce,
	}

	public static class EasingFunctions{

		public delegate float EasingFunc(EasingCurve ec);

		public static readonly Dictionary<EasingTypes,EasingFunc> EasyFunction = new Dictionary<EasingTypes,EasingFunc>(){
			{EasingTypes.linear,(o) => Linear(o.time)},
			{EasingTypes.custom,(o) => Custom(o.curve,o.time)},
			{EasingTypes.easeInSine,(o) => InSine(o.time)},
			{EasingTypes.easeOutSine,(o) => OutSine(o.time)},
			{EasingTypes.easeInOutSine,(o) => InOutSine(o.time)},
			{EasingTypes.easeInQuad,(o) => InQuad(o.time)},
			{EasingTypes.easeOutQuad,(o) => OutQuad(o.time)},
			{EasingTypes.easeInOutQuad,(o) => InOutQuad(o.time)},
			{EasingTypes.easeInCubic,(o) => InCubic(o.time)},
			{EasingTypes.easeOutCubic,(o) => OutCubic(o.time)},
			{EasingTypes.easeInOutCubic,(o) => InOutCubic(o.time)},
			{EasingTypes.easeInQuart,(o) => InQuart(o.time)},
			{EasingTypes.easeOutQuart,(o) => OutQuart(o.time)},
			{EasingTypes.easeInOutQuart,(o) => InOutQuart(o.time)},
			{EasingTypes.easeInQuint,(o) => InQuint(o.time)},
			{EasingTypes.easeOutQuint,(o) => OutQuint(o.time)},
			{EasingTypes.easeInOutQuint,(o) => InOutQuint(o.time)},
			{EasingTypes.easeInExpo,(o) => InExpo(o.time)},
			{EasingTypes.easeOutExpo,(o) => OutExpo(o.time)},
			{EasingTypes.easeInOutExpo,(o) => InOutExpo(o.time)},
			{EasingTypes.easeInCirc,(o) => InCirc(o.time)},
			{EasingTypes.easeOutCirc,(o) => OutCirc(o.time)},
			{EasingTypes.easeInOutCirc,(o) => InOutCirc(o.time)},
			{EasingTypes.easeInBack,(o) => InBack(o.time)},
			{EasingTypes.easeOutBack,(o) => OutBack(o.time)},
			{EasingTypes.easeInOutBack,(o) => InOutBack(o.time)},
			{EasingTypes.easeInElastic,(o) => InElastic(o.time)},
			{EasingTypes.easeOutElastic,(o) => OutElastic(o.time)},
			{EasingTypes.easeInOutElastic,(o) => InOutElastic(o.time)},
			{EasingTypes.easeInBounce,(o) => InBounce(o.time)},
			{EasingTypes.easeOutBounce,(o) => OutBounce(o.time)},
			{EasingTypes.easeInOutBounce,(o) => InOutBounce(o.time)},
		};

		private static float Custom(AnimationCurve curve, float t ) {
			return curve.Evaluate(t);
		}
		private static float Linear(float t ) {
			return Mathf.Clamp01(t);
		}
	
		private static float InSine( float t ) {
			return Mathf.Sin( 1.5707963f * t );
		}

		private static float OutSine( float t ) {
			return 1 + Mathf.Sin( 1.5707963f * (--t) );
		}

		private static float InOutSine( float t ) {
			return 0.5f * (1 + Mathf.Sin( 3.1415926f * (t - 0.5f) ) );
		}

		private static float InQuad( float t ) {
			return t * t;
		}

		private static float OutQuad( float t ) { 
			return t * (2 - t);
		}

		private static float InOutQuad( float t ) {
			return t < 0.5 ? 2 * t * t : t * (4 - 2 * t) - 1;
		}

		private static float InCubic( float t ) {
			return t * t * t;
		}

		private static float OutCubic( float t ) {
			return 1 + (--t) * t * t;
		}

		private static float InOutCubic( float t ) {
			return t < 0.5f ? 4*t*t*t : (t-1)*(2*t-2)*(2*t-2)+1;
		}

		private static float InQuart( float t ) {
			t *= t;
			return t * t;
		}

		private static float OutQuart( float t ) {
			t = (--t) * t;
			return 1 - t * t;
		}

		private static float InOutQuart( float t ) {
			if( t < 0.5 ) {
				t *= t;
				return 8 * t * t;
			} else {
				t = (--t) * t;
				return 1 - 8 * t * t;
			}
		}

		private static float InQuint( float t ) {
			float t2 = t * t;
			return t * t2 * t2;
		}

		private static float OutQuint( float t ) {
			float t2 = (--t) * t;
			return 1 + t * t2 * t2;
		}

		private static float InOutQuint( float t ) {
			float t2;
			if( t < 0.5 ) {
				t2 = t * t;
				return 16 * t * t2 * t2;
			} else {
				t2 = (--t) * t;
				return 1 + 16 * t * t2 * t2;
			}
		}

		private static float InExpo( float t ) {
			return (Mathf.Pow( 2, 8 * t ) - 1) / 255;
		}

		private static float OutExpo( float t ) {
			return 1 - Mathf.Pow( 2, -8 * t );
		}

		private static float InOutExpo( float t ) {
			if( t < 0.5 ) {
				return (Mathf.Pow( 2, 16 * t ) - 1) / 510;
			} else {
				return 1 - 0.5f * Mathf.Pow( 2, -16f * (t - 0.5f) );
			}
		}

		private static float InCirc( float t ) {
			return 1 - Mathf.Sqrt( 1 - t );
		}

		private static float OutCirc( float t ) {
			return Mathf.Sqrt( t );
		}

		private static float InOutCirc( float t ) {
			if( t < 0.5f ) {
				return (1 - Mathf.Sqrt( 1 - 2 * t )) * 0.5f;
			} else {
				return (1 + Mathf.Sqrt( 2 * t - 1 )) * 0.5f;
			}
		}

		private static float InBack( float t ) {
			return t * t * (2.70158f * t - 1.70158f);
		}

		private static float OutBack( float t ) {
			return 1 + (--t) * t * (2.70158f * t + 1.70158f);
		}

		private static float InOutBack( float t ) {
			if( t < 0.5f ) {
				return t * t * (7 * t - 2.5f) * 2;
			} else {
				return 1 + (--t) * t * 2 * (7 * t + 2.5f);
			}
		}

		private static float InElastic( float t ) {
		
			float t2 = t * t;
			return t2 * t2 * Mathf.Sin( t * Mathf.PI * 4.5f );
		}

		private static float OutElastic( float t ) {
			float t2 = (t - 1) * (t - 1);
			return 1 - t2 * t2 * Mathf.Cos( t * Mathf.PI * 4.5f );
		}

		private static float InOutElastic( float t ) {
			float t2;
			if( t < 0.45f ) {
				t2 = t * t;
				return 8 * t2 * t2 * Mathf.Sin( t * Mathf.PI * 9 );
			} else if( t < 0.55f ) {
				return 0.5f + 0.75f * Mathf.Sin( t * Mathf.PI * 4 );
			} else {
				t2 = (t - 1) * (t - 1);
				return 1 - 8 * t2 * t2 * Mathf.Sin( t * Mathf.PI * 9 );
			}
		}

		private static float InBounce( float t ) {
			return Mathf.Pow( 2, 6 * (t - 1) ) * Mathf.Abs( Mathf.Sin( t * Mathf.PI * 3.5f ) );
		}

		private static float OutBounce( float t ) {
			return 1 - Mathf.Pow( 2, -6 * t ) * Mathf.Abs( Mathf.Cos( t * Mathf.PI * 3.5f ) );
		}

		private static float InOutBounce( float t ) {
			if( t < 0.5f ) {
				return 8 * Mathf.Pow( 2, 8 * (t - 1) ) * Mathf.Abs( Mathf.Sin( t * Mathf.PI * 7 ) );
			} else {
				return 1 - 8 * Mathf.Pow( 2, -8 * t ) * Mathf.Abs( Mathf.Sin( t * Mathf.PI * 7 ) );
			}
		}
	}
}