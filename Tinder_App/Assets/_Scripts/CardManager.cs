using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardManager : MonoBehaviour
{
    public List<GameObject> CardList;
    public List<GameObject> IdontKnowList;
    public List<GameObject> IKnowList;
    [HideInInspector]
    public Vector3 InitialPos;
    private bool CheckDntList;

    public GameObject GameOverPanel;
    public TextMeshProUGUI DontKnowText;
    public Button IDontBtn;

    private void Update()
    {
        DontKnowText.text = "I Don't Know : " + IdontKnowList.Count;
        if (CardList.Count == 0)
            IDontBtn.GetComponent<Button>().interactable = true;

        if (IdontKnowList.Count == 0 && CheckDntList)
            GameOverPanel.SetActive(true);
            
    }
    public void IDontKnowButton()
    {
        CheckDntList = true;
        foreach (GameObject obj in IdontKnowList)
        {
            obj.SetActive(true);
            obj.transform.localPosition = InitialPos;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
