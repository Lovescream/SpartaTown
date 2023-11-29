using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_CharacterCard : UI_Base {

    #region Enums

    enum Images {
        Sprite,
    }

    #endregion

    #region Properties

    public string Key { get; protected set; }

    #endregion

    #region MonoBehaviours

    void Awake() {
        Initialize();
    }

    #endregion

    #region Initialize / Set

    public override bool Initialize() {
        if (base.Initialize() == false) return false;

        BindImage(typeof(Images));
        this.gameObject.BindEvent(OnButton);

        return true;
    }

    public void SetInfo(string key) {
        Key = key;

        GetImage((int)Images.Sprite).sprite = string.IsNullOrEmpty(key) ? null : Main.Resource.Load<Sprite>($"{Key}.sprite");
    }

    #endregion

    #region OnButtons

    private void OnButton() {
        if (string.IsNullOrEmpty(Key)) return;
        Main.Game.PlayerKey = Key;
        Main.UI.ClosePopup();
        if (SceneManager.GetActiveScene().name == "LobbyScene") {
            FindObjectOfType<LobbyScene>().UI.Refresh();
        }
        else {
            FindObjectOfType<UI_Popup_ChangeCharacter>().Refresh();
        }
    }

    #endregion


}