using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using YouYou;

public class TestPool : MonoBehaviour
{
    public Transform trans1;
    public Transform trans2;

    void Start()
    {
        GameEntry.Pool.SetClassObjectResideCount<CusUserData>(3);
    }

    void Update()
    {
        //if (Input.GetKeyUp(KeyCode.A))
        //{
        //    CusUserData data = GameEntry.Pool.DequeueClassObject<CusUserData>();

        //    CusUserDataAA data1 = GameEntry.Pool.DequeueClassObject<CusUserDataAA>();

        //    CusUserDataBB data2 = GameEntry.Pool.DequeueClassObject<CusUserDataBB>();

        //    CusUserDataCC data3 = GameEntry.Pool.DequeueClassObject<CusUserDataCC>();

        //    StartCoroutine(EnqueueClassObject(data));
        //    StartCoroutine(EnqueueClassObject(data1));
        //    StartCoroutine(EnqueueClassObject(data2));
        //    StartCoroutine(EnqueueClassObject(data3));
        //}

        if (Input.GetKeyUp(KeyCode.B))
        {
            StartCoroutine(CreateObj());
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            StopAllCoroutines();
            GameEntry.Pool.InitGameObjectPool();
        }
    }

    private IEnumerator CreateObj()
    {
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(0.5f);

            GameEntry.Pool.GameObjectSpawn(1, trans1, (Transform instance) =>
            {
                instance.transform.localPosition += new Vector3(0, 0, i * 2);
                instance.gameObject.SetActive(true);
                StartCoroutine(Despawn(1, instance));
            });

            GameEntry.Pool.GameObjectSpawn(2, trans2, (Transform instance) =>
            {
                instance.transform.localPosition += new Vector3(0, 5, i * 2);
                instance.gameObject.SetActive(true);
                StartCoroutine(Despawn(2, instance));
            });
        }
    }

    private IEnumerator Despawn(byte poolId, Transform instance)
    {
        yield return new WaitForSeconds(20);
        GameEntry.Pool.GameObjectDespawn(poolId, instance);
    }

    //private IEnumerator EnqueueClassObject(object obj)
    //{
    //    yield return new WaitForSeconds(5);
    //    GameEntry.Pool.EnqueueClassObject(obj);
    //}
}