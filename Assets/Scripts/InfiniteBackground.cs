using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InfiniteBackground : MonoBehaviour {
  
  public GameObject TilesPrefab;
  public Transform Target;
  public Camera MainCamera;

  private float tileWidth;
  private int currentTile;
  private List<GameObject> tiles;

  private int nextTile {
    get {
      return currentTile + 1 == tiles.Count ?  0 : currentTile + 1;
    }
  }

	// Use this for initialization
	void Start () {
    GameObject tile = (GameObject) GameObject.Instantiate(TilesPrefab, transform);

	  tileWidth = tile.GetComponent<SpriteRenderer>().sprite.bounds.size.x * tile.transform.localScale.x;
    currentTile = 0;
    
    int neededTiles = (int) Mathf.Ceil((MainCamera.orthographicSize * 2 * MainCamera.aspect) / tileWidth) + 1;

    Debug.Log(neededTiles);

    tiles = new List<GameObject>(neededTiles);

    tiles.Add(tile);

    for(int i = 1; i < neededTiles; i++) {
      tiles.Add((GameObject) GameObject.Instantiate(TilesPrefab, transform));
    }
	}
	
	// Update is called once per frame
	void Update () {
	}
}
