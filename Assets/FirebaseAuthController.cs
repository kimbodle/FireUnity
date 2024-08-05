using Firebase.Auth;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class FirebaseAuthController : MonoBehaviour
{
    public static FirebaseAuthController Instance { get; private set; } // 싱글톤 인스턴스

    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public TMP_Text messageText;
    public TMP_Text UserUidText;

    public FirestoreController firestoreController;
    public GameManager gameManager;

    private FirebaseAuth auth;
    private FirebaseUser user;

    private string message = "";
    public string uid = "";
    private bool isMessageUpdated = false;

    void Awake()
    {
        // 싱글톤 패턴 구현
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject); // 중복 인스턴스가 생성되면 파괴
        }
    }

    void Start()
    {
        InitializeFirebase();
        messageText.text = string.Empty;
    }

    void Update()
    {
        if (isMessageUpdated)
        {
            UpdateUI();
            isMessageUpdated = false;
        }
    }

    void OnDestroy()
    {
        if (auth != null)
        {
            auth.StateChanged -= AuthStateChanged;
            auth = null;
        }
    }

    void InitializeFirebase()
    {
        auth = FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null && auth.CurrentUser.IsValid();
            if (!signedIn && user != null)
            {
                Debug.Log("Signed out " + user.UserId);
                message = "Signed out: " + user.UserId;
                uid = "";
                isMessageUpdated = true;
            }
            user = auth.CurrentUser;
            if (signedIn)
            {
                Debug.Log("Signed in " + user.UserId);
                message = "Signed in: " + user.Email;
                uid = user.UserId;
                isMessageUpdated = true;
                LoadGameState();
            }
        }
    }

    public void Register()
    {
        string email = emailInput.text;
        string password = passwordInput.text;

        auth.CreateUserWithEmailAndPasswordAsync(email, password)
            .ContinueWith(task => {
                if (task.IsCanceled || task.IsFaulted)
                {
                    message = "Sign up failed: " + task.Exception?.Message;
                    isMessageUpdated = true;
                    return;
                }

                AuthResult authResult = task.Result;
                FirebaseUser newUser = authResult.User;
                message = "Sign up successful: " + newUser.Email;
                uid = newUser.UserId;
                isMessageUpdated = true;
            });
    }

    public void Login()
    {
        string email = emailInput.text;
        string password = passwordInput.text;

        auth.SignInWithEmailAndPasswordAsync(email, password)
            .ContinueWith(task => {
                if (task.IsCanceled || task.IsFaulted)
                {
                    message = "Login failed: " + task.Exception?.Message;
                    isMessageUpdated = true;
                    return;
                }

                AuthResult authResult = task.Result;
                FirebaseUser newUser = authResult.User;
                message = "Login successful: " + newUser.Email;
                uid = newUser.UserId;
                isMessageUpdated = true;
                LoadGameState();
            });
    }

    public void Logout()
    {
        auth.SignOut();
    }

    private void UpdateUI()
    {
        messageText.text = message;
        UserUidText.text = uid;
        emailInput.text = "";
        passwordInput.text = "";
    }

    private void LoadGameState()
    {
        firestoreController.LoadGameState(uid, OnGameStateLoaded); //아래 함수를 파라미터로 같이 전달
    }

    private void OnGameStateLoaded(int currentDay, int currentTask, Dictionary<string, bool> gameState) //게임매니저의 게임상태 업데이트 함수호출
    {
        Debug.Log("현재 Day: " + currentDay + ", 현재 Task: " + currentTask);
        if (gameManager != null)
        {
            gameManager.InitializeGameState(currentDay, currentTask, gameState);
        }
    }
}
