using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloLensWithOpenCVForUnityExample;

public class PersonOverlayer : RectQuad
{
    [SerializeField] TextMesh m_Label = null;
    [SerializeField] MeshRenderer m_Renderder = null;
    [SerializeField] Material m_Material = null;
    private Material m_MaterialCache;

    private void Awake()
    {
        m_MaterialCache = new Material(m_Material);
        m_Renderder.material = m_MaterialCache;
    }
    public void SetText(string username, string detail ,Color color = default(Color))
    {
        var text = username;
        if (detail!= null && !detail.Equals(""))
        {
            text = text + "\n" + detail;
        }
        if (color == default(Color))
            color = Color.red;
        m_Label.text = text;
        m_Renderder.material.SetColor("_LineColor", color);
    }

    public string GetText()
    {
        return m_Label.text;
    }
}
