using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public PlayerCharacter Player { get; private set; }
    public LocalizationManager LocalizationManager { get; private set; }
    public AddressableLoader AddressableLoader { get; private set; }

    [Header("Runtime References")]
    public NarrativeManager NarrativeManager { get; private set; }
    public CombatManager CombatManager { get; private set; }

    [Header("Addresses")]
    [SerializeField] private string storyGraphAddress = "story_graph_main";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LocalizationManager = GetComponentInChildren<LocalizationManager>();
        AddressableLoader = GetComponentInChildren<AddressableLoader>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void StartGame()
    {
        CreateDefaultPlayer();
        SceneManager.LoadScene("Game");
    }

    public void StartStoryFlow()
    {
        if (AddressableLoader == null)
        {
            Debug.LogError("AddressableLoader is missing.");
            return;
        }

        if (NarrativeManager == null)
        {
            Debug.LogError("NarrativeManager is missing in Game scene.");
            return;
        }

        AddressableLoader.LoadStoryGraphAsync(storyGraphAddress, graph =>
        {
            NarrativeManager.LoadGraph(graph);
            NarrativeManager.StartStory();
        });
    }

    public void ChangeLanguage(string localeCode)
    {
        LocalizationManager?.SetLocale(localeCode);
    }

    private void CreateDefaultPlayer()
    {
        CharacterStats stats = new CharacterStats();
        stats.InitializeDefaults(14, 12, 13, 10, 10, 8);
        Player = new PlayerCharacter("Aric", 1, stats);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Game")
        {
            NarrativeManager = FindFirstObjectByType<NarrativeManager>();
            CombatManager = FindFirstObjectByType<CombatManager>();

            if (NarrativeManager != null)
            {
                NarrativeManager.OnCombatRequested -= HandleCombatRequested;
                NarrativeManager.OnCombatRequested += HandleCombatRequested;
            }

            if (CombatManager != null)
            {
                CombatManager.OnCombatEnded -= HandleCombatEnded;
                CombatManager.OnCombatEnded += HandleCombatEnded;
            }
        }
    }

    private void HandleCombatRequested(string enemyAddress)
    {
        AddressableLoader.LoadEnemyAsync(enemyAddress, enemyData =>
        {
            EnemyCharacter enemy = enemyData.CreateRuntimeEnemy();
            CombatManager.StartCombat(Player, enemy);
        });
    }

    private void HandleCombatEnded(bool playerWon)
    {
        if (playerWon)
        {
            NarrativeManager.ResumeAfterCombat();
            return;
        }

        StoryNodeData defeatEnding = new StoryNodeData
        {
            NodeId = "ending_defeat",
            NodeType = NodeType.Ending,
            TextKey = "ending_defeat",
            IsFinalNode = true
        };

        NarrativeManager.ForceEnding(defeatEnding);
    }
}