using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event Action NewStageEvent;
    public event Action GameOverEvent;
    public event Action<int> PlayerPointsEvent;

    [HideInInspector] public float gameSpeed;
    [SerializeField] private float startingGameSpeed;

    private const int SecondDevider = 5;
    private const string PLAYER_POINTS_SAVE = "Value";
    private int playerPoints;
    private int pointsRecord;
    private int gameDurationSeconds;
    private bool isGameOver;

    public int PlayerPoints
    {
        get => playerPoints;
        set
        {
            playerPoints = value;
            PlayerPointsEvent?.Invoke(playerPoints);
        }
    }

    public int PlayerRecord { get => pointsRecord; }

    public bool GameOver
    {
        get => isGameOver;
        set
        {
            isGameOver = value;
            if (value == true)
            {
                GameOverEvent?.Invoke();
                gameSpeed = 0;
            }
        }
    }

    private void Awake()
    {
        gameSpeed = startingGameSpeed;
        pointsRecord = PlayerPrefs.GetInt(PLAYER_POINTS_SAVE);
        StartCoroutine(TimerRoutine());
        StartCoroutine(PointIncreaseRoutine());
    }

    public void SaveTheRecord()
    {
        if (PlayerRecord < PlayerPoints)
        {
            pointsRecord = playerPoints;
            PlayerPrefs.SetInt(PLAYER_POINTS_SAVE, pointsRecord);
        }
    }

    private IEnumerator TimerRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            gameDurationSeconds++;

            if (gameDurationSeconds % SecondDevider == 0)
            {
                gameSpeed *= 1.1f;
                NewStageEvent?.Invoke();
            }
        }
    }

    private IEnumerator PointIncreaseRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            PlayerPoints += (int)gameSpeed;
        }
    }
}
