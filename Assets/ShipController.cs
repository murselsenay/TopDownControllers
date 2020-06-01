using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{

    Rigidbody2D shipBody;

    Vector3 currentEulerAngles;
    Vector2 shipMovements;


    float rotationSpeed = 5f;
    float x, y, z;
    float moveSpeed = 0.1f;
    float forceSpeed;

    bool mouseOnTheShip;

    [Range(1, 4)]
    public int switchMoveType = 1;



    void Start()
    {
        shipBody = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (switchMoveType == 1)
        {
            moveWithArrows_1();
        }
        else if (switchMoveType == 2)
        {
            moveWithArrows_2();
        }
        else if (switchMoveType == 3)
        {
            moveTowardsToMouse();
        }
        else if (switchMoveType == 4)
        {
            moveTowardsToMouseAndFollow(2f);
        }




    }
    private void OnMouseOver()
    {
        mouseOnTheShip = true;
    }
    private void OnMouseExit()
    {
        mouseOnTheShip = false;
    }


    public void moveWithArrows_2()
    {
        if (Input.GetAxisRaw("Horizontal") > 0f)
        {
            transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0f)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (Input.GetAxisRaw("Vertical") > 0f)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (Input.GetAxisRaw("Vertical") < 0f)
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }

        shipMovements.x = Input.GetAxisRaw("Horizontal");
        shipMovements.y = Input.GetAxisRaw("Vertical");


        shipBody.MovePosition(shipBody.position + shipMovements * moveSpeed);
    }

    
    public void moveWithArrows_1()
    {
        forceSpeed = Input.GetAxisRaw("Vertical") * 2f;

        if (Input.GetAxisRaw("Horizontal") > 0f)
        {

            currentEulerAngles += new Vector3(x, y, z - rotationSpeed);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0f)
        {

            currentEulerAngles += new Vector3(x, y, z + rotationSpeed);
        }

        transform.eulerAngles = currentEulerAngles;


        Debug.Log(forceSpeed.ToString());
        shipBody.AddRelativeForce(Vector2.up * forceSpeed);
    }
    public void moveTowardsToMouseAndFollow(float moveSpeed_)
    {
        // mouse pozisyonunu screen world tipine dönüştür
        Vector2 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // bakmak istediğin yönü belirle
        Vector2 direction = (mouseScreenPosition - (Vector2)transform.position).normalized;

        // belirlediğin yönü oyun nesnesine gönder
        transform.up = direction;

        if (mouseOnTheShip)
        {
            shipBody.velocity = Vector2.zero;
        }
        else if (!mouseOnTheShip)
        {
            shipBody.velocity = new Vector2(direction.x * moveSpeed_, direction.y * moveSpeed_);
        }
    }

    public void moveTowardsToMouse()
    {

        var direction = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


    }
}
