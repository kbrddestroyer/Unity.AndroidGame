using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Controls;

using Mirror;
using NetworkController;
using System.Net.NetworkInformation;

[RequireComponent(typeof(NetworkIdentity))]
[RequireComponent(typeof(NetworkTransform))]
public class Player : NetworkBehaviour
{
    [Header("Base Settings")]
    [SerializeField, Range(0f, 10f)] protected float speed;
    [SerializeField, Range(0f, 10f)] protected float msens;
    [SerializeField, Range(0f, 90f)] protected float cameraMaxAngle;    // Rotation: [-cameraMaxAngle, cameraMaxAngle]
    [Header("Components")]
    [SerializeField] private Transform mainCamera;
    [SerializeField] private OnScreenStick stick;
    private Vector3 rotation;

    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        Debug.Log("Connected!");
        mainCamera.gameObject.SetActive(isOwned);
    }

    public void OnConnectedToServer()
    {
        Debug.Log("Connected!");
        mainCamera.gameObject.SetActive(isOwned);
    }

    private void Update()
    {
        if (isOwned)
        {
            // Execute code only if this object is owned by client

            // Movement control
            Vector2 control = playerInput.actions["Move"].ReadValue<Vector2>();
            Debug.Log($"Tou: {playerInput.actions["Move"].inProgress}");
            float speedX = control.x;
            float speedY = control.y;
            transform.Translate(new Vector3(speedX, 0, speedY) * speed * Time.deltaTime);
            // Camera control
            if (Touchscreen.current == null) return;
            foreach (TouchControl touch in Touchscreen.current.touches)
            {
                if (touch.isInProgress && !EventSystem.current.IsPointerOverGameObject(touch.touchId.ReadValue()))
                {
                    //
                    //  Execute only if current touch is not on GUI element (button, joystick, etc.)
                    //
                    Vector2 cameraControl = touch.delta.ReadValue();
                    float mouseX = cameraControl.x;
                    float mouseY = cameraControl.y;
                    rotation.x -= mouseY;
                    rotation.y += mouseX;
                    rotation.x = Mathf.Clamp(rotation.x, -cameraMaxAngle, cameraMaxAngle);
                    this.transform.rotation = Quaternion.Euler(0, rotation.y, 0);
                    mainCamera.rotation = Quaternion.Euler(rotation.x, rotation.y, 0);
                }
            }
        }
    }
}
