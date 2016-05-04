using UnityEngine;
using System.Collections;

public class TriggerVeilState : MonoBehaviour
{
    public bool veilOnInsideZone = true;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.enableVeilChange = false; 

            if (veilOnInsideZone)
            {
                GameManager.veilOn = true;
                GameManager.Notifications.PostNotification(this, "VeilOn");
            }
            else
            {
                GameManager.veilOn = false;
                GameManager.Notifications.PostNotification(this, "VeilOff");
            }

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.veilOn = !GameManager.veilOn; 
            GameManager.enableVeilChange = true; 
        }

    }




}
