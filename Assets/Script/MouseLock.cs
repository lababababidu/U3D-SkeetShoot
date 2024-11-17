using UnityEngine;

public class MouseLock : MonoBehaviour
{
    public float mouseSensitivity = 100f; // 鼠标灵敏度

    private float xRotation = 0f; // 用于存储垂直方向上的旋转角度
    private float yRotation = 0f;

    void Start()
    {
        // 锁定并隐藏鼠标指针
        Cursor.lockState = CursorLockMode.Locked;
    }

    void OnGUI(){
        
    }

    void Update()
    {
        // 获取鼠标输入
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // 更新垂直方向上的旋转角度，并限制旋转范围
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // 旋转摄像头
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        yRotation += mouseX;

        // 应用旋转
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}

