using UnityEngine;
using UnityEngine.Serialization;

namespace AnimatedValue
{
	[AddComponentMenu("AnimValue/MaterialFloatValue")]
	public class MaterialFloatValue : AnimValueBase {

		[System.Serializable]
		public class MaterialProp {
			[SerializeField] private int idMaterial;
			[SerializeField] private string prop;
			
			private MaterialPropertyBlock _propertyBlock;

			public void ChangeMaterial(float value, Renderer render){
				_propertyBlock ??= new MaterialPropertyBlock();
				_propertyBlock.SetFloat(prop, value);
				render.SetPropertyBlock(_propertyBlock,idMaterial);
			}
		}

		[System.Serializable]
		public class SharedMaterial {
			[SerializeField] private Material material;
			[SerializeField] private string prop;
			
			public void ChangeMaterial(float value){
				material.SetFloat(prop, value);
			}
		}

		[System.Serializable]
		public class RendererSettings{
			[SerializeField] private Renderer render;
			[SerializeField] private MaterialProp[] materials;

			public void ChangeMaterials(float value){
				foreach (var mat in materials){
					mat.ChangeMaterial(value,render);
				}
			}
		}

		[SerializeField] private bool useIniValue;
		[SerializeField] private float iniValue;
		[SerializeField] private RendererSettings[] renderers;
		[FormerlySerializedAs("originalMaterials")] [SerializeField] private SharedMaterial[] sharedMaterials;
		[SerializeField] private string[] globalVariables;

#if UNITY_EDITOR
		[Space(15), Header("Editor"), Space(5)]
		public bool validate=true;
		[Range(-10,10)] public float _value = 0f;

		private void OnValidate () {
			if (!validate || Application.isPlaying)
				return;
			SetValue(_value);
		}
#endif

		private void Start(){
			if(useIniValue) SetValue(iniValue);
		}

		public override void SetValue(float progress){
			ChangeMaterials(progress);
			ChangeSharedMaterials(progress);
			ChangeGlobal(progress);
		}

		private void ChangeMaterials(float progress){
			foreach (var rend in renderers){
				rend.ChangeMaterials(progress);
			}
		}

		private void ChangeGlobal(float progress){
			foreach (var prop in globalVariables){
				Shader.SetGlobalFloat(prop, progress);
			}
		}

		private void ChangeSharedMaterials(float progress){
			foreach (var mat in sharedMaterials){
				mat.ChangeMaterial(progress);
			}
		}
	}
}
