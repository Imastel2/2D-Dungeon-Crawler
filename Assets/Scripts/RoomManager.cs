using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private GameObject roomClearText;
    [SerializeField] private GameObject playerHasDied;

    private bool roomCleared = false;
    private bool playerDied = false;
    // Start is called before the first frame update
    void Start()
    {
        roomClearText.SetActive(false); //hide text when game starts
        playerHasDied.SetActive(false);
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

        if (playerDied) return;

        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");

        if (player.Length == 0)
        {
            playerDied = true;
            playerHasDied.SetActive(true);
        }
        Debug.Log("Enemy count: " + enemies.Length);
    }
}
