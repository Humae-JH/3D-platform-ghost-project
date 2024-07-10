using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostActorScript : ActorScript
{

    private Animator Anim;

    private static readonly int IdleState = Animator.StringToHash("Base Layer.idle");
    private static readonly int MoveState = Animator.StringToHash("Base Layer.move");
    private static readonly int SurprisedState = Animator.StringToHash("Base Layer.surprised");
    private static readonly int AttackState = Animator.StringToHash("Base Layer.attack_shift");
    private static readonly int DissolveState = Animator.StringToHash("Base Layer.dissolve");
    private static readonly int AttackTag = Animator.StringToHash("Attack");


    [SerializeField] private SkinnedMeshRenderer[] MeshR;
    private float Dissolve_value = 1;
    private bool DissolveFlg = false;
    private const int maxHP = 3;
    private int HP = maxHP;
    private Text HP_text;

    protected BaseAttack attackScript;

    // Start is called before the first frame update
    void Start()
    {
        base.Initialize();
        attackScript = gameObject.AddComponent<BaseAttack>();
        Anim = this.GetComponent<Animator>();

        //HP_text = GameObject.Find("Canvas/HP").GetComponent<Text>();
        //HP_text.text = "HP " + HP.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        //ControlMotion();
        UpdateMotionState();
    }

    public override void Attack()
    {
        Anim.CrossFade(AttackState, 0.1f, 0, 0);
        attackScript.Attack();
    }

    protected override void UpdateMotionState()
    {
        base.UpdateMotionState();
        attackScript.setAttackCollider(gameObject.transform.position + (gameObject.transform.forward * oneStep));
    }


    /*protected override void ControlMotion()
    {
        base.ControlMotion();

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Attack();
        }
        return;
    }*/
}
