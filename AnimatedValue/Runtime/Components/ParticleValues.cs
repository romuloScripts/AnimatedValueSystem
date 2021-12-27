using System;
using UnityEngine;

namespace AnimatedValue
{
    [AddComponentMenu("AnimValue/ParticleValues")]
    public class ParticleValues : AnimValueBase{
        
        [SerializeField] private ParticleSettings[] particles;
        [Header("Test in playmode")]
        [Range(0,1)]
        [SerializeField] private float testValue = 0;
        
        [Serializable]
        public class ParticleSettings{
            [SerializeField] private ParticleSystem particle;
            
            [SerializeField] private bool changeSpeed;
            [SerializeField] private AnimationCurve speedCurve = AnimationCurve.Linear(0,0,1,1);
            
            [SerializeField] private bool changeEmissionRate;
            [SerializeField] private AnimationCurve emissionRateCurve;
            
            [SerializeField] private bool changeSize;
            [SerializeField] private AnimationCurve sizeCurve;
            
            [SerializeField] private bool changePlay;
            [SerializeField] private AnimationCurve activeCurve;
            
            [SerializeField] private bool changeColor;
            [SerializeField] private Gradient colorCurve;

            public ParticleSystem Particle => particle;
            
            public void SetValue(float value){
                ParticleSystem.MainModule main = particle.main;

                if (!AllowPlay(value)) return;
                ChangeSpeed(value, main);
                ChangeEmission(value);
                ChangeSize(value, main);
                ChangeColor(value, main);
            }

            private void ChangeSpeed(float value, ParticleSystem.MainModule main){
                if (changeSpeed) main.simulationSpeed = speedCurve.Evaluate(value);
            }

            private void ChangeEmission(float value){
                if (changeEmissionRate){
                    ParticleSystem.EmissionModule emissionModule = particle.emission;
                    emissionModule.rateOverTime = emissionRateCurve.Evaluate(value);
                }
            }

            private void ChangeSize(float value, ParticleSystem.MainModule main){
                if (changeSize){
                    main.startSize = sizeCurve.Evaluate(value);
                }
            }

            private void ChangeColor(float value, ParticleSystem.MainModule main){
                if (changeColor){
                    ParticleSystem.MinMaxGradient startColor = particle.main.startColor;
                    startColor.color = colorCurve.Evaluate(value);
                    main.startColor = startColor;
                }
            }

            private bool AllowPlay(float value){
                if (changePlay){
                    bool active = activeCurve.Evaluate(value) > 0;
                    switch (particle.isPlaying){
                        case true when !active:
                            particle.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
                            break;
                        case false when active:
                            particle.Play();
                            break;
                    }
                    if (!active) return false;
                }
                return true;
            }
        }

        private void OnValidate(){
            if(!Application.isPlaying) return;
            SetValue(testValue);
        }

        public override void SetValue(float progress){
            if(!Application.isPlaying) return;
            for (var i = 0; i < particles.Length; i++)
            {
                var particle = particles[i];
                particle.SetValue(progress);
            }
        }
    }
}
