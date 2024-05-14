using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeBackground : MonoBehaviour
{
    
    public Image backgroundImage;
    public Sprite backgroundSprites1;
    public Sprite backgroundSprites2;
    public Sprite backgroundSprites3;
    public Sprite backgroundSprites4;
   
    [Tooltip("Save Data PlayerPrefs Key")]
    [SerializeField] public string _saveKey;
    [Header("saveLevel")]
    [SerializeField] public int _save = 1;
    public static int Save;
    private void SaveBackground()
    {
        PlayerPrefs.SetInt(_saveKey, _save);
    }
    private void OnLevel()
    {
        backgroundImage.sprite = backgroundSprites1;

        if (!PlayerPrefs.HasKey(_saveKey))
        {
            PlayerPrefs.SetInt(_saveKey, 0);
        }
        int savedIcon = PlayerPrefs.GetInt(_saveKey);
        switch (savedIcon)
        {
            case 1:
                backgroundImage.sprite = backgroundSprites1;
                break;
            case 2:
                backgroundImage.sprite = backgroundSprites2;
                break;
            case 3:
                backgroundImage.sprite = backgroundSprites3;
                break;
            case 4:
                backgroundImage.sprite = backgroundSprites4;
                break;
            default:

                break;
        }
    }
    private void Start()
    {
        OnLevel();
    }
    public void Change1()
    {
        backgroundImage.sprite = backgroundSprites1;
        Save = 1;
        _save = 1;
        SaveBackground();
        PlayerPrefs.SetInt(_saveKey, _save); 
    }

    public void Change2()
    {
        backgroundImage.sprite = backgroundSprites2;
        Save = 2;
        _save = 2;
        SaveBackground();
        PlayerPrefs.SetInt(_saveKey, _save); 
    }

    public void Change3()
    {
        backgroundImage.sprite = backgroundSprites3;
        Save = 3;
        _save = 3;
        SaveBackground();
        PlayerPrefs.SetInt(_saveKey, _save);
    }

    public void Change4()
    {
        backgroundImage.sprite = backgroundSprites4;
        Save = 4;
        _save = 4;
        SaveBackground();
        PlayerPrefs.SetInt(_saveKey, _save);
    }

}
