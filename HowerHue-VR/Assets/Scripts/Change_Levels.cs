using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Change_Levels : MonoBehaviour
{
    public ParticleSystem particles;
    public string SceneName;
    private Color Firstcolor;
    private float countdown;

    public AudioSource SpawnOut;

    void Start()
    {
        Firstcolor = particles.startColor;
        countdown = 4.0f;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Red" || other.tag == "Blue")
        {
            particles.startColor = Color.green;
        }

    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Red" || other.tag == "Blue")
        {
            countdown -= Time.deltaTime;
            if (countdown <= 0)
            {
                SpawnOut.Play();
                StartCoroutine(GetOut());
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Red" || other.tag == "Blue")
        {
            particles.startColor = Firstcolor;
            countdown = 4.0f;
        }

    }

    IEnumerator GetOut()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(SceneName);
    }

}
