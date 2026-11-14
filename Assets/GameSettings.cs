using UnityEngine;

public class GameSettings
{
    private static string s_VolumeLevel = "VolumeLevel";
    public static float VolumeLevel
    {
        get
        {
            return PlayerPrefs.GetFloat(s_VolumeLevel, 1);
        }
        set
        {
            PlayerPrefs.SetFloat(s_VolumeLevel, value);
        }
    }

    private static string s_AimType = "AimType";
    // 0 = normal mouse aim, 1 = virtual joystick, 2 = NOTHING IT DOES NOT EXIST
    public static int AimType
    {
        get
        {
            return PlayerPrefs.GetInt(s_AimType, 0);
        }
        set
        {
            PlayerPrefs.SetInt(s_AimType, value);
        }
    }
}
