using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class HUD : SingletonMonoBehaviour<HUD>
{
    [Header("Health Slider")]
    public Slider healthSlider;
    public float healthSliderMaxValue = 50;
    public float healthSliderCurrent;
    int healthSliderSize;

    [Header("Stamina Slider")]
    public Slider staminaSlider;
    public float staminaSliderMaxValue = 100;
    public float staminaSliderCurrent;
    int staminaSliderSize;

    [Header("Mana Slider")]
    public Slider magicSlider;
    public float magicSliderMaxValue = 20;
    public float magicSliderCurrent;
    int magicSliderSize;

    [Header("Stamina Fill")]
    [SerializeField] Image staminaFill;
    [SerializeField] Color staminaFillColorDefault = new (0.3645027f, 0.8584906f, 0.1976148f, 0.6f);
    [SerializeField] Color staminaFillColorCooldown = new(0.2636829f, 0.3584906f, 0.2313279f, 0.6f);
    Color staminaFillColor;

    public Animator playerAnimator;


    private void Start()
    {
        //Set Starting parameter values
        healthSliderCurrent = healthSliderMaxValue;
        staminaSliderCurrent = staminaSliderMaxValue;
        magicSliderCurrent = magicSliderMaxValue;

        staminaFillColor = staminaFillColorDefault;
    }
    private void Update()
    {
        ParametersCalculate();
        ParametersDisplay();
    }

    void ParametersCalculate()
    {
        //Set Boundaries for the healthSliderValue Parameter
        if (healthSliderCurrent >= healthSliderMaxValue)
            healthSliderCurrent = healthSliderMaxValue;
        else if (healthSliderCurrent <= 0)
            healthSliderCurrent = 0;

        //Set Boundaries for the staminaSliderValue Parameter
        if (staminaSliderCurrent >= staminaSliderMaxValue)
            staminaSliderCurrent = staminaSliderMaxValue;
        else if (staminaSliderCurrent <= 0)
            staminaSliderCurrent = 0;

        //Set Boundaries for the magicSliderValue Parameter
        if (magicSliderCurrent >= magicSliderMaxValue)
            magicSliderCurrent = magicSliderMaxValue;
        else if (magicSliderCurrent <= 0)
            magicSliderCurrent = 0;

        //Set Slider Size based on Slider Max Value
        healthSliderSize = (int)healthSliderMaxValue * 4;
        staminaSliderSize = (int)staminaSliderMaxValue * 4;
        magicSliderSize = (int)magicSliderMaxValue * 4;
    }
    void ParametersDisplay()
    {
        //Display parameters to the player, based on their values
        healthSlider.value = healthSliderCurrent / healthSliderMaxValue;
        staminaSlider.value = staminaSliderCurrent / staminaSliderMaxValue;
        magicSlider.value = magicSliderCurrent / magicSliderMaxValue;

        //Display Stamina Fill Color
        staminaFill.color = staminaFillColor;

        //Slider Size and Position

        //Health
        healthSlider.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, healthSliderSize);
        healthSlider.transform.position += new Vector3(30f, 0, 0);

        //Stamina
        staminaSlider.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, staminaSliderSize);
        staminaSlider.transform.position += new Vector3(30f, 0, 0);

        //Stamina
        magicSlider.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, magicSliderSize);
        magicSlider.transform.position += new Vector3(30f, 0, 0);
    }

    //Get Health
    public float GetMaxHealthValue()
    {
        return healthSliderMaxValue;
    }
    public float GetCurrentHealthValue()
    {
        return healthSliderCurrent;
    }
    public void SetHealthValue(float value)
    {
        healthSliderCurrent += value;
    }

    //Get Stamina
    public float GetMaxStaminaValue()
    {
        return staminaSliderMaxValue;
    }
    public float GetCurrentStaminaValue()
    {
        return staminaSliderCurrent;
    }
    public void SetStaminaValue(float value)
    {
        staminaSliderCurrent += value;
    }

    //Get Magic
    public float GetMaxMagicValue()
    {
        return magicSliderMaxValue;
    }
    public float GetCurrentMagicValue()
    {
        return magicSliderCurrent;
    }
    public void SetMagicValue(float value)
    {
        magicSliderCurrent += value;
    }


    public void SetStaminaFillColorDefault()
    {
        staminaFillColor = staminaFillColorDefault;
    }
    public void SetStaminaFillColorCooldown()
    {
        staminaFillColor = staminaFillColorCooldown;
    }
}
