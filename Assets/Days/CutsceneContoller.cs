using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneContoller : MonoBehaviour
{
    public Image sceneImage; // 씬에 표시될 이미지
    public TMP_Text dialogueText; // 대사 텍스트
    public Button choiceButton1; // 선택지 1 버튼
    public Button choiceButton2; // 선택지 2 버튼
    public AudioSource soundEffect; // 효과음

    private int dialogueIndex = 0;

    // 대사 배열
    private string[] dialogues = { "Koong", "WTF", "Holymoly","i'm outside" };
        //"쿵", "이게 무슨일이지?", "나가봐야겠어" };

    // 이미지 배열 (이미지들을 Unity 에디터에서 할당)
    public Sprite[] cutsceneImages;

    public float typingSpeed = 0.05f; // 타이핑 속도
    private bool isTyping = false; // 타이핑 진행 여부 확인용 변수

    void Start()
    {
        //soundEffect.Play(); // 첫 효과음 재생
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
            ShowNextDialogue(); // 타이핑이 끝났을 때만 다음 대사로 이동
        }
    }

    public void ShowNextDialogue()
    {
        if (dialogueIndex < dialogues.Length-1)
        {
            sceneImage.sprite = cutsceneImages[dialogueIndex]; // 이미지 변경
            StartCoroutine(TypeDialogue(dialogues[dialogueIndex])); // 타이핑 효과 적용
            dialogueIndex++;
        }
        else
        {
            ShowChoices(); // 대화가 끝나면 선택지 표시
        }
    }

    IEnumerator TypeDialogue(string dialogue)
    {
        dialogueText.text = ""; // 기존 텍스트 초기화
        isTyping = true; // 타이핑 중임을 표시
        foreach (char letter in dialogue.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed); // 한 글자씩 타이핑
        }
        isTyping = false; // 타이핑이 끝나면 표시
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
            // "나간다" 선택 시 이미지 및 대사 업데이트
            //sceneImage.sprite = cutsceneImages[3];
            //ShowNextDialogue를 위한 인덱스 추가
            sceneImage.sprite = cutsceneImages[dialogueIndex];
            StartCoroutine(TypeDialogue(dialogues[dialogueIndex]));
            //위 두 코드 뭔가 이상하다
            Debug.Log("선택지 1 클릭");
        }
        else
        {
            // "나가지 않는다" 선택 시 다른 동작
            Debug.Log("선택지 2 클릭");

        }
    }
}
