using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneContoller : MonoBehaviour
{
    public Image sceneImage; // ���� ǥ�õ� �̹���
    public TMP_Text dialogueText; // ��� �ؽ�Ʈ
    public Button choiceButton1; // ������ 1 ��ư
    public Button choiceButton2; // ������ 2 ��ư
    public AudioSource soundEffect; // ȿ����

    private int dialogueIndex = 0;

    // ��� �迭
    private string[] dialogues = { "Koong", "WTF", "Holymoly","i'm outside" };
        //"��", "�̰� ����������?", "�������߰ھ�" };

    // �̹��� �迭 (�̹������� Unity �����Ϳ��� �Ҵ�)
    public Sprite[] cutsceneImages;

    public float typingSpeed = 0.05f; // Ÿ���� �ӵ�
    private bool isTyping = false; // Ÿ���� ���� ���� Ȯ�ο� ����

    void Start()
    {
        //soundEffect.Play(); // ù ȿ���� ���
        ShowNextDialogue();
        choiceButton1.gameObject.SetActive(false);
        choiceButton2.gameObject.SetActive(false);

        choiceButton1.onClick.AddListener(OnClickButton1);
        choiceButton2.onClick.AddListener(OnClickButton2);
    }

    public void OnImageClick()
    {
        if (!isTyping)
        {
            ShowNextDialogue(); // Ÿ������ ������ ���� ���� ���� �̵�
        }
    }

    public void ShowNextDialogue()
    {
        if (dialogueIndex < dialogues.Length-1)
        {
            sceneImage.sprite = cutsceneImages[dialogueIndex]; // �̹��� ����
            StartCoroutine(TypeDialogue(dialogues[dialogueIndex])); // Ÿ���� ȿ�� ����
            dialogueIndex++;
        }
        else
        {
            ShowChoices(); // ��ȭ�� ������ ������ ǥ��
        }
    }

    IEnumerator TypeDialogue(string dialogue)
    {
        dialogueText.text = ""; // ���� �ؽ�Ʈ �ʱ�ȭ
        isTyping = true; // Ÿ���� ������ ǥ��
        foreach (char letter in dialogue.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed); // �� ���ھ� Ÿ����
        }
        isTyping = false; // Ÿ������ ������ ǥ��
    }

    public void ShowChoices()
    {
        choiceButton1.gameObject.SetActive(true);
        choiceButton2.gameObject.SetActive(true);
    }

    public void OnClickButton1()
    {
        OnChoiceClick(1);
    }
    
    public void OnClickButton2()
    {
        OnChoiceClick(2);
    }

    public void OnChoiceClick(int choice)
    {
        if (choice == 1)
        {
            // "������" ���� �� �̹��� �� ��� ������Ʈ
            //sceneImage.sprite = cutsceneImages[3];
            //ShowNextDialogue�� ���� �ε��� �߰�
            sceneImage.sprite = cutsceneImages[dialogueIndex];
            StartCoroutine(TypeDialogue(dialogues[dialogueIndex]));
            //�� �� �ڵ� ���� �̻��ϴ�
            Debug.Log("������ 1 Ŭ��");
        }
        else
        {
            // "������ �ʴ´�" ���� �� �ٸ� ����
            Debug.Log("������ 2 Ŭ��");

        }
    }
}
