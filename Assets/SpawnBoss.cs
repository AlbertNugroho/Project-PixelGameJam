
using System.Collections;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class SpawnBoss : MonoBehaviour
{
    float TimeElapsed = 0;
    public GameObject Boss;
    public GameObject Player;
    public GameObject BossView;
    public TextMeshProUGUI timeText;
    private bool fightStarted = false;
    public GameObject wall;
    public GameObject BossHealthBar;
    public GameObject Title;
    public GameObject Materialbar;
    public GameObject PlayerHealth;
    public GameObject Bulletsbar;
    public CanvasGroup textGroup;
    public TextMeshProUGUI text;
    public CinemachineCamera cc;
    public Animator bossAnim;
    public BossAI bossAI;
    public BossCannon bossCannon;
    void Start()
    {
        UpdateTimeText();
    }

    // Update is called once per frame
    void Update()
    {
        if (!fightStarted) return;

        TimeElapsed = TimeElapsed + Time.deltaTime;
        UpdateTimeText();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !fightStarted)
        {
            AudioManager.instance.PlayMusic(AudioManager.BgAudio.Boss);
            fightStarted = true;
            StartWave();
            cc.LookAt = BossView.transform;
            cc.Follow = BossView.transform;
            ShowTitle("Master Of The Colloseum", 1f, 2f);
            IsBusy.singleton.isBusy = true;
            Materialbar.SetActive(false);
            PlayerHealth.SetActive(false);
            Bulletsbar.SetActive(false);
            Invoke("lookAtPlayer", 4f);
        }
    }
    public void lookAtPlayer()
    {
        cc.LookAt = Player.transform;
        cc.Follow = Player.transform;
        IsBusy.singleton.isBusy = false;
        Materialbar.SetActive(true);
        PlayerHealth.SetActive(true);
        Bulletsbar.SetActive(true);
        BossHealthBar.SetActive(true);
        bossAI.enabled = true;
        bossCannon.enabled = true;
        int random = Random.Range(0, 2);
        if (random == 0)
        {
            bossAnim.SetTrigger("Walk");
        }
        else
        {
            bossAnim.SetTrigger("Run");
        }

    }
    public void ShowTitle(string message, float fadeDuration, float holdDuration)
    {
        StartCoroutine(FadeRoutine(message, fadeDuration, holdDuration));
    }
    IEnumerator FadeRoutine(string message, float fadeTime, float holdTime)
    {
        text.text = message;
        textGroup.alpha = 0f;
        Title.SetActive(true);

        // Fade in
        float timer = 0f;
        while (timer < fadeTime)
        {
            textGroup.alpha = timer / fadeTime;
            timer += Time.deltaTime;
            yield return null;
        }
        textGroup.alpha = 1f;

        // Hold
        yield return new WaitForSeconds(holdTime);

        // Fade out
        timer = 0f;
        while (timer < fadeTime)
        {
            textGroup.alpha = 1f - (timer / fadeTime);
            timer += Time.deltaTime;
            yield return null;
        }
        textGroup.alpha = 0f;

        Title.SetActive(false);
    }

    private void UpdateTimeText()
    {
        int minutes = (int)(TimeElapsed / 60) % 60;
        int seconds = (int)(TimeElapsed) % 60;
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void StartWave()
    {
        wall.SetActive(true);
        UpdateTimeText();
        SummonBoss();
    }

    public void SummonBoss()
    {
        Boss.SetActive(true);
    }
}
