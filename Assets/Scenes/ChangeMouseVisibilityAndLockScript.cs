using UnityEngine;

public class ChangeMouseVisibilityAndLockScript : MonoBehaviour
{
    [SerializeField] private bool UnlockMouse;
    void Start()
    {
        DoUnlockMouse(UnlockMouse);
    }

    public void DoUnlockMouse(bool toWhat)
    {
        if (toWhat)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;   
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
