using UnityEngine;
using TMPro;
using UnityEngine.Assertions;

public class LetterTile : MonoBehaviour {
    public string Letter;
    public ITileHolder LastTileHolder;

    private TextMeshPro letterDisplay;
    private ITileHolder holder = null;

    // Start is called before the first frame update
    void Start() {
        letterDisplay = GetComponentInChildren<TextMeshPro>();
        Assert.IsNotNull(letterDisplay, "No TMP in LetterTile!");
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

    public void Place(ITileHolder holder) {
        this.holder = holder;
    }
}