using System;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private int gameYear = 1;
    private Season gameSeason = Season.Primavera;
    private int gameDay = 1;
    private int gameHour = 6;
    private int gameMinute = 30;
    private int gameSecond = 0;
    private string gameDayOfWeek = "Lunes";
    private bool gameClockPaused = false;
    private float gameTick = 0f;
    private void Start()
    {
        //Llamamos el evento 
        EventManager.CallAdvanceGameMinuteEvent(gameYear, gameSeason, gameDay, gameDayOfWeek, gameHour, gameMinute, gameSecond);
    }
    private void Update()
    {
        if (!gameClockPaused)
        {
            GameTick();
        }
        //Si precionamos la tecla "O" el dia avanza en el reloj de la user interface
        if (Input.GetKeyDown(KeyCode.O))
        {
            TestAdvanceGameDay();
        }
    }
    //Tic del juego
    private void GameTick()
    {
        gameTick += Time.deltaTime;

        if (gameTick >= Settings.secondsPerGameSecond)
        {
            gameTick -= Settings.secondsPerGameSecond;

            UpdateGameSecond();
        }
    }
    //Actualizamos el tiempo del juego dependiendo de la variable de SecundsPerGameSecund
    private void UpdateGameSecond()
    {
        gameSecond++;
        if (gameSecond > 59)
        {
            gameSecond = 0;
            gameMinute++;
            if (gameMinute > 59)
            {
                gameMinute = 0;
                gameHour++;
                if (gameHour > 23)
                {
                    gameHour = 0;
                    gameDay++;
                    if (gameDay > 30)
                    {
                        gameDay = 1;
                        int gs = (int)gameSeason;
                        gs++;
                        gameSeason = (Season)gs;
                        if (gs > 3)
                        {
                            gs = 0;
                            gameSeason = (Season)gs;
                            gameYear++;
                            if (gameYear > 9999)
                                gameYear = 1;
                            //Llamamos el evento cada vez que aumente el año
                            EventManager.CallAdvanceGameYearEvent(gameYear, gameSeason, gameDay, gameDayOfWeek, gameHour, gameMinute, gameSecond);
                        }
                        EventManager.CallAdvanceGameSeasonEvent(gameYear, gameSeason, gameDay, gameDayOfWeek, gameHour, gameMinute, gameSecond);
                    }
                    gameDayOfWeek = GetDayOfWeek();
                    EventManager.CallAdvanceGameDayEvent(gameYear, gameSeason, gameDay, gameDayOfWeek, gameHour, gameMinute, gameSecond);
                }
                EventManager.CallAdvanceGameHourEvent(gameYear, gameSeason, gameDay, gameDayOfWeek, gameHour, gameMinute, gameSecond);
            }
            EventManager.CallAdvanceGameMinuteEvent(gameYear, gameSeason, gameDay, gameDayOfWeek, gameHour, gameMinute, gameSecond);
        }
    }
    //Evalua que dia de la semana estamos
    private string GetDayOfWeek()
    {
        int totalDays = (((int)gameSeason) * 30) + gameDay;
        int dayOfWeek = totalDays % 7;
        switch (dayOfWeek)
        {
            case 1:
                return "Lunes";

            case 2:
                return "Martes";

            case 3:
                return "Miercoles";

            case 4:
                return "Jueves";

            case 5:
                return "Viernes";

            case 6:
                return "Sabado";

            case 0:
                return "Domingo";

            default:
                return "";
        }
    }
    //Funcion para avanzar de dia
    public void TestAdvanceGameDay()
    {
        for (int i = 0; i < 86400; i++)
        {
            UpdateGameSecond();
        }
    }
}
