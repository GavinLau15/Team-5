using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FishMarketController : MonoBehaviour
{
    public int detectionSquare;
    public Tilemap fishMarketTiles;
    public GameObject fishMarketPrompt;
    public GameObject fishMarketUI;

    private bool isFishMarketUIOpen;

    // Update is called once per frame
    void Update()
    {
        Vector2 playerPos = transform.position;
        int playerX = (int)Mathf.Floor(playerPos.x);
        int playerY = (int)Mathf.Floor(playerPos.y);

        if (!isFishMarketUIOpen)
            TryTriggerShopUI(playerX, playerY);
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
