using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [Header("Player")]
    [SerializeField] private GameObject railgunChrageUpSound;
    [SerializeField] GameObject railgunShotSoundsList;


    public void RailgunCargeUpSoundPlay()
    {

        GameObject chargeUpSound = Instantiate(railgunChrageUpSound);

        SoundGeneral(chargeUpSound, Vector2.zero, false);

    }

    public void RailGunShotSoundPlay()
    {
        GameObject shotSound = Instantiate(railgunShotSoundsList);

        SoundGeneral(shotSound, Vector2.zero, false);
    }


    void SoundGeneral(GameObject sound, Vector2 newPos, bool infinite)
    {


        sound.transform.position = newPos;
        sound.SetActive(true);
        sound.GetComponent<AudioSource>().Play();
        float soundLenght = sound.GetComponent<AudioSource>().clip.length;

        if (infinite == false)
        {
            StartCoroutine(DestroySound(soundLenght, sound));
        }

    }


    IEnumerator DestroySound(float soundLenght, GameObject soundToDestroy)
    {


        yield return new WaitForSeconds(soundLenght);

        Destroy(soundToDestroy);
    }
}
