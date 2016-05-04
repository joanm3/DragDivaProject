using UnityEngine;


public class ControlMirrorCamera : MonoBehaviour {


    [SerializeField]
    private float m_yOffset = 0.5f;

    private GameObject m_mirror;
    private GameObject m_playerCamera;
    private Mesh m_mesh;
    private Vector3 m_normal;
    private Vector3 m_symmetricPoint;

    void Start()
    {

        //GET COMPONENTS
        m_playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
        if (m_playerCamera == null)
            Debug.LogError("No Player GameObject Found for " + gameObject.name);

        m_mirror = transform.parent.gameObject;
        if (m_mirror == null)
            Debug.LogError("No Mirror GameObject Found for " + gameObject.name);

        m_mesh = this.GetComponentInParent<MeshFilter>().sharedMesh;

        //CALCULATE NORMALS
        m_mesh.RecalculateNormals();
        m_normal = m_mesh.normals[1];
        m_normal = this.transform.parent.transform.TransformVector(m_normal);

    }


    void FixedUpdate()
    {

        //UPDATE POSITION TO SYMMETRIC POINT
        Plane plane = new Plane(m_normal, transform.parent.transform.position);
        Vector3 projectionPlane = Formulas.ProjectPointOnPlane(m_playerCamera.transform.position, plane);
        m_symmetricPoint = projectionPlane - (m_playerCamera.transform.position - projectionPlane);
        transform.position = new Vector3(m_symmetricPoint.x, m_symmetricPoint.y + m_yOffset, m_symmetricPoint.z);
        Vector3 cameraDirection = m_playerCamera.transform.forward;
        float scalarProduct = Vector3.Dot(m_normal, cameraDirection);
        Vector3 mirroredDirection = cameraDirection - (2 * scalarProduct * m_normal.normalized);
       // transform.LookAt(transform.position + mirroredDirection);

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
        Gizmos.DrawSphere(symmetricPoint, 1f);
        Gizmos.DrawLine(transform.parent.transform.position, transform.parent.transform.position + normal);

    }
}
