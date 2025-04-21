using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{
    public LevelManager levelManager;
    public GridLayoutGroup gridLayoutGroup;
    public Sprite emptyArrow;
    public Sprite fullArrow;
    public Sprite DinoHead1;
    public Sprite DinoHead2;
    public Sprite DinoHead3;
    public Sprite DinoHead4;
    public GameObject DinoHeadPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        gridLayoutGroup.constraintCount = levelManager.characters.Count;
        for (int i = 0; i < levelManager.characters.Count; i++)
        {
            GameObject newDinoHead = Instantiate(DinoHeadPrefab, gridLayoutGroup.transform);
            if (levelManager.characters[i].name == "Player")
            {
                newDinoHead.transform.GetChild(1).GetComponent<Image>().sprite = DinoHead1;
            }
            if (levelManager.characters[i].name == "Player2")
            {
                newDinoHead.transform.GetChild(1).GetComponent<Image>().sprite = DinoHead2;
            }
            if (levelManager.characters[i].name == "Player3")
            {
                newDinoHead.transform.GetChild(1).GetComponent<Image>().sprite = DinoHead3;
            }
            if (levelManager.characters[i].name == "Player4")
            {
                newDinoHead.transform.GetChild(1).GetComponent<Image>().sprite = DinoHead4;
            }
            if(i == 0 ) newDinoHead.transform.GetChild(0).GetComponent<Image>().sprite = fullArrow;
            else newDinoHead.transform.GetChild(0).GetComponent<Image>().sprite = emptyArrow;
        }
    }

    public void UpdateHud()
    {
        for(int i = 0; i < gridLayoutGroup.constraintCount; i++)
        {
            if (gridLayoutGroup.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite == fullArrow)
            {
                gridLayoutGroup.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = emptyArrow;
                if (i+1 < gridLayoutGroup.constraintCount)
                {
                    gridLayoutGroup.transform.GetChild(i + 1).GetChild(0).GetComponent<Image>().sprite = fullArrow;
                    break;
                }
            }
        }
    }
}
