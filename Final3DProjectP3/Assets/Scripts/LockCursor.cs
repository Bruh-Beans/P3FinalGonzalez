using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockCursor : MonoBehaviour
{
    void Start()
    {
        // Initially unlock the cursor, assuming the game starts at the title screen
        Unlock();
    }

    public void Lock()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Unlock()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
