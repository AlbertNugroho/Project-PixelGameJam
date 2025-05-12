using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject Loading;
    public Image loadingfill;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1;
    }
    public void ChangeScene(int Id)
    {
        StartCoroutine(ChangeSceneAsync(Id));
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    private IEnumerator ChangeSceneAsync(int Id)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(Id);

        Loading.SetActive(true);

        while (!asyncLoad.isDone)
        {
            float progressval = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            loadingfill.fillAmount = progressval;
            yield return null;
        }
        Time.timeScale = 1f;
    }
    void Update()
    {
        
    }
}
