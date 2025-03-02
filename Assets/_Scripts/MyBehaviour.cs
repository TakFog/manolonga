using System.Collections;
using UnityEngine;

public class MyBehaviour : MonoBehaviour
{
    private bool _started = false;

    protected bool Started
    {
        get { return _started; }
        set
        {
            if (!value) return;
            if (!_started)
            {
                _started = true;
                Debug.Log("OnEnableAfterStart", gameObject);
                OnEnableAfterStart();
            }
        }
    }

    public void OnEnable()
    {
        Debug.Log("enable "+_started, gameObject);
        if (_started) OnEnableAfterStart();
    }

    public virtual void Start()
    {
        Debug.Log("Start", gameObject);
        Started = true;
    }

    protected virtual void OnEnableAfterStart()
    {

    }
}
