using UnityEngine;
using System.Collections;

public class CameraOrbit : MonoBehaviour {

    public Transform target;

    public float horizMove = 1f;
    public float vertMove = 1f;

    public Vector3 offset;

    public TileMap tm;

    void Start()
    {
        tm = target.GetComponent<TileMap>();
        offset = new Vector3(tm.size_x / 2f, 0f, tm.size_z / 2);
    }

    public void MoveHorizontal(bool left)
    {
        
        float dir = 0.7f;

        if (!left)
            dir *= -1f;
        transform.RotateAround(target.position + offset, Vector3.up, dir);
    }

    public void MoveVertical(bool up)
    {
        float dir = .7f;

        if (!up)
            dir *= -1f;
        transform.RotateAround(target.position + offset, transform.TransformDirection(Vector3.right), dir);
    }

    public void zoomIn(bool zoIn)
    {
        float dir = 41f;
        if (!zoIn)
        {
            dir *= -1f;
        }

        transform.Translate(new Vector3(0f, 0f, dir));
    }
}
