using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    #region Properties
    public float CamWidth { get; private set; }
    public float CamHeight { get; private set; }

    #endregion

    #region Fields

    private Transform target;

    #endregion

    void Awake() {
        Camera.main.orthographicSize = 5f;
        CamHeight = Camera.main.orthographicSize;
        CamWidth = CamHeight * Camera.main.aspect;
    }

    void LateUpdate() {
        Follow();
    }

    public void SetTarget(Transform target) => this.target = target;

    private void Follow() {
        if (target == null || Main.Game.CurrentMap == null) return;

        Vector3 position = target.transform.position;
        Vector2 MapCenter = Main.Game.CurrentMap.Center;

        float limitX = Main.Game.CurrentMap.Size.x * 0.5f - CamWidth;
        float limitY = Main.Game.CurrentMap.Size.y * 0.5f - CamHeight;

        float x = Mathf.Clamp(position.x, MapCenter.x - limitX, MapCenter.x + limitX);
        float y = Mathf.Clamp(position.y, MapCenter.y - limitY, MapCenter.y + limitY);
        float z = -10;

        this.transform.position = new(x, y, z);
    }

}