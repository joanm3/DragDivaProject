using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NotificationsManager : MonoBehaviour
{

    //Private variables
    //-----------------------------------------------
    //Internal reference to all listeners for notifications
    private Dictionary<string, List<Component>> Listeners = new Dictionary<string, List<Component>>();

    //methods
    //-----------------------------------------------


    ///Function to add a listener for a notification to the listeners list
    public void AddListener(Component Sender, string NotificationName)
    {
        //add listener to dictionary 
        if (!Listeners.ContainsKey(NotificationName))
            Listeners.Add(NotificationName, new List<Component>());

        //add object to listener list for this notification
        Listeners[NotificationName].Add(Sender);
    }

    //Function to remove a listener for a notification
    public void RemoveListener(Component Sender, string NotificationName)
    {
        //if no key in dictionary existes, then exit
        if (!Listeners.ContainsKey(NotificationName))
            return;

        //cycle through listeners and identify component, and then remove
        for (int i = Listeners[NotificationName].Count - 1; i >= 0; i--)
        {
            //check instance ID
            if (Listeners[NotificationName][i].GetInstanceID() == Sender.GetInstanceID())
                Listeners[NotificationName].RemoveAt(i); //match and remove from list

        }
    }

    //-----------------------------------------------------
    ///Function to post a notification to a listener
    public void PostNotification(Component Sender, string NotificationName)
    {
        //if no key in dictiornay exists, exit
        if (!Listeners.ContainsKey(NotificationName))
            return;

        //post notification to all matching listeners
        foreach (Component Listener in Listeners[NotificationName])
            Listener.SendMessage(NotificationName, Sender, SendMessageOptions.DontRequireReceiver);
    }

    //------------------------------------------------------
    ///Function to clear all listeners
    public void ClearListeners()
    {
        //remove all listeners
        Listeners.Clear(); 
    }

    //------------------------------------------------------
    ///Function to remove redundant listeners 
    public void RemoveRedundancies()
    {
        //create new temporary Dictionary
        Dictionary<string, List<Component>> TmpListeners = new Dictionary<string, List<Component>>(); 

        //cycle through all dictionary entries
        foreach(KeyValuePair<string, List<Component>> Item in Listeners)
        {
            //cycle through all listener object in list, remove null objects
            for(int i = Item.Value.Count-1; i >= 0; i--)
            {
                //if null, then remove item
                if (Item.Value[i] == null)
                    Item.Value.RemoveAt(i); 
            }

            //if items remain in list for this notification, then add this to tmpdictionary
            if (Item.Value.Count > 0)
                TmpListeners.Add(Item.Key, Item.Value);

        }

        //replace listeners object with new, optimized dictionary, 
        Listeners = TmpListeners; 

    }

    //-------------------------------------------------------------
    //called when a new level is loaded
    ///remove redundant entreis from dictionary, in casi leftovers from previous scene
    void OnLevelWasLoaded()
    {

        //Clear redundancies
        RemoveRedundancies(); 
    }


}



