using UnityEngine;
using TMPro;
using UnityEngine.Assertions;

public class LetterTile : MonoBehaviour {
    public string Letter;
    public TileHolder LastTileHolder;

    public bool Marked { 
        get { 
            return Marked; } 
        set {

        }
    }

    private TileHolder holder;
    private TextMeshPro letterDisplay;

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

    public void OnPlaced(TileHolder holder) {
        LastTileHolder = holder;
        this.holder = holder;
    }

    public override string ToString() {
        return string.IsNullOrEmpty(Letter) ? " " : Letter;
    }
}