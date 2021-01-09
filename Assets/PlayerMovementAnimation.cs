using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementAnimation : MonoBehaviour
{
    Animator animator;
    float velocityZ = 0.0f;
    float velocityX = 0.0f;
    public float acceleration = 2.0f;
    public float deceleration = 2.0f;
    public float maximumWalkVelocity = 0.5f;
    public float maximumRunVelocity = 2f;

    int VelocityZHash;
    int VelocityXHash;
    // Start is called before the first frame update
    void Start()
    {
        //animatör componenti player nesnesinden alındı
        animator = GetComponent<Animator>();

        //Java daki final String tarzı bir olay 
        VelocityZHash = Animator.StringToHash("VelocityZ");
        VelocityXHash = Animator.StringToHash("VelocityX");
    }

    // Update is called once per frame
    void Update()
    {
        //hangi tuşa basılırsa o değişken "true" olacak bu değeler üzerinden
        //Velocity değerlerini düzenleyeceğiz o değerlerde animasyonları ayarlayacak
        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool leftPressed = Input.GetKey(KeyCode.A);
        bool rightPressed = Input.GetKey(KeyCode.D);
        bool runPressed = Input.GetKey(KeyCode.LeftShift);

        //eğer "left shift" e basılıyorsa değer "2f" basılmıyorsa değer "0.5f"
        float currentMaxVelocity = runPressed ? maximumRunVelocity : maximumWalkVelocity;

        //eğer "w" ye basıldıysa "z" ekseninde ilerle
        if (forwardPressed && velocityZ < currentMaxVelocity)
        {
            velocityZ += Time.deltaTime * acceleration; 
        }
        //"a" ya basıldığında sola gitmek için X değeri - yapılıyor
        if (leftPressed && velocityZ > -currentMaxVelocity)
        {
            velocityX -= Time.deltaTime * acceleration;
        }
        //"d" ye basıldığında sağa gitmek için X değeri + yapılıyor
        if (rightPressed && velocityZ < currentMaxVelocity)
        {
            velocityX += Time.deltaTime * acceleration;
        }

        //"W" ye basılmıyorsa ve velocityZ değeri 0 dan büyükse 
        //karakter hareket ediyor ancak w ye basıomıyor demektir
        //karakteri yavaşça durduruyoruz
        if (!forwardPressed && velocityZ > 0.0f)
        {
            velocityZ -= Time.deltaTime * deceleration;
        }

        //velocity yi resetledik
        if (!forwardPressed && velocityZ < 0.0f)
        {
            velocityZ = 0.0f;
        }

        //"a" ye basılmıyorsa ve velocityZ değeri 0 dan büyükse 
        //karakter hareket ediyor ancak a ye basıomıyor demektir
        //karakteri yavaşça durduruyoruz
        if (!leftPressed && velocityX < 0.0f)
        {
            velocityX += Time.deltaTime * deceleration;
        }

        //"d" ye basılmıyorsa ve velocityZ değeri 0 dan büyükse 
        //karakter hareket ediyor ancak d ye basıomıyor demektir
        //karakteri yavaşça durduruyoruz
        if (!rightPressed && velocityX > 0.0f)
        {
            velocityX += Time.deltaTime * deceleration;
        }

        //velocity yi resetledik
        if (!leftPressed && rightPressed && velocityX != 0.0f && (velocityX > -0.5f && velocityX < 0.5f))
        {
            velocityX = 0.0f;
        }

        //ileri bak
        if (forwardPressed && runPressed && velocityZ > currentMaxVelocity)
        {
            velocityX = currentMaxVelocity;
        }
        else if(forwardPressed && velocityZ > currentMaxVelocity)
        {
            velocityZ -= Time.deltaTime * deceleration;
            if(velocityZ > currentMaxVelocity && velocityZ < (currentMaxVelocity + 0.5))
            {
                velocityZ = currentMaxVelocity;
            }
        }


        //local değerleri animatörde set edip global  hale getirdik
        animator.SetFloat(VelocityZHash, velocityZ);
        animator.SetFloat(VelocityXHash, velocityX);

    }
}
