using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using static UnityEngine.VFX.VFXTypeAttribute;

public class SavePanel : MonoBehaviour
{
    public Image backgroundImage;
    public Sprite backgroundSprites1;
    public Sprite backgroundSprites2;
    public Sprite backgroundSprites3;
    public Sprite backgroundSprites4;

    public void Awake()
    {
        if (ChangeBackground.Save == 1)
        {
            backgroundImage.sprite = backgroundSprites1;
        }
        if (ChangeBackground.Save == 2)
        {
            backgroundImage.sprite = backgroundSprites2;
        }
        if (ChangeBackground.Save == 3)
        {
            backgroundImage.sprite = backgroundSprites3;
        }
        if (ChangeBackground.Save == 4)
        {
            backgroundImage.sprite = backgroundSprites4;
        }
    }
}
