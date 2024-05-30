using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LockCursor : MonoBehaviour
{

    void Start()
    {
        // esc
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Unlock()
    {
        // esc
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
    