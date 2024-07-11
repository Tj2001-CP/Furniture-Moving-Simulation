using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Light directionalLight;
    public float dayLength = 120f; // Length of a day in seconds
    public Gradient lightColor;
    public AnimationCurve lightIntensity;

    private float timeOfDay;

    void Update()
    {
        timeOfDay += Time.deltaTime / dayLength;
        timeOfDay %= 1f; // Keep timeOfDay between 0 and 1

        UpdateLighting();
    }

    void UpdateLighting()
    {
        // Rotate the directional light
        float angle = timeOfDay * 360f;
        directionalLight.transform.localRotation = Quaternion.Euler(angle, 0, 0);

        // Update light color and intensity
        directionalLight.color = lightColor.Evaluate(timeOfDay);
        directionalLight.intensity = lightIntensity.Evaluate(timeOfDay);
    }

    public void SetTimeOfDay(float value)
    {
        timeOfDay = value;
        UpdateLighting();
    }

}
