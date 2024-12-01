using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] 
    private string[] levels;
    int actualLevel = 0;
    
    private static GameManager instance;

    void Awake()
    {
        // Sprawdź, czy istnieje już instancja GameManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Nie niszcz tego obiektu przy zmianie sceny
        }
        else
        {
            Destroy(gameObject); // Zniszcz duplikaty GameManagera
        }
    }

    void Start()
    {
        if (levels.Length == 0)
        {
            Debug.LogError("Level list is empty");
            return;
        }
        LoadLevel(levels[0]);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            restartLevel();
        }   
    }

    public void LoadLevel(string levelName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelName);
    }

    public void LoadNextLevel()
    {
        if (actualLevel < levels.Length - 1)
        {
            actualLevel++;
            LoadLevel(levels[actualLevel]);
        }
        else
        {
            Debug.Log("No more levels");
            // END GAME
        }
    }

    public void restartLevel()
    {
        LoadLevel(levels[actualLevel]);
    }
}
