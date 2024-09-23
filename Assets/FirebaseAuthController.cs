using Firebase.Auth;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class FirebaseAuthController : MonoBehaviour
{
    public static FirebaseAuthController Instance { get; private set; } // �̱��� �ν��Ͻ�

    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public TMP_Text messageText;
    public TMP_Text UserUidText;

    private FirestoreController firestoreController;
    public GameManager gameManager;

    private FirebaseAuth auth;
    public FirebaseUser User {  get; set; }

    private string message = "";
    public string uid = "";
    private bool isMessageUpdated = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        InitializeFirebase();
        messageText.text = string.Empty;

        /*// �α��� ���� Ȯ��
        if (auth.CurrentUser != null)
        {
            // ����ڰ� �̹� �α��� �Ǿ� ������ �ش� ���¸� �����ϵ��� �ϰų� �α׾ƿ�
            Debug.Log("User is already logged in: " + auth.CurrentUser.Email);
        }
        else
        {
            // ����ڰ� �α������� ���� ���¶�� �ʱ� ���·� ����
            Debug.Log("No user is logged in.");
        }*/
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
        firestoreController = FindObjectOfType<FirestoreController>();
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != User)
        {
            bool signedIn = User != auth.CurrentUser && auth.CurrentUser != null && auth.CurrentUser.IsValid();
            if (!signedIn && User != null)
            {
                Debug.Log("Signed out " + User.UserId);
                message = "Signed out: " + User.UserId;
                uid = "";
                isMessageUpdated = true;
            }
            User = auth.CurrentUser;
            if (signedIn)
            {
                Debug.Log("Signed in " + User.UserId);
                message = "Signed in: " + User.Email;
                uid = User.UserId;
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

                //�α��� �� �ҷ�����
                LoadGameState();
            });
    }

    public void Logout()
    {
        auth.SignOut();
        User = null;
    }

    void OnApplicationQuit()
    {
        Logout(); // �� ���� �� �α׾ƿ� ó��
        
    }

    public string GetReturnUid()
    {
        return uid;
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
        firestoreController.LoadGameState(OnGameStateLoaded); //�Ʒ� �Լ��� �Ķ���ͷ� ���� ����
    }

    private void OnGameStateLoaded(int currentDay, string currentTask, Dictionary<string, bool> gameState) //���ӸŴ����� ���ӻ��� ������Ʈ �Լ�ȣ��
    {
        Debug.Log("���� Day: " + currentDay + ", ���� Task: " + currentTask);
        if (gameManager != null)
        {
            gameManager.InitializeGameState(currentDay, currentTask, gameState);
        }
    }

    public bool IsLoggedIn()
    {
        return User != null; // User ������Ƽ�� Ȯ��
    }
}
