using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoSingletonPersistent<Player>
{
    public GameObject toolBar;    

    private bool isToolbarOpen;

    // Start is called before the first frame update
    void Start()
    {
        isToolbarOpen = false;
    }

    // Update is called once per frame
   
    void Update()
    {
        toolBar.SetActive(isToolbarOpen);
        if (Input.GetKeyDown(KeyCode.T))
        {
            isToolbarOpen = !isToolbarOpen;
        }

        // press 1 to place tile directly under player
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ItemSystem.Instance.GetItem(0).Use();
        }

        //press 2 to remove tile directly under player
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            BuildingSystem.Instance.RemoveTile(Vector3Int.RoundToInt(this.transform.position + new Vector3(-0.5f, 0.5f, 0f)), BuildingSystem.Map.Static);
        }
    }

    
}
