using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;  

public class AmmoWorks : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static AmmoWorks singleton;
    public TextMeshProUGUI ammotxt;
    public int maxAmmo;
    public int limitShowAmmo = 38;
    public int currentAmmo;
    public List<GameObject> Bulletimg = new List<GameObject>();
    internal bool isReloading;
    private float timer;
    private float reloadtime = 0.4f;
    public InputActionReference reloadaction;
    public Animator gunanim;
    private void Awake()
    {
        singleton = this;
    }
    void Start()
    {
        currentAmmo = maxAmmo;
        foreach (Transform child in transform)
        {
            Bulletimg.Add(child.gameObject);
        }
        updateAmmoGUI();
        UpdateTxt();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer>0)
        {
            isReloading = true;
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                timer = 0;
                currentAmmo = maxAmmo;
                isReloading = false;
                updateAmmoGUI();
                UpdateTxt();
            }
        }

        if(reloadaction.action.WasPressedThisFrame())
        {
            reload();
            UpdateTxt();
        }
    }

    private void UpdateTxt()
    {
        ammotxt.text = currentAmmo + "/" + maxAmmo;
    }

    private void updateAmmoGUI()
    {
        if (currentAmmo > limitShowAmmo)
        {
            showAmmo(limitShowAmmo);
            return;
        }
        showAmmo(currentAmmo);
    }

    private void showAmmo(int howmuch)
    {
        foreach(var child in Bulletimg)
        {
            child.SetActive(false);
        }
        for(int i = 0; i < howmuch; i++)
        {
            Bulletimg[i].SetActive(true);
        }
    }

    public void useAmmo(int bullet)
    {
        currentAmmo = Mathf.Max(0, currentAmmo - bullet);
        updateAmmoGUI();
        UpdateTxt();
    }

    public void reload()
    {
        if (isReloading) return;
        AudioManager.instance.PlayClip(AudioManager.instance.Reloadfx);
        timer = reloadtime;
        isReloading = true;
        gunanim.SetTrigger("Reload");
    }

    public void setMaxAmmo(int max)
    {
        maxAmmo = max;
    }

    public int getcurAmmo()
    {
        return currentAmmo;
    }
}
