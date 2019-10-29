using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public interface ITileHolder {
    /// <summary>
    /// Place the chosen tile on this <see cref="ITileHolder"/>
    /// </summary>
    /// <param name="tile">Tile to remove</param>
    void PlaceTile(LetterTile tile);

    /// <summary>
    /// Remove the chosen tile from this <see cref="ITileHolder"/>
    /// </summary>
    /// <param name="tile"></param>
    void RemoveTile(LetterTile tile);
}
