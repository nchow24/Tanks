using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* changeTerrainType : MonoBehaviour
 *
 * Changes the text and terrain type
 * TODO:
 *  - Add additional terrains that will change onClick
 */

[System.Serializable]
public class TerrainType : MonoBehaviour {
    private string[] terrainType = { "DESERT", "MOUNTAIN" };
    private Sprite[] terrainPicture;

    private int currentTerrain;
    private Text terrainText;
    private Image terrainImage;

	public string getTerrainType(){ return terrainText.text; } 

    void Start()
    {
        terrainText = GameObject.Find("Terrain Type Text").GetComponent<Text>();
        terrainImage = GameObject.Find("Terrain Panel").GetComponent<Image>();
        currentTerrain = 0;

        terrainPicture = new Sprite[2] {
            GameObject.Find("bg-Desert").GetComponent<SpriteRenderer>().sprite,
            GameObject.Find("bg-Mountains").GetComponent<SpriteRenderer>().sprite
        };
    }

    void Update()
    {
		terrainText.text = terrainType[currentTerrain];
    }

    public void incrementTerrain()
    {
        currentTerrain = (currentTerrain + 1) % terrainType.Length;
        terrainText.text = terrainType[currentTerrain];
        terrainImage.sprite = terrainPicture[currentTerrain];
    }

    public void decrementTerrain()
    {
        currentTerrain = (currentTerrain - 1) % terrainType.Length;
        if (currentTerrain == -1)
        {
            currentTerrain = terrainType.Length - 1;
        }
        terrainText.text = terrainType[currentTerrain];
        terrainImage.sprite = terrainPicture[currentTerrain];
    }
}
