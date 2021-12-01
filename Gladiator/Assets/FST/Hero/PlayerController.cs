using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed, TurnSpeed,jumpForce;
    HPbar bar;

    public bool isJump, isDive, isAttack;
    public bool isGrounded;
    float XCam;
    Rigidbody rb;
    Animator animator;
    void Start()
    {
        bar=gameObject.GetComponent<HPbar>();
        rb=GetComponent<Rigidbody>();
        animator=GetComponent<Animator>();

        InvokeRepeating("Delay",0,1.5f);
    }

    void Delay(){
        isDive=false;
        isJump=false;
        isAttack=false;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && isAttack==false){
            Attack();
        }

        AnimationRun();

        XCam += TurnSpeed * Input.GetAxis("Mouse X");
        transform.eulerAngles = new Vector3(0,XCam,0); 

         CharacterMove();
    }

    void AnimationRun(){
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 Movedir = new Vector3 (x, 0 ,z);
        
        if(Movedir.z>0 ||Movedir.x>0){
            animator.SetBool("IsRunning",true);
            
        }
        if(Movedir.z<0){
            animator.SetBool("RunningBack",true);
        }
        if(Movedir.x == 0 && Movedir.z==0){
            animator.SetBool("IsRunning",false);
            animator.SetBool("RunningBack",false);
        }

        if((Input.GetKeyDown(KeyCode.LeftShift))&&(isJump==false)){
            isDive=true;
            animator.Play("Dive");
            
        }

        if((Input.GetKeyDown(KeyCode.Space))&&(isDive==false)&&(isGrounded==true)){
            Debug.Log("Jump");
            rb.AddForce(new Vector3(0,1,0)*jumpForce*1000);
            isJump=true;
            animator.Play("Jump");
                
            
            
        }

    }

    void Attack(){
        animator.Play("Attack");
        bar.StaminaUsed(20);
        isAttack=true;
        
    }
    void CharacterMove(){
        if(Input.GetKey(KeyCode.A)) {
             transform.position -= transform.right * Time.deltaTime * speed;
         }
         if(Input.GetKey(KeyCode.S)) {
             transform.position -= transform.forward * Time.deltaTime * speed;
         }
         if(Input.GetKey(KeyCode.D)) {
             transform.position += transform.right * Time.deltaTime * speed;
         }
         if(Input.GetKey(KeyCode.W)) {
             transform.position += transform.forward * Time.deltaTime * speed;
         }
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag=="Ground"){
            isGrounded=true;
        }
    }

    private void OnCollisionExit(Collision other) {
        if(other.gameObject.tag=="Ground"){
            isGrounded=false;
        }
    }

}
