using UnityEngine;

public class Color_Cubes : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float moveRange = 0.5f;
    public Material material;
    public Color color;
    private Vector3 startPosition;
    private Renderer renderer;
    void Start()
    {
        renderer = GetComponent<Renderer>();
        startPosition = transform.position;
        material = this.gameObject.GetComponent<MeshRenderer>().material;
    }
    void Update()
    {
        if (this.gameObject.tag == "Not_Colored")
        {
            float newY = startPosition.y + Mathf.Sin(Time.time * moveSpeed) * moveRange;
            transform.position = new Vector3(startPosition.x, newY, startPosition.z);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (this.gameObject.tag == "Not_Colored")
        {
            if (other.gameObject.tag == "Player")
            {
                color = other.GetComponent<Renderer>().material.color;
                material.color = color;
                this.gameObject.tag = "Untagged";
            }

            if (other.gameObject.tag == "Whisp")
            {
                renderer.sharedMaterial = other.GetComponent<Renderer>().material;
                this.gameObject.tag = "Untagged";
            }
        }

        if (other.tag == "Red")
        {
            this.gameObject.tag = "Red";
            material.color = Color.red;
        }

        if (other.tag == "Blue")
        {
            this.gameObject.tag = "Blue";
            material.color = Color.blue;
        }
    }
}
