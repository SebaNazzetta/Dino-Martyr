using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<GameObject> characters;
    public Transform characterSpawnPoint;
    public DeadTransition deadTransition;

    private void Awake()
    {
        characterSpawnPoint = GameObject.Find("StartPoint").transform;
        characterSpawnPoint.GetComponent<SpriteRenderer>().enabled = false;
        deadTransition = FindObjectOfType<DeadTransition>();
    }
    public void Start()
    {
        InstantiateCharacter();
        StartCoroutine(deadTransition.FadeOut());
    }
    public void InstantiateCharacter()
    {
        if (characters.Count != 0)
        {
            Instantiate(characters[0], characterSpawnPoint.transform.position, Quaternion.identity);
            characters.RemoveAt(0);
        }
        else
        {
            Debug.Log("perdiste");
        }
        
    }
}
