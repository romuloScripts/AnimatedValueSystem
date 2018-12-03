using UnityEngine;

public class LocalOffsetValueEaseCurve : ObjectValue {

	public float amplitude=1;
	public EasingCurve curve;
	public Vector3 localPos,localPos2;
	public Vector3 posIni;
	public bool ResetPosIni;

	private void Awake()
	{
		if (ResetPosIni)
			SetPosIni();
		transform.localPosition = posIni + localPos;
	}

	[Button("Set Pos Ini")]
	public void SetPosIni()
	{
		posIni = transform.localPosition;
	}

	public void SetPosIni(Vector3 pos)
	{
		posIni = pos;
	}

	public override void setValue (float value)
	{
		transform.localPosition = posIni + Vector3.LerpUnclamped (localPos, localPos2, curve.Evaluate (value))*amplitude;
	}

	public void SetAmplitude(float n) 
	{
		amplitude = n;
	}
}
