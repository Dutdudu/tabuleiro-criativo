using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NaFila : MonoBehaviour {

    public TMP_Text naFilaButton;

    public void inQueue() {
        naFilaButton.text = "Na Fila";
    }

    public void exitQueue() {
        naFilaButton.text = "Jogar (Multiplayer)";
    }
}
