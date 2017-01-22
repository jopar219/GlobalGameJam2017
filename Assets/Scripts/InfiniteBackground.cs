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
  private int neededTiles;

  private int nextTile {
    get {
      return currentTile + 1 == tiles.Count ?  0 : currentTile + 1;
    }
  }

  void PlaceTile() {
    tiles[nextTile].transform.position = new Vector2(tiles[currentTile].transform.position.x + tileWidth, tiles[currentTile].transform.position.y);

    currentTile = nextTile;
  }

	// Use this for initialization
	void Start () {
    GameObject tile = (GameObject) GameObject.Instantiate(TilesPrefab, transform);
    tile.transform.localPosition = Vector3.zero;

	  tileWidth = tile.GetComponent<SpriteRenderer>().sprite.bounds.size.x * tile.transform.localScale.x - 0.05f;
    currentTile = 1;
    
    neededTiles = (int) Mathf.Ceil((MainCamera.orthographicSize * 2 * MainCamera.aspect) / tileWidth) + 2;

    tiles = new List<GameObject>(neededTiles);

    tiles.Add(tile);

    for(int i = 1; i < neededTiles; i++) {
      tiles.Add((GameObject) GameObject.Instantiate(TilesPrefab, transform));
      tiles[i].transform.localPosition = Vector3.zero;
    }
	}
	
	// Update is called once per frame
	void Update () {
    neededTiles = (int) Mathf.Ceil((MainCamera.orthographicSize * 2 * MainCamera.aspect) / tileWidth) + 2;
    
    if(neededTiles > tiles.Count) {
      for(int i = tiles.Count; i < neededTiles; i++) {
        tiles.Add((GameObject) GameObject.Instantiate(TilesPrefab, transform));
        tiles[i].transform.localPosition = Vector3.zero;
      }
    }

    if(tiles[currentTile].transform.position.x - Target.position.x < tileWidth) {
      PlaceTile();
    }
	}
}
