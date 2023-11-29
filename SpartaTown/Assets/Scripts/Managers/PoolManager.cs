using System.Collections.Generic;
using UnityEngine.Pool;
using UnityEngine;

public class Pool {
    private GameObject prefab;
    private IObjectPool<GameObject> pool;
    private Transform root;
    private Transform Root {
        get {
            if (root == null) {
                GameObject obj = new() { name = $"[Pool_Root] {prefab.name}" };
                root = obj.transform;
            }
            return root;
        }
    }

    public Pool(GameObject prefab) {
        this.prefab = prefab;
        this.pool = new ObjectPool<GameObject>(OnCreate, OnGet, OnRelease, OnDestroy);
    }

    public GameObject Pop() {
        return pool.Get();
    }

    public void Push(GameObject obj) {
        pool.Release(obj);
    }
    #region Callbacks

    private GameObject OnCreate() {
        GameObject obj = GameObject.Instantiate(prefab);
        obj.transform.SetParent(Root);
        obj.name = prefab.name;
        return obj;
    }
    private void OnGet(GameObject obj) {
        obj.SetActive(true);
    }
    private void OnRelease(GameObject obj) {
        obj.SetActive(false);
    }
    private void OnDestroy(GameObject obj) {
        GameObject.Destroy(obj);
    }

    #endregion
}

public class PoolManager {

    private Dictionary<string, Pool> pools = new();

    public GameObject Pop(GameObject prefab) {
        // #1. Ǯ�� ������ ���� �����.
        if (pools.ContainsKey(prefab.name) == false) CreatePool(prefab);

        // #2. �ش� Ǯ���� �ϳ��� �����´�.
        return pools[prefab.name].Pop();
    }

    public bool Push(GameObject obj) {
        // #1. Ǯ�� �ִ��� Ȯ���Ѵ�. (���� �ִ� ���� ����)
        if (pools.ContainsKey(obj.name) == false) return false;

        // #2. Ǯ�� ���ӿ�����Ʈ�� �����ش�.
        pools[obj.name].Push(obj);

        return true;
    }

    private void CreatePool(GameObject prefab) {
        Pool pool = new(prefab);
        pools.Add(prefab.name, pool);
    }

}