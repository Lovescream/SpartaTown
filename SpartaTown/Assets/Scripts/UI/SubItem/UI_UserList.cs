using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_UserList : UI_Base {

    #region Enums

    enum Objects {
        Content,
    }

    #endregion

    #region Fields

    private List<UI_UserListName> userList = new();

    #endregion

    #region MonoBehaviours

    void OnEnable() {
        Initialize();
    }

    #endregion

    public override bool Initialize() {
        if(!base.Initialize()) return false;

        BindObject(typeof(Objects));
        GetObject((int)Objects.Content).DestroyChilds();

        Refresh();

        return true;
    }

    public void Refresh() {
        Initialize();

        List<User> users = Main.Object.Users;
        for (int i = userList.Count - 1; i >= 0; i--) {
            if (userList[i].User == null) {
                UI_UserListName userName = userList[i];
                userList.RemoveAt(i);
                Main.Resource.Destroy(userName.gameObject);
            }
            if (users.Contains(userList[i].User)) users.Remove(userList[i].User);
        }
        foreach (User user in users) {
            UI_UserListName userName = Main.UI.CreateSubItem<UI_UserListName>(GetObject((int)Objects.Content).transform);
            userName.SetUser(user);
            userList.Add(userName);
        }

    }
}