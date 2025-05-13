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
        if (Input.GetKeyDown(KeyCode.L))
        {
            Inventory.Singleton.SpawnInventoryItem();
        }
        if (openInv.action.WasPressedThisFrame())
        {
            if (!invopen && !IsBusy.singleton.isBusy)
            {
                PauseManager.Instance.PauseGame();
                invPanel.SetActive(true);
                invopen = true;
                IsBusy.singleton.isBusy = true;
            }
            else if (invopen && IsBusy.singleton.isBusy)
            {
                PauseManager.Instance.ResumeGame();
                invPanel.SetActive(false);
                invopen = false;
                IsBusy.singleton.isBusy = false;
            }
        }
    }
}
