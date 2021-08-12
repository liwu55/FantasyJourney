using UnityEngine;

public class Third : SingleTonMono<Third>
{
    public const float DISTANCE_DEFAULT = 6;
    private float distance= DISTANCE_DEFAULT;//真实距离
    Transform transPlayer;
    float yOffset = 1.5f;
    Vector3 lookAtP;
    Quaternion rotationCamera;
    public bool canAdjustDistance;

    float speedMoveX = 3600;
    float speedMoveY = 1800;
    float distanceChangeRate = 0.1f;
    void Start()
    {
        initCameraPosition();
    }

    private void initCameraPosition()
    {
        rotationCamera = transform.rotation;
        AdjustCamera();
    }

    public void BindPlayer(Transform player)
    {
        transPlayer = player;
    }

    private void AdjustCamera()
    {
        if (transPlayer == null)
        {
            return;
        }
        transform.position= transPlayer.position + rotationCamera * Vector3.back * distance;
        lookAtP = transPlayer.position + Vector3.up * yOffset;
        transform.LookAt(lookAtP);
    }

    void Update()
    {
        if (transPlayer == null)
        {
            return;
        }
        AdjustCameraRotation();
        if (canAdjustDistance) { 
            AdjustCameraDistance();
        }
        AdjustCamera();
        FixCameraPosition();
    }

    /// <summary>
    /// 修复地形穿透
    /// </summary>
    private void FixCameraPosition()
    {
        Vector3 origin = lookAtP;
        Vector3 dir =  transform.position - origin;
        float maxLength = dir.magnitude;
        int layerMask = LayerMask.NameToLayer("Terrain");
        RaycastHit[] hits = Physics.RaycastAll(origin, dir, maxLength, 1<<layerMask);
        float minLength = float.MaxValue;
        bool hasHitPoint = false;
        Vector3 fixPoint=Vector3.zero;
        foreach (RaycastHit hit in hits)
        {
            //打到玩家跳过
            if (hit.collider.CompareTag("Player"))
            {
                continue;
            }
            if (hit.distance < minLength)
            {
                minLength = hit.distance;
                hasHitPoint = true;
                fixPoint = hit.point;
            }
        }
        if (hasHitPoint)
        {
            transform.position = fixPoint;
            transform.position += Vector3.up * 0.5f;
            transform.LookAt(lookAtP);
        }
    }

    private void AdjustCameraDistance()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0) {
            distance = distance * (1- distanceChangeRate);
        }else if (scroll < 0)
        {
            distance = distance * (1+ distanceChangeRate);
        }
    }

    private void AdjustCameraRotation()
    {
        //鼠标中键
        if (Input.GetMouseButton(2))
        {
            float mouseMoveX = Input.GetAxis("Mouse X");
            float mouseMoveY = Input.GetAxis("Mouse Y");
            //Debug.Log("moveX="+mouseMoveX+" moveY="+mouseMoveY);
            float nowRotationX = rotationCamera.eulerAngles.y;
            nowRotationX += mouseMoveX * speedMoveX*Time.deltaTime;
            float nowRotationY = rotationCamera.eulerAngles.x;
            nowRotationY += -mouseMoveY * speedMoveY * Time.deltaTime;
            rotationCamera = Quaternion.Euler(nowRotationY,nowRotationX,0);
        }
    }
}
