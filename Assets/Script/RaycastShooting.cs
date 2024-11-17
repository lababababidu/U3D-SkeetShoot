using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastShooting : MonoBehaviour
{
    public GameObject bulletsample;
    public Camera playerCamera; // 玩家摄像头
    public float rayDistance = 100f; // 射线的最大距离
    // public GameObject hitEffect; // 射中效果（可以是粒子效果）
    // public LineRenderer lineRenderer;
    private ScoreCounter scoreCounter;
    private AudioSource audioSource;

    void Start(){
        GameObject skeetDistributor = GameObject.Find("SkeetDistributor");
        scoreCounter = skeetDistributor.GetComponent<ScoreCounter>();

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            PlayAudio();
            ShootRay();
            // 鼠标左键按下
        }

        // if (Input.GetMouseButton(0))
        // {
        //     // Debug.Log("左键正在被按住");
        //     ShootRay();
        //     // 鼠标左键按住
        // }
        
    }
    public void PlayAudio()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogError("AudioSource 组件未找到！");
        }
    }

    void ShootRay()
    {
        // 获取摄像头的正前方方向
        Vector3 rayDirection = playerCamera.transform.forward;
        
        // 从摄像头的位置发射射线
        Ray ray = new Ray(playerCamera.transform.position, rayDirection);
        RaycastHit hit;

        // 发射射线并检测是否击中物体
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            // 如果击中物体，更新射线终点为击中点
            // lineRenderer.SetPosition(1, hit.point);
            Debug.Log("Hit: " + hit.collider.name);
            if(hit.collider.tag == "Skeet"){
                // hit.rigidbody.AddExplosionForce(15f,playerCamera.transform.position,100f);
                Vector3 force = hit.point - playerCamera.transform.position;
                force.Normalize();
                force *= 270;
                hit.rigidbody.AddForceAtPosition(force,hit.point);
                scoreCounter.HitSkeet(hit.collider.gameObject);
            }
        }
        // else
        // {
        //     // 如果没有击中，射线终点设置为最大距离
        //     // lineRenderer.SetPosition( 1 , ray.origin + rayDirection * rayDistance );
        // }

        // GameObject bullet = Instantiate(bulletsample, playerCamera.transform.position, Quaternion.identity);
        // Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        // if (bulletRb != null)
        // {
        //     float bulletSpeed = 90f;
        //     bulletRb.velocity = rayDirection * bulletSpeed;
        // }

        // 更新射线起点为摄像头位置
        // lineRenderer.SetPosition(0, ray.origin);
    }


}
