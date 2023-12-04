public class LevelManager : MonoSingleton<LevelManager>
{
    public LevelSO levelSO;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
