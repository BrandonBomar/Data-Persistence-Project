using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class GameManager : MonoBehaviour
{
    [SerializeField] Text HighScoreText;
    public TextMeshProUGUI userName;

    public new string name;

    public string bestName;
    public int highScore;

    public static GameManager Instance;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadGameData();
    }
    // Start is called before the first frame update
    private void Start()
    {
        if (bestName != name)
        {
            HighScoreText.text = "High Score : " + bestName + " : " + highScore;
        }
    }

    public void SetHighScore(int score)
    {
        if (score > highScore)
        {
            highScore = score;
            bestName = name;
            SaveGameData();
            MainManager.Instance.HighScoreText.text = "High Score : " + bestName + " : " + highScore;
        }
    }

    [System.Serializable]
    class SaveData 
    {
        public string bestName;
        public int highScore;
    }

    public void SaveGameData()
    {
        SaveData data = new SaveData();
        data.bestName = bestName;
        data.highScore = highScore;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadGameData()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestName = data.bestName;
            highScore = data.highScore;
        }
    }
}
