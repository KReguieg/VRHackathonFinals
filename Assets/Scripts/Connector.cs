/*===============================================================
Product:    #PROJECTNAME#
Developer:  #DEVELOPERNAME#
Company:    #COMPANY#
Date:       #CREATIONDATE#
================================================================*/

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class Connector : InteractableObject
{
	private LineRenderer _lRenderer;

	private Controller _triggeredController;
	public Connector CurrentlyConnectedTo{get; private set;}

	public float ConnectionMaxLength = 500f;
	public float ConnectionTickRate = .1f;

	private float _currentConnectionLength = 0;

	
	private void Awake ()
    {
		_lRenderer = GetComponent<LineRenderer>();
	}

	private void Update(){
		if(_triggeredController && !CurrentlyConnectedTo){
			if(_lRenderer.positionCount < 2) Debug.Log("wat");
			// _lRenderer.SetPosition(0,transform.position);
			float lastTickLength = (_lRenderer.GetPosition(_lRenderer.positionCount - 2) - 
				_lRenderer.GetPosition(_lRenderer.positionCount - 1)).magnitude;


			if(_currentConnectionLength + lastTickLength > ConnectionMaxLength) {
				ConnectTo(null);
				return;
			}

			if(lastTickLength > ConnectionTickRate){
				_lRenderer.positionCount++;
				_currentConnectionLength += lastTickLength;
				Debug.Log(_currentConnectionLength);

				_lRenderer.endColor = Color.Lerp(Color.blue, Color.red, 
					_currentConnectionLength / ConnectionMaxLength);
				// _lRenderer.SetPosition(_lRenderer.positionCount-1, _triggeredController.transform.position);
			}
			_lRenderer.SetPosition(_lRenderer.positionCount - 1,
			_triggeredController.transform.position);
			// if(_lRenderer.positions[_lRenderer.positionCount - 1])
		}
	}

	public override void OnControllerExit(Controller controller){
		if(CurrentlyConnectedTo && _triggeredController){
			CurrentlyConnectedTo.ConnectTo(null);
		}
		base.OnControllerExit(controller);
	}

	public override void OnControllerTriggerDown(Controller controller){
		_triggeredController = controller;
		_lRenderer.positionCount = 2;
		_lRenderer.SetPosition(0, transform.position);
		_lRenderer.SetPosition(1, controller.transform.position);
		base.OnControllerTriggerDown(controller);
	}
	
	public override void OnControllerTriggerUp(Controller controller){
		if(_triggeredController){
			_triggeredController = null;
			if(!CurrentlyConnectedTo){
				_lRenderer.positionCount = 0;
				_currentConnectionLength = 0;
				_lRenderer.startColor = Color.blue;
				_lRenderer.endColor = Color.blue;
			}
		}
		// } else{
		// 	_lRenderer.positionCount = 2;
		// }
		base.OnControllerTriggerUp(controller);
	}

	public override void OnControllerEnter(Controller controller){
		Connector otherConnector = controller.TriggeredObject as Connector;
		if(otherConnector){
			otherConnector.ConnectTo(this);
		}
		base.OnControllerEnter(controller);
	}

	private void ConnectTo(Connector other){
		CurrentlyConnectedTo = other;
		if(!other){
			_lRenderer.positionCount = 0;
			_lRenderer.startColor = Color.blue;
			_lRenderer.endColor = Color.blue;
			_currentConnectionLength = 0;
			return;
		}
		if(!_triggeredController) _triggeredController = null;
		_lRenderer.positionCount = 2;
		_lRenderer.SetPosition(0, transform.position);
		_lRenderer.SetPosition(1, other.transform.position);
	}
}