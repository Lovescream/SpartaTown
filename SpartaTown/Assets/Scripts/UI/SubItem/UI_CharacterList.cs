using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_CharacterList : UI_Base {

    #region Properties

    public int Page {
        get => currentPage;
        set {
            if (value <= 0) {
                currentPage = 0;
            }
            else if (value >= maxPage) {
                currentPage = maxPage;
            }
            else
                currentPage = value;
            Refresh();
        }
    }
    public int MaxPage => maxPage;

    #endregion

    #region Fields

    private string[] keys;
    private UI_CharacterCard[] cards;
    private int maxCard = 3;
    private int currentPage = 0;
    private int maxPage = 0;

    #endregion

    #region MonoBehaviours

    void OnEnable() {
        Initialize();
    }

    #endregion

    public override bool Initialize() {
        if (base.Initialize() == false) return false;

        keys = Main.Game.CharacterKeys;
        maxPage = keys.Length - maxCard;

        this.gameObject.DestroyChilds();
        cards = new UI_CharacterCard[maxCard];
        for (int i = 0; i < maxCard; i++) {
            UI_CharacterCard card = Main.UI.CreateSubItem<UI_CharacterCard>(this.transform);
            card.GetComponent<UI_CharacterCard>().SetInfo(keys[Page + i]);
            cards[i] = card;
        }

        Refresh();

        return true;
    }

    private void Refresh() {
        Initialize();
        for (int i = 0; i < maxCard; i++) {
            string key = Page + i < keys.Length ? keys[Page + i] : "";
            cards[i].SetInfo(key);
        }
    }

}