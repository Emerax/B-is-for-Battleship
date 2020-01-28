using UnityEngine;

/// <summary>
/// 
/// </summary>
public abstract class TileHolder : MonoBehaviour {
    public float tileHoverHeight;

    /// <summary>
    /// Place the chosen tile on this <see cref="TileHolder"/>
    /// </summary>
    /// <param name="tile">Tile to remove</param>
    /// <param name="hit">Optional hit info for placing the tile</param>
    public abstract void PlaceTile(LetterTile tile, RaycastHit hit);

    /// <summary>
    /// Remove the chosen tile from this <see cref="TileHolder"/>
    /// </summary>
    /// <param name="tile"></param>
    public abstract void RemoveTile(LetterTile tile);

    /// <summary>
    /// Set tile to hover above this holder.
    /// </summary>
    /// <param name="tile"></param>
    /// <param name="hit">Optional hit info for placing the tile</param>
    public abstract void OnTileHover(LetterTile tile, RaycastHit hit);
}
