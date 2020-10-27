using UnityEngine;

public class BoardTile : TileHolder {
    private LetterTile tile = null;

    public override string ToString() {
        return (tile != null) ? "[" + tile.ToString() + "]" : "[ ]";
    }

    public override void PlaceTile(LetterTile tile, RaycastHit hit) {
        Vector3 newPos = transform.position;
        newPos.y = transform.position.y + transform.lossyScale.y / 2;
        tile.transform.position = newPos;
        this.tile = tile;
    }

    public override void RemoveTile(LetterTile tile) {
        this.tile = null;
    }

    public override void OnTileHover(LetterTile tile, RaycastHit hit) {
        Vector3 newPos = transform.position;
        newPos.y = tileHoverHeight;
        tile.transform.position = newPos;
    }

    public override void OnTileStopHover() {
        //Nothing yet
    }

    public override bool Vacant() {
        return tile == null;
    }
}
