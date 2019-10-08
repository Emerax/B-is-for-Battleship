using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Player : MonoBehaviour {
    private static readonly string BOARD_TILE_NAME = "BoardTile";
    private static readonly string TILE_TRAY_NAME = "TileTray";

    private LetterTile pickedTile;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        UpdateMouse();
    }

    private void UpdateMouse() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit)) {
            // Left mouse button down.
            if (Input.GetMouseButtonDown(0)) {
                LetterTile tile = hit.collider.GetComponent<LetterTile>();
                if(tile != null) {
                    pickedTile = tile;
                    pickedTile.Select();
                }
            // Left mouse kept down.
            } else if (Input.GetMouseButton(0)) {
                if (pickedTile != null) {
                    if(hit.collider.gameObject.name == BOARD_TILE_NAME) {
                        //Hovering over game board square.
                        Vector3 newPos = hit.collider.transform.position;
                        newPos.y = pickedTile.transform.position.y;
                        pickedTile.transform.position = newPos;
                    } else if(hit.collider.gameObject.name == TILE_TRAY_NAME) {
                        //Hovering over hand of letter tiles.
                        TileTray tray = hit.collider.transform.parent.GetComponent<TileTray>();
                        Vector3 newPos = hit.collider.transform.position;
                        newPos.x = (int)hit.point.x;
                        newPos.y = pickedTile.transform.position.y;
                        pickedTile.transform.position = newPos;
                    }
                }
            // Left mouse released.
            } else {
                if(pickedTile != null) {
                    pickedTile.Delesect();
                }
                pickedTile = null;
            }
        }
    }
}
