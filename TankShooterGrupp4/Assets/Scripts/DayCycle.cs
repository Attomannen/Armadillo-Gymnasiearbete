using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Experimental.Rendering.Universal;


public class DayCycle : MonoBehaviour
{

    //gryning vid x 175 och 355
    //natt vid 190 till 350
    [SerializeField] float dayTimeCycle;
    [SerializeField] float nightTimeCycle;
    [SerializeField] GameObject globalLight;
    [SerializeField] GameObject spotLight;
    [SerializeField] bool dayCycleIsActive;
    [SerializeField] float cycleSpeed;
    [SerializeField] float currentRotationAmount;
    private float lerp;
    [SerializeField] float materialChangingSpeed;



    [Header("Multi Parameters ")]
    private bool isDay = false;
    private bool isNight = false;
    private bool isDawnDusk = false;

    [SerializeField] Color dayTimeColor;
    [SerializeField] Color nightTimeColor;
    [SerializeField] Color midDayColor;
    [SerializeField] Color midNightColor;

    [SerializeField] Material dayMaterial;
    [SerializeField] Material nightMaterial;
    [SerializeField] Material dawnDuskMaterial;


    private float startingRotationAmount;


    void Start()
    {
        startingRotationAmount = globalLight.transform.rotation.x;
    }

    void Update()
    {
        if (dayCycleIsActive)
        {
            DayCycleHolder();
            TurnGlobalLight();
            TurnSpotLight();
        }
        currentRotationAmount = globalLight.transform.rotation.eulerAngles.x;
    }

    void TurnGlobalLight()
    {
        globalLight.transform.Rotate(Time.deltaTime * cycleSpeed, 0, 0);

    }
    void TurnSpotLight()
    {
        spotLight.transform.Rotate(Time.deltaTime * cycleSpeed, 0, 0);
    }


    void DayCycleHolder()
    {
        if (currentRotationAmount >= 0 && currentRotationAmount <= 135)
        {
            isDay = true;
            isDawnDusk = false;
            isNight = false;
        }
        if ((currentRotationAmount > 135 && currentRotationAmount <= 180)||(currentRotationAmount > 315 && currentRotationAmount < 360))
        {
            isDay = false;
            isDawnDusk = true;
            isNight = false;
        }


        if (currentRotationAmount >= 180 && currentRotationAmount <= 315)
        {
            isDay = false;
            isDawnDusk = false;
            isNight = true;
        }

        if (isDay)
        {
            //RenderSettings.fog = true;
            RenderSettings.fogColor = dayTimeColor;
            RenderSettings.skybox = dayMaterial;
        }
        if (isNight)
        {
            //RenderSettings.fog = true;
            RenderSettings.fogColor = nightTimeColor;
            RenderSettings.skybox = nightMaterial;
        }
        if (isDawnDusk)
        {
            RenderSettings.fog = false;
            RenderSettings.skybox = dawnDuskMaterial;
        }
    }

}
