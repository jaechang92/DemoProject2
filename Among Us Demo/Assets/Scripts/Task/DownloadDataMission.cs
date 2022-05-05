using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownloadDataMission : MonoBehaviour
{

    public void DownloadButton(ProgressBar pb)
    {
        StartCoroutine(pb.UpdateLodingGage());
    }
}
