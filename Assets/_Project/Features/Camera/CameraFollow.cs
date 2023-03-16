using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    
    void LateUpdate()
    {
        transform.position = new Vector3(Player.Instance.transform.position.x, Player.Instance.transform.position.y ,transform.position.z);
    }
}
