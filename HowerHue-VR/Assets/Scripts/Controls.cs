using UnityEngine;

public class Controls : MonoBehaviour
{
    public GameObject LeftControl;
    public GameObject RightControl;
    public GameObject Hover_Board;

    public GameObject XR_Rig;

    public float angleForMove;

    [Range(0f, 1f)] public float configuration;

    public float speed;
    private Vector3 Left;
    private Vector3 Right;

    private float angleForLeft = 0;

    void Start()
    {
        Left = new Vector3(-1, 0, 0);
        Right = new Vector3(1, 0, 0);

    }

    void Update()
    {
        //make sure XR object doset fall off
        XR_Rig.transform.position = this.gameObject.transform.position;

        //To see if the controlers are in the same direction
        float ControlerCheck = Vector3.Dot(LeftControl.transform.forward.normalized, RightControl.transform.forward.normalized);

        //To see if the angle of the dot product is similar
        float dotProduct = Vector3.Dot(Right.normalized, LeftControl.transform.forward.normalized);
        angleForLeft = Mathf.Acos(dotProduct) * Mathf.Rad2Deg;

        if (ControlerCheck > configuration)
        {
            if (angleForLeft < angleForMove)
            {
                Hover_Board.transform.Translate(Right * speed * Time.deltaTime);
            }

            if (angleForLeft > 180 - angleForMove)
            {
                Hover_Board.transform.Translate(Left * speed * Time.deltaTime);
            }
        }
    }
}
