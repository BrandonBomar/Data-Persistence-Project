using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class UIMenuHandler : MonoBehaviour
{
    [SerializeField] private string playerName = "";
    public TMP_InputField userName;

    public Text highScoreText;

    void Start()
    {
        userName = GameObject.Find("Username").GetComponent<TMP_InputField>();
        GameManager.Instance.LoadGameData();
        highScoreText.text = "High Score : " + GameManager.Instance.bestName + " : " + GameManager.Instance.highScore;
    }

    void Update()
    {
        Name();
    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #else
        Application.Quit();
        #endif
    }

    public void Name()
    {
        playerName = userName.text;
        Debug.Log(playerName);

        GameManager.Instance.name = playerName;
    }
}
