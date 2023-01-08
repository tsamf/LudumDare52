using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class PlayerOrgan : MonoBehaviour
{
    [Header("Set Dynamically")]

    [SerializeField] internal Image toolImage;

    internal void ChangeOrganImage(Sprite sprite)
    {
        if (sprite == null)
        {
            toolImage.enabled = false;
            return;
        }

        if (toolImage && sprite)
        {
            toolImage.sprite = sprite;
            toolImage.enabled = true;
        }

    }


    private void Awake()
    {
        toolImage = GetComponent<Image>();
    }
}