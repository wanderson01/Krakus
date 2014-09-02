
var contx = 0;
var conty= 0;

function Start () {

}

function Update () {

	contx +=1;
	
	if (contx >= 5){
		transform.position.y +=1 * Time.deltaTime;
		contx = 0;
		conty +=1;
	}
	
	if (conty < 5) {
		transform.position.x -=1 * Time.deltaTime;
		conty = 1;
	} 
	else {
		transform.position.x +=1 * Time.deltaTime;
	}

}