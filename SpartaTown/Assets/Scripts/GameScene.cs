using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour {
    
    public UI_GameScene UI { get; protected set; }

    void Start() {
        if (Main.Resource.Loaded) {
            InitializeGame();
        }
        else {
            Main.Resource.LoadAllAsync<Object>("PreLoad", (key, count, totalCount) => {
                if (count >= totalCount) {
                    InitializeGame();
                }
            });
        }
    }

    private void InitializeGame() {
        Player player = Main.Object.SpawnUser<Player>(Main.Game.PlayerName, Main.Game.PlayerKey, Vector2.zero);
        Main.Object.LoadMap("Map.prefab");
        Main.Object.SpawnUser<NPC>("NPC1", Main.Game.CharacterKeys[1], new Vector2(-8, 11));
        Main.Object.SpawnUser<NPC>("NPC2", Main.Game.CharacterKeys[2], new Vector2(+3, 11));
        Main.Object.SpawnUser<NPC>("NPC3", Main.Game.CharacterKeys[3], new Vector2(15, 11));
        UI = Main.UI.ShowSceneUI<UI_GameScene>();
    }

}