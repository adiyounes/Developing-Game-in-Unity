using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private AudioSource deathSoundEffect;
    void Start()
    {
        
    }

    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name=="Luffy")
        {
            deathSoundEffect.Play();
            Invoke("Over",2f);

        }
    }
    private void Over()
    {
        SceneManager.LoadScene(4);
    }
}
