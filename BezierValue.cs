using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class BezierValue : ObjectValue {
	
	public BezierCurve[] bezier;
	public AnimationCurve curva = AnimationCurve.Linear(0,0,1,1);
	public bool lockAt=true;
	public bool invertBezier;
	

	public override void setValue(float valor) {
		float evaluate = curva.Evaluate(valor);
		float idBezier = evaluate * bezier.Length;
		
		if((int)idBezier <bezier.Length){
			float porBezier = idBezier%1f;
			float front = porBezier+0.1f;
			if(invertBezier){
				front = 1-(porBezier-0.1f);
				porBezier = 1- porBezier;
			}
			
			transform.position = bezier[(int)idBezier].getPos(porBezier);
			if(lockAt){
				Vector3 pos = bezier[(int)idBezier].getPos(front);
				Vector3 relativePos = pos - transform.position;
				Quaternion rotation; 
				if(Application.isPlaying) 
					rotation = Quaternion.Lerp(transform.rotation,Quaternion.LookRotation(relativePos),3*Time.deltaTime);
				else
					rotation = Quaternion.LookRotation(relativePos);
				transform.rotation = rotation;
			}
		}
	}
}
