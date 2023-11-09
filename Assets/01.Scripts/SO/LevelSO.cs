using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/LevelSO")]
public class LevelSO : ScriptableObject
{
    public string name;
    public string artist;
    public int score;
    public string combo;
    public int totalNote;
    public int[] judges = new int[5];
    public AudioSource bgm;
    public Sprite bga;
    public string path;
}
