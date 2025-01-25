using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace _Scripts.States
{
    public class UpdateLight: MonoBehaviour
    {
        public Light2D light2D;

        private void OnEnable()
        {
            StateManager.Instance.OnRoundCompleted += UpdateLight2D;
            Debug.Log("Update light subscribed to OnRoundCompleted event");
        }

        private void OnDisable()
        {
            StateManager.Instance.OnRoundCompleted -= UpdateLight2D;
            Debug.Log("Update light unsubscribed from OnRoundCompleted event");
        }

        private void UpdateLight2D()
        {
            if (light2D != null)
            {
                // Decrease the Outer Radius by 1
                light2D.pointLightOuterRadius -= 1;

                Debug.Log("Updated Outer Radius: " + light2D.pointLightOuterRadius);
            }
            else
            {
                Debug.LogError("Light2D component not found on GameObject!");
            }
        }
    }
}