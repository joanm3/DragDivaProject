using UnityEngine;
using System.Collections;

public class ActivationMecanism : MonoBehaviour
{

    [SerializeField]
    private bool m_activateWithBullet;

    [SerializeField]
    private Color m_activatedColor;
    [SerializeField]
    private Color m_desactivatedColor;

    [SerializeField]
    private bool m_stayActive;

    [SerializeField]
    private Transform m_activator;

    [SerializeField]
    private Transform[] m_objectsToActivate;

    private Material m_material;

    private bool m_playerTouching = false;
    private bool m_activatorTouching = false;
    private bool m_bulletTouching;

    private bool m_activated = false;


    // Use this for initialization
    void Start()
    {

        m_material = m_activator.GetComponent<MeshRenderer>().material;
        m_material.color = m_desactivatedColor;
    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
            m_playerTouching = true;

        if (collider.gameObject.tag == "Activator")
            m_activatorTouching = true;

        if (collider.gameObject.tag == "Bullet")
            m_bulletTouching = true;


        if (m_activateWithBullet && !m_activated)
        {
            m_material.color = m_activatedColor;
            foreach (Transform objectToActivate in m_objectsToActivate)
            {
                m_activated = true;
                objectToActivate.gameObject.SetActive(false);

            }
        }
        else if (m_activateWithBullet && m_activated)
        {
            m_material.color = m_desactivatedColor;
            foreach (Transform objectToActivate in m_objectsToActivate)
            {
                m_activated = false;
                objectToActivate.gameObject.SetActive(true);

            }

            if (m_activateWithBullet)
                return;


            if (m_playerTouching || m_activatorTouching)
            {
                m_material.color = m_activatedColor;
                foreach (Transform objectToActivate in m_objectsToActivate)
                {
                    m_activated = true;
                    objectToActivate.gameObject.SetActive(false);

                }
            }



        }
    }


        void OnTriggerExit(Collider collider)
    {
            if (m_stayActive || m_activateWithBullet)
                return;

            if (collider.gameObject.tag == "Player")
                m_playerTouching = false;

            if (collider.gameObject.tag == "Activator")
                m_activatorTouching = false;


            if (!m_activatorTouching && !m_playerTouching || !m_bulletTouching && m_activateWithBullet)
            {
                m_material.color = m_desactivatedColor;
                foreach (Transform objectToActivate in m_objectsToActivate)
                {
                    m_activated = false;
                    objectToActivate.gameObject.SetActive(true);

                }
            }
        }


    }
