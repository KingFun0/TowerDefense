using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using UnityEngine.UI;
using System;
using System.Data.Common;
using System.IO;

public class RatingUI : MonoBehaviour
{
    public Text ratingText;
    private string path;
    public SqliteConnection dbConnection;
    // Update is called once per frame
    string login = Authorization.newLogin;
    public void SetConnection()
    {
        path = Application.dataPath + "/BD/DataBase/mydb.db";

        dbConnection = new SqliteConnection("URI=file:" + path);
        dbConnection.Open();
        if (dbConnection.State == ConnectionState.Open)
        {
            Debug.Log("�������� ����������� � ���� ������");
        }
        else
        {
            Debug.Log("������ �����������");
        }
    }

    //PlayerStats.Rating.ToString()
    void Update()
    {
        SetConnection(); // �������� ����� ��������� ���������� � ����� ������

        string selectQuery = "SELECT score FROM las WHERE login = @login";

        using (SqliteCommand cmd = new SqliteCommand(selectQuery, dbConnection))
        {
            cmd.Parameters.AddWithValue("@login", login);

            using (SqliteDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    int rating = reader.GetInt32(0); // �����������, ��� ������� ��� ����� �����
                    ratingText.text =  rating.ToString();
                }
            }
        }

        dbConnection.Close(); // ��������� ���������� � ����� ������ ����� �������������
    }

}
