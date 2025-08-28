using UnityEngine;

[System.Serializable]
public class GameData
{
    public int saveVersion = 1;

    public int points;
    public int playerHp;

    public GameData()
    {
        points = 0;
        playerHp = 10;
    }
}
