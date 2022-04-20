using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageScroll : MonoBehaviour
{
    RawImage rawImage;
    Rect uvRect;
    [SerializeField]
    private float diffuseTillingYSize;
    public float DiffuseTillingYSize
    {
        get
        {
            return diffuseTillingYSize;
        }
        set
        {
            diffuseTillingYSize = value;

            inst.mainTextureScale = new Vector2(1, diffuseTillingYSize);
            rawImage.material = inst;
        }

    }

    [SerializeField]
    private float rectHeight;
    public float RectHeight {
        get
        {
            return rectHeight;
        }
        set
        {
            rectHeight = value;
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, rectHeight);
        }
    }

    [SerializeField]
    private float speed = 1.0f;
    private RectTransform rt;
    private Material inst;

    private void Awake()
    {
        rt = GetComponent<RectTransform>();
        rawImage = GetComponent<RawImage>();
        inst = Instantiate(rawImage.material);
    }

    void Update()
    {
        uvRect = rawImage.uvRect;
        uvRect.y += Time.deltaTime * speed / rectHeight;
        rawImage.uvRect = uvRect;
    }


}
