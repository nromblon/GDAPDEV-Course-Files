using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;
using TMPro;

public class NotificationHandler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI intentTxt;

    private void Awake()
    {
        BuildDefaultNotificationChannel();
        BuildRepeatNotificationChannel();
    }

    private void Start()
    {
        AndroidNotificationCenter.OnNotificationReceived += OnReceivedIntent;
    }

    private void OnDisable()
    {
        AndroidNotificationCenter.OnNotificationReceived -= OnReceivedIntent;
    }

    private void OnReceivedIntent(AndroidNotificationIntentData data)
    {
        intentTxt.text = data.Notification.IntentData;
    }

    private void BuildDefaultNotificationChannel()
    {
        // How we would refer to the channel in code
        string channel_id = "default";

        // How the channel would appear in the settings
        string title = "Default Channel";

        // Importance of the channel
        Importance importance = Importance.Default;

        // Description of the channel
        string description = "Default Channel for this game";

        AndroidNotificationChannel defaultChannel = new AndroidNotificationChannel(channel_id, title, description, importance);

        AndroidNotificationCenter.RegisterNotificationChannel(defaultChannel);
    }

    private void BuildRepeatNotificationChannel()
    {
        // How we would refer to the channel in code
        string channel_id = "repeat";

        // How the channel would appear in the settings
        string title = "Repeat Channel";

        // Importance of the channel
        Importance importance = Importance.Default;

        // Description of the channel
        string description = "Channel for repeating notifications";

        AndroidNotificationChannel repeatChannel = new AndroidNotificationChannel(channel_id, title, description, importance);

        AndroidNotificationCenter.RegisterNotificationChannel(repeatChannel);
    }

    public void SendSimpleNotif()
    {
        string title = "Simple Notif";

        string notif_message = "This is a simple notification";

        System.DateTime fireTime = System.DateTime.Now.AddSeconds(10);

        AndroidNotification notif = new AndroidNotification(title, notif_message, fireTime);

        notif.SmallIcon = "controller";
        notif.LargeIcon = "spongebob";

        AndroidNotificationCenter.SendNotification(notif, "default");
    }

    public void SendRepeatNotif()
    {
        string title = "Repeat Notif";

        string notif_message = "This is a repeat notification";

        System.DateTime fireTime = System.DateTime.Now.AddSeconds(10);

        System.TimeSpan interval = new System.TimeSpan(0, 0, 15);

        AndroidNotification notif = new AndroidNotification(title, notif_message, fireTime, interval);

        AndroidNotificationCenter.SendNotification(notif, "repeat");
    }

    public void SendDataNotif()
    {
        string title = "Data Notif";

        string notif_message = "This notification contains data";

        System.DateTime fireTime = System.DateTime.Now.AddSeconds(10);

        AndroidNotification notif = new AndroidNotification(title, notif_message, fireTime);

        notif.SmallIcon = "controller";
        notif.LargeIcon = "spongebob";
        notif.IntentData = "Hello World!";

        AndroidNotificationCenter.SendNotification(notif, "default");
    }
}
