using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPbar : MonoBehaviour
{
    public Image Bar, StaminaBar;
    public float knockback;
    public bool cd;
    public float Hpmax,currentHP, StaminaMax, currentStamina;
    public float StaminaRegen, HpRegen;

    bool isDying;

    PlayerController Pc; 

    Animator animator;

    public void Start() {
        Pc = gameObject.GetComponent<PlayerController>();
        animator = gameObject.GetComponent<Animator>();
        Hpmax=100;
        StaminaMax=100;
        currentHP=Hpmax;
        currentStamina=StaminaMax;
        cd=false;
        InvokeRepeating("CDhit",0,1f);
    }

    void CDhit(){
        cd=false;
    }
    void Update()
    {
        if(isDying){
        Pc.isAttack=true;
        Pc.isDive=true;
        Pc.isJump=true;
        }
        

        currentStamina += StaminaRegen * Time.deltaTime; 
        currentHP += HpRegen * Time.deltaTime;

        if(currentStamina >= StaminaMax){
            currentStamina = StaminaMax;
        if(currentStamina<=0){
            currentStamina=0;
        }

        }
        if(currentHP >= Hpmax){
            currentHP = Hpmax;
        }
        if(currentHP<=0){
            currentHP=0;
        }

        //bar update every sec
        Bar.fillAmount = currentHP/Hpmax;
        StaminaBar.fillAmount = currentStamina/StaminaMax;
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag=="Enemy" && cd==false){
            takeDamage(20);
            Debug.Log("Take Damage");
            //transform.position -= transform.forward * knockback;
            cd=true;
        }
    }

    public void takeDamage(int dmg){
        currentHP-=dmg;
        animator.Play("Hurt");

        if(currentHP<0){
            //die
            Debug.Log("Die");
            Death();
        }
    }

    public void StaminaUsed(int amnt){
        currentStamina -= amnt;
    }

    void Death(){
        animator.Play("Die");
        isDying=true;
        Pc.speed=0;
    }
}   
