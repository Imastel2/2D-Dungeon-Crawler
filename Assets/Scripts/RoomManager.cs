using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private GameObject roomClearText;

    private bool roomCleared = false;
    // Start is called before the first frame update
    void Start()
    {
        roomClearText.SetActive(false); //hide text when game starts
    }

    // Update is called once per frame
    void Update()
    {
        if (roomCleared) return;
        
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length == 0)
        {
            roomCleared = true;
            roomClearText.SetActive(true);          

            Debug.Log("All enemies gone");
        }
        Debug.Log("Enemy count: " + enemies.Length);
    }
}
