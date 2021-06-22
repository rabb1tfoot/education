using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnemy : MonoBehaviour
{
    public GameObject prefabFloatingText;
    public GameObject effect;
    public GameObject parent;

    public string atkSound;

    private PlayerState playerstate;


    void Start()
    {
        playerstate = FindObjectOfType<PlayerState>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "enemy")
        {
            int dmg = collision.gameObject.GetComponent<EnemyState>().Hit(playerstate.atk);
            AudioManager.instance.Play(atkSound);

            Vector3 vector = collision.transform.position;
            Instantiate(effect, vector, Quaternion.Euler(Vector3.zero));
            vector.y += 60;

            GameObject clone = Instantiate(prefabFloatingText, vector, Quaternion.Euler(Vector3.zero));
            clone.GetComponent<FloatingText>().text.text = dmg.ToString();
            clone.GetComponent<FloatingText>().text.color = Color.white;
            clone.GetComponent<FloatingText>().text.fontSize = 25;
            clone.transform.SetParent(parent.transform);
        }
    }
}
