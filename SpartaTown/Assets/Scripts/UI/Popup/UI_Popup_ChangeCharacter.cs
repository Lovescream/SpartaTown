using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Popup_ChangeCharacter : UI_Popup {

    #region Enums

    enum Objects {
        InputField,
    }
    enum Images {
        imgCharacterSprite,
    }
    enum Buttons {
        btnChange,
        btnConfirm,
    }

    #endregion

    #region MonoBehaviours

    void OnEnable() {
        Initialize();
    }

    #endregion

    public override bool Initialize() {
        if (base.Initialize() == false) return false;

        BindObject(typeof(Objects));
        BindImage(typeof(Images));
        BindButton(typeof(Buttons));

        GetButton((int)Buttons.btnChange).gameObject.BindEvent(OnBtnChange);
        GetButton((int)Buttons.btnConfirm).gameObject.BindEvent(OnBtnConfirm);

        Refresh();

        return true;
    }

    public void Refresh() {
        Initialize();

        GetObject((int)Objects.InputField).GetComponent<TMP_InputField>().text = Main.Game.PlayerName;
        GetImage((int)Images.imgCharacterSprite).sprite = Main.Resource.Load<Sprite>($"{Main.Game.PlayerKey}.sprite");
    }


    #region OnButtons

    private void OnBtnChange() {
        Main.UI.ShowPopupUI<UI_Popup_CharacterSelect>();
    }
    private void OnBtnConfirm() {
        Main.Game.PlayerName = GetObject((int)Objects.InputField).GetComponent<TMP_InputField>().text;
        ClosePopupUI();
        Main.Object.Player.SetInfo(Main.Game.PlayerName, Main.Game.PlayerKey);
    }

    #endregion

}