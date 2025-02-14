using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private int currentDay = 1;
    public string currentTask = "Intro";
    public Dictionary<string, bool> gameState = new Dictionary<string, bool>();

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

        //싱글톤 인스턴스에서 가져오기
        firestoreController = FirestoreController.Instance;
        authController = FirebaseAuthController.Instance;
        stateManager = StateManager.Instance;

        SceneManager.sceneLoaded += OnSceneLoaded;
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
            //SaveGame(); //각 Task가 끝날때마다 자동 저장
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

    //로그인시 실행
    public void InitializeGameState(int day, string task, Dictionary<string, bool> loadedGameState)
    {
        //Debug.Log("InitializeGameState 안");
        currentDay = day;
        currentTask = task;
        gameState = loadedGameState;

        
        string sceneName = "Day" + currentDay + "Scene";
        SceneManager.LoadScene(sceneName);
        // StateManager에서 DayController 활성화
        stateManager.ActivateDayController(currentDay);
        currentDayController = GetCurrentDayController();

        // 씬이 로드되면 OnSceneLoaded에서 LoadDayController 호출
    }


    //로그인시 실행
    public void LoadGame()
    {
        if (authController != null && !string.IsNullOrEmpty(authController.uid))
        {
            //Debug.Log("LoadGame 안 if");
            firestoreController.LoadGameState((day, task, loadedGameState) =>
            {
                InitializeGameState(day, task, loadedGameState);
            });
        }
    }

    //씬 전환시 실행
    private void LoadDayController(int currentDay)
    {
        // StateManager에서 DayController 활성화
        stateManager.ActivateDayController(currentDay);
        stateManager.UpdateStateCurrentTask(currentTask);
    }

    private void LoadNextDay()
    {
        string nextSceneName = "Day" + currentDay + "Scene";
        currentTask = "Intro";
        gameState.Clear(); //gameState[nextSceneName] = true;
        //InitializeGameState(currentDay, currentTask, gameState);
        SaveGame();
        SceneManager.LoadScene(nextSceneName);
    }

    private DayController GetCurrentDayController()
    {
        // DayController를 배열로 관리하도록 수정
        return stateManager.dayControllers[currentDay - 1]; // 인덱스 조정
    }
    public int GetCurrentDay()
    {
        return currentDay;
    }
}
