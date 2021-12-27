using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace AnimatedValue
{
    [AddComponentMenu("AnimValue/MaterialColorValue")]
    public class MaterialColorValue : AnimValueBase
    {
        [SerializeField] private bool setIniValueOnEnable;
        [SerializeField] private bool setIniValueOnDisable;
        [SerializeField] private bool useMaterialPropertyBlock=true;
        [SerializeField] private float seekValueVelocity = 0;
        [SerializeField] private Renderers[] renderers;
        [FormerlySerializedAs("originalMaterials")] [SerializeField] private SharedMaterial[] sharedMaterials;
        [SerializeField] private GlobalVariables[] globalVariables;

        private float _lastValue;
        private float _target;
        private Coroutine _coroutine;
    
        [Serializable]
        public class MaterialProp {
            [SerializeField] private string prop;
            [SerializeField] private int idMaterial;
            [GradientUsage(true),SerializeField] private Gradient colorCurve;
            
            private MaterialPropertyBlock _propertyBlock;
            
            public void ChangeMaterial(float value, Renderer render, bool useMaterialPropertyBlock){
                var color = colorCurve.Evaluate(value);
                if (!useMaterialPropertyBlock){
                    render.materials[idMaterial].SetColor(prop, color);
                }
                else{
                    _propertyBlock ??= new MaterialPropertyBlock();
                    _propertyBlock.SetColor(prop, color);
                    render.SetPropertyBlock(_propertyBlock,idMaterial);
                }
            }
        }

        [Serializable]
        public class SharedMaterial {
            [SerializeField] private Material material;
            [SerializeField] private string prop;
            [GradientUsage(true)]
            [SerializeField] private Gradient colorCurve;

            public void ChangeMaterial(float value){
                var color = colorCurve.Evaluate(value);
                material.SetColor(prop, color);
            }
        }

        [Serializable]
        public class Renderers{
            [SerializeField] private Renderer render;
            [SerializeField] private MaterialProp[] materials;

            public void ChangeMaterials(float value, bool useMaterialPropertyBlock){
                foreach (var mat in materials){
                    mat.ChangeMaterial(value,render,useMaterialPropertyBlock);
                }
            }
        }
    
        [Serializable]
        public class GlobalVariables
        {
            [SerializeField] private string prop;
            [GradientUsage(true)]
            [SerializeField] private Gradient colorCurve;

            public void ChangeProp(float value){
                var color = colorCurve.Evaluate(value);
                Shader.SetGlobalColor(prop, color);
            }
        }
        
#if UNITY_EDITOR
        [Space(15), Header("Editor"), Space(5)]
        [SerializeField] private bool validate=true;
        [Range(0,1)] [SerializeField] private float value = 0f;

        private void OnValidate () {
            if (!validate || Application.isPlaying) return;
            SetValue(value);
        }
#endif

        private void OnEnable(){
            if(setIniValueOnEnable) SetValue(0);
        }

        private void OnDisable(){
            if(setIniValueOnDisable) SetValue(0);
        }

        public override void SetValue(float progress){
            if (seekValueVelocity <= 0){
                ChangeColor(progress);
            }else if(Application.isPlaying){
                _target = progress;
                _coroutine ??= StartCoroutine(UpdateValue());
            }
        }

        private void ChangeColor(float value){
            ChangeMaterials(value);
            ChangeSharedMaterials(value);
            ChangeGlobal(value);
        }

        private void ChangeMaterials(float value){
            if(!Application.isPlaying && !useMaterialPropertyBlock) return;
            
            foreach (var rend in renderers){
                rend.ChangeMaterials(value,useMaterialPropertyBlock);
            }
        }

        private void ChangeSharedMaterials(float value){
            foreach (var mat in sharedMaterials){
                mat.ChangeMaterial(value);
            }
        }

        private void ChangeGlobal(float value){
            foreach (var prop in globalVariables){
                prop.ChangeProp(value);
            }
        }

        private IEnumerator UpdateValue(){
            while (Math.Abs(_lastValue - _target) > Mathf.Epsilon){
                yield return null;
                _lastValue = Mathf.MoveTowards(_lastValue, _target, seekValueVelocity);
                ChangeColor(_lastValue);
            }
            _coroutine = null;
        }
    }
}
