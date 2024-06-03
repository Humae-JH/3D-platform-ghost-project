using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class MainMenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onStartButtonClick()
    {
        Debug.Log("Start!");
        SceneManager.LoadScene("Scene 01");
    }

    public void onExitButtonClick()
    {
        Debug.Log("Exit!");

        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit(); // 실행된 게임의 프로그램 종료
        #endif
    }

    public void OnClickButton()
    {
        Debug.Log("button");
    }


}

