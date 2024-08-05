using System.Collections.Generic;
using Firebase.Firestore;
using UnityEngine;
using Firebase.Extensions;

public class FirestoreController : MonoBehaviour
{
    public static FirestoreController Instance { get; private set; } // �̱��� �ν��Ͻ�

    private FirebaseFirestore db;

    void Awake()
    {
        // �̱��� ���� ����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �� �ı����� �ʵ��� ����
        }
        else
        {
            Destroy(gameObject); // �ߺ� �ν��Ͻ��� �����Ǹ� �ı�
        }
    }

    void Start()
    {
        db = FirebaseFirestore.DefaultInstance;
    }

    public void SaveGameState(string uid, int currentDay, int currentTask, Dictionary<string, bool> gameState)
    {
        DocumentReference docRef = db.Collection("users").Document(uid);
        Dictionary<string, object> data = new Dictionary<string, object>
        {
            { "currentDay", currentDay },
            { "currentTask", currentTask },
            { "gameState", gameState }
        };

        docRef.SetAsync(data).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("���� ���� ���� �Ϸ�");
            }
            else
            {
                Debug.LogError("���� ���� ���� ����: " + task.Exception);
            }
        });
    }

    public void LoadGameState(string uid, System.Action<int, int, Dictionary<string, bool>> onGameStateLoaded)
    {
        DocumentReference docRef = db.Collection("users").Document(uid);

        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DocumentSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    int currentDay = snapshot.GetValue<int>("currentDay");
                    int currentTask = snapshot.GetValue<int>("currentTask");
                    Dictionary<string, bool> gameState = snapshot.GetValue<Dictionary<string, bool>>("gameState");
                    onGameStateLoaded(currentDay, currentTask, gameState);
                    Debug.Log("����� ���� ���� �ҷ�����. ����");
                    Debug.Log("���� Day: " + currentDay + ", ���� Task: " + currentTask);
                }
                else
                {
                    Debug.Log("����� ���� ���°� �����ϴ�.");
                    onGameStateLoaded(1, 0, new Dictionary<string, bool>()); // �⺻�� ����
                }
            }
            else
            {
                Debug.LogError("���� ���� �ҷ����� ����: " + task.Exception);
                onGameStateLoaded(1, 0, new Dictionary<string, bool>()); // ���� �� �⺻�� ����
            }
        });
    }
}
