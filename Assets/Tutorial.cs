using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tutorial : MonoBehaviour
{
    [System.Serializable]
    public class TextEntry
    {
        [TextArea]
        public string text;
    }

    public InputActionReference interact;
    public TextMeshProUGUI text;
    public GameObject textobj;
    public GameObject indicator;

    public TextEntry[] textEntries;

    private bool isReading = false;
    private bool playerInTrigger = false;
    private int currentIndex = 0;

    private void OnEnable()
    {
        interact.action.Enable();
    }

    private void OnDisable()
    {
        interact.action.Disable();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            indicator.SetActive(true);
            playerInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            indicator.SetActive(false);
            playerInTrigger = false;
        }
    }

    private void Update()
    {
        if (playerInTrigger && interact.action.WasPressedThisFrame())
        {
            if (!isReading && !IsBusy.singleton.isBusy)
            {

                AudioManager.instance.PlayClip(AudioManager.instance.Hover);
                currentIndex = 0;
                isReading = true;
                IsBusy.singleton.isBusy = true;
                PauseManager.Instance.movement.rb.linearVelocity = Vector2.zero;
                PauseManager.Instance.movement.playeranim.SetBool("walking", false);
                PauseManager.Instance.PauseMovement();
                textobj.SetActive(true);
                text.text = textEntries[currentIndex].text;
            }
            else if (isReading)
            {
                currentIndex++;
                if (currentIndex < textEntries.Length)
                {
                    AudioManager.instance.PlayClip(AudioManager.instance.Hover);
                    text.text = textEntries[currentIndex].text;
                }
                else
                {
                    textobj.SetActive(false);
                    isReading = false;
                    IsBusy.singleton.isBusy = false;
                    PauseManager.Instance.ResumeMovement();
                }
            }
        }
    }
}
