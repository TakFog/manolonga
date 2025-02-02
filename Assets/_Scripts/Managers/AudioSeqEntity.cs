using System.Collections;
using UnityEngine;

public class AudioSeqEntity : MonoBehaviour
{
    public AudioSource audioSource;
    
    public AudioClip[] audioClipsLow;
    public AudioClip[] audioClipsMid;
    public AudioClip[] audioClipsHigh;

    public float maxDistLow;
    public float maxDistMid;

    private int dist = 2;
    private int index = 0;

    public void Update()
    {
        if (audioSource.isPlaying) return;
        int actDist = GetDistance();
        if (dist == actDist)
            index++;
        else
            index = 0;
        dist = actDist;

        AudioClip[] clips;
        switch(actDist)
        {
            case 0:
                clips = audioClipsHigh;
                break;
            case 1:
                clips = audioClipsMid;
                break;
            default:
                clips = audioClipsLow;
                break;
        }
        index = index % clips.Length;
        Debug.Log("index " + index + " dist " + dist);
        audioSource.PlayOneShot(clips[index]);
    }

    public int GetDistance()
    {
        var distance = Vector3.Distance(Globals.Monster.transform.position, Globals.Child.transform.position);
        Debug.Log(distance);

        if (distance > maxDistLow)
            return 2;
        else if (distance > maxDistMid)
            return 1;
        else
            return 0;
    }

}
