using UnityEngine;

public class Main : MonoBehaviour {

    #region Singleton

    private static Main instance;
    private static bool initialized;
    public static Main Instance {
        get {
            if (!initialized) {
                initialized = true;

                GameObject obj = GameObject.Find("@Main");
                if (obj == null) {
                    obj = new() { name = @"Main" };
                    obj.AddComponent<Main>();
                    DontDestroyOnLoad(obj);
                    instance = obj.GetComponent<Main>();
                }
            }
            return instance;
        }
    }
    #endregion

    private PoolManager pool = new();
    private ResourceManager resource = new();
    private ObjectManager objects = new();
    private UIManager ui = new();
    private GameManager game = new();

    public static PoolManager Pool => Instance?.pool;
    public static ResourceManager Resource => Instance?.resource;
    public static ObjectManager Object => Instance?.objects;
    public static UIManager UI => Instance?.ui;
    public static GameManager Game => Instance?.game;

}