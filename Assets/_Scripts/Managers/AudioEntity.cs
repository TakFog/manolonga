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

    private int index1 = 0;
    private int index2 = 0;
    private int index3 = 0;
    
    void OnEnable()
    {
        if (audioClipsLow.Length == 1)
        {
            PlayLoop(audioSourceLow, audioClipsLow[0]);
            PlayLoop(audioSourceMid, audioClipsMid[0]);
            PlayLoop(audioSourceHigh, audioClipsHigh[0]);
        }
        StateManager.Instance.OnRoundCompleted += UpdateDistance;
    }

    private void PlayLoop(AudioSource audioSource, AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.Play();
    }

    void OnDisable()
    {
        StateManager.Instance.OnRoundCompleted -= UpdateDistance;
    }

    public void Update()
    {
        if (audioClipsLow.Length > 1 && !audioSourceLow.isPlaying)
        {
            audioSourceLow.PlayOneShot(audioClipsLow[index1]);
            index1 = (index1 + 1) % audioClipsLow.Length;
        }
        if (audioClipsMid.Length > 1 && !audioSourceMid.isPlaying)
        {
            audioSourceMid.PlayOneShot(audioClipsMid[index2]);
            index2 = (index2 + 1) % audioClipsMid.Length;
        }
        if (audioClipsHigh.Length > 1 && !audioSourceHigh.isPlaying)
        {
            audioSourceHigh.PlayOneShot(audioClipsHigh[index3]);
            index3 = (index3 + 1) % audioClipsLow.Length;
        }
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
