using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_GameScene : UI_Scene {

    #region Enums

    enum Objects {
        UI_UserList,
    }
    enum Buttons {
        btnCharacterChange,
        btnToggleUserList,
    }

    #endregion

    #region Fields

    private GameObject userList;

    #endregion

    #region MonoBehaviours

    void OnEnable() {
        Initialize();
    }

    #endregion

    public override bool Initialize() {
        if (base.Initialize() == false) return false;

        BindObject(typeof(Objects));
        BindButton(typeof(Buttons));
        this.userList = GetObject((int)Objects.UI_UserList);
        userList.GetComponent<UI_UserList>().Refresh();

        GetButton((int)Buttons.btnCharacterChange).gameObject.BindEvent(OnBtnChangeCharacter);
        GetButton((int)Buttons.btnToggleUserList).gameObject.BindEvent(OnBtnToggleUserList);

        return true;
    }

    #region OnButtons

    private void OnBtnChangeCharacter() {
        Main.UI.ShowPopupUI<UI_Popup_ChangeCharacter>();
    }
    private void OnBtnToggleUserList() {
        if (userList.activeSelf) {
            userList.SetActive(false);
        }
        else {
            userList.SetActive(true);
            userList.GetComponent<UI_UserList>().Refresh();
        }
    }

    #endregion
}