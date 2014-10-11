
var target : Transform;

var distance = 10.0;

var height = 5.0;

var heightDamping = 2.0;
var rotationDamping = 3.0;

@script AddComponentMenu("Camera-Control/Smooth Follow")

function LateUpdate () {

	if (!target)
		return;
	
	var wantedHeight = target.position.y + height;
		
	var currentHeight = transform.position.y;

	// Damp the height
	currentHeight = Mathf.Lerp (currentHeight, wantedHeight, heightDamping * Time.deltaTime);

	transform.position = new Vector3 (target.position.x, target.position.y, transform.position.z);

	// Set the height of the camera
	transform.position.y = currentHeight;

}