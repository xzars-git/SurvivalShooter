using UnityEngine;

public class PlayerMovement : UnityEngine.MonoBehaviour
{
    public float speed = 6f;
    Vector3 movement;
    Animator anim;
    Rigidbody playerRigidbody;
    int floorMask;
    float camRayLength = 100f;

    void Awake()
    {
        // mendapatkan nilai mask dari layer yang bernama floor
        floorMask = LayerMask.GetMask("Floor");

        //Mendapatkan komponen animator
        anim = GetComponent<Animator>();

        //Mendapatkan komponen Rigidbody 
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Mendapatkan nilai input horizontal (-1, 0, 1)
        float h = Input.GetAxisRaw("Horizontal");

        // Mendapatkan nilai vertical (-1, 0, 1)
        float v = Input.GetAxisRaw("Vertical");

        Move(h, v);
        Turning();
        Animating(h, v);
    }

    void Turning()
    {
        //Buat Ray dari posisi mouse di layar
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Buat raycast untuk floorHit
        RaycastHit floorHit;

        //Lakukan raycast
        if(Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            // Mendapatkan vector dari posisi player dan posisi fkiirHit
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            //Mendapatkan look rotation baru ke hit position
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            //Rotasi Player
            playerRigidbody.MoveRotation(newRotation);
        }
    }

    public void Animating(float h, float v)
    {
        bool walking = h != 0f || v != 0f;
        anim.SetBool("IsWalking", walking);
    }

    //Method player dapat berjalan
    public void Move(float h, float v)
    {
        // set nilai x dan y
        movement.Set(h, 0f, v);

        //Menormalisasi nilai vector agar total panjang dari vector adalah 1
        movement = movement.normalized * speed * Time.deltaTime;

        //move to position
        playerRigidbody.MovePosition(transform.position + movement);
    }
}
