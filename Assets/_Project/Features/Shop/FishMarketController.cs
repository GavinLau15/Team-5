using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Tilemaps;

public class FishMarketController : MonoBehaviour
{
    public int detectionSquare;
    public Tilemap fishMarketTiles;
    public GameObject fishMarketPrompt;
    public GameObject fishMarketUI;
    public float delay = 1f;

    private bool isFishMarketUIOpen;
    private float delayState = 0f;

    private void Start()
    {
        delayState = 0f;
    }


    // Update is called once per frame
    void Update()
    {
        delayState += Time.deltaTime;
        fishMarketUI.SetActive(isFishMarketUIOpen);
        Vector2 playerPos = transform.position;
        int playerX = (int)Mathf.Floor(playerPos.x);
        int playerY = (int)Mathf.Floor(playerPos.y);

        if (!isFishMarketUIOpen)
            TryTriggerShopUI(playerX, playerY);


        if (fishMarketPrompt.activeInHierarchy)
        {
            if (Input.GetKey(KeyCode.E) && !isFishMarketUIOpen && delayState >= delay)
            {
                fishMarketPrompt.SetActive(false);
                isFishMarketUIOpen = true;
                fishMarketUI.SetActive(true);
                delayState = 0f;
            }
        }
        else if (!fishMarketUI.activeInHierarchy)
        {
            isFishMarketUIOpen = false;
        }
        else if (fishMarketUI.activeInHierarchy)
        {
            if (Input.GetKey(KeyCode.E) && delayState >= delay)
            {
                fishMarketPrompt.SetActive(true);
                isFishMarketUIOpen = false;
                fishMarketUI.SetActive(false);
                delayState = 0f;
            }
        }
    }

    void TryTriggerShopUI(int playerX, int playerY)
    {
        Vector3Int playerPosInCellPos = fishMarketTiles.WorldToCell(new Vector2(playerX, playerY));
        playerX = playerPosInCellPos.x;
        playerY = playerPosInCellPos.y;
        for (int y = playerY - detectionSquare; y <= playerY + detectionSquare; y++)
        {
            for (int x = playerX - detectionSquare; x <= playerX + detectionSquare; x++)
            {
                TileBase tile = fishMarketTiles.GetTile(new Vector3Int(x, y, 0));
                if (tile != null)
                {
                    fishMarketPrompt.SetActive(true);
                    return;
                }
                else
                {
                    fishMarketPrompt.SetActive(false);
                }
            }
        }
    }
}