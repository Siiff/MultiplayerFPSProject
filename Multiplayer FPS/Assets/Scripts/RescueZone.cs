using System.Collections.Generic;
//using System.Security.Policy;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RescueZone : MonoBehaviour
{
    public int boxToGo = 4;
    public Material greenMaterial;
    public Material yellowMaterial;
    public List<Collider> TriggerList = new List<Collider>();

    private bool endLevelGo;
    public GameObject panel;
    private Image panelRenderer;
    //private GameObject player;
    public Canvas endLevelGUI;
    private TMP_Text boxToGoText;
    public TMP_Text counterText;

    private void Start()
    {
        //panel = GameObject.Find("Panel");
        panelRenderer = panel.GetComponent<Image>();
        //player = GameObject.FindGameObjectWithTag("Player");
        //endLevelGUI = player.transform.Find("EndLevelGUI").gameObject.GetComponent<Canvas>();
        counterText = panel.transform.Find("Txt_Counter").gameObject.GetComponent<TextMeshProUGUI>();
        boxToGoText = panel.transform.Find("Txt_BoxToGo").gameObject.GetComponent<TextMeshProUGUI>();
        boxToGoText.SetText(boxToGo.ToString());
        SetColor(yellowMaterial);
    }

    private void Update()
    {
        counterText.SetText(TriggerList.Count.ToString());

        if (TriggerList.Count >= boxToGo)
        {
            SetColor(greenMaterial);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!TriggerList.Contains(other) && other.CompareTag("Collectable") && other.GetType() == typeof(BoxCollider))
        {
            TriggerList.Add(other);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (TriggerList.Count >= boxToGo && other.CompareTag("Player"))
        {
            endLevelGUI.enabled = true;
        }

        //if (Input.GetKeyDown(KeyCode.G) && other.CompareTag("Player"))
        //{
        //    endLevelGUI.enabled = true;
        //}
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            endLevelGUI.enabled = false;
        }

        if(other.CompareTag("Collectable"))
        {
            SubtractCounter();
        }
    }

    public void SubtractCounter()
    {
        if (TriggerList.Count > 0)
        {
            TriggerList.RemoveAt(TriggerList.Count - 1);
            TriggerList.TrimExcess();
        }

        if (TriggerList.Count <= boxToGo)
        {
            SetColor(yellowMaterial);
            endLevelGUI.enabled = false;
        }
    }

    void SetColor(Material material)
    {
        panelRenderer.transform.GetComponent<Image>().material = material;
    }

    public bool LevelEnd()
    {
        if (endLevelGUI.enabled) endLevelGo = true;
        return endLevelGo;
    }
}