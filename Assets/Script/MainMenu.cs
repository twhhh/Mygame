using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject loadingScreen;//������
    public Slider slider;
    public Text progressText;

   // public void PlayGame()
    //{
        //SceneManager.LoadScene(1);//�Ѳ˵��ŵ�1��λ��
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
            float progress = operation.progress / 0.9f;//operation.progress�ķ�Χ��0~0.9
            slider.value = progress;
            progressText.text = Mathf.FloorToInt(progress * 100f).ToString() + "%";//ǰ���Ǹ���Ĭ�ϱ��string����
            yield return null;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
