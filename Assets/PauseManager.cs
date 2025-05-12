using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public PlayerMove movement;
    public Dash dash;
    public FaceMouse faceMouse;
    public Shoot shoot;
    public GameObject Loading;
    public Image loadingfill;
    public static PauseManager Instance { get; private set; }
    public bool isPaused = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            ExitGame();
        }
    }
    public void PauseGame()
    {
        if (isPaused) return;

        PauseMovement();

        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        if (!isPaused) return;

        ResumeMovement();

        Time.timeScale = 1f;
        isPaused = false;
    }
    public void PauseMovement()
    {
        movement.enabled = false;
        dash.enabled = false;
        faceMouse.enabled = false;
        shoot.enabled = false;
    }

    public void ExitGame()
    {
        StartCoroutine(ExitGameCoroutine());
    }
    private IEnumerator ExitGameCoroutine()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(0);

        Loading.SetActive(true);

        while (!asyncLoad.isDone)
        {
            float progressval = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            loadingfill.fillAmount = progressval;
            yield return null;
        }
        Time.timeScale = 1f;
    }
    public void ResumeMovement()
    {
        movement.enabled = true;
        dash.enabled = true;
        faceMouse.enabled = true;
        shoot.enabled = true;
    }
    public void TogglePause()
    {
        if (isPaused)
            ResumeGame();
        else
            PauseGame();
    }
}
