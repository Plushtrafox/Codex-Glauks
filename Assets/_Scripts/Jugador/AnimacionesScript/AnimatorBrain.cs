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
    JugadorRecargaAtaqueAnimacion,
    NONE
}
public enum CapasAnimacion
{
    CapaSuperior,
    CapaInferior
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
        Animator.StringToHash("JugadorRecargarAtaque"),
        Animator.StringToHash("Movement"),
    };
    [SerializeField] private Animator animatorJugador;
    private AnimacionesJugador[] animacionesActivas;
    private bool[] capaBloqueada;



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

    public void ReproducirAnimacion(AnimacionesJugador animacion, CapasAnimacion capaAnimacion, bool seBloquea, bool pasarBloqueo, float crossfade=0.2f)
    {
        int capaAIterar = capaAnimacion == CapasAnimacion.CapaSuperior ? capaCuerpoSuperior : capaCuerpoInferior;
        if (animacion== AnimacionesJugador.NONE)
        {
            animacionesActivas[capaAIterar] = animacion;
            capaBloqueada[capaAIterar] = false;

            animatorJugador.CrossFade("Movement", 0.1f);
            return;
        }

        if (capaBloqueada[capaAIterar] && !pasarBloqueo) {  return; }
        capaBloqueada[capaAIterar] = seBloquea;
        if (animacionesActivas[capaAIterar] ==animacion) return;

        animacionesActivas[capaAIterar] = animacion;

        animatorJugador.CrossFade(animacionesJugador[(int)animacionesActivas[capaAIterar]], crossfade, capaAIterar, 0f,0f);

    }
}
