using UnityEngine;
using System.Collections;

public class PlayerUI : MonoBehaviour {
    CursorLockMode cursorState;
    bool cursorLocked;

    void Start ()
    {
        cursorLocked = true;
        cursorState = CursorLockMode.Locked;
    }

    void Update()
   {
        if (Input.GetKeyDown(KeyCode.Escape))
            LockCursor();

        CursorState();

    }

    void CursorState() //applys the state of cursorState to the Cursor Class
    {
        Cursor.lockState = cursorState;
        Cursor.visible = (CursorLockMode.Locked != cursorState);
    }

    void LockCursor()
    {
        cursorLocked = !cursorLocked;
        if (cursorLocked)
            cursorState = CursorLockMode.Locked;
        else if (!cursorLocked)
            cursorState = CursorLockMode.None;
    }
}
