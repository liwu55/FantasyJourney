using System.Collections;
using DG.Tweening;
using Frame.Utility;
using UnityEngine;

public class DamageNumber : MonoBehaviour, PoolObject
{
    private TextMesh tm;
    private Tweener move;
    private Tweener fade;
    
    private void Awake()
    {
        tm=GetComponent<TextMesh>();
        Material mat = GetComponent<MeshRenderer>().material;
        
        fade = mat.DOFade(0, 0.6f);
        fade.SetAutoKill(false);
    }

    public void OnSpawn(object obj)
    {
        Debug.Log(GetType().Name+" OnSpawn");
        object[] data = (object[]) obj;
        float damage = (float) data[0];
        Vector3 position = (Vector3) data[1];
        tm.text = damage.ToString();
        transform.position = position;
        //回收
        StartCoroutine(DelayRecycle(3));

        move = transform.DOMove(Vector3.up * 2, 0.6f);
        move.SetRelative(true);
        move.Restart();
        fade.Restart();
    }

    IEnumerator DelayRecycle(float time)
    {
        yield return new WaitForSeconds(time);
        ObjectPool.Instance.RecycleObj(gameObject);
    }

    public void OnRecycle()
    {
    }

    public void OnPause()
    {
    }

    public void OnResume()
    {
    }
}