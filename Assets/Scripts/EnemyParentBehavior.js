var childObject : GameObject;
var child : Transform;
var enemyAI : EnemyAI;

function Start () {
  child = transform.Find("Alien2");
  enemyAI = gameObject.GetComponentInChildren(EnemyAI);
}

function Update () {
   
//  if(enemyAI.jumpEnd){
    //ToChildWorldPosition();
 // }
}

function ToChildWorldPosition(){

  transform.position = transform.TransformPoint(child.position);
  
}