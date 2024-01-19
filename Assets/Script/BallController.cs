using UnityEngine;

using System.Collections;

public class BallController : MonoBehaviour
{
    public float ballSpeed = 1f;
    public float speedUp = 1f;  
    public float jumpForce = 5f;
    public Joystick joystick;
    private float currentHeight = 0f;
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private GameObject greenBoom;
    [SerializeField] private GameObject blueBoom;
    [SerializeField] private GameObject pinkBoom;
    private ParticleSystem.MainModule mainModule;
    private float updateInterval = .1f;
    private float timeSinceLastUpdate = 0f;


    private void Start()
    {
        mainModule = particleSystem.main;
    }
    void Update()
    {
        MovementPlayer();
        timeSinceLastUpdate += Time.deltaTime;

        if (timeSinceLastUpdate >= updateInterval)
        {
            Debug.Log("Slow Update");
            ScoreManager.instance.AddScore();
            timeSinceLastUpdate = 0f;
        }

    }

    void MovementPlayer()
    {
        Vector3 movement = new Vector3(joystick.Horizontal * ballSpeed * Time.deltaTime, speedUp * Time.deltaTime, 0);

        transform.Translate(movement);
        /*if (movement.y > 0)
        {
            currentHeight += movement.y;
            var score = (transform.position.y * ComboManager.instance.score) / 10;
            ScoreManager.instance.AddScore((int)Math.Floor(score));
        }*/
    }




    private void IncreaseHeightSpeed()
    {
        if(speedUp > 4)
        {
            return;
        }
        else
        {
            speedUp += .2f;
        }
        
        Debug.Log(speedUp);
    }

    // Логіка збільшення швидкості горизонтального руху
    private void IncreaseHorizontalSpeed()
    {
        ballSpeed++;
    }


    void SwichColor(int r, int g, int b)
    {

        mainModule.startColor = new Color(r, g, b, 255);
        Debug.Log(mainModule.startColor.color);
    }

    private void CollisionEffect(GameObject pref)
    {
        var effect = Instantiate(pref, transform.position, Quaternion.identity);
    }



    // Вызывается при соприкосновении с коллайдером другого объекта
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Red":
                IncreaseHeightSpeed();
                SwichColor(255, 0, 255);
                CollisionEffect(pinkBoom);
                ComboManager.instance.OnSuccessfulBounce();
                //ForceCollision(collision.gameObject);
                break;
            case "Green":
                IncreaseHorizontalSpeed();
                SwichColor(0, 255, 0);
                CollisionEffect(greenBoom);
                ComboManager.instance.OnSuccessfulBounce();
                //ForceCollision(collision.gameObject);
                break;
            case "Blue":
                ComboManager.instance.IncreaseComboMultiplier();
                SwichColor(0, 0, 255);
                CollisionEffect(blueBoom);
                ComboManager.instance.OnSuccessfulBounce();
                // ForceCollision(collision.gameObject);
                break;
            case "Black":
                SwichColor(255, 255, 255);
                ComboManager.instance.ResetCombo();
                ComboManager.instance.OnSuccessfulBounce();
                //ForceCollision(collision.gameObject);
                break;
                

                default:
                break;



        }

    }


}