using DG.Tweening;
using System.Collections;
using UnityEngine;

public class AudioSeqEntity : MonoBehaviour
{
    public AudioSource audioSource;

    public float fadeTime;
    
    public AudioClip[] audioClipsLow;
    public AudioClip[] audioClipsMid;
    public AudioClip[] audioClipsHigh;

    public float maxDistLow;
    public float maxDistMid;

    private int dist = 2;
    private int index = 0;

    void OnEnable()
    {
        StateManager.Instance.OnRoundCompleted += UpdateDistance;
    }

    void OnDisable()
    {
        StateManager.Instance.OnRoundCompleted -= UpdateDistance;
    }

    public void Update()
    {
        if (audioSource.isPlaying) return;
        ChangeClip();
    }

    private void ChangeClip() 
    { 
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
        audioSource.clip = clips[index];
        audioSource.Play();
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

    [ContextMenu("Update")]
    public void UpdateDistance()
    {
        int actDist = GetDistance();
        if (dist != actDist)
            StartCoroutine(C_ChangeClip());
    }

    IEnumerator C_ChangeClip()
    {
        yield return audioSource.DOFade(0, fadeTime).WaitForCompletion();
        audioSource.volume = 1f;
        ChangeClip();
    }
}
