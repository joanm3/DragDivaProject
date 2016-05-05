using UnityEngine;
using System.Collections;

public class cursorController : MonoBehaviour {


    public bool bdebug = false;

    void Start()
    {
        if (!bdebug)
            Cursor.visible = true;
    }


    void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bdebug = !bdebug;
        }

        if (!bdebug)
            Cursor.visible = true;
        else
            Cursor.visible = false;
    }
}
