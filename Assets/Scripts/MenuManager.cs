using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager pauseInstance;

    enum What
    {
        Main,
        Pause,

    }

    [SerializeField] What what;
    [SerializeField] GameObject pauseParent;
    [SerializeField] GameObject pauseButton;

    private void Awake()
    {
        if (pauseInstance == null && what == What.Pause)
        {
            pauseInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (what == What.Pause)
        {
            Destroy(gameObject);
            return;
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        if (pauseParent != null && pauseParent.activeInHierarchy)
        {
            Pause();
        }
    }

    public void StartButton()
    {
        //AudioManager.instance.PlaySFX(AudioManager.instance.buttonClip);
        StartCoroutine(CameraController.instance.Transition(1));
    }

    public void Pause()
    {
        //AudioManager.instance.PlaySFX(AudioManager.instance.buttonClip);
        pauseParent.SetActive(!pauseParent.activeInHierarchy);
        pauseButton.SetActive(!pauseParent.activeInHierarchy);
        OptionsManager.instance.gameObject.SetActive(pauseParent.activeInHierarchy);

        PlayerController player = FindAnyObjectByType<PlayerController>();
        if (player != null )
        {
            player.canControl = !pauseParent.activeInHierarchy;
        }
    }

    public void MainMenu()
    {
        //AudioManager.instance.PlaySFX(AudioManager.instance.buttonClip);

        CameraController.instance.Transition(0);
        Time.timeScale = 1f;
    }
}
