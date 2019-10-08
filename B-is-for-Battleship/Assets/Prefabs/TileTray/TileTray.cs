using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileTray : MonoBehaviour {
    private readonly List<LetterTile> playerHand = new List<LetterTile>();

    public float tileOffsetY;
    public float tileOffsetX;
    public GameObject tileTray;

    public int handSize;
    public TilePile tilePile;

    private void Start() {
        FillHand();
    }

    public void AddTile(LetterTile tile, int pos) {
        playerHand.Insert(pos, tile);
        tile.transform.parent = tileTray.transform;

        ReorderTiles();
    }

    public void MakeRoom(int pos) {
        for (int i = 0; i < playerHand.Count; ++i) {
            if( i != pos) {
                Vector3 newPos = new Vector3((i - 3) * tileOffsetX, tileOffsetY, 0);
                playerHand[i].transform.localPosition = newPos;
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
            newTile.transform.parent = transform;
            playerHand.Add(newTile);
        }
        ReorderTiles();
    }
}
