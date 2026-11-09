using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    public static OptionsManager instance;

    [Header("Audio")]
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sFXSlider;

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
        masterSlider.value = PlayerPrefs.GetFloat("Master", 1);
        musicSlider.value = PlayerPrefs.GetFloat("Music", 1);
        sFXSlider.value = PlayerPrefs.GetFloat("SFX", 1);
        GetAudio();
    }

    public void SetAudio()
    {
        PlayerPrefs.SetFloat("Master", masterSlider.value);
        PlayerPrefs.SetFloat("Music", musicSlider.value);
        PlayerPrefs.SetFloat("SFX", sFXSlider.value);
    }

    void GetAudio()
    {
        masterSlider.value = PlayerPrefs.GetFloat("Master", 1);
        musicSlider.value = PlayerPrefs.GetFloat("Music", 1);
        sFXSlider.value = PlayerPrefs.GetFloat("SFX", 1);
    }
}
