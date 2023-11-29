using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_LobbyScene : UI_Scene {

    #region Enums

    enum Objects {
        InputField,
    }

    enum Images {
        imgCharacterSprite,
    }

    enum Buttons {
        btnChange,
        btnJoin,
    }

    #endregion

    public override bool Initialize() {
        if (base.Initialize() == false) return false;

        BindObject(typeof(Objects));
        BindButton(typeof(Buttons));
        BindImage(typeof(Images));

        GetButton((int)Buttons.btnChange).gameObject.BindEvent(OnBtnChange);
        GetButton((int)Buttons.btnJoin).gameObject.BindEvent(OnBtnJoin);
        
        Refresh();

        return true;
    }

    public void Refresh() {
        Initialize();

        GetImage((int)Images.imgCharacterSprite).sprite = Main.Resource.Load<Sprite>($"{Main.Game.PlayerKey}.sprite");
    }

    #region OnButtons

    private void OnBtnChange() {
        Main.UI.ShowPopupUI<UI_Popup_CharacterSelect>();
    }

    private void OnBtnJoin() {
        Main.Game.PlayerName = GetObject((int)Objects.InputField).GetComponent<TMP_InputField>().text;
        SceneManager.LoadScene("GameScene");
    }

    #endregion
}