using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageBox : MonoBehaviour
{
    private Transform _cameraTransform;
    [SerializeField] private float maxWidth = 300f;  // 최대 너비 제한
    [SerializeField] private TMP_Text text;
    [SerializeField] private RectTransform background;
    [SerializeField] private Vector2 padding = new Vector2(5f, 5f); // 좌우 여백 (X: 좌우, Y: 상하)


    private void Update() => SetBackgroundWidth();

    private void LateUpdate() => SetUIForward(_cameraTransform.forward);

    // Start is called before the first frame update
    void Start()
    {
        SetCamera();
    }
    public void SetCamera()
    {
        _cameraTransform = Camera.main.transform;
    }

    private void SetUIForward(Vector3 target)
    {
        if (target == null || !gameObject.activeSelf ) return;
        transform.forward = target;
    }

    private void SetBackgroundWidth()
    {
        Vector2 textSize = text.GetPreferredValues();
        float width = Mathf.Min(textSize.x, maxWidth) + padding.x * 2;
        background.sizeDelta = new Vector2(width, textSize.y + padding.y);
    }
}
