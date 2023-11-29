using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_UserListName : UI_Base {

    public User User { get; private set; }

    private TextMeshProUGUI txtName;

    public override bool Initialize() {
        if (!base.Initialize()) return false;

        this.txtName = this.GetComponent<TextMeshProUGUI>();

        return true;
    }

    public void SetUser(User user) {
        Initialize();
        User = user;
        SetName();
        user.cbOnNameChanged -= SetName;
        user.cbOnNameChanged += SetName;
    }

    private void SetName() {
        txtName.text = User.Name;
    }

}