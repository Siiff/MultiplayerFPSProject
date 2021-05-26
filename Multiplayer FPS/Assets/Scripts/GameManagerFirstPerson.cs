using Cinemachine.Examples;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManagerFirstPerson : MonoBehaviour
{
    public string level, goToScene;
    public bool gamePaused = false;
    public bool playerUnable = false;
    public GameObject mainPanel, pausePanel, endPanel, retryPanel;

    private GameObject rescueZone;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rescueZone = GameObject.FindGameObjectWithTag("Rescue Zone");
        level = SceneManager.GetActiveScene().name;
        ResumePause();
    }

    private void Update()
    {
        if (CheckForPanels() && (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)))                                Pause();

        else if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))           ResumePause();

        bool chico = rescueZone.GetComponent<RescueZone>().LevelEnd();
        if (CheckForPanels() && chico && Input.GetKeyDown(KeyCode.Return))
        {
            DisablePlayer();
            endPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("Victory");
        }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(goToScene);
    }

    public void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gamePaused = true;
    }

    public void ResumePause()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gamePaused = false;
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void ReturnMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void Victory()
    {
        SceneManager.LoadScene("Victory");
    }

    public void LoadFirstLevel()
    {
        SceneManager.LoadScene("lvl 1");
    }

    public void Died()
    {
        if (CheckForPanels())
        {
            retryPanel.SetActive(true);
            DisablePlayer();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public bool CheckForPanels()
    {
        return !mainPanel.activeInHierarchy
            && !pausePanel.activeInHierarchy
            && !endPanel.activeInHierarchy
            && !retryPanel.activeInHierarchy;
    }

    public void DisablePlayer()
    {
        playerUnable = true;
        GameObject.Find("Revolver").gameObject.SetActive(false);
        player.GetComponent<Movement>().enabled = false;
        player.GetComponent<BoxDetector>().enabled = false;
    }
}