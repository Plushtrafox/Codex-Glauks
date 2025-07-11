using System;
using System.Runtime.CompilerServices;
using UnityEngine;




public enum AnimacionesJugador
{
    JugadorAtaqueCortoAlcanceAnimacion1,
    JugadorAtaqueCortoAlcanceAnimacion2,
    JugadorAtaqueLargoAlcance,
    JugadorAtaquePesado,
    JugadorHabilidadGiratoria,
    JugadorDashAnimacion,
    JugadorDePieAnimacion,
    JugadorMovimientoAdelanteAnimacion,
    JugadorMovimientoAtrasAnimacion,
    JugadorMovimientoIzquierdaAnimacion,
    JugadorMovimientoDerechaAnimacion,
    JugadorMuereAnimacion1,
    JugadorMuereAnimacion2,
    NONE
}


public class AnimatorBrain : MonoBehaviour
{

    private readonly static int[] animacionesJugador =
    {
        Animator.StringToHash("JugadorAtaqueCortoAlcanceAnimacion1"),
        Animator.StringToHash("JugadorAtaqueCortoAlcanceAnimacion2"),
        Animator.StringToHash("JugadorAtaqueLargoAlcance"),
        Animator.StringToHash("JugadorAtaquePesado"),
        Animator.StringToHash("JugadorHabilidadGiratoria"),
        Animator.StringToHash("JugadorDashAnimacion"),
        Animator.StringToHash("JugadorDePieAnimacion"),
        Animator.StringToHash("JugadorMovimientoAdelanteAnimacion"),
        Animator.StringToHash("JugadorMovimientoAtrasAnimacion"),
        Animator.StringToHash("JugadorMovimientoIzquierdaAnimacion"),
        Animator.StringToHash("JugadorMovimientoDerechaAnimacion"),
        Animator.StringToHash("JugadorMuereAnimacion1"),
        Animator.StringToHash("JugadorMuereAnimacion2"),
    };
    [SerializeField] private Animator animatorJugador;
    private AnimacionesJugador[] animacionesActivas;
    private bool[] capaBloqueada;
    private Action<int> AnimacionDefecto;


    //info para sistema de animaciones
    private const int capaCuerpoSuperior = 0; // capa de torso para arriba
    private const int capaCuerpoInferior = 1; // capa de torso para abajo

    private void Start()
    {
        Inicio(animatorJugador.layerCount, AnimacionesJugador.JugadorDePieAnimacion);
    }
    public void Inicio(int cantidadCapas, AnimacionesJugador animacionInicial)
    {

        capaBloqueada = new bool[cantidadCapas];
        animacionesActivas = new AnimacionesJugador[cantidadCapas];


        for (int i = 0; i < cantidadCapas; i++)
        {
            animacionesActivas[i] = animacionInicial;
            capaBloqueada[i] = false;
        }

    }
    public AnimacionesJugador GetAnimacionActual(int capa)
    {
        if (capa < 0 || capa >= animacionesActivas.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(capa), "Capa fuera de rango");
        }
        return animacionesActivas[capa];
    }


    private void RevisarAnimacionSuperior()
    {


    }
    private void RevisarAnimacionInferior()
    {

    }

    private void RevisarMovimiento()
    {

    }

    public void SetBloquearCapa(bool seBloquea, int capaACambiar)
    {
        capaBloqueada[capaACambiar] = seBloquea;

    }

    public void ReproducirAnimacion(AnimacionesJugador animacion, int capa, bool seBloquea, bool pasarBloqueo, float crossfade=0.2f)
    {
        if (animacion== AnimacionesJugador.NONE)
        {
            AnimacionDefecto(capa);
            return;
        }
        if (capaBloqueada[capa] && !pasarBloqueo) return;
        capaBloqueada[capa] = seBloquea;

        if (animacionesActivas[capa]==animacion) return;

        animacionesActivas[capa] = animacion;

        animatorJugador.CrossFade(animacionesJugador[(int)animacionesActivas[capa]], crossfade, capa);

    }
}
