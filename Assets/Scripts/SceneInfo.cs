using UnityEngine;

[CreateAssetMenu(fileName = "SceneInfo", menuName = "Persistence", order = 0)]
public class SceneInfo : ScriptableObject
{
    [SerializeField] private bool levelOneAccessed = false;
    [SerializeField] private bool levelTwoAccessed = false;
    [SerializeField] private bool levelThreeAccessed = false;

    [SerializeField] public float mouseSens;

    public void SetLevelOneAccessed(bool accessed)
    {
        levelOneAccessed = accessed;
    }
    public void SetLevelTwoAccessed(bool accessed)
    {
        levelTwoAccessed = accessed;
    }
    public void SetLevelThreeAccessed(bool accessed)
    {
        levelThreeAccessed = accessed;
    }

    public bool GetLevelOneAccess()
    {
        return levelOneAccessed;
    }
    public bool GetLevelTwoAccess()
    {
        return levelTwoAccessed;
    }
    public bool GetLevelThreeAcess()
    {
        return levelThreeAccessed;
    }
}
