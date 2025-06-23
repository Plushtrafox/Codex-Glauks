using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemigoStateManager : MonoBehaviour
{
    [Header("REFERENCIAS")]
    [SerializeField] private Transform objetivo;
    [SerializeField] private NavMeshAgent agenteMovimiento;
    [SerializeField] private HealthSO healthCharacter;
    [SerializeField] private Image simboloExclamacion;

    [Header("AJUSTES DE ATAQUE CORTO ALCANCE")]
    [SerializeField] private float attackRange = 2f;
    private bool isAttacking = false;
    [SerializeField] private float cooldown = 1f;
    [SerializeField] private int attackcount = 20;


    [Header("AJUSTES DE VISION Y DISTANCIA")]
    [SerializeField] private float rangoDeVision = 10f;
    [SerializeField] private float distanciaActual;
    [SerializeField] private bool estaEnVista = false; // Indica si el enemigo ha visto al jugador o no, se usa para mostrar el icono de exclamacion cuando lo ve

    //accesores
    public Transform Objetivo { get { return objetivo; } set { objetivo = value; } }
    public NavMeshAgent AgenteMovimiento { get { return agenteMovimiento; } set { agenteMovimiento = value; } }
    public HealthSO HealthCharacter { get { return healthCharacter; } set { healthCharacter = value; } }
    public Image SimboloExclamacion { get { return simboloExclamacion; } set { simboloExclamacion = value; } }
    public float AttackRange { get { return attackRange; } set { attackRange = value; } }
    public bool IsAttacking { get { return isAttacking; } set { isAttacking = value; } }
    public float Cooldown { get { return cooldown; } set { cooldown = value; } }
    public int AttackCount { get { return attackcount; } set { attackcount = value; } }
    public float RangoDeVision { get { return rangoDeVision; } set { rangoDeVision = value; } }
    public float DistanciaActual { get { return distanciaActual; } set { distanciaActual = value; } }
    public bool EstaEnVista { get { return estaEnVista; } set { estaEnVista = value; } }


    
    private EnemigoEstatico enemigoEstatico = new EnemigoEstatico();
    private EnemigoPersigueJugador enemigoPersigueJugador = new EnemigoPersigueJugador();
    private EnemigoDispara enemigoDispara = new EnemigoDispara();
    private EnemigoAtaqueCortoAlcance enemigoAtaqueCortoAlcance = new EnemigoAtaqueCortoAlcance();
    private EnemigoEstuneado enemigoEstuneado = new EnemigoEstuneado();


    //accesores de estados de enemigo
    public EnemigoEstatico EnemigoEstatico { get { return enemigoEstatico; } }
    public EnemigoPersigueJugador EnemigoPersigueJugador { get { return enemigoPersigueJugador; } }
    public EnemigoDispara EnemigoDispara { get { return EnemigoDispara; } }
    public EnemigoAtaqueCortoAlcance EnemigoAtaqueCortoAlcance { get { return enemigoAtaqueCortoAlcance; } }
    public EnemigoEstuneado EnemigoEstuneado { get { return enemigoEstuneado; } }


    //estado actual, se usa el tipo del padre abstracto para poder referenciar a todos los hijos sin problema
    EnemigoBase currentState;

    private void Start()
    {
        //inicializamos el estado actual
        currentState = enemigoEstatico;
        currentState.OnEnterState(this);
    }
    private void Update()
    {
        //actualizamos el estado actual
        if (currentState != null)
        {
            currentState.OnUpdateState(this);
        }
    }

    public void ChangeState(EnemigoBase newState)
    {
        //salimos del estado actual
        if (currentState != null)
        {
            currentState.OnExitState(this);
        }
        //cambiamos al nuevo estado
        currentState = newState;
        currentState.OnEnterState(this);
        print("Cambiando estado a: " + currentState.GetType().Name);
    }



    #region ataque corto alcance
    public void IniciarHacerDamage()
    {
        InvokeRepeating("HacerDamage", cooldown, cooldown);
    }
    private void HacerDamage()
    {
        healthCharacter.Damage(attackcount);
    }
    public void DetenerHacerDamage()
    {
        CancelInvoke("HacerDamage");
    }
    #endregion



    #region icono detectar jugador
    public void MostrarUIDetectar()
    {
        StartCoroutine(MostrarUIDetectarCoroutine());
    }
    IEnumerator MostrarUIDetectarCoroutine()
    {
        simboloExclamacion.color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(2f);
        simboloExclamacion.color = new Color(1, 1, 1, 0);
        yield return null;
    }
    #endregion


}
