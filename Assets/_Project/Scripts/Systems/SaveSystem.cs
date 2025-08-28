using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    private string m_saveFilePath;

    private void Awake()
    {
        m_saveFilePath = Path.Combine(Application.persistentDataPath, "gameSave.json");
    }

    public void SaveGame(GameData gameData)
    {
        string json = JsonUtility.ToJson(gameData);
        File.WriteAllText(m_saveFilePath, json);
        Debug.Log("Deu certo");
    }

    public GameData LoadGame(GameData gameData)
    {
        if (File.Exists(m_saveFilePath))
        {
            string json = File.ReadAllText(m_saveFilePath);
            gameData = JsonUtility.FromJson<GameData>(json);
            Debug.Log("Deu certo 2");

            return gameData;
        }
        return null;
    }
}