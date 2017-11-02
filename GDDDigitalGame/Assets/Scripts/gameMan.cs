using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameMan : MonoBehaviour
{

    //Array of player scores, array is initialized at game start
    private int[] score;

    private GameObject[] players;

    //Number of points needed to win.  Can not be changed after game start.
    private int goalPoints;

    //Variable used to set goalPoints
    public int goalSetter = 10;
    public int numPlayers = 2;

    public Text[] textArr = new Text[2];

    // Use this for initialization
    void Start()
    {

        goalPoints = goalSetter;

        players = new GameObject[numPlayers];

        score = new int[numPlayers];

        for (int i = 0; i < numPlayers; i++)
        {
            if(i == 0)
            {
                players[i] = GameObject.FindGameObjectsWithTag("Player")[0];
                
            }
            else
            {
                players[i] = GameObject.FindGameObjectsWithTag("Player " + (i + 1))[0];
            }
            textArr[i].text = "Stocks Left: " + goalPoints;
            score[i] = goalPoints;
        }
        

        
    }

    // Update is called once per frame
    void Update()
    {
        checkEndGame();
    }

    /// <summary>
    /// Returns the score array
    /// </summary>
    /// <returns></returns>
    public int[] getScore()
    {
        return score;
    }

    /// <summary>
    /// Increases score of player, finds by player;
    /// </summary>
    /// <param name="index"></param>
    public void incrementPlayerScore(GameObject player)
    {
        int playerNum = -1;
        for (int i = 0; i < players.Length; i++)
        {
           
            if (players[i] == player)
            {
                playerNum = i;
            }
        }
        
        if(playerNum >= 0)
        {
            score[playerNum]--;
            textArr[playerNum].text = "Stocks left: " + score[playerNum];
        }

        Debug.Log(score[playerNum]);
    }

    /// <summary>
    /// Gives score of player by player number, not index eg. Player 1 is getPlayerScore(1);
    /// </summary>
    /// <param name="playerNum"></param>
    /// <returns></returns>
    public int getPlayerScore(GameObject player)
    {
        int playerNum = 0;
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] == player)
            {
                playerNum = i;
            }
        }

        return score[playerNum];
    }

    public void checkEndGame()
    {
        for (int i = 0; i < score.Length; i++)
        {
            if (score[i] == goalPoints)
            {
                endGame();
            }
        }
    }

    public void endGame()
    {

    }
}