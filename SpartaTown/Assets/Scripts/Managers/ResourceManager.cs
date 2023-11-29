using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class ResourceManager {

    public bool Loaded { get; set; }

    private Dictionary<string, UnityEngine.Object> resources = new();

    #region Addressable

    // key�� �޾� ���ҽ��� �񵿱� �ε��ϰ�, �Ϸ�Ǹ� �ݹ� ȣ��.
    public void LoadAsync<T>(string key, Action<T> callback = null) where T : UnityEngine.Object {
        // �̹� �ε�� ���ҽ��� �ٽ� �ε����� �ʰ� �ݹ鸸 ȣ�����ش�.
        if (resources.TryGetValue(key, out UnityEngine.Object resource)) {
            callback?.Invoke(resource as T);
            return;
        }

        // key�� �޾�, ������ � ���ҽ��� �ε��� ������ ���Ѵ�.
        // Ex. Sprite�� ��� key�� �״�� �ε��ϸ� Texture2D�� �ε�ǹǷ�, Sprite ������ ��� Ű���� ���� �ε��ؾ� �Ѵ�.
        string loadKey = key;
        if (key.Contains(".sprite")) loadKey = $"{key}[{key.Replace(".sprite", "")}]";

        // ���ҽ� �񵿱� �ε� ����.
        if (key.Contains(".sprite")) {
            var asyncOperation = Addressables.LoadAssetAsync<Sprite>(loadKey);
            asyncOperation.Completed += op => {
                resources.Add(key, op.Result);
                callback?.Invoke(op.Result as T);
            };
        }
        else {
            var asyncOperation = Addressables.LoadAssetAsync<T>(loadKey);
            asyncOperation.Completed += op => {
                resources.Add(key, op.Result);
                callback?.Invoke(op.Result);
            };
        }
    }

    // �ش� label�� ���� ��� ���ҽ��� �񵿱� �ε��ϰ�, �Ϸ�Ǹ� �ݹ�(key, ����ε��, ��ü�ε��) ȣ��.
    public void LoadAllAsync<T>(string label, Action<string, int, int> callback) where T : UnityEngine.Object {
        var operation = Addressables.LoadResourceLocationsAsync(label, typeof(T));
        operation.Completed += op => {
            int loadCount = 0;
            int totalCount = op.Result.Count;

            foreach (var result in op.Result) {
                LoadAsync<T>(result.PrimaryKey, obj => {
                    loadCount++;
                    callback?.Invoke(result.PrimaryKey, loadCount, totalCount);
                });
            }
        };
    }

    #endregion


    public T Load<T>(string key) where T : UnityEngine.Object {
        if (!resources.TryGetValue(key, out UnityEngine.Object resource)) return null;
        return resource as T;
    }

    // �ش� key�� �������� �ε��Ͽ� Ǯ���� �������ų� �ν��Ͻ�ȭ�Ѵ�.
    public GameObject Instantiate(string key, Transform parent = null, bool pooling = false) {
        GameObject prefab = Load<GameObject>(key);
        if (prefab == null) {
            Debug.LogError($"[ResourceManager] Instantiate({key}): Failed to load prefab.");
            return null;
        }

        if (pooling) return Main.Pool.Pop(prefab);

        GameObject obj = UnityEngine.Object.Instantiate(prefab, parent);
        obj.name = prefab.name;
        return obj;
    }

    // �ش� ������Ʈ�� Ǯ�� �������ų� �ı��Ѵ�.
    public void Destroy(GameObject obj) {
        if (obj == null) return;

        if (Main.Pool.Push(obj)) return;

        UnityEngine.Object.Destroy(obj);
    }


}