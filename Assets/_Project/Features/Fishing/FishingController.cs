using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEditor.Experimental.GraphView.GraphView;

public class FishingController : MonoBehaviour
{
    public int detectionSquare = 2;
    public Tilemap waterTiles;
    public GameObject fishingPrompt;
    public GameObject fishingMiniGameUI;

    private bool isFishingUIOpen = false;
    private bool isFishingSucceeded = false;

    void Update()
    {
        Vector2 playerPos = transform.position;
        int playerX = (int)Mathf.Round(playerPos.x);
        int playerY = (int)Mathf.Round(playerPos.y);

        if (!isFishingUIOpen)
            TryTriggerFishingPrompt(playerX, playerY);

        if (fishingPrompt.activeInHierarchy)
        {
            // fishingPrompt is visible, so we check if we need to open the minigame
            if (Input.GetKey(KeyCode.E) && !isFishingUIOpen)
            {
                fishingPrompt.SetActive(false);
                isFishingUIOpen = true;
                fishingMiniGameUI.SetActive(true);
            }
        }
        else if (!fishingMiniGameUI.activeInHierarchy)
        {
            // fishingPrompt is NOT visible AND fishingMiniGameUI is not visible, so change state of UI
            isFishingUIOpen = false;
        }

        if (FindObjectOfType<FishingUIController>() != null)
            if (FindObjectOfType<FishingUIController>().overlappedTime >= FishingUIController.MAX_OVERLAPPED_TIME)
            {
                FindObjectOfType<FishingUIController>().overlappedTime = FishingUIController.START_OVERLAPPED_TIME;
                fishingMiniGameUI.SetActive(false);
                isFishingSucceeded = true;
                print("You got a fish");
            }
            else if (FindObjectOfType<FishingUIController>().overlappedTime <= 0)
            {
                FindObjectOfType<FishingUIController>().overlappedTime = FishingUIController.START_OVERLAPPED_TIME;
                fishingMiniGameUI.SetActive(false);
                isFishingSucceeded = false;
                print("The fish has escaped!");
            }

        if (isFishingSucceeded)
        {
            AwardFish();
        }
    }

    private void TryTriggerFishingPrompt(int playerX, int playerY)
    {
        Vector3Int playerPosInCellPos = waterTiles.WorldToCell(new Vector2(playerX, playerY));
        playerX = playerPosInCellPos.x;
        playerY = playerPosInCellPos.y;
        for (int y = playerY - detectionSquare; y <= playerY + detectionSquare; y++)
        {
            for (int x = playerX - detectionSquare; x <= playerX + detectionSquare; x++)
            {
                TileBase tile = waterTiles.GetTile(new Vector3Int(x, y, 0));
                if (tile != null && tile.name == "tile_water")
                {
                    fishingPrompt.SetActive(true);
                    return;
                }
                else
                {
                    fishingPrompt.SetActive(false);
                }
            }
        }
    }

    private void AwardFish()
    {

    }
}
