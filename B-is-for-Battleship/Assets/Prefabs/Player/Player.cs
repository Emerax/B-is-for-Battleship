using UnityEngine;

public class Player : MonoBehaviour {
    public float tileHoverHeight;

    private static readonly string BOARD_TILE_NAME = "BoardTile";
    private static readonly string TILE_TRAY_NAME = "TileTray";
    private LetterTile pickedTile;

    // Update is called once per frame
    void Update() {
        UpdateMouse();
    }

    private void UpdateMouse() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        int layerMask = ~0;
        if (pickedTile != null) {
            //Ignore all letterTiles while we are holding one.
            layerMask = ~(1 << 8);
        }
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask)) {
            Debug.Log("Hit: " + hit.collider.gameObject.name);
            if (Input.GetMouseButtonDown(0)) {
                LeftMouseDown(hit);
            }
            if (Input.GetMouseButton(0)) {
                LeftMouseHeld(hit);
            }
            if (Input.GetMouseButtonUp(0)) {
                LeftMouseReleased(hit);
            }
        }
    }

    private void LeftMouseDown(RaycastHit hit) {
        LetterTile tile = hit.collider.GetComponent<LetterTile>();
        if (tile != null) {
            pickedTile = tile;
            pickedTile.Select();
        }
    }

    private void LeftMouseHeld(RaycastHit hit) {
        if (pickedTile != null) {
            if (hit.collider.gameObject.name == BOARD_TILE_NAME) {
                //Hovering over game board square.
                Vector3 newPos = hit.collider.transform.position;
                newPos.y = tileHoverHeight;
                pickedTile.transform.position = newPos;
                //TODO: Make tiles/board ITileHolder pickedTile.LastTileHolder = 
            } else if (hit.collider.gameObject.name == TILE_TRAY_NAME) {
                //Hovering over tile tray.
                TileTray tray = hit.collider.transform.parent.GetComponent<TileTray>();
                Vector3 newPos = hit.collider.transform.position;
                //Debug.Log(hit.point.x + 0.5f + " " + Mathf.FloorToInt(hit.point.x + 0.5f));
                newPos.x = Mathf.FloorToInt(hit.point.x + 0.5f);
                newPos.y = tileHoverHeight;
                pickedTile.transform.position = newPos;
                pickedTile.LastTileHolder = tray;

                tray.OnTileHover(pickedTile);
            }
        }
    }

    private void LeftMouseReleased(RaycastHit hit) {
        if (pickedTile != null) {
            if(hit.collider.gameObject.name == TILE_TRAY_NAME) {
                //Released over tile tray.
                TileTray tray = hit.collider.transform.parent.GetComponent<TileTray>();
                tray.PlaceTile(pickedTile, Mathf.FloorToInt(pickedTile.transform.position.x + 3));
            } else {
                pickedTile.LastTileHolder.PlaceTile(pickedTile);
            }
            pickedTile.Delesect();
        }
        pickedTile = null;
    }
}
