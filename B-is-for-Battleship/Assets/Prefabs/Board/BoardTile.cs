using UnityEngine;

public class BoardTile : TileHolder {
    private Board board;

    private LetterTile tile = null;

    public void Init(Board board) {
        this.board = board;
    }

    public override string ToString() {
        return (tile != null) ? "[" + tile.ToString() + "]" : "[ ]";
    }

    public override void PlaceTile(LetterTile tile, RaycastHit hit) {
        Vector3 newPos = transform.position;
        newPos.y = transform.position.y + transform.lossyScale.y / 2;
        tile.transform.position = newPos;
        tile.Place(this);
    }

    public override void RemoveTile(LetterTile tile) {
    }

    public override void OnTileHover(LetterTile tile, RaycastHit hit) {
        Vector3 newPos = transform.position;
        newPos.y = tileHoverHeight;
        tile.transform.position = newPos;
    }
}
