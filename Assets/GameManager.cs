using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private int currentDay = 1;
    [SerializeField] public string currentTask = "Intro";
    [SerializeField] public Dictionary<string, bool> gameState = new Dictionary<string, bool>();


    private FirestoreController firestoreController;
    private FirebaseAuthController authController;
    private DayController currentDayController;

    private StateManager stateManager;

    private void Awake()
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
        // �̱��� �ν��Ͻ����� FirestoreController�� FirebaseAuthController ��������
        firestoreController = FirestoreController.Instance;
        authController = FirebaseAuthController.Instance;
        stateManager = StateManager.Instance;  // StateManager �ν��Ͻ� ã��

        SceneManager.sceneLoaded += OnSceneLoaded;
        LoadDayController(currentDay);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LoadDayController(currentDay);
    }

    public void CompleteTask()
    {
        if (currentDayController != null)
        {
            currentDayController.CompleteTask(currentTask);
        }

        if (currentDayController != null && currentDayController.IsDayComplete(currentTask))
        {
            currentDay++;
            SaveGame();
            LoadNextDay();
        }
        else
        {
            //SaveGame(); //�� Task�� ���������� �ڵ� ����
        }
    }

    public void SaveGame()
    {
        if (authController != null && !string.IsNullOrEmpty(authController.uid))
        {
            var currentState = currentDayController?.GetGameState() ?? new Dictionary<string, bool>();
            firestoreController.SaveGameState(currentDay, currentTask, currentState);
        }
    }

    public void InitializeGameState(int day, string task, Dictionary<string, bool> loadedGameState)
    {
        Debug.Log("�̴ϼ� ���� ������Ʈ �Լ� ��");
        currentDay = day;
        currentTask = task;
        // gameState ����
        gameState = loadedGameState;

        
        string sceneName = "Day" + currentDay + "Scene";
        Debug.Log(sceneName);
        SceneManager.LoadScene(sceneName);
        stateManager.ActivateDayController(currentDay);  // StateManager���� DayController Ȱ��ȭ
        currentDayController = GetCurrentDayController(); //���ڵ尡 ���� �ʿ��Ѱ�?

        // ���� �ε�Ǹ� OnSceneLoaded���� LoadDayController�� ȣ��˴ϴ�.
    }

    private DayController GetCurrentDayController()
    {
        // DayController�� �迭�� �����ϵ��� ����
        return stateManager.dayControllers[currentDay - 1]; // �ε��� ����
    }

    private void LoadDayController(int day)
    {
        stateManager.ActivateDayController(day);  // StateManager���� DayController Ȱ��ȭ
        stateManager.GetCurrentTask(currentTask);

        //string controllerName = "Day" + day + "Controller";
        //GameObject controllerObject = GameObject.Find(controllerName);
        //if (controllerObject != null)
        //{
        //    currentDayController = controllerObject.GetComponent<DayController>();
        //    if (currentDayController != null)
        //    {
        //        Debug.Log($"���� �Ŵ����� currentTask {currentTask}");
        //        currentDayController.Initialize(currentTask);
        //        Debug.Log("DayController�ε� ����"+ day);
        //     }
        //}
        //else
        //{
        //    Debug.Log("DayContoller ��ã��!");
        //}
    }

    private void LoadNextDay()
    {
        string nextSceneName = "Day" + currentDay + "Scene";
        InitializeGameState(currentDay, "Start", new Dictionary<string, bool>());
        SceneManager.LoadScene(nextSceneName);
    }

    public void LoadGame()
    {
        if (authController != null && !string.IsNullOrEmpty(authController.uid))
        {
            Debug.Log("�ε� ���� �Լ� �� ������");
            firestoreController.LoadGameState((day, task, loadedGameState) =>
            {
                InitializeGameState(day, task, loadedGameState);
            });
        }
    }
}
