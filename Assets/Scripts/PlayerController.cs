using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public KeyCode changeVeilKey = KeyCode.E;
    public float chrono;
    public float fadeTime;

    private float m_chronoIn;

    // Use this for initialization
    void Start()
    {

           GameManager.Notifications.AddListener(this, "VeilOn");
          GameManager.Notifications.AddListener(this, "VeilOff");
        GameManager.veilChangeKeyCode = changeVeilKey;

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //If we havent blocked the veil change
        if (GameManager.enableVeilChange)
        {

            //if we click the veil change key
            if (Input.GetKeyDown(GameManager.veilChangeKeyCode))
            {
                //veil passes to the other state
                GameManager.veilOn = !GameManager.veilOn;
                //after we check at what state we are and we send the right notification to all VeilObjectController functions
                if (GameManager.veilOn)
                    GameManager.Notifications.PostNotification(this, "VeilOn");
                if (!GameManager.veilOn)
                    GameManager.Notifications.PostNotification(this, "VeilOff");


                //            //veil turn ON
                //            GameManager.veilOn = false;
                //            m_chronoIn = chrono;

                //        }
                //    }

                //    m_chronoIn -= Time.deltaTime / fadeTime;
                //    if (m_chronoIn <= 0)
                //    {
                //        GameManager.veilOn = true;
                //    }

                //    //after we check at what state we are and we send the right notification to all VeilObjectController functions
                //    if (GameManager.veilOn)
                //        GameManager.Notifications.PostNotification(this, "VeilOn");
                //    if (!GameManager.veilOn)
                //        GameManager.Notifications.PostNotification(this, "VeilOff");


            }
        }
    }





    //void FixedUpdate()
    //{

    //    //If we havent blocked the veil change
    //    if (GameManager.enableVeilChange)
    //    {
    //        //if we click the veil change key
    //        if (Input.GetKeyDown(GameManager.veilChangeKeyCode))
    //        {
    //            GameManager.veilOn = true;
    //            GameManager.Notifications.PostNotification(this, "VeilOn");
    //            StartCoroutine(VeilCountdown());
    //        }
    //    }
    //}


    IEnumerator VeilCountdown()

    {
        m_chronoIn = chrono; 
        while (m_chronoIn > 0)
        {
            m_chronoIn -= Time.deltaTime / fadeTime;
         //   Debug.Log(m_chronoIn);
        }
        Debug.Log("exit chrono loop"); 
        GameManager.veilOn = false;
        GameManager.Notifications.PostNotification(this, "VeilOff");
        yield return null; 
    }



}
