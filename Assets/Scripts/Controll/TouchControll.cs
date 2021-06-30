using UnityEngine;
using UnityEngine.AI;

public class TouchControll : MonoBehaviour
{

    [SerializeField]
    NavMeshAgent agent;

    Animator characterAnim;

    Camera mainCam;

    GameObject tempBonus = null;

    public delegate void Action(Transform val);
    public event Action OnMinusBonus;

    int layer;

    private void Awake()
    {
        layer= LayerMask.GetMask("Ground", "Bonus");
        mainCam = Camera.main;
        characterAnim = agent.GetComponent<Animator>();
    }

    private void Update()
    {
        // Animation
        if (agent.remainingDistance <= 0f)
        {
            characterAnim.SetBool("IsRuning", false);
            if (tempBonus != null)
            {
                GetBonus();
            }
        }

        // Touch
        if (Input.GetMouseButtonDown(0))
        {
            if(tempBonus !=null)
                tempBonus.GetComponent<BoxCollider>().enabled = true;
            tempBonus = null;
            RayCheck();
        }
    }


    //CompareTag 


    void RayCheck() 
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();


        if (Physics.Raycast(ray.origin, ray.direction, out hit,100, layer))
        {
            string tag = hit.transform.tag;
            if (tag == "Ground")
            {
                characterAnim.SetBool("IsRuning", true);
                agent.SetDestination(hit.point);
            }
            if (tag == "Bonus")
            {
                // если бонус мы нашли, то запомним
                tempBonus = hit.transform.gameObject;
                // и отключим колайдер 
                tempBonus.GetComponent<BoxCollider>().enabled = false;
                // и поищем под ним землю
                RayCheck();
            }
        }
    }


    void GetBonus() 
    {
       
        OnMinusBonus?.Invoke(tempBonus.transform);
        Hero.PlusHp();
        tempBonus.SetActive(false);
        tempBonus = null;
    }


}
