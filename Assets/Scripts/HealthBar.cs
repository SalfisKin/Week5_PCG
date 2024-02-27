using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider;

    public void SetHeath(int number)
    {
        slider.value = number;
    }


    public void SetMaxHeath(int number)
    {
        slider.maxValue = number;
        slider.value = number;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
