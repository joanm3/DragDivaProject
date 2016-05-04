using UnityEngine;
using System.Collections;

public class pickupveil : MonoBehaviour {

	public float chrono;
	public float fadeTime;
	private float m_chronoIn;

	void Start()
	{

		GameManager.Notifications.AddListener(this, "VeilOn");
		GameManager.Notifications.AddListener(this, "VeilOff");

	}


	// Update is called once per frame
	void OnTriggerEnter () {

	
			
				if (GameManager.enableVeilChange)
				{

						//veil passes to the other state
						GameManager.veilOn = !GameManager.veilOn;
						//after we check at what state we are and we send the right notification to all VeilObjectController functions
						if (GameManager.veilOn)
							GameManager.Notifications.PostNotification(this, "VeilOn");
						if (!GameManager.veilOn)
							GameManager.Notifications.PostNotification(this, "VeilOff");


					}
				}



			IEnumerator VeilCountdown()

			{
				m_chronoIn = chrono; 
				while (m_chronoIn > 0)
				{
					m_chronoIn -= Time.deltaTime / fadeTime;
					//   Debug.Log(m_chronoIn);
				}
				Debug.Log("exit chrono loop"); 
				GameManager.veilOn = false;
				GameManager.Notifications.PostNotification(this, "VeilOff");
				yield return null; 
			}



}