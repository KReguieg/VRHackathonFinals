using UnityEngine;
using System.Collections;

public class SetTrackerPositionLocal : MonoBehaviour
{
	public Transform Tracker;
	public Transform FlyingObj;
	public float Multiplier;
	public bool SetRotation;
	public bool Active = false;
	public bool ActiveOnStartUp = true;
	public float SmoothTime = 0.3f;
	private Vector3 _defaultLocalPosTracker, _defaultLocalPosObj;
	private Vector3 _velocity = Vector3.zero;
	public Material AntiMotionSicknessMaterial;
	public float MaxVelocity;
	
	private void Awake()
	{
		StartCoroutine(WaitForTrackerDefaultPos(2f));
	}

	private IEnumerator WaitForTrackerDefaultPos(float delay){
		yield return new WaitForSeconds(delay);
		while(!Tracker.gameObject.activeSelf){
			yield return new WaitForSeconds(1f);
		}
		SetDefaultPositions();
		Active = ActiveOnStartUp;
	}

    private void FixedUpdate ()
    {
		if(!Active) return;
		Vector3 currentPos = FlyingObj.localPosition;
		Vector3 targetPos = _defaultLocalPosObj + 
			(Tracker.localPosition - _defaultLocalPosTracker) * Multiplier;
		if(_velocity.magnitude > MaxVelocity) _velocity = _velocity.normalized * MaxVelocity;
		transform.position = Vector3.SmoothDamp(currentPos, targetPos, 
			ref _velocity, SmoothTime);
		Color newColor = AntiMotionSicknessMaterial.color;
		newColor.a = _velocity.magnitude/20 - .2f;
		AntiMotionSicknessMaterial.color = newColor;
		// AntiMotionSicknessMaterial.SetColor("_EmissionColor", Color.white * _velocity.magnitude/25);
		if(SetRotation)
		{
			FlyingObj.localRotation = Tracker.localRotation;
		}

    }

	public void SetDefaultPositions()
	{
		SetDefaultPositions(Tracker.transform.localPosition, FlyingObj.transform.localPosition);
	}

	public void SetDefaultPositions(Vector3 trackerLocalPos, Vector3 objLocalPos)
	{
		_defaultLocalPosObj = objLocalPos;
		_defaultLocalPosTracker = trackerLocalPos;
	}
}