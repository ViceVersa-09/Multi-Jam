using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] GameObject selectionObj;

    public int luckyNumber;

    PlayerController player;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        player = FindAnyObjectByType<PlayerController>();

        Selection();
    }

    private void OnLevelWasLoaded(int level)
    {
        player = FindAnyObjectByType<PlayerController>();

        Selection();
    }

    void Selection()
    {
        player.canControl = false;

        selectionObj.SetActive(true);
    }

    public void SetNumber(int number)
    {
        instance.luckyNumber = number;
        player.canControl = true;

        selectionObj.SetActive(false);
    }
}
