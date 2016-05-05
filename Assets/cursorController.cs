using UnityEngine;
using System.Collections;

public class cursorController : MonoBehaviour {


    public bool bdebug = false;

    void Start()
    {
        if (!bdebug)
            Screen.lockCursor = true;
    }


    void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bdebug = !bdebug;
        }

        if (!bdebug)
            Screen.lockCursor = true;
        else
            Screen.lockCursor = false;
    }
}
