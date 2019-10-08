using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LetterTile : MonoBehaviour {
    public string Letter;
    public Vector3 pickedOffset;

    private TextMeshPro letterDisplay;
    private static readonly int RAYCAST_IGNORE_LAYER = 2;
    private static readonly int RAYCAST_DEFAULT_LAYER = 0;

    // Start is called before the first frame update
    void Start() {
        letterDisplay = GetComponentInChildren<TextMeshPro>() ?? throw new MissingComponentException("No TextMeshPro found in LetterTile");
        letterDisplay.text = Letter;
    }

    // Update is called once per frame
    void Update() {

    }

    public void Select() {
        transform.position = transform.position + pickedOffset;
        gameObject.layer = RAYCAST_IGNORE_LAYER;
    }

    public void Delesect() {
        transform.position = transform.position - pickedOffset;
        gameObject.layer = RAYCAST_DEFAULT_LAYER;
    }

    public void Init(string letter) {
        Letter = letter;
    }
}