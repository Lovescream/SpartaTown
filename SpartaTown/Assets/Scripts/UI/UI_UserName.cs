using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_UserName : UI_Base {

    #region Enums

    enum Texts {
        txtUserName,
    }

    #endregion


    public override bool Initialize() {
        if (!base.Initialize()) return false;

        BindText(typeof(Texts));

        return true;
    }

    public void SetText(string name) {
        Initialize();
        GetText((int)Texts.txtUserName).text = name;
    }

    
}