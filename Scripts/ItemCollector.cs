using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    [SerializeField]private Text coinsText;
    private int counter=0;
    // Start is called before the first frame update
    
    [SerializeField] private AudioSource coinSoundEffect;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Coins"))
        {
            coinSoundEffect.Play();
            Destroy(collision.gameObject);
            counter++;
            coinsText.text = "Coins: " + counter;
        }
    }
}
