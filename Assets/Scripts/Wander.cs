using UnityEngine;
using MonsterLove.StateMachine;
using System.Collections;

public class Wander : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] Rigidbody2D rb;

    static float ROOM_HALFWIDTH = 12.8f;
    static float ROOM_HALFHEIGHT = 8f;

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
        fsm.ChangeState(State.Idle);
    }

    void OnEnable()
    {
        fsm.ChangeState(State.Idle);
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    void FixedUpdate()
    {
        fsm.Driver.FixedUpdate.Invoke();
    }

    IEnumerator Idle_Enter()
    {
        rb.drag = 10;
        yield return new WaitForSeconds(2);
        GetRandomDestination();
        fsm.ChangeState(State.Move);
    }

    void Move_Enter()
    {
        rb.drag = 2;
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
        destination = new Vector2(
            Random.Range(-ROOM_HALFWIDTH, ROOM_HALFWIDTH),
            Random.Range(-ROOM_HALFHEIGHT, ROOM_HALFHEIGHT));
    }
    
}
