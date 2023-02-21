using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject loadingScreen;//进度条
    public Slider slider;
    public Text progressText;

   // public void PlayGame()
    //{
        //SceneManager.LoadScene(1);//把菜单放到1的位置
   // }

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(AsyncLoadLevel(sceneIndex));
    }

    IEnumerator AsyncLoadLevel(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = operation.progress / 0.9f;//operation.progress的范围是0~0.9
            slider.value = progress;
            progressText.text = Mathf.FloorToInt(progress * 100f).ToString() + "%";//前面那个会默认变成string类型
            yield return null;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
