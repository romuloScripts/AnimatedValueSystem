using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ParticleValues : MonoBehaviour {

    public ParticleSystem[] particle;

    public void SetSimulationSpeed(float value) {
        for (int i = 0; i < particle.Length; i++) {
            ParticleSystem.MainModule main = particle[i].main;
            main.simulationSpeed = value;
        }
    }

    public void SetEmissionRate(float value) {
        for (int i = 0; i < particle.Length; i++) {
            ParticleSystem.EmissionModule emissionModule = particle[i].emission;
            emissionModule.rateOverTime= value;
        }
    }

    public void SetSize(float value) {
        for (int i = 0; i < particle.Length; i++) {
            ParticleSystem.MainModule main = particle[i].main;
            main.startSize = value;
        }
    }

    public void SetColor(Color color) {
        for (int i = 0; i < particle.Length; i++) {
            ParticleSystem.MinMaxGradient startColor = particle[i].main.startColor;
            startColor.color = color;
            ParticleSystem.MainModule main = particle[i].main;
            main.startColor = startColor;
        }
    }

    //public void SetColorAlpha(float value) {
    //    for (int i = 0; i < particle.Length; i++) {
    //        ParticleSystem.MinMaxGradient startColor = particle[i].main.startColor;
    //        Color color = startColor.color;
    //        color.a = value;
    //        startColor.color = color;
    //        ParticleSystem.MainModule main = particle[i].main;
    //        main.startColor = startColor;

    //        Debug.Log(particle[i].main.startColor.color.a);
    //    }
    //}
}
