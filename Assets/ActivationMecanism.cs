using UnityEngine;
using System.Collections;

public class ActivationMecanism : MonoBehaviour {

    [SerializeField]
    private Color m_activatedColor;
    [SerializeField]
    private Color m_desactivatedColor;

    [SerializeField]
    private bool m_stayActive;

    [SerializeField]
    private Transform activator;

    [SerializeField]
    private Transform[] objectsToActivate; 

    private Material m_material;

    private bool m_playerTouching = false;
    private bool m_activatorTouching = false; 


	// Use this for initialization
	void Start () {

        m_material = activator.GetComponent<MeshRenderer>().material;
        m_material.color = m_desactivatedColor; 

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
            m_playerTouching = true; 

        if (collider.gameObject.tag == "Activator")
            m_activatorTouching = true; 

        if (m_playerTouching || m_activatorTouching)
        {
            m_material.color = m_activatedColor;
            foreach (Transform objectToActivate in objectsToActivate)
            {
                objectToActivate.gameObject.SetActive(false);

            }
        }
    }


    void OnTriggerExit(Collider collider)
    {
        if (m_stayActive)
            return;

        if (collider.gameObject.tag == "Player")
            m_playerTouching = false;

        if (collider.gameObject.tag == "Activator")
            m_activatorTouching = false;

        if (!m_activatorTouching && !m_playerTouching)
        {
            m_material.color = m_desactivatedColor;
            foreach (Transform objectToActivate in objectsToActivate)
            {
                objectToActivate.gameObject.SetActive(true);

            }
        }
    }


}
