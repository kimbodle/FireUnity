using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissionTimer : MonoBehaviour
{
    public float timeLimit = 120f; // ���� �ð� (��)
    public TMP_Text timerText; // Ÿ�̸� UI
    public bool isMissionActive = false; // �̼� Ȱ��ȭ ����

    private TextMeshProUGUI textMeshPro; 
    private void Start()
    {
        textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
        textMeshPro.gameObject.SetActive(false);
    }
    void Update()
    {
        if (isMissionActive)
        {
            textMeshPro.gameObject.SetActive(true);
            timeLimit -= Time.deltaTime;
            UpdateTimerUI();

            if (timeLimit <= 0)
            {
                isMissionActive = false;
                MissionFailed();
            }
        }
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(timeLimit / 60f);
        int seconds = Mathf.FloorToInt(timeLimit % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void MissionFailed()
    {
        Debug.Log("Mission Failed");
        // ���⼭ �̼� ���� ó�� (���� ���� ȭ�� �Ǵ� ��õ� ��ư ��)
    }

    public void CompleteMission()
    {
        isMissionActive = false;
        Debug.Log("Mission Complete");
        // �̼� �Ϸ� ó��
    }
}
