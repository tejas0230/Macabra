using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureHelper : MonoBehaviour
{
    [SerializeField] private bool useColorForBack;
    [SerializeField] private bool useColorForFront;
    public Color frontColor;
    public Color backColor;
    [SerializeField]private Texture wallFront;
    [SerializeField]private Texture wallBack;

    public Material front;
    public Material back;

    private Renderer wall;

    private void Awake()
    {
        wall = GetComponent<Renderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if(useColorForFront)
        {
            wall.materials[0].mainTexture = null;
            wall.materials[0].color = frontColor;
        }
        else
        {
            wall.materials[0].color = Color.white;
            wall.materials[0].mainTexture = wallFront;
        }

        if (useColorForBack)
        {
            wall.materials[1].mainTexture = null;
            wall.materials[1].color =backColor;
        }
        else
        {
            wall.materials[1].color = Color.white;
            wall.materials[1].mainTexture = wallBack;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
