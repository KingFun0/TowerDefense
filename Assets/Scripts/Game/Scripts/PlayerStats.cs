using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using UnityEngine.UI;
using System;
using System.Data.Common;
using System.IO;
public class PlayerStats : MonoBehaviour
{
    private string path;
    public SqliteConnection dbConnection;
    public static int Money;
    public int startMoney = 400;

    public static int Lives;
    public int startLives = 20;

    public static int Rating;
    public string startRating;

    public static int Rounds;
    string login = Authorization.newLogin;
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

    public int GetRatingFromDatabase()
    {
        int rating = 0;

        try
        {
            SetConnection();

            string selectQuery = "SELECT score FROM las WHERE login = @login";

            using (SqliteCommand cmd = new SqliteCommand(selectQuery, dbConnection))
            {
                cmd.Parameters.AddWithValue("@login", login);

                using (SqliteDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        rating = reader.GetInt32(0); // Предполагая, что рейтинг это целое число
                    }
                }
            }
        }
        finally
        {
            dbConnection.Close();
        }

        return rating;
    }

    void Start()
    {
        Money = startMoney;
        Lives = startLives;
        Rounds = 0;

        Rating = GetRatingFromDatabase();
    }

}
