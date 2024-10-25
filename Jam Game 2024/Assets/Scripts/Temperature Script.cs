using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TemperatureScript : MonoBehaviour
{
    public Slider slider;

    public void setTempMax(int temp)
    {
        slider.maxValue = temp;
        slider.value = temp;
    }
    public void SetTemp(int temp)
    {
        slider.value = temp;
    }

    
}
