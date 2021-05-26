using Cinemachine.Examples;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public string level, goToScene;
    public bool gamePaused = false;
    public bool playerUnable = false;
    public GameObject mainPanel, pausePanel, endPanel, retryPanel;

    private GameObject rescueZone;

    private void Start()
    {
        rescueZone = GameObject.FindGameObjectWithTag("Rescue Zone");
        level = SceneManager.GetActiveScene().name;
        ResumePause();
    }

    private void Update()
    {
        if (CheckForPanels() && (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)))    Pause();

        else if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))                   ResumePause();

        bool chico = rescueZone.GetComponent<RescueZone>().LevelEnd();
        if (CheckForPanels() && chico && Input.GetKeyDown(KeyCode.Return))                          endPanel.SetActive(true);

        if (Input.GetKey(KeyCode.O))
            SceneManager.LoadScene("Victory");
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
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        gamePaused = true;
    }

    public void ResumePause()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
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
    public void LoadChooseLevel()
    {
        SceneManager.LoadScene("ChooseLevel");
    }
    public void LoadFirstLevel()
    {
        SceneManager.LoadScene("lvl 1");
    }

    public void LoadSecondLevel()
    {
        SceneManager.LoadScene("lvl 2");
    }
    public void LoadThirdLevel()
    {
        SceneManager.LoadScene("lvl 3");
    }
    public void LoadFourthLevel()
    {
        SceneManager.LoadScene("lvl 4");
    }

    public void Died()
    {
        if (CheckForPanels())       retryPanel.SetActive(true);
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
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<CharacterMovement>().enabled = false;
        player.GetComponent<DropItem>().enabled = false;
    }
}