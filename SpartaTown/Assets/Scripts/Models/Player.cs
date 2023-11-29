using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : User {

    #region Inspector

    [SerializeField]
    private float speed;

    #endregion

    #region Fields

    private Vector2 input;

    #endregion

    void Update() {
        if (input.x != 0 || input.y != 0) {
            this.rb.MovePosition(rb.position + input * speed * Time.deltaTime);
            animator.SetFloat("Speed", speed);
        }
        else animator.SetFloat("Speed", 0);
    }

    public override bool Initialize() {
        base.Initialize();

        FindObjectOfType<CameraController>().SetTarget(this.transform);

        return true;
    }

    #region Input

    private void OnMove(InputValue value) {
        input = value.Get<Vector2>().normalized;
    }
    private void OnLook(InputValue value) {
        spriter.flipX = Camera.main.ScreenToWorldPoint(value.Get<Vector2>()).x < this.transform.position.x;
    }

    #endregion

}