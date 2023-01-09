using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class PlayerTool : MonoBehaviour
{
    [Header("Set Dynamically")]

    [SerializeField] internal Image toolImage;


    internal void ChangeToolImage(Sprite sprite)
    {

        if (sprite == null)
        {
            toolImage.enabled = false;
            return;
        }

        //Debug.LogFormat("Cahnge tool image {0}", sprite.name);


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
