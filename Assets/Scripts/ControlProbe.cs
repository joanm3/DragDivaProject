using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ReflectionProbe))]
//[ExecuteInEditMode]
public class ControlProbe : MonoBehaviour
{
    //http://docs.unity3d.com/Manual/AdvancedRefProbe.html per fer doble reflexe mirar. 
    public ReflectionType thisType = ReflectionType.mirror;
    //the vector the mirror is facing
    public Direction1 directionFaced;

    [SerializeField]
    private float m_yOffset = 0.5f;


    private float m_offset;
    private GameObject m_mirror;
    private GameObject m_player;

    public enum Direction1
    {
        X, Y, Z
    };

    public enum ReflectionType
    {
        mirror,
        window
    }


    void Start()
    {

        //get components
        m_player = GameObject.FindGameObjectWithTag("Player");
        if (m_player == null)
            Debug.LogError("No Player GameObject Found for " + gameObject.name);

        m_mirror = transform.parent.gameObject;
        if (m_mirror == null)
            Debug.LogError("No Mirror GameObject Found for " + gameObject.name);

    }


    void FixedUpdate()
    {

            //follows the direction the mirror is facing for the movement
            switch (directionFaced)
            {
                case Direction1.X:
                    SetDirection(Direction1.X, m_mirror.transform.position.x + m_offset, m_player.transform.position.y + m_yOffset, m_player.transform.position.z);
                    break;

                case Direction1.Y:
                    SetDirection(Direction1.Y, m_player.transform.position.x, m_mirror.transform.position.y + m_offset + m_yOffset, m_player.transform.position.z);
                    break;

                case Direction1.Z:

                    SetDirection(Direction1.Z, m_player.transform.position.x, m_player.transform.position.y + m_yOffset, m_mirror.transform.position.z + m_offset);
                    break;

                default:
                    SetDirection(Direction1.X, m_mirror.transform.position.x + m_offset, m_player.transform.position.y + m_yOffset, m_player.transform.position.z);
                    break;

            }


    }


    // calculates the director for static mirrors and windows
    private void SetDirection(Direction1 direction, float x, float y, float z)
    {
        m_offset = CalculateOffset(direction);
        gameObject.transform.position = new Vector3(x, y, z);

    }



    //calculates the offset between the character and the mirror or window
    private float CalculateOffset(Direction1 direction)
    {
        if (direction == Direction1.X)
            return m_mirror.transform.position.x - m_player.transform.position.x;
        else if (direction == Direction1.Y)
            return m_mirror.transform.position.y - m_player.transform.position.y;
        else if (direction == Direction1.Z)
            return m_mirror.transform.position.z - m_player.transform.position.z;
        else
        {
            Debug.Log("vector couldnt be calculated, set to default = x");
            return m_mirror.transform.position.x - m_player.transform.position.x;
        }

    }


    private Vector3 Offset()
    {

        return m_mirror.transform.position - m_player.transform.position;

    }

    void OnDrawGizmos()
    {

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        MeshFilter meshFilter = this.GetComponentInParent<MeshFilter>();
        Mesh mesh = meshFilter.sharedMesh;
        mesh.RecalculateNormals(); 

        Vector3 normal = mesh.normals[1];
        normal = this.transform.parent.transform.TransformVector(normal);

        Plane plane = new Plane(normal, transform.parent.transform.position);
        Vector3 projectionPlane = Formulas.ProjectPointOnPlane(player.transform.position, plane);
        Vector3 symmetricPoint = projectionPlane - (player.transform.position - projectionPlane); 
        Gizmos.color = Color.black;
        Gizmos.DrawSphere(symmetricPoint, 5f); 
        Gizmos.DrawLine(transform.parent.transform.position, transform.parent.transform.position + normal);

    }
}
