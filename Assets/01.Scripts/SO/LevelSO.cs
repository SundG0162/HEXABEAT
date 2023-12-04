using UnityEngine;

public enum Difficulty
{
    Easy,
    Normal,
    Hard,
    Insane
}
[CreateAssetMenu(menuName = "SO/LevelSO")]
public class LevelSO : ScriptableObject
{
    public string name;
    public string artist;
    public int score;
    public int prevScore;   
    public string combo;
    public int prevCombo;
    public int totalNote;
    public int[] judges = new int[5];
    public int[] prevJudges = new int[5];
    public AudioClip bgm;
    public Sprite bga;
    public Difficulty difficulty;
}
