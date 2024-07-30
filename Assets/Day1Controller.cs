using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase.Firestore;

public class Day1Controller : MonoBehaviour
{
    public FirestoreController firestoreController;
    public FirebaseAuthController firebaseAuthController;
    public string userId; // Set this with the logged-in user ID

    public void CompleteDay1()
    {
        // ���� ���� ����...
        userId = firebaseAuthController.uid;

        // Day1�� �Ϸ������Ƿ� Day2�� ������Ʈ
        firestoreController.SaveProgress(userId, 2);

        Debug.Log("Day1 �Ϸ�");
        // Day2 �� �ε�
        //SceneManager.LoadScene("Day2");
    }
}
