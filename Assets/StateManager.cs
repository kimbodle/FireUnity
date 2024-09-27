using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{
    public static StateManager Instance { get; private set; }

    private GameManager gameManager;

    [SerializeField] private string currentTask;

    public DayController[] dayControllers; // DayController�� �迭�� ����

    private DayController currentDayController;

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
        gameManager = FindObjectOfType<GameManager>();

        // DayController���� �̸� ��Ȱ��ȭ
        foreach (var controller in dayControllers)
        {
            if (controller != null)
            {
                controller.gameObject.SetActive(false);
            }
        }
    }

    // Day�� ���� DayController Ȱ��ȭ
    public void ActivateDayController(int currentDay)
    {
        DeactivateAllDayControllers();

        if (currentDay >= 1 && currentDay <= dayControllers.Length)
        {
            var controller = dayControllers[currentDay - 1]; // �ε��� ����
            
            if (controller != null)
            {
                currentDayController = controller;
                controller.gameObject.SetActive(true);
                controller.Initialize(currentTask); 
            }
        }
    }

    // ��� DayController ��Ȱ��ȭ
    private void DeactivateAllDayControllers()
    {
        foreach (var controller in dayControllers)
        {
            if (controller != null)
            {
                controller.gameObject.SetActive(false);
            }
        }
    }

    // �� �̵� �� ���� ���� ó��
    public void LoadSubScene(string sceneName)
    {
        SaveCurrentState();
        SceneManager.LoadScene(sceneName);
    }

    public void SaveCurrentState()
    {
        if (gameManager != null)
        {
            gameManager.SaveGame();  // ���� ���¸� ����
        }
    }

    public void LoadStateFromFirestore()
    {
        if (gameManager != null)
        {
            gameManager.LoadGame();  // Firestore���� ���¸� �ҷ���
        }
    }

    public void GetCurrentTask(string task)
    {
        currentTask = task;
    }

    public DayController GetCurrentDayController()
    {
        return currentDayController;
    }
}
