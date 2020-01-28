using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class TileTray : TileHolder {
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

    public override void OnTileHover(LetterTile tile, RaycastHit hit) {
        int position = WorldToHandPos(hit.point);
        int tileIndex = 0;
        for (int i = 0; i < handSize; ++i) {
            Vector3 newPos = new Vector3(i * tileOffsetX - 3, tileOffsetY, 0);
            if(tileIndex < playerHand.Count && i - 3 != position) {
                playerHand[tileIndex].transform.localPosition = newPos;
                ++tileIndex;
            } else {
                newPos.y = tileHoverHeight;
                tile.transform.position = transform.TransformPoint(newPos);
                tile.LastTileHolder = this;
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

    public override void PlaceTile(LetterTile tile, RaycastHit hit) {
        int position = WorldToHandPos(hit.point);
        tile.Place(this);
        tile.transform.parent = transform;
        playerHand.Insert(position + 3, tile);
        ReorderTiles();
    }

    public override void RemoveTile(LetterTile tile) {
        playerHand.Remove(tile);
    }

    private int WorldToHandPos(Vector3 pos) {
        return Mathf.Clamp((int)Mathf.Floor(pos.x + 0.5f), -3, 3);
    }
}
