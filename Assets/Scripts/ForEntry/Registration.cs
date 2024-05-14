using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using UnityEngine.UI;
using System;

public class Registration : MonoBehaviour
{
    public SqliteConnection dbConnection;
    private string path;

    public InputField loginInputField;
    public InputField passwordInputField;
    public InputField passwordInputField2;
    public Button registerButton;
    public Text errorMessageText;

    public GameObject initialCanvas;
    public GameObject secondCanvas;
    private void Awake()
    {
        secondCanvas.SetActive(false);
        SetConnection();
        registerButton.onClick.AddListener(OnRegisterButtonClick);
        passwordInputField.contentType = InputField.ContentType.Password;
        passwordInputField.inputType = InputField.InputType.Password;
        passwordInputField2.contentType = InputField.ContentType.Password;
        passwordInputField2.inputType = InputField.InputType.Password;

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
    private void TrueOrFalse()
    {
        string insertQuery = "INSERT INTO lal (login, levels1, levels2, levels3, levels4, levels5) VALUES (@login, @levels1, @levels2, @levels3, @levels4, @levels5)";
        string login = loginInputField.text;
        using (SqliteCommand cmd = new SqliteCommand(insertQuery, dbConnection))
        {
            cmd.Parameters.AddWithValue("@login", login);
            cmd.Parameters.AddWithValue("@levels1", 1);
            cmd.Parameters.AddWithValue("@levels2", 0);
            cmd.Parameters.AddWithValue("@levels3", 0);
            cmd.Parameters.AddWithValue("@levels4", 0);
            cmd.Parameters.AddWithValue("@levels5", 0);

            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                Debug.Log("������������ ������� ���������������.");
                initialCanvas.SetActive(false);
                secondCanvas.SetActive(true);
            }
            else
            {
                Debug.LogError("����������� ������������ �� �������.");
            }
        }
    }
    private void Score()
    {
        string insertQuery = "INSERT INTO las (login, score) VALUES (@login, @score)";
        string login = loginInputField.text;
        using (SqliteCommand cmd = new SqliteCommand(insertQuery, dbConnection))
        {
            cmd.Parameters.AddWithValue("@login", login);
            cmd.Parameters.AddWithValue("@score", 0);
    

            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                Debug.Log("������������ ������� ���������������.");
                initialCanvas.SetActive(false);
                secondCanvas.SetActive(true);
            }
            else
            {
                Debug.LogError("����������� ������������ �� �������.");
            }
        }
    }
    private void RegisterUser()
    {
        //SQL �������
        string insertQuery = "INSERT INTO lap (login, password) VALUES (@login, @password)";
        string selectQuery = "SELECT COUNT(*) FROM lap WHERE login = @login";
        //SQL �������

        string login = loginInputField.text;
        string password = passwordInputField.text;
        string password2 = passwordInputField2.text;
        if (dbConnection == null || dbConnection.State != ConnectionState.Open)
        {
            Debug.LogError("���������� � ����� ������ �� �������.");
            return;
        }



        if (password.Length < 5)
        {
            errorMessageText.text = "����� ������ ������ 5 ��������.";
            return;
        }

        if (password != password2)
        {
            errorMessageText.text = "������ �� ���������.";
            return;
        }

        using (SqliteCommand cmd = new SqliteCommand(selectQuery, dbConnection))
        {
            cmd.Parameters.AddWithValue("@login", login);

            int count = Convert.ToInt32(cmd.ExecuteScalar());
            if (count > 0)
            {
                errorMessageText.text = "������������ � ����� ������� ��� ����������.";
                return;
            }
        }

       

        using (SqliteCommand cmd = new SqliteCommand(insertQuery, dbConnection))
        {
            cmd.Parameters.AddWithValue("@login", login);
            cmd.Parameters.AddWithValue("@password", password);

            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                Debug.Log("������������ ������� ���������������.");
                initialCanvas.SetActive(false);
                TrueOrFalse();
                Score();
                secondCanvas.SetActive(true);
            }
            else
            {
                Debug.LogError("����������� ������������ �� �������.");
            }
        }
    }


    private void OnRegisterButtonClick()
    {

            RegisterUser();
        
    }
}
