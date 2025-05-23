using System.Collections;
using System.Linq;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public PlayerMove movement;
    public Dash dash;
    public FaceMouse faceMouse;
    public Shoot shoot;
    public GameObject PauseMenu;
    public Image spriteRenderer;
    public InputActionReference pauseAction;
    public float fadeDuration = 1f;
    private Coroutine fadeCoroutine;
    public static PauseManager Instance { get; private set; }
    public bool isPaused = false;
    public bool waiting = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    private void Start()
    {
        FadeOut();
    }
    private void Update()
    {
        if(pauseAction.action.WasPerformedThisFrame())
        {
            PauseMenu.SetActive(!PauseMenu.activeSelf);
            TogglePause();
        }
    }
    public void ResumeButton()
    {
        PauseMenu.SetActive(false);
        ResumeGame();
    }
    public void MainMenuButton()
    {
        FadeIn();
        StartCoroutine(ChangeSceneAsync(0));
    }
    public void FadeOut()
    {
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeTo(0f));
        IsBusy.singleton.isBusy = false;
    }

    public void FadeIn()
    {
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeTo(1f));
        IsBusy.singleton.isBusy = true;
    }
    private IEnumerator FadeTo(float targetAlpha)
    {
        Color startColor = spriteRenderer.color;
        float startAlpha = startColor.a;
        float time = 0f;

        while (time < fadeDuration)
        {
            float t = time / fadeDuration;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, t);
            spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            time += Time.deltaTime;
            yield return null;
        }

        // Ensure it reaches the final alpha
        spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, targetAlpha);
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
        StartCoroutine(ChangeSceneAsync(0));
    }
    
    public void ReloadGame()
    {
        StartCoroutine(ChangeSceneAsync(SceneManager.GetActiveScene().buildIndex));
    }
    private IEnumerator ChangeSceneAsync(int Id)
    {
        Time.timeScale = 1f;
        FadeIn();
        yield return new WaitForSeconds(fadeDuration);
        yield return null;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(Id);
        asyncLoad.allowSceneActivation = false;
        while (asyncLoad.progress < 0.9f)
        {
            yield return null;
        }
        asyncLoad.allowSceneActivation = true;

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
