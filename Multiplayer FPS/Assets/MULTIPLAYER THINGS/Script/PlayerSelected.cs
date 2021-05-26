using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSelected : MonoBehaviour
{
    void LoadPlayerScene(int value)
    {
        PlayerPrefs.SetInt("HERO", value);
        SceneManager.LoadScene("SampleScene");
    }

    public void Btn1()
    {
        LoadPlayerScene(0);
    }
    public void Btn2()
    {
        LoadPlayerScene(1);
    }
}
