using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraShake : MonoBehaviour
{
    CinemachineVirtualCamera vcam;
    CinemachineBasicMultiChannelPerlin noise;
    GameObject player;
    void Start()
    {

        vcam = GameObject.FindGameObjectWithTag("CMCam").GetComponent<CinemachineVirtualCamera>();
        noise = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }



    public IEnumerator StartNoise(float amp, float mag)
    {

        noise.m_AmplitudeGain = amp;
        noise.m_FrequencyGain = mag;
        yield return new WaitForSecondsRealtime(0.25f);
        noise.m_AmplitudeGain = 0;
        noise.m_FrequencyGain = 0;


    }
}
