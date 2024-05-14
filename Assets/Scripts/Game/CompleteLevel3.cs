using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using System.Data.Common;
using System.IO;

public class CompleteLevel3 : MonoBehaviour
{
    public GameObject ui;
    public string menuSceneName = "MainMenu";
    public SqliteConnection dbConnection;
    private string path;
    public string nextLevel = "Level2";
    public int levelToUnlock = 2;

    public SceneFader sceneFader;

    public void Continue()
    {
        PlayerPrefs.SetInt("levelReached", levelToUnlock);
        sceneFader.FadeTo(nextLevel);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            Toggle();
        }
    }

    public void SetConnection()
    {
        path = Application.dataPath + "/BD/DataBase/mydb.db";
        dbConnection = new SqliteConnection("URI=file:" + path);

        dbConnection.Open();
        if (dbConnection.State == ConnectionState.Open)
        {
            Debug.Log("Успешное подключение к базе данных");
        }
        else
        {
            Debug.Log("Ошибка подключения");
        }
    }

    private void TrueOrFalse()
    {
        SetConnection();

        string login = Authorization.newLogin;

        // Проверить, существует ли запись с этим логином
        if (CheckIfRecordExists(login))
        {
            // Если существует, обновить значения в этой записи
            UpdateRecord(login);
        }
        else
        {
            // Иначе, создать новую запись
            CreateNewRecord(login);
        }
    }

    private bool CheckIfRecordExists(string login)
    {
        string query = "SELECT COUNT(*) FROM lal WHERE login = @login";
        using (SqliteCommand cmd = new SqliteCommand(query, dbConnection))
        {
            cmd.Parameters.AddWithValue("@login", login);
            int count = Convert.ToInt32(cmd.ExecuteScalar());
            return count > 0;
        }
    }

    private void CreateNewRecord(string login)
    {
        string insertQuery = "INSERT INTO lal (login, levels1, levels2, levels3, levels4, levels5) VALUES (@login, @levels1, @levels2, @levels3, @levels4, @levels5)";
        using (SqliteCommand cmd = new SqliteCommand(insertQuery, dbConnection))
        {
            cmd.Parameters.AddWithValue("@login", login);
            cmd.Parameters.AddWithValue("@levels1", 1);
            cmd.Parameters.AddWithValue("@levels2", 1);
            cmd.Parameters.AddWithValue("@levels3", 1);
            cmd.Parameters.AddWithValue("@levels4", 1);
            cmd.Parameters.AddWithValue("@levels5", 0);

            int rowsAffected = cmd.ExecuteNonQuery();
        }
    }

    private void UpdateRecord(string login)
    {
        string updateQuery = "UPDATE lal SET levels1 = @levels1, levels2 = @levels2, levels3 = @levels3, levels4 = @levels4, levels5 = @levels5 WHERE login = @login";
        using (SqliteCommand cmd = new SqliteCommand(updateQuery, dbConnection))
        {
            cmd.Parameters.AddWithValue("@login", login);
            cmd.Parameters.AddWithValue("@levels1", 1);
            cmd.Parameters.AddWithValue("@levels2", 1);
            cmd.Parameters.AddWithValue("@levels3", 1);
            cmd.Parameters.AddWithValue("@levels4", 1);
            cmd.Parameters.AddWithValue("@levels5", 0);

            int rowsAffected = cmd.ExecuteNonQuery();
        }
    }

    public void Menu()
    {
        Toggle();
        TrueOrFalse();
        sceneFader.FadeTo(menuSceneName);
    }

    public void Toggle()
    {
        ui.SetActive(!ui.activeSelf);

        if (ui.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void Retry()
    {
        Toggle();
        WaveSpawner.countdown = 2f;
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }
}
