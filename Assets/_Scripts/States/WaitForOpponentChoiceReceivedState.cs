using UnityEngine;
using UnityEngine.Rendering.Universal;

public class WaitForOpponentChoiceReceivedState : State
{
    public override void Enter()
    {
        base.Enter();
        CommunicationManager.Instance.OnMovesReceived += MovesReceived;
    }
    void MovesReceived(CommunicationData communicationData)
    {
        GameObject lightObject = GameObject.Find("Light 2D");

        if (lightObject != null)
        {
            // Get the Light2D component
            Light2D light2D = lightObject.GetComponent<Light2D>();

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
        else
        {
            Debug.LogError("GameObject 'Light 2D' not found!");
        }
        StateManager.Instance.ChangeState(new WaitForExecutionState());
    }
    public override void Exit()
    {
        base.Exit();
        CommunicationManager.Instance.OnMovesReceived -= MovesReceived;
    }
}