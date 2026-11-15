using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [Header("Player")]
    [SerializeField] private GameObject railgunChrageUpSound;
    [SerializeField] private GameObject railgunShotSoundsList;
    [SerializeField] List<GameObject> shotgunShotSoundList = new List<GameObject>();
    [SerializeField] private GameObject shotgunShellsSound;


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

    public void ShotgunShotSoundPlay(Vector2 pos)
    {
        int whatShotgunSound = Random.Range(0, shotgunShotSoundList.Count);

        GameObject shotSound = Instantiate(shotgunShotSoundList[whatShotgunSound]);


        StartCoroutine(ShotgunShellsSoundRoutine(pos));
        SoundGeneral(shotSound, Vector2.zero, false);
    }

    IEnumerator ShotgunShellsSoundRoutine(Vector2 pos)
    {

        yield return new WaitForSeconds(0.2f);


        GameObject shellSound = Instantiate(shotgunShellsSound);

        SoundGeneral(shellSound, pos, false);
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
