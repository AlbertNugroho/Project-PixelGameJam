using UnityEngine;

public class DropItems : MonoBehaviour
{
    private void OnDestroy()
    {
        AudioManager.instance.PlayClip(AudioManager.instance.enemydeathfx);
        int rot = Random.Range(0, 6);
        int bone = Random.Range(0, 6);
        int eye = Random.Range(0, 6);
        if(rot != 0)
        {
            MaterialsWork.instance.AddRottenFlesh(rot);
        }
        if(bone != 0) {
            MaterialsWork.instance.AddBones(bone);
        }
        if (eye != 0)
        {
            MaterialsWork.instance.AddEyes(eye);
        }
        

    }
}
