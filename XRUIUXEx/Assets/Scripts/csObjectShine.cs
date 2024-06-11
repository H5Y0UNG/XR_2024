using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csObjectShine : MonoBehaviour
{
    public Color GlowColor;
    public float LerpFactor = 10;

    private Material _material;
    private Color _currentColor;
    private Color _targetColor;

    void Start()
    {
        _material = GetComponent<Renderer>().material;
        _currentColor = _material.GetColor("_EmissionColor");
    }

    void Update()
    {
        _targetColor = GlowColor;
        _currentColor = Color.Lerp(_currentColor, _targetColor, LerpFactor * Time.deltaTime);

        _material.SetColor("_EmissionColor", _currentColor);
    }

}
