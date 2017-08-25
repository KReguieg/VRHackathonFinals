using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(SphereCollider),typeof(SteamVR_TrackedObject))]
public class Controller : MonoBehaviour
{

	public enum ButtonKind
	{
		Trigger,
		Grip,
		ApplicationMenu,
		Touchpad
	};

	public static Dictionary<ButtonKind, ulong> BUTTONMASKS =
		new Dictionary<ButtonKind, ulong>()
		{
			{ButtonKind.Trigger, SteamVR_Controller.ButtonMask.Trigger},
			{ButtonKind.Grip, SteamVR_Controller.ButtonMask.Grip},
			{ButtonKind.ApplicationMenu, SteamVR_Controller.ButtonMask.ApplicationMenu},
			{ButtonKind.Touchpad, SteamVR_Controller.ButtonMask.Touchpad}
		};

			
	private SphereCollider _sCol;
	private SteamVR_TrackedObject _trackedObject;
	private SteamVR_Controller.Device _device = null;
	private int _trackedObjectIndex = -1;
	
	public InteractableObject ObjectInRange{get; private set;}
	public InteractableObject TriggeredObject{get; private set;}
	private float _defaultColRadius;

	private void Awake() {
		_trackedObject = GetComponent<SteamVR_TrackedObject>();
		_trackedObjectIndex = _trackedObject ? (int) _trackedObject.index : -1;
		_sCol = GetComponent<SphereCollider>();
		_defaultColRadius = _sCol.radius;
	}


	private void Update(){
		if(!CheckDevice()) return;
		
		if(ObjectInRange && GetButtonDown()){
			TriggeredObject = ObjectInRange;
			TriggeredObject.OnControllerTriggerDown(this);
		}
		if(TriggeredObject && GetButtonUp()){
			TriggeredObject.OnControllerTriggerUp(this);
			TriggeredObject = null;
		} 
		if(TriggeredObject){
			TriggeredObject.OnControllerTriggerStay(this);
		} 
		if(ObjectInRange){
			ObjectInRange.OnControllerStay(this);
		}
	}

	private bool CheckDevice()
	{
		if (!_trackedObject)
		{
			_trackedObject = GetComponent<SteamVR_TrackedObject>();
		}
		if (_device != null && _trackedObjectIndex == ((int) _trackedObject.index))
		{
			return true;
		}
		_trackedObjectIndex = (int) _trackedObject.index;
		_device = SteamVR_Controller.Input(_trackedObjectIndex);
		return _device != null;
	}

	public bool GetButtonDown(ButtonKind button = ButtonKind.Trigger) {
		return _device.GetPressDown(BUTTONMASKS[button]);
	}

	public bool GetButtonPress(ButtonKind button = ButtonKind.Trigger) {
		return _device.GetPress(BUTTONMASKS[button]);
	}

	public bool GetButtonUp(ButtonKind button = ButtonKind.Trigger) {
		return _device.GetPressUp(BUTTONMASKS[button]);
	}

	private void OnTriggerEnter(Collider col){
		Debug.Log("enter");
		InteractableObject objInRange = 
			col.GetComponent<InteractableObject>();
		if(objInRange){
			if(ObjectInRange){
				ObjectInRange.OnControllerExit(this);
			}
			objInRange.OnControllerEnter(this);
			ObjectInRange = objInRange;
			_sCol.radius = _defaultColRadius * 1.2f;
		}
	}

	private void OnTriggerExit(Collider col){
		if(!ObjectInRange) return;
		InteractableObject objInRange = 
			col.GetComponent<InteractableObject>();
		if(ObjectInRange == objInRange){
			ObjectInRange.OnControllerExit(this);
			ObjectInRange = null;
			_sCol.radius = _defaultColRadius;
		}
	}
}