using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using UnityEngine.UI;
using System;
using System.Data.Common;
using System.IO;
using UnityEngine.SceneManagement;

public class LevelsSave : MonoBehaviour
{
    public SqliteConnection dbConnection;
    private string path;

    public Button Button1;
    public Button Button2;
    public Button Button3;
    public Button Button4;
    public Button Button5;
    private void Awake()
    {
        SetConnection();
        string login = Authorization.newLogin;

        AuthenticateUser(login);
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

    private void AuthenticateUser(string login)
    {
        string selectQuery = "SELECT levels1, levels2, levels3, levels4, levels5 FROM lal WHERE login = @login";
        using (SqliteCommand cmd = new SqliteCommand(selectQuery, dbConnection))
        {
            cmd.Parameters.AddWithValue("@login", login);

            using (SqliteDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {

                    int levels1 = reader.GetInt32(0);
                    int levels2 = reader.GetInt32(1);
                    int levels3 = reader.GetInt32(2);
                    int levels4 = reader.GetInt32(3);
                    int levels5 = reader.GetInt32(4);


                    if (levels1 == 1)
                    {
                        Button1.interactable = true;
                    }
                    else
                    {
                        Button1.interactable = false;
                    }
                    if (levels2 == 1)
                    {
                        Button2.interactable = true;
                    }
                    else
                    {
                        Button2.interactable = false;
                    }
                    if (levels3 == 1)
                    {
                        Button3.interactable = true;
                    }
                    else
                    {
                        Button3.interactable = false;
                    }
                    if (levels4 == 1)
                    {
                        Button4.interactable = true;
                    }
                    else
                    {
                        Button4.interactable = false;
                    }
                    if (levels5 == 1)
                    {
                        Button5.interactable = true;
                    }
                    else
                    {
                        Button5.interactable = false;
                    }

                }
            }


        }
    }
}
