using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EarthquakeWaver : MonoBehaviour
{
    private CinemachineVirtualCamera _cmVC;
    private Rigidbody _rb;

    [SerializeField]
    private float _strength = 0.5f;
    [SerializeField]
    private float _frequency = 2f;
    [SerializeField]
    private bool _rotated;

    [SerializeField]
    private bool _dinoMode;


    // Start is called before the first frame update
    void Start()
    {
        _cmVC = FindObjectOfType<CinemachineVirtualCamera>();
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CinemachineBasicMultiChannelPerlin cmBMCP = _cmVC.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        if (_dinoMode)
        {
            if (!_rotated)
                _rb.AddTorque((cmBMCP.m_AmplitudeGain * transform.forward * Mathf.Sin(Time.time * _frequency) + cmBMCP.m_AmplitudeGain * transform.right * Mathf.Cos(Time.time * _frequency)) * _strength);
            else
                _rb.AddTorque((cmBMCP.m_AmplitudeGain * transform.up * Mathf.Sin(Time.time * _frequency) + cmBMCP.m_AmplitudeGain * transform.right * Mathf.Cos(Time.time * _frequency)) * _strength);
            return;
        }
        if (!_rotated)
            _rb.AddTorque((cmBMCP.m_FrequencyGain * transform.forward * Mathf.Sin(Time.time * _frequency) + cmBMCP.m_FrequencyGain * transform.right * Mathf.Cos(Time.time * _frequency)) * _strength);
        else
            _rb.AddTorque((cmBMCP.m_FrequencyGain * transform.up * Mathf.Sin(Time.time * _frequency) + cmBMCP.m_FrequencyGain * transform.right * Mathf.Cos(Time.time * _frequency)) * _strength);
    }
}
