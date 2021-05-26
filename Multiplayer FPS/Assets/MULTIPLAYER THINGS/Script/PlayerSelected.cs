using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSelected : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
    public void Btn3()
    {
        LoadPlayerScene(2);
    }
}
