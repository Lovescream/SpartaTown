using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour {

    #region Properties

    public string Name {
        get => _name;
        set {
            _name = value;
            txtName.SetText(value);
            cbOnNameChanged?.Invoke();
        }
    }
    public string CharacterKey { get; private set; }

    #endregion

    #region Fields

    private string _name;

    // Components.
    protected UI_UserName txtName;
    protected SpriteRenderer spriter;
    protected Animator animator;
    protected Rigidbody2D rb;

    public Action cbOnNameChanged;

    private bool initialized;

    #endregion

    #region MonoBehaviours

    void Awake() {
        Initialize();
    }

    #endregion

    #region Initialize / SetInfo

    public virtual bool Initialize() {
        if (initialized) return false;
        initialized = true;

        txtName = this.transform.Find("UI_UserName").GetComponent<UI_UserName>();
        spriter = this.GetComponent<SpriteRenderer>();
        animator = this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();

        return true;
    }

    public virtual void SetInfo(string name, string key) {
        Initialize();

        Name = name;
        CharacterKey = key;

        SetVisual();
    }

    private void SetVisual() {
        // #1. Sprite 설정.
        spriter.sprite = Main.Resource.Load<Sprite>($"{CharacterKey}.sprite");

        // #2. Animator 설정.
        animator.runtimeAnimatorController = Main.Resource.Load<RuntimeAnimatorController>($"{CharacterKey}.animController");
    }

    #endregion

}