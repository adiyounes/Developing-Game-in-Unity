using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    private bool b=false;
    [SerializeField] AudioSource endSoundEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name=="Luffy" && !b)
        {
            b=true;
            endSoundEffect.Play();
            Invoke("CompleteLevel",3f);
        }
    }

    private void CompleteLevel()
    {
        SceneManager.LoadScene(3);
    }
}
