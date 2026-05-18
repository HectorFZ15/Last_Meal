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
    public AudioClip musicMenu2;
    public AudioClip godMusic;
    public AudioClip devilMusic;

    private bool isInMenu = false;
    private AudioClip currentMusic;

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

    public AudioClip contractedSalida;
    public AudioClip empezarPartner;
    public AudioClip terminarPartner;
    public AudioClip entradaBien;
    public AudioClip entradaMal;
    public AudioClip finalSonida;
    public AudioClip muerteLadron;
    public AudioClip muerteAldeano;


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
            Rebobinar();
        }
    }

    public void MusicMenu()
    {
        isMusic = !isMusic;
        canvaMusic.SetActive(!canvaMusic.activeSelf);
        canvasScene.SetActive(!canvasScene.activeSelf);
        if (!canvaMusic.activeSelf)
        {
            if (SceneManager.GetActiveScene().name == "Ciudad")
            {
                EventSystem.current.SetSelectedGameObject(continueCityButton);
            }
            else
            {
                EventSystem.current.SetSelectedGameObject(butonLocal);
            }
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
            currentMusic = musicMenu2;
            // Elegir aleatoriamente entre las dos canciones del menú
            musicSource.clip = currentMusic;
            musicSource.Play();
        }
        else  if (sceneName == "Ciudad")
        {
            if (GameManager.mood == false)
            {
                currentMusic = godMusic;
                musicSource.clip = currentMusic;
            }
            else
            {
                currentMusic = devilMusic;
                musicSource.clip = currentMusic;
            }  
            musicSource.Play();
        }
        else
        {
            isInMenu = false;
            musicSource.Stop();
        }
    }

    private void Rebobinar()
    {
        // Cambia a la otra canción
        musicSource.clip = currentMusic;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    //SFX rápido último dia por deberes
    public void PlayFinalSFX()
    {
        SFXSource.PlayOneShot(finalSonida);
    }

    public void PlayContractedSalida()
    {
        SFXSource.PlayOneShot(contractedSalida);
    }

    public void PlayEmpezarPartner()
    {
        SFXSource.PlayOneShot(empezarPartner);
    }

    public void PlayTerminarPartner()
    {
        SFXSource.PlayOneShot(terminarPartner);
    }

    public void PlayEntradaBien()
    {
        SFXSource.PlayOneShot(entradaBien);
    }

    public void PlayEntradaMal()
    {
        SFXSource.PlayOneShot(entradaMal);
    }

    public void PlayMuerteLadron()
    {
        SFXSource.PlayOneShot(muerteLadron);
    }

    public void PlayMuerteAldeano()
    {
        SFXSource.PlayOneShot(muerteAldeano);
    }
}