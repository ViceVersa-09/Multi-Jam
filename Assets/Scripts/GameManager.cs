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
    }

    private void Start()
    {
        player = FindAnyObjectByType<PlayerController>();
    }

    private void OnLevelWasLoaded(int level)
    {
        player = FindAnyObjectByType<PlayerController>();
    }
}
