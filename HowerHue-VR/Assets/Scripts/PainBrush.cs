using UnityEngine;

public class PainBrush : MonoBehaviour
{
    public Color color;
    public Material material;
    public AudioSource Get_Color;

    void Start()
    {
        material = this.gameObject.GetComponent<MeshRenderer>().material;
    }

    void Update()
    {
        material.color = color;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Coloring")
        {
            color = other.GetComponent<Renderer>().material.color;
            Get_Color.Play();
        }

        if (other.tag == "Darker")
        {
            color = new Color(color.r - 0.1f, color.g - 0.1f, color.b - 0.1f);
        }

        if (other.tag == "Lighter")
        {
            color = new Color(color.r + 0.1f, color.g + 0.1f, color.b + 0.1f);
        }

        if (other.tag == "Remove")
        {
            color = Color.white;
            this.gameObject.tag = "Remove";
        }

        if (other.tag == "Red")
        {
            color = Color.red;
            this.gameObject.tag = "Red";
        }

        if (other.tag == "Blue")
        {
            color = Color.blue;
            this.gameObject.tag = "Blue";
        }

    }
}
