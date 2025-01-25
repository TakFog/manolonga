using UnityEngine;
using UnityEngine.Rendering.Universal;

public class UpdateLight : MonoBehaviour
{
    public Light2D light2D;
    public float decreaseAmount = 1f;
    public float defaultRadius = 2.5f;

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
            if (light2D.pointLightOuterRadius + points < 0)
            {
                Debug.LogError("Outer Radius cannot be less than 0!");
                return;
            }
            if (light2D.pointLightOuterRadius + points > 3)
            {
                Debug.LogError("Outer Radius cannot be more than 3!");
                return;
            }
            
            light2D.pointLightOuterRadius += points;

            Debug.Log("Updated Outer Radius: " + light2D.pointLightOuterRadius);
        }
        else
        {
            Debug.LogError("Light2D component not found on GameObject!");
        }
    }
}