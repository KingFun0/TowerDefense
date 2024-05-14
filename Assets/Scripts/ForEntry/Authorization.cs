using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
//using Unity.VisualScripting.Dependencies.Sqlite;

public class Authorization : MonoBehaviour
{
    public SqliteConnection dbConnection;
    private string path;
    public static string newLogin;
    public InputField loginInputField;
    public InputField passwordInputField;
    public Button registerButton;
    public Text errorMessageText;

    private void Awake()
    {
        SetConnection();
        CheckIfDatabaseIsEmpty();
        registerButton.onClick.AddListener(OnRegisterButtonClick);
        passwordInputField.contentType = InputField.ContentType.Password;
        passwordInputField.inputType = InputField.InputType.Password;

    }


    private void CheckIfDatabaseIsEmpty()
    {
        path = Application.dataPath + "/BD/DataBase/mydb.db";
        dbConnection = new SqliteConnection("URI=file:" + path);

        using (SqliteCommand command = new SqliteCommand("SELECT COUNT(*) FROM lap WHERE login IS NOT NULL AND password IS NOT NULL", dbConnection))
        {
            dbConnection.Open();
            int count = Convert.ToInt32(command.ExecuteScalar());
            Debug.Log(count);
            if (count == 0)
            {
                errorMessageText.text = "���� ������ �����.";
            }
        }
    }


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
    private bool AuthenticateUser(string login, string password)
    {
        path = Application.dataPath + "/BD/DataBase/mydb.db";
        dbConnection = new SqliteConnection("URI=file:" + path);
        string selectQuery = "SELECT COUNT(*) FROM lap WHERE login = @login AND password = @password";

        using (SqliteCommand cmd = new SqliteCommand(selectQuery, dbConnection))
        {
            cmd.Parameters.AddWithValue("@login", login);
            cmd.Parameters.AddWithValue("@password", password);

            dbConnection.Open();
            object result = cmd.ExecuteScalar();

            if (result != null && result != DBNull.Value)
            {
                int count;
                if (int.TryParse(result.ToString(), out count))
                {
                    return count > 0;
                }
                else
                {
                    Debug.LogError("�������� ��������� ������� � ���� ������: " + result.ToString());
                }
            }
            else
            {
                Debug.LogError("��� ���������� �� ������� � ���� ������");
            }

            return false;

        }

    }



    private void OnRegisterButtonClick()
    {
        string login = loginInputField.text;
        string password = passwordInputField.text;

        bool isAuthenticated = AuthenticateUser(login, password);


        if (isAuthenticated)
        {
            SceneManager.LoadScene(3);
            newLogin = login;
            Debug.LogError("�������!�.");
            return;
        }
        else
        {
            errorMessageText.text = "��������� ������������ �������� ������!";
        }

    }

}