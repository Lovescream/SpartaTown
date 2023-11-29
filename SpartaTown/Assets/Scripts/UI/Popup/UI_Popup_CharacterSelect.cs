using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Popup_CharacterSelect : UI_Popup {

    #region Enums

    enum Objects {
        UI_CharacterList,
    }

    enum Buttons {
        btnNext,
        btnPrev,
        btnClose,
    }

    #endregion

    #region Fields

    private UI_CharacterList list;

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
        
        this.list = GetObject((int)Objects.UI_CharacterList).GetComponent<UI_CharacterList>();
        list.Initialize();
        GetButton((int)Buttons.btnNext).gameObject.BindEvent(OnBtnNext);
        GetButton((int)Buttons.btnPrev).gameObject.BindEvent(OnBtnPrev);
        GetButton((int)Buttons.btnClose).gameObject.BindEvent(OnBtnClose);

        Refresh();

        return true;
    }

    private void Refresh() {
        GetButton((int)Buttons.btnPrev).gameObject.SetActive(list.Page > 0);
        GetButton((int)Buttons.btnNext).gameObject.SetActive(list.Page < list.MaxPage);
    }

    #region OnButtons

    private void OnBtnNext() {
        list.Page++;
        Refresh();
    }
    private void OnBtnPrev() {
        list.Page--;
        Refresh();
    }
    private void OnBtnClose() {
        Main.UI.ClosePopup();
    }

    #endregion

}