using UnityEngine;
using System.Collections;
public class CameraController : MonoBehaviour 
{ 
    public Transform charObj; 
    public float[] camDistance; 
    public float[] heightOffset; 
    public float[] mouseSpeed; 
    public float[] yAngleLimit; 
    private float startRotation; 
    private float camNum = 0; 
    private CamType cameraType; 
    private float camDist; 
    private float hOffset; 
    private float x = 0.0f; 
    private float y = 0.0f; 
    
    enum CamType 
    { 
        FP, 
        SP, 
        TP 
    } 
    
    void Start() 
    { 
    
    } 
    
    void Update() 
    {
    
    } 
    
    public void Initialize(Transform Player) 
    { 
        camNum = 3; 
        cameraType = CamType.FP; 
        startRotation = charObj.transform.eulerAngles.y; 
        x = transform.eulerAngles.y; 
        y = transform.eulerAngles.x; 
        charObj = Player; 
        SetCameraValues(); 
    } 
    void ChangeCamType() 
    { 
        if (camNum == 1) 
        { 
            cameraType = CamType.SP; 
            camNum = 2; 
        } 
        else if (camNum == 2) 
        { 
            cameraType = CamType.TP; camNum = 3; 
        } 
        else if (camNum == 3) 
        { 
            cameraType = CamType.FP; camNum = 1; 
        } 
    } 
    
    void SetCameraValues() 
    { 
        ChangeCamType(); 
        switch (cameraType) 
        { 
            case CamType.FP: 
                camDist = camDistance[0]; 
                hOffset = heightOffset[0]; 
                break; 
            case CamType.SP: 
                camDist = camDistance[1]; 
                hOffset = heightOffset[1]; 
                break; 
            case CamType.TP: 
                camDist = camDistance[2]; 
                hOffset = heightOffset[2]; break; 
        } 
    } 
    
    void Apply() 
    { 
        x += Input.GetAxis("Mouse X") * mouseSpeed[0] * Time.deltaTime; 
        y -= Input.GetAxis("Mouse Y") * mouseSpeed[1] * Time.deltaTime; 
        y = ClampAngle(y, yAngleLimit[0], yAngleLimit[1]); 
        var rotation = Quaternion.Euler(charObj.transform.eulerAngles.x, x + startRotation, charObj.transform.eulerAngles.z); 
        var targPos = rotation * (new Vector3(0.0f, 0.0f, camDist)) + charObj.position; 
        targPos.y += hOffset; transform.rotation = rotation; transform.position = targPos; 
    } 
    
    float ClampAngle(float angle, float min, float max) 
    { 
        if (angle < -360) angle += 360; if (angle > 360) angle -= 360; 
            return Mathf.Clamp(angle, min, max); 
    } 
    
    void LateUpdate() 
        { 
            Apply(); 
        } 

    void SetCamValues() 
    { 
        ChangeCamType(); 
        switch (cameraType) 
        { 
            case CamType.FP: 
                camDist = camDistance[0]; 
                hOffset = heightOffset[0]; 
                break; 
        } 
    } 
}
