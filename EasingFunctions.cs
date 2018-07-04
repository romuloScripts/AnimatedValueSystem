using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class EasingCurve {
	public easingTypes easingtype = easingTypes.easeInSine;
	public AnimationCurve curve = AnimationCurve.Linear(0,0,1,1);

	public float t;

	public float Evaluate(float t){
		this.t=t;
		return EasingFunctions.easyFunction[easingtype].Invoke(this);
	}
}

public enum easingTypes{
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

	public delegate float easingFunc(EasingCurve ec);

	public static Dictionary<easingTypes,easingFunc> easyFunction = new Dictionary<easingTypes,easingFunc>(){
		{easingTypes.custom,(o) => custom(o.curve,o.t)},
		{easingTypes.easeInSine,(o) => easeInSine(o.t)},
		{easingTypes.easeOutSine,(o) => easeOutSine(o.t)},
		{easingTypes.easeInOutSine,(o) => easeInOutSine(o.t)},
		{easingTypes.easeInQuad,(o) => easeInQuad(o.t)},
		{easingTypes.easeOutQuad,(o) => easeOutQuad(o.t)},
		{easingTypes.easeInOutQuad,(o) => easeInOutQuad(o.t)},
		{easingTypes.easeInCubic,(o) => easeInCubic(o.t)},
		{easingTypes.easeOutCubic,(o) => easeOutCubic(o.t)},
		{easingTypes.easeInOutCubic,(o) => easeInOutCubic(o.t)},
		{easingTypes.easeInQuart,(o) => easeInQuart(o.t)},
		{easingTypes.easeOutQuart,(o) => easeOutQuart(o.t)},
		{easingTypes.easeInOutQuart,(o) => easeInOutQuart(o.t)},
		{easingTypes.easeInQuint,(o) => easeInQuint(o.t)},
		{easingTypes.easeOutQuint,(o) => easeOutQuint(o.t)},
		{easingTypes.easeInOutQuint,(o) => easeInOutQuint(o.t)},
		{easingTypes.easeInExpo,(o) => easeInExpo(o.t)},
		{easingTypes.easeOutExpo,(o) => easeOutExpo(o.t)},
		{easingTypes.easeInOutExpo,(o) => easeInOutExpo(o.t)},
		{easingTypes.easeInCirc,(o) => easeInCirc(o.t)},
		{easingTypes.easeOutCirc,(o) => easeOutCirc(o.t)},
		{easingTypes.easeInOutCirc,(o) => easeInOutCirc(o.t)},
		{easingTypes.easeInBack,(o) => easeInBack(o.t)},
		{easingTypes.easeOutBack,(o) => easeOutBack(o.t)},
		{easingTypes.easeInOutBack,(o) => easeInOutBack(o.t)},
		{easingTypes.easeInElastic,(o) => easeInElastic(o.t)},
		{easingTypes.easeOutElastic,(o) => easeOutElastic(o.t)},
		{easingTypes.easeInOutElastic,(o) => easeInOutElastic(o.t)},
		{easingTypes.easeInBounce,(o) => easeInBounce(o.t)},
		{easingTypes.easeOutBounce,(o) => easeOutBounce(o.t)},
		{easingTypes.easeInOutBounce,(o) => easeInOutBounce(o.t)},
	};

	static float custom(AnimationCurve curve, float t ) {
		return curve.Evaluate(t);
	}

	static float easeInSine( float t ) {
		return Mathf.Sin( 1.5707963f * t );
	}

	static float easeOutSine( float t ) {
		return 1 + Mathf.Sin( 1.5707963f * (--t) );
	}

	static float easeInOutSine( float t ) {
		return 0.5f * (1 + Mathf.Sin( 3.1415926f * (t - 0.5f) ) );
	}

	static float easeInQuad( float t ) {
		return t * t;
	}

	static float easeOutQuad( float t ) { 
		return t * (2 - t);
	}

	static float easeInOutQuad( float t ) {
		return t < 0.5 ? 2 * t * t : t * (4 - 2 * t) - 1;
	}

