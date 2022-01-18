using System;
using System.Collections;
using System.Collections.Generic;using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform weaponArmTransform;
    
    [SerializeField] private float playerMovementSpeed;

    private Camera _mainCamera;
    
    private Rigidbody2D _playerRigidbody2D;
    
    private Vector2 _playerMovementInput;

    private Animator _playerAnimator;
    
    private void Start()
    {
        _playerRigidbody2D = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        _playerMovementInput.x = Input.GetAxisRaw("Horizontal");
        _playerMovementInput.y = Input.GetAxisRaw("Vertical");

        _playerMovementInput.Normalize();
        
        _playerRigidbody2D.velocity = _playerMovementInput * playerMovementSpeed;

        Vector3 mousePosition = Input.mousePosition;
        Vector3 screenPoint = _mainCamera.WorldToScreenPoint(transform.localPosition);

        Vector2 offset = new Vector2(mousePosition.x - screenPoint.x, mousePosition.y - screenPoint.y);
        float weaponRotateAngle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        
        weaponArmTransform.rotation = Quaternion.Euler(0f, 0f, weaponRotateAngle);

        if (mousePosition.x < screenPoint.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            weaponArmTransform.localScale = new Vector3(-1f, -1f, 1f);
        }
        else
        {
            transform.localScale = Vector3.one;
            weaponArmTransform.localScale = Vector3.one;
        }

        if (_playerMovementInput != Vector2.zero)
        {
            _playerAnimator.SetBool("isWalking", true);
        }
        else
        {
            _playerAnimator.SetBool("isWalking", false);
        }
    }
}
