using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Singleton GameManager, exists only one of this gameObject
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {   
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    //Runs when the game starts
    public void StartGame()
    {
        GameData gameData = new GameData();

    }
}
