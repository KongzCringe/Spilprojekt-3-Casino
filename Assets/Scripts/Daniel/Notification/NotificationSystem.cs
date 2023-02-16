using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotificationSystem : MonoBehaviour
{
    private static List<GameObject> notifications = new();

    public NotificationSystem(string signal, string title, string text)
    {
        var prefab = (GameObject) Resources.Load("Assets/Scripts/Daniel/Notification/NotifiationObj.prefab");
        var obj = Instantiate(prefab);

        var signalObj = NPC.FindChild(obj, "Signal");
        var titleObj = NPC.FindChild(obj, "Title");
        var textObj = NPC.FindChild(obj, "Text");

        signalObj.GetComponent<TMP_Text>().text = signal;
        titleObj.GetComponent<TMP_Text>().text = title;
        textObj.GetComponent<TMP_Text>().text = text;
        
        notifications.Add(obj);
    }
    
    public static List<GameObject> GetNotifications()
    {
        return notifications;
    }
}
