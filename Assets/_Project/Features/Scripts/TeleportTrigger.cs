using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTrigger : MonoBehaviour
{
    [SerializeField] private Vector2 teleportTarget;
    private bool isDirty;
    private void Update()
    {
        UpdateDirty();

        if (CheckPlayerPos())
        {
            isDirty = true;
            Player.Instance.Teleport(teleportTarget);
        }
    }
    private bool CheckPlayerPos()
    {
        // isDirty = teleport triggered
        if (isDirty)
        {
            return false;
        }
        
        return transform.position == Player.Instance.transform.position;
    }

    private void UpdateDirty()
    {
        if (isDirty)
        {
            if (transform.position != Player.Instance.transform.position)
            {
                isDirty = false;
            }

        }
    }


}
