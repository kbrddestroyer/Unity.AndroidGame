using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Controls;

public class Player : MonoBehaviour
{
    [Header("Base Settings")]
    [SerializeField, Range(0f, 10f)] protected float speed;
    [SerializeField, Range(0f, 10f)] protected float msens;
    [Header("Components")]
    [SerializeField] private Transform mainCamera;
    [SerializeField] private OnScreenStick stick;
    private Vector3 rotation;

    private PlayerInput playerInput;
    

    private void Awake()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        Vector2 control = playerInput.actions["Move"].ReadValue<Vector2>();
        Debug.Log($"Tou: {playerInput.actions["Move"].inProgress}");
        float speedX = control.x;
        float speedY = control.y;
        
        // transform.position += new Vector3(speedX, 0, speedY) * speed * Time.deltaTime;

        transform.Translate(new Vector3(speedX, 0, speedY) * speed * Time.deltaTime);

        // Camera control
        foreach (TouchControl touch in Touchscreen.current.touches)
        {
            if (touch.isInProgress && !EventSystem.current.IsPointerOverGameObject(touch.touchId.ReadValue()))
            {
                Vector2 cameraControl = touch.delta.ReadValue();
                float mouseX = cameraControl.x;
                float mouseY = cameraControl.y;
                rotation.x -= mouseY;
                rotation.y += mouseX;
                rotation.x = Mathf.Clamp(rotation.x, -69f, 69f);
                this.transform.rotation = Quaternion.Euler(0, rotation.y, 0);
                mainCamera.rotation = Quaternion.Euler(rotation.x, rotation.y, 0);
            }
        }
    }
}
