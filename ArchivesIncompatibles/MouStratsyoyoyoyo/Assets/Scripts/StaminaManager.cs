using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class StaminaManager : MonoBehaviour
{
    public movementMouse mouse;

    [SerializeField]
    public Slider StaminaSlider;

    public const float maxStamina = 100;

    public float staminaCostOnRun;
    public float currentStamina = maxStamina;

    float howManySugar = 1;

    void Start()
    {

        StaminaSlider.onValueChanged.AddListener(v => currentStamina = v);

    }
    private void Awake()
    {

    }

    void Update()
    {       
        if (mouse.isRunning)
        {
            currentStamina -= Time.deltaTime * staminaCostOnRun;
            StaminaSlider.value = currentStamina;
        }
    }
    public void UpdateStaminaOnAction(float amount)
    {

        currentStamina -= amount;
        StaminaSlider.value = currentStamina;
    }
    public void UpdateStaminaOnTakingItem(float amount)
    {
        howManySugar *= 0.9f;
        currentStamina += amount * howManySugar;
        StaminaSlider.value = currentStamina;
        Debug.Log(amount);
    }
}
