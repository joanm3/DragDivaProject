using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ReflectionProbe))]
public class ProbeOptimization : MonoBehaviour
{
    [SerializeField]
    private float m_requiredFrameUpdateRate = 30;

    [SerializeField]
    private float m_maxResolutionStartDistance = 20f;
    [SerializeField]
    private float m_mediumResolutionStartDistance = 40f;

    [SerializeField]
    private int m_maxResolution = 2048;

    [SerializeField]
    private int m_mediumResolution = 1024;

    [SerializeField]
    private int m_minResolution = 512; 


    private ReflectionProbe m_rProbe;
    private float m_forWaitForSecFloat;
    private GameObject m_player;


    void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
        if (m_player == null)
            Debug.LogError("Player object not found for " + gameObject.name); 

        m_forWaitForSecFloat = 1 / m_requiredFrameUpdateRate;
        m_rProbe = GetComponent<ReflectionProbe>();
        StartCoroutine(RefreshProbe());
    }

    IEnumerator RefreshProbe()
    {
        
        while (true)
        {

            float distanceFromPlayer = Vector3.Distance(gameObject.transform.parent.transform.position, m_player.transform.position);
           // Debug.Log("distance from player for reflection probe is " + distanceFromPlayer);

            if(distanceFromPlayer < m_maxResolutionStartDistance)
            {

                m_rProbe.resolution = m_maxResolution; 

            } else if (distanceFromPlayer > m_maxResolutionStartDistance && distanceFromPlayer < m_mediumResolutionStartDistance)
            {
                m_rProbe.resolution = m_mediumResolution ;
            }
            else
            {
                m_rProbe.resolution = m_minResolution ;

            }

                m_rProbe.RenderProbe();
            yield return new WaitForSeconds(m_forWaitForSecFloat);
        }

    }


    void OnBecameVisible()
    {
        Debug.Log("OnBecameVisibleCalled"); 
        StartCoroutine(RefreshProbe());

    }

    void OnBecameInvisible()
    {
        Debug.Log("OnBecameInvisibleCalled");

        StopCoroutine(RefreshProbe()); 

    }

}