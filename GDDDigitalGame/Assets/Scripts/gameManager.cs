using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour {

    //Array of player scores, array is initialized at game start
    private int score;

    //Numer of players, score array is initialized at that size.
    public int numPlayers = 2;

    //Number of points needed to win.  Can not be changed after game start.
    private int goalPoints;

    //Variable used to set goalPoints
    public int goalSetter = 10;

	// Use this for initialization
	void Start () {
        score = new score[numPlayers];

        goalPoints = goalSetter;
	}
	
	// Update is called once per frame
	void Update () {
		
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
    /// Increases score of player, finds by player number, not index eg. Player 1 is incrementPlayerScore(1);
    /// </summary>
    /// <param name="index"></param>
    public void incrementPlayerScore(int playerNum)
    {
        playerNum--;

        score[playerNum]++;
    }

    /// <summary>
    /// Gives score of player by player number, not index eg. Player 1 is getPlayerScore(1);
    /// </summary>
    /// <param name="playerNum"></param>
    /// <returns></returns>
    public int getPlayerScore(int playerNum)
    {
        playerNum--;

        return score[playerNum];
    }

}
