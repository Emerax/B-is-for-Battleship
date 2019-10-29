using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class TileTray : MonoBehaviour, ITileHolder {
    private readonly List<LetterTile> playerHand = new List<LetterTile>();

    public float tileOffsetY;
    public float tileOffsetX;
    public GameObject tileTray;

    public int handSize;
    public TilePile tilePile;

    private void Start() {
        FillHand();
    }

    private void Update() {
        //Debug.Log(HandToString());
    }

    public string HandToString() {
        StringBuilder stringBuilder = new StringBuilder();
        foreach(LetterTile tile in playerHand) {
            stringBuilder.Append(tile.Letter); 
        }

        return stringBuilder.ToString();
    }

    public void OnTileHover(LetterTile tile) {
        int tilePos = (int)tile.transform.position.x;
        int tileIndex = 0;
        for (int i = 0; i < handSize; ++i) {
            if(tileIndex < playerHand.Count && i - 3 != tilePos) {
                Vector3 newPos = new Vector3(i * tileOffsetX - 3, tileOffsetY, 0);
                playerHand[tileIndex].transform.localPosition = newPos;
                ++tileIndex;
            }
        }
    }

    private void ReorderTiles() {
        for(int i = 0; i < playerHand.Count; ++i) {
            Vector3 newPos = new Vector3((i - 3) * tileOffsetX, tileOffsetY, 0);
            playerHand[i].transform.localPosition = newPos;
        }
    }

    private void FillHand() {
        while (playerHand.Count < handSize) {
            LetterTile newTile = tilePile.DrawTile();
            PlaceTile(newTile);
        }
        
    }

    public void PlaceTile(LetterTile tile) {
        tile.Place(this);
        tile.transform.parent = transform;
        playerHand.Add(tile);
        ReorderTiles();
    }

    public void PlaceTile(LetterTile tile, int index) {
        tile.Place(this);
        tile.transform.parent = transform;
        playerHand.Insert(Mathf.Min(index, playerHand.Count), tile);
        ReorderTiles();
    }

    public void RemoveTile(LetterTile tile) {
        playerHand.Remove(tile);
    }
}
