using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScene : MonoBehaviour {

    public UI_LobbyScene UI { get; protected set; }

    void Start() {
        if (Main.Resource.Loaded) {
            InitializeLobby();
        }
        else {
            Main.Resource.LoadAllAsync<Object>("PreLoad", (key, count, totalCount) => {
                if (count >= totalCount) {
                    InitializeLobby();
                }
            });
        }
    }

    private void InitializeLobby() {
        Main.Game.PlayerKey = Main.Game.CharacterKeys[0];
        UI = Main.UI.ShowSceneUI<UI_LobbyScene>();
    }
}