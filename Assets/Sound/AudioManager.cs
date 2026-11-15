using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [Header("Player")]
    [SerializeField] private GameObject partsExplodeSound;
    [SerializeField] List<GameObject> partsDropSoundList = new List<GameObject>();
    [SerializeField] private GameObject equpPartSound;
    [SerializeField] private GameObject inBagSound;

    [Space]

    [Header("Weapons")]
    [SerializeField] private GameObject railgunChrageUpSound;
    [SerializeField] private GameObject railgunShotSoundsList;
    [SerializeField] List<GameObject> shotgunShotSoundList = new List<GameObject>();
    [SerializeField] private GameObject shotgunShellsSound;
    [SerializeField] List<GameObject> rifleShotSoundList = new List<GameObject>();
    [SerializeField] private GameObject punchSound;

    [Space]

    [Header("Enemies")]
    [SerializeField] List<GameObject> enemyDeathSoundList = new List<GameObject>();

    public void PartsDestroySoundPlay()
    {
        GameObject partEplode = Instantiate(partsExplodeSound);

        SoundGeneral(partEplode, Vector2.zero, false);


        int whatPartDropSound = Random.Range(0, partsDropSoundList.Count);

        GameObject partDrop = Instantiate(partsDropSoundList[whatPartDropSound]);

        SoundGeneral(partDrop, Vector2.zero, false);
    }

    public void EquipPartSoundPlay()
    {
        GameObject equipPart = Instantiate(equpPartSound);

        SoundGeneral(equipPart, Vector2.zero, false);
    }

    public void InBagSound()
    {
        GameObject bagSound = Instantiate(inBagSound);

        SoundGeneral(bagSound, Vector2.zero, false);
    }

    #region Weapons
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

    public void RifleShootSound()
    {
        int whatShotSound = Random.Range(0, rifleShotSoundList.Count);

        GameObject shotSound = Instantiate(rifleShotSoundList[whatShotSound]);


        SoundGeneral(shotSound, Vector2.zero, false);
    }

    public void PunchSoundPlay()
    {
        GameObject punchSound = Instantiate(this.punchSound);

        SoundGeneral(punchSound, Vector2.zero, false);
    }

    #endregion

    public void EnemyDieSoundPlay(Vector2 pos)
    {
        int whatDeathSound = Random.Range(0, enemyDeathSoundList.Count);

        GameObject deathSound = Instantiate(enemyDeathSoundList[whatDeathSound]);

        SoundGeneral(deathSound, pos, false);
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
