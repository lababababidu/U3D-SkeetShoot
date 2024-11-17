using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletModel : MonoBehaviour
{
    public float lifeTime = 2f;  // 子弹存活时间

    private Material bulletMaterial;
    private Renderer bulletRenderer;
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        // 设置连续碰撞检测模式
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.useGravity = false;


        // bulletMaterial = GetComponent<Renderer>().material;
        // bulletMaterial.color = new Color(bulletMaterial.color.r, bulletMaterial.color.g, bulletMaterial.color.b, 0);

        // 启动协程逐渐显示子弹
        // StartCoroutine(FadeIn());

        bulletRenderer = GetComponent<Renderer>();

        // 初始隐藏子弹
        bulletRenderer.enabled = false;

        Invoke("MakeVisible", 0.07f);
        // 在指定的时间后销毁子弹对象
        Destroy(gameObject, lifeTime);
    }

    // private IEnumerator FadeIn()
    // {
    //     float elapsedTime = 0f;
    //     while (elapsedTime < 1.5f)
    //     {
    //         float alpha = elapsedTime / 1.5f;
    //         bulletMaterial.color = new Color(bulletMaterial.color.r, bulletMaterial.color.g, bulletMaterial.color.b, alpha);
    //         elapsedTime += Time.deltaTime;
    //         Debug.Log("FadeIn" + elapsedTime);
    //         yield return null;
    //     }
    //     bulletMaterial.color = new Color(bulletMaterial.color.r, bulletMaterial.color.g, bulletMaterial.color.b, 1);
    // }

    void MakeVisible()
    {
        bulletRenderer.enabled = true;
    }
}
