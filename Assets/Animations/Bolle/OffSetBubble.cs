using UnityEngine;

public class AnimationOffset : MonoBehaviour
{
    public Animator animator;
    public float startOffset; // Set this in the Inspector for each object

    void Start()
    {
        // Set the offset for the animation
        animator.SetFloat("Offset", startOffset);
    }
}