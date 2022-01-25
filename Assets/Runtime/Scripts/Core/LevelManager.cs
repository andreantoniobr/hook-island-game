using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameLevel
{
    public int LevelNumberID;
    public Scene LevelScene;
    public bool IsFinished;
}

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameLevel currentGameLevel;
    [SerializeField] private bool currentGameLevelIsFinished;
    [SerializeField] private GameLevel[] gameLevels;

    //TODO: VERIFY WITH EVENTS
    private void Update()
    {
        VerifyIfCurrentLevelFinished();
    }

    private void VerifyIfCurrentLevelFinished()
    {
        if (currentGameLevel.IsFinished)
        {
            PassToNextLevel();            
        }
    }

    private void PassToNextLevel()
    {
        GameLevel gameLevel = GetNextLevel();
        if (gameLevel != null && gameLevel.LevelScene != null)
        {
            currentGameLevel = gameLevel;
            SceneManager.LoadScene(gameLevel.LevelScene.name);
        }
    }

    private GameLevel GetNextLevel()
    {
        GameLevel nextLevel = null;
        foreach (GameLevel gameLevel in gameLevels)
        {
            if (gameLevel.LevelNumberID == currentGameLevel.LevelNumberID + 1 && gameLevel.IsFinished == false)
            {
                nextLevel = gameLevel;
                break;
            }
        }
        return nextLevel;
    }
}
