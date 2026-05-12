using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("----------- Audio Source -----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("----------- Audio SFX -----------")]
    public AudioClip click; //Para cuando suene por ejemplo la muerte o cualquier otra cosa usar esto
    [Header("----------- Background Music -----------")]
    public AudioClip musicMenu1;
    public AudioClip musicMenu2;
    public AudioClip godMusic;
    public AudioClip devilMusic;

    private bool isInMenu = false;
    private AudioClip currentMenuClip;

    public GameObject canvaMusic;
    public GameObject canvasScene;
    public GameObject configButton;
    public GameObject controlsCanvas;

    //Button nav
    public GameObject exitButton;
    public GameObject butonLocal;
    public GameObject controlExistButton;
    public bool isConfig = false;
    public bool isMusic = false;

    //Pa controlar cuando entra y sale de escena
    public GameObject exitCityButton;
    public GameObject continueCityButton;
    public GameObject exitPauseCityButton;
    public GameObject configPauseCityButton;
    //pt2
    public GameObject playButton;
    public GameObject configButtonMenu;
    public GameObject highScoreButton;
    public GameObject downScoreButton;
    public GameObject exitMenuButton;
    public GameObject returnUrButton;
    public GameObject returnDrButton;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        canvasScene = GameObject.Find("Canvas");
        configButton = GameObject.Find("ConfigButton");
        butonLocal = GameObject.Find("Play");
        controlExistButton = GameObject.Find("ExitControls");

        configButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(MusicMenu);

}

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        CambiarMusica(SceneManager.GetActiveScene().name);
        canvaMusic.SetActive(false);
        controlsCanvas.SetActive(false);
        isConfig = false;
        isMusic = false;
    }

    private void Update()
    {
        if (isInMenu && !musicSource.isPlaying)
        {
            AlternarMusicaMenu();
        }
    }

    public void MusicMenu()
    {
        isMusic = !isMusic;
        canvaMusic.SetActive(!canvaMusic.activeSelf);
        canvasScene.SetActive(!canvasScene.activeSelf);
        if (!canvaMusic.activeSelf)
        {
            EventSystem.current.SetSelectedGameObject(butonLocal);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(exitButton);
        }
    }

    public void ControlsMenu()
    {
        isConfig = !isConfig;
        controlsCanvas.SetActive(!controlsCanvas.activeSelf);
        canvaMusic.SetActive(!canvaMusic.activeSelf);
        if (!controlsCanvas.activeSelf)
        {
            EventSystem.current.SetSelectedGameObject(exitButton);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(controlExistButton);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CambiarMusica(scene.name);
        canvasScene = GameObject.Find("Canvas");
        configButton = GameObject.Find("ConfigButton");
        butonLocal = GameObject.Find("Play");

        if (scene.name == "MenuPrincipal")
        {
            playButton = GameObject.Find("Play");
            configButtonMenu = GameObject.Find("ConfigButton");
            highScoreButton = GameObject.Find("UpScore");
            downScoreButton = GameObject.Find("DownScore");
            exitMenuButton = GameObject.Find("Exit");
            returnUrButton = GameObject.Find("ReturnUr");
            returnDrButton = GameObject.Find("ReturnDr");

            playButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => PlaySFX(click));
            configButtonMenu.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => PlaySFX(click));
            highScoreButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => PlaySFX(click));
            downScoreButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => PlaySFX(click));
            exitMenuButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => PlaySFX(click));
            returnUrButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => PlaySFX(click));
            returnDrButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => PlaySFX(click));   
        }

        if (scene.name == "Ciudad")
        {
            //FALTA HACER LO MISMO PARA EL INICIO
                exitCityButton = GameObject.Find("BackMenu");
                continueCityButton = GameObject.Find("Continue");
                exitPauseCityButton = GameObject.Find("Exit");
                configPauseCityButton = GameObject.Find("ConfigButton");
                exitCityButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => PlaySFX(click));
                continueCityButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => PlaySFX(click));
                exitPauseCityButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => PlaySFX(click));
                configPauseCityButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => PlaySFX(click));
            canvasScene.SetActive(false);  
        }
        configButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(MusicMenu);
       

    }

    private void CambiarMusica(string sceneName)
    {
        if (sceneName == "MenuPrincipal")
        {
            isInMenu = true;
            // Elegir aleatoriamente entre las dos canciones del menú
            currentMenuClip = Random.Range(0, 2) == 0 ? musicMenu1 : musicMenu2;
            musicSource.clip = currentMenuClip;
            musicSource.Play();
        }
        else  if (sceneName == "Ciudad")
        {
            if (GameManager.mood == false)
            {
                musicSource.clip = godMusic;
            }
            else
            {
                musicSource.clip = devilMusic;
            }  
            musicSource.Play();
        }
        else
        {
            isInMenu = false;
            musicSource.Stop();
        }
    }

    private void AlternarMusicaMenu()
    {
        // Cambia a la otra canción
        currentMenuClip = (currentMenuClip == musicMenu1) ? musicMenu2 : musicMenu1;
        musicSource.clip = currentMenuClip;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}