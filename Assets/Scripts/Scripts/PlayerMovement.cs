using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // The speed at which the object moves
    RaycastHit clickInfo;
    public LayerMask clickableArea;
    private NavMeshAgent agent;
    public bool isMoving = false;
    Animator animator;
    Vector3 stopPos = new Vector3(0, 0, 0);
    public GameObject clothUI;
    public TextMeshProUGUI clothName;
    public TextMeshProUGUI price;
    public TextMeshProUGUI coinBalance_;
    public TextMeshProUGUI clothesPurchased_;
    public bool gettingCloth;
    public GameObject currentCloth;
    public int coinBalance;
    public int currentPrice;
    public int clothesBought;
    public GameObject brokeUI;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = gameObject.GetComponent<Animator>();
        coinBalance_.text = coinBalance.ToString();
    }
    void Update()
    {
        // Move the object vertically with the up/down arrow keys
        float verticalInput = Input.GetAxis("Vertical");
        transform.position += new Vector3(0f, 0f, verticalInput * speed * Time.deltaTime);
        

        // Move the object horizontally with the left/right arrow keys
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.position += new Vector3(horizontalInput * speed * Time.deltaTime, 0f, 0f);
        
        if (verticalInput != 0)
        {
            animator.SetFloat("MoveY", Mathf.MoveTowards(0.0f, verticalInput, 1000 * Time.deltaTime));
        }
        else if(horizontalInput != 0)
        {
            animator.SetFloat("MoveY", Mathf.MoveTowards(0.0f, horizontalInput, 1000 * Time.deltaTime));
        }
        if (verticalInput != 0f || horizontalInput != 0f)
        {
            agent.SetDestination(transform.position);
            Vector3 movementDirection = new Vector3(horizontalInput, 0f, verticalInput);
            transform.rotation = Quaternion.LookRotation(movementDirection);
        }

        if (Input.GetMouseButtonDown(0))
        {

            if (EventSystem.current.IsPointerOverGameObject()) return;
            Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);



            //REGISTERED LMB 0 CLICK COLLISION
            if (Physics.Raycast(myRay, out clickInfo, 100, clickableArea))
            {
                //if empty plate is clicked on and player is not holding something and is allowed to move(not cooking for example) then go and take the plate
                if (clickInfo.collider.gameObject.CompareTag("Cloth"))
                {
                    Debug.Log("Going get cloth");
                    gettingCloth = true;
                    currentCloth = clickInfo.collider.gameObject;
                    startMoving(clickInfo.collider.gameObject.transform.position);
                    return;
                }
                else
                {
                    startMoving(clickInfo.collider.gameObject.transform.position);
                    return;
                }
            }
        }
        if (isMoving && Vector2.Distance( new Vector2 (agent.transform.position.x, agent.transform.position.z), new Vector2 (stopPos.x,stopPos.z)) < 1f)
        {
            stopMoving();
            transform.LookAt(agent.transform.position);

        }
    }
    public void startMoving(Vector3 location)
    {
        agent.SetDestination(location);

        stopPos = location;
        animator.SetFloat("MoveY", Mathf.MoveTowards(0.0f, 1f, 1000 * Time.deltaTime));
        isMoving = true;
    }
    public void stopMoving()
    {
        if (gettingCloth)
        {
            clothUI.SetActive(true);
            currentPrice = currentCloth.GetComponent<ClothController>().showPrice(clothName,price);
            gettingCloth = false;
        }
        animator.SetFloat("MoveY", Mathf.MoveTowards(1f, 0.0f, 1500 * Time.deltaTime));
        stopPos.x = 0;
        stopPos.y = 0;
        stopPos.z = 0;
        isMoving = false;
    }

    public void initiatePurchase()
    {
        if(coinBalance >= currentPrice)
        {
            coinBalance -= currentPrice;
            coinBalance_.text = coinBalance.ToString();
            clothesBought += 1;
            clothesPurchased_.text = clothesBought.ToString();
            currentCloth.SetActive(false);
            currentCloth = null;
            clothUI.SetActive(false);
        }
        else
        {
            brokeUI.SetActive(true);
        }
    }
}