using System;
using System.Text;
using UnityEngine;

public class Board : MonoBehaviour {
    private static readonly int BOARD_WIDTH = 15;

    private readonly BoardTile[,] board = new BoardTile[BOARD_WIDTH, BOARD_WIDTH];

    // Start is called before the first frame update
    void Start() {
        int indexLimit = BOARD_WIDTH / 2;
        foreach(BoardTile tile in GetComponentsInChildren<BoardTile>()) {
            int x = Mathf.RoundToInt(tile.transform.localPosition.x) + indexLimit;
            int z = Mathf.RoundToInt(tile.transform.localPosition.z) + indexLimit;
            board[z, x] = tile;
        }
    }

    // Update is called once per frame
    void Update() {
        Debug.Log(ToString());
    }

    public override string ToString() {
        StringBuilder sb = new StringBuilder();
        sb.Append("\n");
        for(int z = 0; z < BOARD_WIDTH; ++z) {
            for(int x = 0; x < BOARD_WIDTH; ++x) {
                sb.Append(board[x, z].ToString());
            }
            sb.Append("\n");
        }
        return sb.ToString();
    }
}
