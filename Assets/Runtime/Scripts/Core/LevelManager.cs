using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<Target> targets = new List<Target>();

    private static LevelManager instance;
    public static LevelManager Instance => instance;

    private void Awake()
    {
        SetThisInstance();
    }

    private void Update()
    {
        CheckActiveTargetsInLevel();
    }    

    private void SetThisInstance()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void CheckActiveTargetsInLevel()
    {
        if (targets.Count > 0 && AllTargetsAreActive())
        {
            LevelsSceneManager.Instance.GoToNextLevel();
        }
    }

    private bool AllTargetsAreActive()
    {
        bool targetsActives = true;
        foreach (Target target in targets)
        {
            if (!target.IsActive)
            {
                targetsActives = false;
                break;
            }
        }
        return targetsActives;
    }

    public void AddTarget(Target target)
    {
        if (target != null)
        {
            targets.Add(target);
        }
    }

    public void ClearAllTargets() 
    {
        targets.Clear();
    }

}
