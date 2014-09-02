var controller : CharacterController;
var _animator : Animator;

function Start () {
	controller  = GetComponent(CharacterController);
	_animator = GetComponent(Animator);
}

function Update () {

	if (controller.isGrounded) {
		_animator.SetFloat("speed", Mathf.Abs(Input.GetAxis("Horizontal")));
	}
	else{
		_animator.SetFloat("speed", 0);
	}
}

