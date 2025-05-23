using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Timeline.DirectorControlPlayable;

public class MainMenu : MonoBehaviour
{
    public Image spriteRenderer;
    public float fadeDuration = 1f;
    private Coroutine fadeCoroutine;
    public GameObject Settings;
    public InputActionReference SettingsAction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1;
        FadeOut();
    }
    void Update()
    {
        if (SettingsAction.action.WasPerformedThisFrame())
        {
            Settings.SetActive(false);
        }
    }
    public void ChangeScene(int Id)
    {
        StartCoroutine(ChangeSceneAsync(Id));
    }
    public void SettingsButton()
    {
        Settings.SetActive(true);
    }
    public void exitSettingsButton()
    {
        Settings.SetActive(false);
    }
    public void FadeOut()
    {
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeTo(0f));
    }

    public void FadeIn()
    {
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeTo(1f));
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

}
