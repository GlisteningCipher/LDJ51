using UnityEngine;
using MonsterLove.StateMachine;
using System.Collections;

public class Wander : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] State currentState;

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

    void FixedUpdate()
    {
        fsm.Driver.FixedUpdate.Invoke();
    }

    IEnumerator Idle_Enter()
    {
        rb.drag = 10;
        yield return new WaitForSeconds(Random.Range(0.5f,3f));
        fsm.ChangeState(State.Move);
    }

    void Move_Enter()
    {
        rb.drag = 2;
        speed = Random.Range(5,10);
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
