using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractionManager : MonoBehaviour
{
    private Canvas canvasObj;

    public GameObject examineButtonPrefab; // ������ ��ư ������
    public GameObject interactionTextPrefab; // ������ �޽��� �ؽ�Ʈ ������

    private GameObject examineButton; // ���캸�� ��ư
    private TMP_Text interactionText; // ��ȣ�ۿ� �޽��� �ؽ�Ʈ

    private Button buttonComponent;

    private IInteractable currentInteractable; // ���� ��ȣ�ۿ� ������ ������Ʈ

    public bool isInteraction = true;

    private void Start()
    {
        canvasObj = FindAnyObjectByType<Canvas>(); //���� ���̶�Ű���� ĵ������ü�� ã��
        CreateUIElements();
        examineButton.SetActive(false);
        interactionText.gameObject.SetActive(false);
        
    }

    private void CreateUIElements()
    {
        // Canvas ����
        /*GameObject canvasObj = new GameObject("InteractionCanvas");
        Canvas canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasObj.AddComponent<CanvasScaler>();
        canvasObj.AddComponent<GraphicRaycaster>();*/

        // ��ư �ν��Ͻ�ȭ
        examineButton = Instantiate(examineButtonPrefab, canvasObj.transform); //�θ��� ĵ���� ��ü�Ʒ��� �ν��Ͻ� ����
        buttonComponent = examineButton.GetComponent<Button>();
        buttonComponent.onClick.AddListener(OnInteractButtonClicked);
        RectTransform buttonRect = examineButton.GetComponent<RectTransform>(); //��ġ����
        buttonRect.anchoredPosition = new Vector2(0, -50); // ��ư ��ġ ����

        // �ؽ�Ʈ �ν��Ͻ�ȭ
        GameObject textObj = Instantiate(interactionTextPrefab, canvasObj.transform);
        interactionText = textObj.GetComponent<TMP_Text>();
        RectTransform textRect = interactionText.GetComponent<RectTransform>();
        textRect.anchoredPosition = new Vector2(0, 50); // �ؽ�Ʈ ��ġ ����
    }

    public void ShowInteractionUI(string message)
    {
        Debug.Log("ShowInteractionUI �ߵ�!");
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
            // E Ű�� ������ ��ư Ŭ�� ȿ���� �ùķ��̼�
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
        Debug.Log("OnTriggerEnter2D �ߵ�!");
        if (isInteraction)
        {
            IInteractable interactable = collision.GetComponent<IInteractable>();
            Debug.Log(interactable != null ? "��ȣ�ۿ� ������ ������Ʈ �߰�" : "��ȣ�ۿ� ������ ������Ʈ ����");
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
            Debug.Log("OnTriggerExit2D �ߵ�!");
            IInteractable interactable = collision.GetComponent<IInteractable>();
            if (interactable != null && interactable == currentInteractable)
            {
                HideInteractionUI();
                currentInteractable = null;
            }
        }
    }
}
