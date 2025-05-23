using UnityEngine;
using UnityEngine.InputSystem;

public class OpenInv : MonoBehaviour
{
    public InputActionReference openInv;
    public GameObject invPanel;
    public bool invopen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (openInv.action.WasPressedThisFrame())
        {
            if (!invopen && !IsBusy.singleton.isBusy)
            {
                PauseManager.Instance.PauseGame();
                invPanel.SetActive(true);
                invopen = true;
                IsBusy.singleton.isBusy = true;
                AudioManager.instance.PlayClip(AudioManager.instance.Hover);
            }
            else if (invopen && IsBusy.singleton.isBusy)
            {
                PauseManager.Instance.ResumeGame();
                invPanel.SetActive(false);
                invopen = false;
                IsBusy.singleton.isBusy = false;
                AudioManager.instance.PlayClip(AudioManager.instance.Hover);
            }
        }
    }
}
