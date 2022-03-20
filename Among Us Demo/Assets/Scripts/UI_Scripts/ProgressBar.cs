using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField]
    private Image image;
    [SerializeField]
    private float completeTime;

    private float currentTime = 0;

    void Init()
    {
        currentTime = 0;
        image.fillAmount = 0;
    }
    private void OnEnable()
    {
        Init();
    }

    public IEnumerator UpdateLodingGage()
    {
        currentTime = 0;
        image.fillAmount = 0;
        while (image.fillAmount < 1)
        {
            currentTime += Time.deltaTime / completeTime;
            image.fillAmount = Mathf.Lerp(0, 1, currentTime);
            //image.fillAmount += Time.deltaTime / completeTime;
            yield return null;
        }


        ClearTask();
    }

    public void ClearTask()
    {
        // 클리어했을때 해줘야 하는일
        // 테스크 매니저에 클리어 했다는 결과 값을 전달한다
    }

}
