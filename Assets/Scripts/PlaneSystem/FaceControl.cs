using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FaceControl : MonoBehaviour
{
    public MeshRenderer Mesh;
    public TMP_Text Text;

    public void showTextFace(string text)
    {
        Mesh.material.SetColor("_BaseColor", new Color(0, 0, 0, 0));
        Text.text = text;
    }

    public void showSpriteFace(Texture texture)
    {
        Text.text = string.Empty;
        Mesh.material.SetTexture("_BaseMap", texture);
        Mesh.material.SetColor("_BaseColor", new Color(0, 0, 0, 1));
    }
    public void hideAll()
    {
        Text.text = string.Empty;
        Mesh.material.SetColor("_BaseColor", new Color(0, 0, 0, 0));
    }
}
