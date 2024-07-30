using Firebase.Firestore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirestoreController : MonoBehaviour
{
    private FirebaseFirestore db;

    void Start()
    {
        db = FirebaseFirestore.DefaultInstance;
    }

    public void SaveProgress(string userId, int day)
    {
        DocumentReference docRef = db.Collection("users").Document(userId);
        Dictionary<string, object> userProgress = new Dictionary<string, object>
        {
            { "currentDay", day }
        };

        docRef.SetAsync(userProgress).ContinueWith(task => {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError("SaveProgress failed: " + task.Exception?.Message);
                return;
            }

            Debug.Log("Progress saved successfully");
        });
    }
}
