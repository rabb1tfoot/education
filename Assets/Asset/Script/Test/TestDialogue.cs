using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDialogue : MonoBehaviour
{
    [SerializeField]
    public Dialogue dialogue;

    private DialogueManager DM;

    // Start is called before the first frame update
    void Start()
    {
        DM = FindObjectOfType<DialogueManager>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            DM.ShowDialogue(dialogue);
        }
    }
}
