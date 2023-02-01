using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisHat : Equipable
{
    [System.Serializable]
    class TetrisPiece {
        public string enabled;
        public Color color = Color.white;
    }
    [SerializeField] List<TetrisPiece> pieces = new List<TetrisPiece>();

    float timer = 0;
    float interval = 5;
    int pieceIndex = 0;
    void Start() {
        pieceIndex = Random.Range(0, pieces.Count);
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0) {
            timer = interval;
            if(pieceIndex >= pieces.Count) {
                pieceIndex = 0;
            }

            TetrisPiece piece = pieces[pieceIndex];
            
            transform.GetChild(0).GetComponent<Renderer>().sharedMaterial.color = piece.color;

            char[] enabled = piece.enabled.ToCharArray();
            int enabledIndex = 0;
            foreach(Transform child in transform) {
                while(enabled[enabledIndex] != '0' && enabled[enabledIndex] != '1' && pieceIndex <= enabled.Length) {
                    enabledIndex++;
                }
                if(enabledIndex > enabled.Length) {
                    return;
                }
                Debug.Log(enabled[enabledIndex].ToString() + ": " + (enabled[enabledIndex] == '1'));
                child.gameObject.GetComponent<Renderer>().enabled = enabled[enabledIndex] == '1';
                enabledIndex++;
            }
            pieceIndex++;
            if(pieceIndex >= pieces.Count) {
                pieceIndex = 0;
            }
        }
    }
}
