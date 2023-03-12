using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEditor.Experimental.GraphView.GraphView;

public class ShopController : MonoBehaviour
{
    public int detectionSquare;
    public Tilemap shopTiles;
    public GameObject shopPrompt;
    public GameObject shopUI;
    public float delay = 1f;
    
    private bool isShopUIOpen;
    private float delayState;

    private void Start()
    {
        delayState = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        delayState += Time.deltaTime;
        shopUI.SetActive(isShopUIOpen);
        Vector2 playerPos = transform.position;
        int playerX = (int)Mathf.Floor(playerPos.x);
        int playerY = (int)Mathf.Floor(playerPos.y);

        if (!isShopUIOpen)
            TryTriggerShopUI(playerX, playerY);

        if (shopPrompt.activeInHierarchy)
        {
            if (Input.GetKey(KeyCode.E) && delayState >= delay && !isShopUIOpen)
            {
                shopPrompt.SetActive(false);
                isShopUIOpen = true;
                shopUI.SetActive(true);
                delayState = 0f;
            }
        }
        else if (!shopUI.activeInHierarchy)
        {
            isShopUIOpen = false;
        }
        else if (shopUI.activeInHierarchy)
        {
            if (Input.GetKey(KeyCode.E) && delayState >= delay)
            {
                shopPrompt.SetActive(true);
                isShopUIOpen = false;
                shopUI.SetActive(false);
                delayState = 0f;
            }
        }
    }

    void TryTriggerShopUI(int playerX, int playerY)
    {
        Vector3Int playerPosInCellPos = shopTiles.WorldToCell(new Vector2(playerX, playerY));
        playerX = playerPosInCellPos.x;
        playerY = playerPosInCellPos.y;
        for (int y = playerY - detectionSquare; y <= playerY + detectionSquare; y++)
        {
            for (int x = playerX - detectionSquare; x <= playerX + detectionSquare; x++)
            {
                TileBase tile = shopTiles.GetTile(new Vector3Int(x, y, 0));
                if (tile != null)
                {
                    shopPrompt.SetActive(true);
                    return;
                }
                else
                {
                    shopPrompt.SetActive(false);
                }
            }
        }
    }
}
