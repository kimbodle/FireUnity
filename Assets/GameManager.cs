using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private FirestoreController firestoreController;
    private FirebaseAuthController authController;

    [SerializeField]
    private int currentDay = 1;
    [SerializeField]
    private int currentTask = 0;
    private DayController currentDayController;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �� �ı����� �ʵ��� ����
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

        currentTask++;
        if (currentDayController != null && currentDayController.IsDayComplete(currentTask))
        {
            currentDay++;
            currentTask = 1; //�������� �Ѿ����� �ٽ� Task�� 1�� �ʱ�ȭ
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
            var gameState = currentDayController?.GetGameState() ?? new Dictionary<string, bool>();
            firestoreController.SaveGameState(currentDay, currentTask, gameState);
        }
    }

    public void InitializeGameState(int day, int task, Dictionary<string, bool> gameState)
    {
        currentDay = day;
        currentTask = task;

        string sceneName = "Day" + currentDay + "Scene";
        SceneManager.LoadScene(sceneName);

        // ���� �ε�Ǹ� OnSceneLoaded���� LoadDayController�� ȣ��˴ϴ�.
    }

    private void LoadDayController(int day)
    {
        string controllerName = "Day" + day + "Controller";
        GameObject controllerObject = GameObject.Find(controllerName);
        if (controllerObject != null)
        {
            currentDayController = controllerObject.GetComponent<DayController>();
            if (currentDayController != null)
            {
                currentDayController.Initialize(currentTask);
                Debug.Log("DayController�ε� ����"+ day);
             }
        }
        else
        {
            Debug.Log("DayContoller ��ã��!");
        }
    }

    private void LoadNextDay()
    {
        string nextSceneName = "Day" + currentDay + "Scene";
        SceneManager.LoadScene(nextSceneName);
    }

    public void LoadGame()
    {
        if (authController != null && !string.IsNullOrEmpty(authController.uid))
        {
            firestoreController.LoadGameState((day, task, gameState) =>
            {
                InitializeGameState(day, task, gameState);
            });
        }
    }
}
