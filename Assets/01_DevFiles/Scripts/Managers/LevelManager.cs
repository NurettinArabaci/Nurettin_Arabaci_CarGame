using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelManager : MonoSingleton<LevelManager>
{
    [SerializeField] List<GameObject> _levels = new List<GameObject>();

    const string Levels = "Levels";

    int levelAmount = 1;

    int ActiveLevel
    {
        get
        {
            return levelAmount % _levels.Count;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        LoadAllLevels();
        LoadLevel();
    }

    private void LoadAllLevels()
    {
        _levels = Resources.LoadAll<GameObject>(Levels).ToList();
    }

    private void OpenActiveLevel()
    {
        Instantiate(_levels[ActiveLevel], transform);
    }

    public void LoadLevel()
    {
        DestroyOtherLevels();
        OpenActiveLevel();
    }

    public void NextLevel()
    {
        levelAmount++;
        LoadLevel();
    }



    void DestroyOtherLevels()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
