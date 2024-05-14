using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    private int numberScene;
    public void TransitionScene(int numberScene)
    {
        SceneManager.LoadScene(numberScene);
    }
}
