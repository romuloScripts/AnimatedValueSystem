using UnityEngine;
using System.Collections;

public class MaterialFloatValue : ObjectValue {

	[System.Serializable]
	public struct TMaterial {
		public int idMaterial;
		public string prop;
	}

	[System.Serializable]
	public struct TRenderer{
		public Renderer render;
		public TMaterial[] materials;
	}

	public TRenderer[] renderers;

	public override void setValue(float valor) {
		foreach (var rend in renderers) {
			foreach (var mat in rend.materials) {
				rend.render.materials[mat.idMaterial].SetFloat(mat.prop,valor);
			}
		}
	}
}
