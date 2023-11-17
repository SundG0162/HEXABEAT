using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoSingleton<LevelManager>
{
    public LevelSO levelSO;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
