using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPosition : MonoBehaviour
{
    [SerializeField]
    private Transform[] positions;
    private int idx;

    public int Index { get { return idx; } }

    public Vector3 GetSpawnPosition()
    {
        Vector3 pos = positions[idx++].position;
        if (idx >= positions.Length)
        {
            idx = 0;
        }
        return pos;
    }

}
