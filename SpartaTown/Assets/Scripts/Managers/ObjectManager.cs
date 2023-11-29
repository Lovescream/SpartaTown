using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager {

    public Player Player { get; private set; }
    public List<User> Users { get; private set; } = new List<User>();

    public T SpawnUser<T>(string name, string key, Vector2 position) where T : User {
        System.Type type = typeof(T);

        if (type == typeof(Player)) {
            GameObject obj = Main.Resource.Instantiate("Player.prefab");
            obj.transform.position = position;

            Player = obj.GetOrAddComponent<Player>();
            Player.SetInfo(name, key);
            Users.Add(Player);

            return Player as T;
        }
        else if (type == typeof(NPC)) {
            GameObject obj = Main.Resource.Instantiate("NPC.prefab");
            obj.transform.position = position;

            NPC npc = obj.GetOrAddComponent<NPC>();
            npc.SetInfo(name, key);
            Users.Add(npc);

            return npc as T;
        }

        return null;
    }

    public void LoadMap(string mapName) {
        GameObject mapObject = Main.Resource.Instantiate(mapName);
        mapObject.transform.position = Vector3.zero;
        mapObject.name = "@Map";
        mapObject.GetComponent<Map>().Initialize();
    }
}