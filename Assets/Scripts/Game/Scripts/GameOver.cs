using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

	public string menuSceneName = "MainMenu";

	public SceneFader sceneFader;

	public void Retry ()
	{
        WaveSpawner.EnemiesAlive = 0;
        WaveSpawner.countdown = 2f;
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

	public void Menu ()
	{
        WaveSpawner.EnemiesAlive = 0;
        WaveSpawner.countdown = 2f;
        sceneFader.FadeTo(menuSceneName);
	}

}
