using UnityEngine;
using TMPro;
using UnityEngine.Assertions;

public class LetterTile : MonoBehaviour {
    public string Letter;
    public TileHolder LastTileHolder;

    private TextMeshPro letterDisplay;
    private TileHolder holder = null;

    // Start is called before the first frame update
    void Start() {
        letterDisplay = GetComponentInChildren<TextMeshPro>();
        Assert.IsNotNull(letterDisplay, "No TextMeshPro in LetterTile!");
        letterDisplay.text = Letter;
    }

    public void Select() {
        holder.RemoveTile(this);
    }

    public void Delesect() {
    }

    public void Init(string letter) {
        Letter = letter;
    }

    public void Place(TileHolder holder) {
        this.holder = holder;
    }
}