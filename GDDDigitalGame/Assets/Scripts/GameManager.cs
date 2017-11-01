using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{

    //Array of player scores, array is initialized at game start
    private int[] score;

    private GameObject[] players;

    //Number of points needed to win.  Can not be changed after game start.
    private int goalPoints;

    //Variable used to set goalPoints
    public int goalSetter = 10;

    // Use this for initialization
    void Start()
    {

        goalPoints = goalSetter;

        players = GameObject.FindGameObjectsWithTag("player");

        score = new int[players.Length];
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
        int playerNum = 0;
        for(int i = 0; i < players.Length; i++)
        {
            if(players[i] == player)
            {
                playerNum = i;
            }
        }

        score[playerNum]++;
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
