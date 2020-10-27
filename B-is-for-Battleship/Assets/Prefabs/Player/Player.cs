using UnityEngine;

public class Player : MonoBehaviour {
    private LetterTile pickedTile;
    private TileHolder lastHolder;

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
            if (Input.GetMouseButtonDown(0)) {
                LeftMouseDown(hit);
            }
            if (Input.GetMouseButton(0)) {
                LeftMouseHeld(hit);
            }
            if (Input.GetMouseButtonUp(0)) {
                LeftMouseReleased(hit);
            }
        } else {
            if (Input.GetMouseButtonUp(0)) {
                //Last resort if the player releases left mouse outside the play area.
                LeftMouseReleased();
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
            TileHolder holder = hit.collider.gameObject.GetComponent<TileHolder>();
            if (holder != null) {
                if (lastHolder != null && lastHolder != holder) {
                    lastHolder.OnTileStopHover();
                }
                lastHolder = holder;

                holder.OnTileHover(pickedTile, hit);
                if (holder.Vacant()) {
                    pickedTile.LastTileHolder = holder;
                }

            }
        }
    }

    private void LeftMouseReleased(RaycastHit hit) {
        if (pickedTile != null) {
            TileHolder holder = hit.collider.gameObject.GetComponent<TileHolder>();
            if(holder != null && holder.Vacant()) {
                holder.PlaceTile(pickedTile, hit);
                pickedTile.OnPlaced(holder);
            } else {
                // Drop held tile on last known TileHolder.
                pickedTile.LastTileHolder.PlaceTile(pickedTile, hit);
                pickedTile.OnPlaced(pickedTile.LastTileHolder);
            }
            pickedTile.Delesect();
        }
        pickedTile = null;
    }

    /// <summary>
    /// Overload for when there is no hit point to use. Drop held tile on last seen board tile immediately.
    /// </summary>
    private void LeftMouseReleased() {
        if (pickedTile != null) {
            pickedTile.LastTileHolder.PlaceTile(pickedTile, new RaycastHit());
            pickedTile.Delesect();
            pickedTile = null;
        }
    }
}
