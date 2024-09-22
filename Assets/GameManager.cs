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
            DontDestroyOnLoad(gameObject); // 씬 전환 시 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // 싱글톤 인스턴스에서 FirestoreController와 FirebaseAuthController 가져오기
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
            currentTask = 1; //다음날로 넘어갔을경우 다시 Task를 1로 초기화
            SaveGame();
            LoadNextDay();
        }
        else
        {
            //SaveGame(); //각 Task가 끝날때마다 자동 저장
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

        // 씬이 로드되면 OnSceneLoaded에서 LoadDayController가 호출됩니다.
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
                Debug.Log("DayController로드 성공"+ day);
             }
        }
        else
        {
            Debug.Log("DayContoller 못찾음!");
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