	static float easeInCubic( float t ) {
		return t * t * t;
	}

	static float easeOutCubic( float t ) {
		return 1 + (--t) * t * t;
	}

	static float easeInOutCubic( float t ) {
		return t < 0.5 ? 4 * t * t * t : 1 + (--t) * (2 * (--t)) * (2 * t);
	}

	static float easeInQuart( float t ) {
		t *= t;
		return t * t;
	}

	static float easeOutQuart( float t ) {
		t = (--t) * t;
		return 1 - t * t;
	}

	static float easeInOutQuart( float t ) {
		if( t < 0.5 ) {
			t *= t;
			return 8 * t * t;
		} else {
			t = (--t) * t;
			return 1 - 8 * t * t;
		}
	}

	static float easeInQuint( float t ) {
		float t2 = t * t;
		return t * t2 * t2;
	}

	static float easeOutQuint( float t ) {
		float t2 = (--t) * t;
		return 1 + t * t2 * t2;
	}

	static float easeInOutQuint( float t ) {
		float t2;
		if( t < 0.5 ) {
			t2 = t * t;
			return 16 * t * t2 * t2;
		} else {
			t2 = (--t) * t;
			return 1 + 16 * t * t2 * t2;
		}
	}

	static float easeInExpo( float t ) {
		return (Mathf.Pow( 2, 8 * t ) - 1) / 255;
	}

	static float easeOutExpo( float t ) {
		return 1 - Mathf.Pow( 2, -8 * t );
	}

	static float easeInOutExpo( float t ) {
		if( t < 0.5 ) {
			return (Mathf.Pow( 2, 16 * t ) - 1) / 510;
		} else {
			return 1 - 0.5f * Mathf.Pow( 2, -16f * (t - 0.5f) );
		}
	}

	static float easeInCirc( float t ) {
		return 1 - Mathf.Sqrt( 1 - t );
	}

	static float easeOutCirc( float t ) {
		return Mathf.Sqrt( t );
	}

	static float easeInOutCirc( float t ) {
		if( t < 0.5f ) {
			return (1 - Mathf.Sqrt( 1 - 2 * t )) * 0.5f;
		} else {
			return (1 + Mathf.Sqrt( 2 * t - 1 )) * 0.5f;
		}
	}

	static float easeInBack( float t ) {
		return t * t * (2.70158f * t - 1.70158f);
	}

	static float easeOutBack( float t ) {
		return 1 + (--t) * t * (2.70158f * t + 1.70158f);
	}

	static float easeInOutBack( float t ) {
		if( t < 0.5f ) {
			return t * t * (7 * t - 2.5f) * 2;
		} else {
			return 1 + (--t) * t * 2 * (7 * t + 2.5f);
		}
	}

	static float easeInElastic( float t ) {
		
		float t2 = t * t;
		return t2 * t2 * Mathf.Sin( t * Mathf.PI * 4.5f );
	}

	static float easeOutElastic( float t ) {
		float t2 = (t - 1) * (t - 1);
		return 1 - t2 * t2 * Mathf.Cos( t * Mathf.PI * 4.5f );
	}

	static float easeInOutElastic( float t ) {
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

	static float easeInBounce( float t ) {
		return Mathf.Pow( 2, 6 * (t - 1) ) * Mathf.Abs( Mathf.Sin( t * Mathf.PI * 3.5f ) );
	}

	static float easeOutBounce( float t ) {
		return 1 - Mathf.Pow( 2, -6 * t ) * Mathf.Abs( Mathf.Cos( t * Mathf.PI * 3.5f ) );
	}

	static float easeInOutBounce( float t ) {
		if( t < 0.5f ) {
			return 8 * Mathf.Pow( 2, 8 * (t - 1) ) * Mathf.Abs( Mathf.Sin( t * Mathf.PI * 7 ) );
		} else {
			return 1 - 8 * Mathf.Pow( 2, -8 * t ) * Mathf.Abs( Mathf.Sin( t * Mathf.PI * 7 ) );
		}
	}
}
