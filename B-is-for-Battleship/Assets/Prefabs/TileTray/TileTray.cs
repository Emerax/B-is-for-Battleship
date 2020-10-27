using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class TileTray : TileHolder {
    private readonly List<LetterTile> playerHand = new List<LetterTile>();
    private Lexicon lexicon;

    public float tileOffsetY;
    public float tileOffsetX;
    public GameObject tileTray;

    public int handSize;
    public TilePile tilePile;

    private void Start() {
        lexicon = FindObjectOfType<Lexicon>();
        lexicon.Init(Languages.ENGLISH);
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
        ReorderTilesAround(position);
        PlaceTileHovering(tile, hit);
    }

    public override void OnTileStopHover() {
        ReorderTiles();
    }

    private void ReorderTiles() {
        for(int i = 0; i < playerHand.Count; ++i) {
            Vector3 newPos = new Vector3((i - 3) * tileOffsetX, tileOffsetY, 0);
            playerHand[i].transform.localPosition = newPos;
        }
        MarkLegalWords();
    }

    private void MarkLegalWords() {
        HashSet<int> indices = lexicon.FindWordIndices(HandToString());
        for(int i = 0; i < playerHand.Count; ++i) {
            if (indices.Contains(i)) {
                //TODO: Mark the corresponding index in the hand.
            }
        }
    }

    private void ReorderTilesAround(int avoidPos) {
        int nextPlacementPos = 0;
        foreach (LetterTile tile in playerHand) {
            if(nextPlacementPos == avoidPos + 3) {
                ++nextPlacementPos;
            }
            Vector3 newPos = new Vector3((nextPlacementPos - 3) * tileOffsetX, tileOffsetY, 0);
            tile.transform.localPosition = newPos;
            ++nextPlacementPos;
        }
    }

    private void FillHand() {
        while (playerHand.Count < handSize) {
            LetterTile newTile = tilePile.DrawTile();
            PlaceTile(newTile);
            newTile.OnPlaced(this);
        }
    }

    public void PlaceTile(LetterTile tile) {
        tile.transform.parent = transform;
        playerHand.Add(tile);
        ReorderTiles();
    }

    public override void PlaceTile(LetterTile tile, RaycastHit hit) {
        int position = WorldToHandPos(hit.point);
        tile.transform.parent = transform;
        playerHand.Insert(Mathf.Clamp(position + 3, 0, playerHand.Count), tile);
        ReorderTiles();
    }

    private void PlaceTileHovering(LetterTile tile, RaycastHit hit) {
        Vector3 newPos = new Vector3(WorldToHandPos(hit.point) * tileOffsetX, tileOffsetY, 0) {
            y = tileHoverHeight
        };
        tile.transform.position = transform.TransformPoint(newPos);
        tile.LastTileHolder = this;
    }

    public override void RemoveTile(LetterTile tile) {
        playerHand.Remove(tile);
        ReorderTiles();
    }

    private int WorldToHandPos(Vector3 pos) {
        return Mathf.Clamp((int)Mathf.Floor(pos.x + 0.5f), -3, 3);
    }

    public override bool Vacant() {
        return playerHand.Count > handSize;
    }
}
