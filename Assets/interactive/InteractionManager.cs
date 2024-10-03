using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractionManager : MonoBehaviour
{
    private Canvas canvasObj;

    public GameObject examineButtonPrefab; // 복사할 버튼 프리팹
    public GameObject interactionTextPrefab; // 복사할 메시지 텍스트 프리팹

    private GameObject examineButton; // 살펴보기 버튼
    private TMP_Text interactionText; // 상호작용 메시지 텍스트

    private Button buttonComponent;

    private IInteractable currentInteractable; // 현재 상호작용 가능한 오브젝트

    public bool isInteraction = true;

    private void Start()
    {
        canvasObj = FindAnyObjectByType<Canvas>(); //현재 하이라키에서 캔버스객체를 찾음
        CreateUIElements();
        examineButton.SetActive(false);
        interactionText.gameObject.SetActive(false);
        
    }

    private void CreateUIElements()
    {
        // Canvas 생성
        /*GameObject canvasObj = new GameObject("InteractionCanvas");
        Canvas canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasObj.AddComponent<CanvasScaler>();
        canvasObj.AddComponent<GraphicRaycaster>();*/

        // 버튼 인스턴스화
        examineButton = Instantiate(examineButtonPrefab, canvasObj.transform); //부모인 캔버스 객체아래에 인스턴스 생성
        buttonComponent = examineButton.GetComponent<Button>();
        buttonComponent.onClick.AddListener(OnInteractButtonClicked);
        RectTransform buttonRect = examineButton.GetComponent<RectTransform>(); //위치정보
        buttonRect.anchoredPosition = new Vector2(0, -50); // 버튼 위치 설정

        // 텍스트 인스턴스화
        GameObject textObj = Instantiate(interactionTextPrefab, canvasObj.transform);
        interactionText = textObj.GetComponent<TMP_Text>();
        RectTransform textRect = interactionText.GetComponent<RectTransform>();
        textRect.anchoredPosition = new Vector2(0, 50); // 텍스트 위치 설정
    }

    public void ShowInteractionUI(string message)
    {
        Debug.Log("ShowInteractionUI 발동!");
        examineButton.SetActive(true);
        interactionText.gameObject.SetActive(true);
        interactionText.text = message;
    }

    public void HideInteractionUI()
    {
        examineButton.SetActive(false);
        interactionText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && currentInteractable != null)
        {
            //currentInteractable.OnInteract();
            // E 키가 눌리면 버튼 클릭 효과를 시뮬레이션
            buttonComponent.onClick.Invoke();
        }
    }

    private void OnInteractButtonClicked()
    {
        if (currentInteractable != null)
        {
            currentInteractable.OnInteract();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter2D 발동!");
        if (isInteraction)
        {
            IInteractable interactable = collision.GetComponent<IInteractable>();
            Debug.Log(interactable != null ? "상호작용 가능한 오브젝트 발견" : "상호작용 가능한 오브젝트 없음");
            if (interactable != null)
            {
                currentInteractable = interactable;
                ShowInteractionUI(interactable.GetInteractionMessage());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isInteraction)
        {
            Debug.Log("OnTriggerExit2D 발동!");
            IInteractable interactable = collision.GetComponent<IInteractable>();
            if (interactable != null && interactable == currentInteractable)
            {
                HideInteractionUI();
                currentInteractable = null;
            }
        }
    }
}
