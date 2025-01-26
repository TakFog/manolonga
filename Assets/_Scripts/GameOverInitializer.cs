using UnityEngine;
using UnityEngine.UI;

public class Choose : MonoBehaviour
{
    public Image splashScreenImage;
    public Sprite childWinsSprite;
    public Sprite monsterWinsSprite;

    private void Awake()
    {
        if (splashScreenImage == null)
        {
            Debug.LogError("No splash screen image set in parameters!");
            return;
        }

        splashScreenImage.overrideSprite = Globals.WinnerType == PlayerType.Child ? childWinsSprite : monsterWinsSprite;
    }
}