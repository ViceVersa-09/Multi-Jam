using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int luckyNumber;

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
}
