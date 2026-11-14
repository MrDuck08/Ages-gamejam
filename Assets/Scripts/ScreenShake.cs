using Unity.Cinemachine;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    [SerializeField] CinemachineImpulseSource recoilSource;

    [SerializeField] Transform player; // Just nu serialized för lat

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerRifleRecoil()
    {

        recoilSource.GenerateImpulse(-player.up);

    }
}
