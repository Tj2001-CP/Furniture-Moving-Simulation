using UnityEngine;
using UnityEngine.UI;

public class LightingUI : MonoBehaviour
{
    public Slider timeOfDaySlider;
    public DayNightCycle dayNightCycle;

    void Start()
    {
        timeOfDaySlider.onValueChanged.AddListener(UpdateTimeOfDay);
    }

    void UpdateTimeOfDay(float value)
    {
        dayNightCycle.SetTimeOfDay(value);
    }


}
