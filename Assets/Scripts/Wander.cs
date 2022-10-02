using UnityEngine;
using MonsterLove.StateMachine;
using System.Collections;

public class Wander : MonoBehaviour
{
    static float speedMin = 5f;
    static float speedMax = 10f;
    static float idleMin = 0.5f;
    static float idleMax = 3f;
    static float dragIdle = 10f;
    static float dragMove = 2f;
    static float dragFreeze = 5f;

    [SerializeField] Rigidbody2D rb;
    [SerializeField] State currentState;

    float speed;

    public enum State { Idle, Move }

    public class Driver
    {
        public StateEvent FixedUpdate;
    }

    public StateMachine<State, Driver> fsm;

    Vector2 destination;

    Coroutine idleIfStuck;

    void Awake()
    {
        fsm = new StateMachine<State, Driver>(this);
        Party.onLightsOff += ReactToLightsOff;
        Party.onLightsOn += ReactToLightsOn;
        Party.onResumeParty += OnResumeParty;
    }

    void Update()
    {
        currentState = fsm.State;
    }

    void OnDestroy()
    {
        Party.onLightsOff -= ReactToLightsOff;
        Party.onLightsOn -= ReactToLightsOn;
        Party.onResumeParty -= OnResumeParty;
    }

    void OnEnable()
    {
        fsm.ChangeState(State.Idle);
    }

    void OnDisable()
    {
        rb.drag = dragFreeze;
    }

    void FixedUpdate()
    {
        fsm.Driver.FixedUpdate.Invoke();
    }

    IEnumerator Idle_Enter()
    {
        rb.drag = dragIdle;
        yield return new WaitForSeconds(Random.Range(idleMin, idleMax));
        fsm.ChangeState(State.Move);
    }

    void Move_Enter()
    {
        rb.drag = dragMove;
        speed = Random.Range(speedMin, speedMax);
        GetRandomDestination();
        idleIfStuck = StartCoroutine(IdleIfStuck());
    }

    IEnumerator IdleIfStuck()
    {
        yield return new WaitForSeconds(5);
        fsm.ChangeState(State.Idle);
    }

    void Move_FixedUpdate()
    {
        var distance = (destination - rb.position);
        if (distance.magnitude < 0.1f)
        {
            fsm.ChangeState(State.Idle);
            return;
        }

        var direction = distance.normalized;
        rb.AddForce(direction * speed);
    }

    void Move_Exit()
    {
        StopCoroutine(idleIfStuck);
    }

    void GetRandomDestination()
    {
        destination = Party.GetRandomPoint();
    }

    void ReactToLightsOff()
    {
        enabled = false;
    }

    void ReactToLightsOn()
    {
        enabled = false;
    }

    void OnResumeParty()
    {
        enabled = true;
    }
    
}
