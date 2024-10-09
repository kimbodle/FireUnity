using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance { get; private set; }

    public MapRegion[] mapRegions; // ��� ���� ������ �迭

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

    private void Start()
    {
        UpdateMapRegions();
    }

    // ��� ���� ������ ���� ������Ʈ
    public void UpdateMapRegions()
    {
        foreach (var region in mapRegions)
        {
            region.UpdateRegionStatus();
        }
    }

    // Ư�� ���� Ȱ��ȭ
    public void UnlockRegion(string regionName)
    {
        foreach (var region in mapRegions)
        {
            if (region.regionName == regionName)
            {
                region.Unlock();
                break;
            }
        }
    }

    // Ư�� ���� ��Ȱ��ȭ
    public void LockRegion(string regionName)
    {
        foreach (var region in mapRegions)
        {
            if (region.regionName == regionName)
            {
                region.Lock();
                break;
            }
        }
    }
}
