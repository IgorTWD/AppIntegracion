using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class FirebaseInit : MonoBehaviour
{
    public UnityEvent OnFirebaseInitialized = new UnityEvent();
    public TMP_Text outputText;


    // Start is called before the first frame update
    void Start()
    {
        outputText.text = "entra en start";
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            Firebase.FirebaseException firebaseEX = task.Exception.GetBaseException() as Firebase.FirebaseException;

            outputText.text = "Analitics ON: " + firebaseEX.Message;
            FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
        });

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            Firebase.FirebaseException firebaseEX = task.Exception.GetBaseException() as Firebase.FirebaseException;

            outputText.text = "Analitics ON: " + firebaseEX.Message;
            if (task.Exception != null)
            {
                Debug.Log("Fallo inicializacion Firebase");
                return;
            }

            OnFirebaseInitialized.Invoke();
        });
    }
}
