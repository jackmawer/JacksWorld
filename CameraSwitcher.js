#pragma strict
var cam1 : GameObject; var cam2 : GameObject;

function Start() { cam1.active = true; cam2.active = false; }

function Update() {

    if (Input.GetKeyDown(KeyCode.C)) {
    cam1.active = !cam1.active;
    cam2.active = !cam2.active;
    }
     

} 