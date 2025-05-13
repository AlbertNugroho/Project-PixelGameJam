using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ToggleText : MonoBehaviour
{
    public InputActionReference interact;
    public TextMeshProUGUI text;
    public GameObject textobj;
    public GameObject indicator;
    private bool isreading = false;
    [TextArea]
    public string textString;

    public OpenInv inv;
    public bool playerInTrigger = false;
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
            if (!isreading && !IsBusy.singleton.isBusy)
            {
                PauseManager.Instance.movement.rb.linearVelocity = Vector2.zero;
                PauseManager.Instance.movement.playeranim.SetBool("walking", false);
                PauseManager.Instance.PauseMovement();
                textobj.SetActive(true);
                text.text = textString;
                isreading = true;
                IsBusy.singleton.isBusy = true;
            }
            else if (isreading && IsBusy.singleton.isBusy)
            {
                PauseManager.Instance.ResumeMovement();
                textobj.SetActive(false);
                isreading = false;
                IsBusy.singleton.isBusy = false;
            }
        }
    }
}
