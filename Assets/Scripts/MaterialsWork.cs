using TMPro;
using UnityEngine;

public class MaterialsWork : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static MaterialsWork instance;
    public int rottenflesh;
    public int bones;
    public int eyes;
    public TextMeshProUGUI rottxt;
    public TextMeshProUGUI bonestxt;
    public TextMeshProUGUI eyetxt;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddRottenFlesh(int amount)
    {
        rottenflesh += amount;
        rottxt.text = rottenflesh.ToString();
    }
    public void AddBones(int amount)
    {
        bones += amount;
        bonestxt.text = bones.ToString();
    }
    public void AddEyes(int amount)
    {
        eyes += amount;
        eyetxt.text = eyes.ToString();
    }
    public void Craft(int rottenfleshs, int bone, int eye)
    {
        rottenflesh -= rottenfleshs;
        bones -= bone;
        eyes -= eye;
        rottxt.text = rottenflesh.ToString();
        bonestxt.text = bones.ToString();
        eyetxt.text = eyes.ToString();
    }
}
