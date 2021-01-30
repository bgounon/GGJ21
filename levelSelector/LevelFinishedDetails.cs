using System;

[Serializable]
public struct LevelFinishedDetails
{
    public string levelName;
    public int score;

    public LevelFinishedDetails(string levelName, int score)
    {
        this.levelName = levelName;
        this.score = score;
    }
}