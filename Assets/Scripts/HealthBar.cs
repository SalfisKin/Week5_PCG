using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider;
    TextMeshProUGUI tmp;
    public void SetHealth(int number)
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
        tmp = GetComponent<TextMeshProUGUI>();
        
    }

    // Update is called once per frame
    void Update()
    {
        tmp.text = slider.value.ToString() + "/" + slider.maxValue.ToString();
    }
}
