using System;
using Game;
using Game.bean;
using UnityEngine;

public class PlayerShow : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform playerParents;
    [SerializeField] private float speed=5;
    [SerializeField] private RectTransform rect;
    private bool IsRotate=true;
    public static PlayerShow Instance;


    public GameObject Player
    {
        get => player;
        set => player = setPlayer(value);
    }
    public GameObject setPlayer(GameObject obj)
    {
        if (obj == null) return player;
        if(player!=null)Destroy(player);
        GameObject playerClone = Instantiate<GameObject>(obj, playerParents);
        playerClone.transform.rotation=Quaternion.Euler(0,210,0);
        return playerClone;
    }
    private void Awake()
    {
        UIEvent.RefreshMainPageHero+=setPlayerClone;
        Instance = this;
        setPlayerClone();
    }

    private void OnDestroy()
    {
        UIEvent.RefreshMainPageHero-=setPlayerClone;
    }

    private void Update()
    {
        isRotate();
        rotatePlayerClone();
    }

    public void setPlayerClone()
    {
        Player=Resources.Load<GameObject>(PlayerInfo.Instance.GetChooseHeroPath());
    }
    public void rotatePlayerClone()
    {
        if (!IsRotate) return;
        if (!Input.GetMouseButton(0)) return;
        float OffsetX = Input.GetAxis("Mouse X");//获取鼠标x轴的偏移量
        player.transform.Rotate(new Vector3(0, -OffsetX, 0) * speed, Space.World);
    }
    public void isRotate()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        if (RectTransformUtility.RectangleContainsScreenPoint(rect, Input.mousePosition))
            IsRotate = true;
        else
            IsRotate = false;
    }
}
