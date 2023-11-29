using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour {

    #region Properties

    public Vector2 Size { get; private set; }
    public Vector2 Center { get; private set; }

    #endregion

    #region Fields

    // Components.
    private Grid grid;
    private TilemapCollider2D tilemapCollider;

    #endregion

    #region Initialize

    public void Initialize() {
        grid = this.gameObject.GetOrAddComponent<Grid>();
        tilemapCollider = grid.transform.GetChild(0).GetComponent<TilemapCollider2D>();
        Size = tilemapCollider.bounds.size;
        Center = tilemapCollider.bounds.center;
        Main.Game.CurrentMap = this;
    }

    #endregion

}