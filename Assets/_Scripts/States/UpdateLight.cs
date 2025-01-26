using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class UpdateLight : MonoBehaviour
{
    Light2D light2D;
    [SerializeField] float decreaseAmount = 0.5f;
    [SerializeField] float minimumRadius = 1.5f;
    float defaultRadius;

    private void Awake()
    {
        light2D = GetComponent<Light2D>();
        defaultRadius = light2D.pointLightOuterRadius;
    }

    private void OnEnable()
    {
        StateManager.Instance.OnRoundCompleted += DecreaseLight;
        Debug.Log("Update light subscribed to OnRoundCompleted event");
    }

    private void OnDisable()
    {
        StateManager.Instance.OnRoundCompleted -= DecreaseLight;
        Debug.Log("Update light unsubscribed from OnRoundCompleted event");
    }

    private void DecreaseLight()
    {
        UpdateLight2D(decreaseAmount * -1);
    }

    public void ResetLight()
    {
        light2D.pointLightOuterRadius = defaultRadius;
    }
    
    private void UpdateLight2D(float points)
    {
        if (light2D != null)
        {
            if (light2D.pointLightOuterRadius + points < minimumRadius)
                return;
            

            light2D.pointLightOuterRadius += points;

            Debug.Log("Updated Outer Radius: " + light2D.pointLightOuterRadius);
        }
    }
}