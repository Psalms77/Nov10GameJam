using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour
{
    public int mine_size = 100;
    public float noiseFreq = 0.05f;
    public float caveParam = 0.3f;
    public float seed;
    public Texture2D noiseTexture;
    public Sprite tile;
    public Sprite dirt;
    public Sprite stone;
    public Sprite diamond;



    private void Start()
    {
        seed = Random.Range(-10000, 10000);
        GenerateNoiseTexture();
        GenerateTerrain();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateTerrain()
    {
        for (int x = 0; x < mine_size; x++)
        {
            for (int y = 0; y < mine_size; y++)
            {
                if (noiseTexture.GetPixel(x, y).r > caveParam)
                {
                    GameObject newtile = new GameObject(name = "tile");
                    newtile.transform.parent = this.transform;
                    newtile.AddComponent<SpriteRenderer>();
                    newtile.GetComponent<SpriteRenderer>().sprite = tile;
                    newtile.transform.position = new Vector2(x+0.5f, y + 0.5f);
                }

            }
        }
    }

    public void GenerateNoiseTexture()
    {
        noiseTexture = new Texture2D(mine_size, mine_size);

        for (int x = 0; x < noiseTexture.width; x++)
        {
            for (int y = 0; y < noiseTexture.height; y++)
            {
                float v = Mathf.PerlinNoise((x+seed) * noiseFreq, (y+seed) * noiseFreq);
                noiseTexture.SetPixel(x, y, new Color(v, v, v));
            }
        }

        noiseTexture.Apply();
    }



}
