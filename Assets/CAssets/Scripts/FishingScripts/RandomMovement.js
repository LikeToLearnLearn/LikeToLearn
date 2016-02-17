var vx;
var vy;
var vz;
var move = true;

function Awake() {
    vx = Random.Range(-0.05, 0.05);
    vy = Random.Range(-0.01, 0.01);
    vz = Random.Range(-0.03, 0.03);
}
function Update() {
    if (move) {
        transform.position.x += vx;
        transform.position.y += vy;
        transform.position.z += vz;
    }
    else {
        transform.position.x -= vx;
        transform.position.y -= vy;
        transform.position.z -= vz;
    }

    if (transform.position.x < -5.0) {
        vx = vx * -1;
    } else if (transform.position.x > 5.0) {
        vx = vx * -1;
    }

    if (transform.position.y < -1.0) {
        vy = vy * -1;
    } else if (transform.position.y > 0.01) {
        vy = vy * -1;
    }
    if (transform.position.z < -2.0) {
        vz = vz * -1;
    } else if (transform.position.z > 2.0) {
        vz = vz * -1;
    }
    var fwd = transform.TransformDirection(Vector3.forward);
    if (Physics.Raycast(transform.position, fwd, 10)) {
        print("There is something in front of the object!");
        move = false;
    }
}