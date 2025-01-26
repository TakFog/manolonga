using System.Collections;
using UnityEngine;

public class AudioEntity : MonoBehaviour
{
    public AudioSource audioSourceLow;
    public AudioSource audioSourceMid;
    public AudioSource audioSourceHigh;
    
    public AudioClip[] audioClipsLow;
    public AudioClip[] audioClipsMid;
    public AudioClip[] audioClipsHigh;

    public float maxDistLow;
    public float distLowToMid;
    public float maxDistMid;
    public float distMidToHigh;

    void OnEnable()
    {
        if (audioClipsLow.Length > 0)
            StartCoroutine(C_PlayAllSequence());
        StateManager.Instance.OnRoundCompleted += UpdateDistance;
    }

    void OnDisable()
    {
        StateManager.Instance.OnRoundCompleted -= UpdateDistance;
    }

    IEnumerator C_PlayAllSequence()
    {
        for (int i = 0; i < audioClipsLow.Length; i++)
        {
            audioSourceLow.PlayOneShot(audioClipsLow[i]);
            audioSourceMid.PlayOneShot(audioClipsMid[i]);
            audioSourceHigh.PlayOneShot(audioClipsHigh[i]);
            yield return new WaitForSeconds(audioClipsLow[i].length);
        }
        StartCoroutine(C_PlayAllSequence());
    }

    public void UpdateDistance()
    {
        var distance = Vector3.Distance(Globals.Monster.transform.position, Globals.Child.transform.position);
        Debug.Log(distance);
        
        if (distance > maxDistLow)
        {
            audioSourceLow.volume = 1f;
            audioSourceMid.volume = 0f;
            audioSourceHigh.volume = 0f;
        } 
        else if (distance > distLowToMid)
        {
            var frac = FractionBetween(distance, distLowToMid, maxDistLow);
            audioSourceLow.volume = frac;
            audioSourceMid.volume = 1 - frac;
            audioSourceHigh.volume = 0f;
        }
        else if (distance > maxDistMid)
        {
            audioSourceLow.volume = 0f;
            audioSourceMid.volume = 1f;
            audioSourceHigh.volume = 0f;
        } 
        else if (distance > distMidToHigh)
        {
            var frac = FractionBetween(distance, distMidToHigh, maxDistMid);
            audioSourceLow.volume = 0f;
            audioSourceMid.volume = frac;
            audioSourceHigh.volume = 1 - frac;
        }
        else
        {
            audioSourceLow.volume = 0f;
            audioSourceMid.volume = 0f;
            audioSourceHigh.volume = 1f;
        } 

    }

    private float FractionBetween(float value, float min, float max)
    {
        return (value - min) / (max - min);
    }

}
