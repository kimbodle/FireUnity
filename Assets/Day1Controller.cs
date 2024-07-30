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
        // 게임 진행 로직...
        userId = firebaseAuthController.uid;

        // Day1을 완료했으므로 Day2로 업데이트
        firestoreController.SaveProgress(userId, 2);

        Debug.Log("Day1 완료");
        // Day2 씬 로드
        //SceneManager.LoadScene("Day2");
    }
}
