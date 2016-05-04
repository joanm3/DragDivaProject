using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NotificationsManager))] //send and recive messages
public class GameManager : MonoBehaviour
{

    public bool debugMode = true;

    //------------------------------------------
    //list of veil cases
    // public enum VeilState { veilOn, veilOff };

    //the static reference to the veil state
    // public static VeilState veilState;
    public static KeyCode veilChangeKeyCode;


    //State of the veil, on or off
    public static bool veilOn;

    //enable changing veil or not
    public static bool enableVeilChange;

    //-------------------------------------------
    //retrieve currently active instance of object if any
    public static GameManager Instance
    {
        get
        {
            //create gameManager object if needed
            if (instance == null) instance = new GameObject("GameManager").AddComponent<GameManager>();
            return instance;
        }
    }

    //----------------------------------------
    // retrieve notifications manager
    public static NotificationsManager Notifications
    {
        get
        {
            if (notifications == null) notifications = Instance.GetComponent<NotificationsManager>();
            return notifications;
        }
    }

    //Internal reference to single active instance of object singleton
    private static GameManager instance = null;

    //internal reference to notification object
    private static NotificationsManager notifications = null;



    void Awake()
    {
        //check if there is an existing instance of this object
        if ((instance) && (instance.GetInstanceID() != GetInstanceID()))
            DestroyImmediate(gameObject); //delete duplicate
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject); //do not destroy
        }

        //set veil
        veilOn = true;

        //enable veil changing
        enableVeilChange = true;
    }

    //We should use it alse in OnLevelLoad to call it everytime we create a new level
    void Start()
    {
        if (veilOn)
            Notifications.PostNotification(this, "VeilOn");
        if (!veilOn)
            Notifications.PostNotification(this, "VeilOff");

        if (!debugMode)
            Cursor.visible = false;

    }

    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = !Cursor.visible;


        }


    }
}
