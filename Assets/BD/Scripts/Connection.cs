using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;
using UnityEngine.UI;
//using UnityEditor.Experimental.GraphView;

public class Connection : MonoBehaviour
{
    
    public SqliteConnection dbConnection;
    private string path;
    public void SetConnection()
    {
        
        path = Application.dataPath + "/TowerDefense/mydb.bytes";
        dbConnection = new SqliteConnection("Data Source = " + path);
        dbConnection.Open();
        if(dbConnection.State == ConnectionState.Open)
        {
            Debug.Log("Database connection successful");
            SqliteCommand cmd = new SqliteCommand();
            cmd.Connection = dbConnection;
            cmd.CommandText = "SELECT * FROM LoginAndPassword";
            SqliteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Debug.Log("Database connection successful");
            }
            
        }
        else
        {
            Debug.Log("Error connection");
        }
    }
}
