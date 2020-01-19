using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class StaminaManager : MonoBehaviour
{
    movementMouse mouse;

    [SerializeField]
    public Slider StaminaSlider;

    public const float maxStamina = 100;

    public float staminaCostOnRun;
    public float currentStamina = maxStamina;

    float howManySugar = 1;

    void Start()
    {
        StaminaSlider = GameObject.Find("Stamina").GetComponentInChildren<Slider>();
        StaminaSlider.onValueChanged.AddListener(v => currentStamina = v);
        mouse = GetComponent<movementMouse>();
        //StaminaSlider = FindObjectsOfType<Slider>().FirstOrDefault(s => s.name == "Stamina").gameObject;
        //mouse = GetComponent<movementMouse>();
        //StaminaSlider.GetComponent<Slider>().onValueChanged.AddListener(v => currentStamina = v);
        //currentStamina = 100;
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

    public void UpdateStamina(float amount)
    {
        howManySugar *= 0.9f;
        currentStamina += amount * howManySugar;
        StaminaSlider.value = currentStamina;
        Debug.Log(amount);
    }
}
