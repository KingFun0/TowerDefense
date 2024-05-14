
using System.Data.Common;
using System.Data;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class Enemy : MonoBehaviour
{
    private string path;
    public SqliteConnection dbConnection;
    public enum EnemyType
    {
        Simple,
        Fast,
        Strange
    }

    public EnemyType enemyType;
    public string enemyTagSimple = "Simple";
    public string enemyTagFast = "Fast";
    public string enemyTagStrange = "Strange";
    public float startSpeed = 10f;
    [HideInInspector] public float speed;
    public float startHealth = 100;
    private float health;

    public int worth;
    public int rating;
    public GameObject deathEffect;

    [Header("Unity Stuff")]
    public Image healthBar;

    private bool isDead = false;

    void Start()
    {
        speed = startSpeed;
        health = startHealth;
        worth = UnityEngine.Random.Range(3, 7);
        rating = UnityEngine.Random.Range(8, 14);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        healthBar.fillAmount = health / startHealth;
        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    public void Slow(float pct)
    {
        speed = startSpeed * (1f - pct);
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
    private void Update()
    {
        TrueOrFalse();
    }
    private void TrueOrFalse()
    {
        SetConnection();

        string login = Authorization.newLogin;

        // Проверить, существует ли запись с этим логином
        if (CheckIfRecordExists(login))
        {

            UpdateRecord(login);
        }
        else
        {
            
            CreateNewRecord(login);
        }
    }

    private bool CheckIfRecordExists(string login)
    {
        string query = "SELECT COUNT(*) FROM las WHERE login = @login";
        using (SqliteCommand cmd = new SqliteCommand(query, dbConnection))
        {
            cmd.Parameters.AddWithValue("@login", login);
            int count = Convert.ToInt32(cmd.ExecuteScalar());
            return count > 0;
        }
    }

    private void CreateNewRecord(string login)
    {
        string insertQuery = "INSERT INTO las (login, score) VALUES (@login, @score)";
        using (SqliteCommand cmd = new SqliteCommand(insertQuery, dbConnection))
        {
            cmd.Parameters.AddWithValue("@login", login);
            cmd.Parameters.AddWithValue("@score", PlayerStats.Rating);


            int rowsAffected = cmd.ExecuteNonQuery();
        }
    }

    private void UpdateRecord(string login)
    {
        string updateQuery = "UPDATE las SET score = @score WHERE login = @login";
        using (SqliteCommand cmd = new SqliteCommand(updateQuery, dbConnection))
        {
            cmd.Parameters.AddWithValue("@login", login);
            cmd.Parameters.AddWithValue("@score", PlayerStats.Rating);


            int rowsAffected = cmd.ExecuteNonQuery();
        }
    }
    void Die()
    {
       
        isDead = true;

        PlayerStats.Money += worth;
        PlayerStats.Rating += rating;
        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);

        WaveSpawner.EnemiesAlive--;

        Destroy(gameObject);
    }
}
