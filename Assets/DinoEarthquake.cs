using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoEarthquake : MonoBehaviour
{
    [SerializeField]
    private CamShaker _camShaker;

    private void ShakeCam(float amplitude)
    {
        _camShaker.ShakeCamera(amplitude, amplitude / 5);
    }
}
