using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AsteroidController : MonoBehaviour
{
    // 운석이 날아가는 것만 구현

    private Vector2 dir;// 운석의 방향 결정 랜덤값으로
    [SerializeField]
    private RectTransform rt;
    [SerializeField]
    private BoxCollider2D m_collider;
    [SerializeField]
    private Image m_Image;
    [SerializeField]
    private Vector2 speed;
    [SerializeField]
    private float rotSpeed;
    public int idx;
    void Init()
    {
        m_Image = GetComponent<Image>();
        m_Image.SetNativeSize();
        rt = GetComponent<RectTransform>();
        m_collider = GetComponent<BoxCollider2D>();
        m_collider.size = new Vector2(rt.rect.width, rt.rect.height);
        origin = rt.anchoredPosition;
        speed = new Vector2(Random.Range(-500, -100), Random.Range(-300, 300));
    }

    public void Destroyed()
    {
        m_Image.SetNativeSize();
        m_collider.size = new Vector2(rt.rect.width, rt.rect.height);
        m_collider.enabled = false;
        speed = new Vector2(0, 0);
        Destroy(gameObject, 1.0f);
    }

    void Start()
    {
        Init();

    }
    [SerializeField]
    private Vector2 origin;
    private void Update()
    {
        origin = rt.anchoredPosition;

        origin.x += speed.x * Time.deltaTime;
        origin.y += speed.y * Time.deltaTime;
        rt.anchoredPosition = origin;
        rt.Rotate(new Vector3(0, 0, rt.rotation.z + rotSpeed));
    }

    


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("BindArea"))
        {
            Destroy(gameObject);
        }
    }

}
