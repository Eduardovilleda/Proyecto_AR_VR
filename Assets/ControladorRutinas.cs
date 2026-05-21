using UnityEngine;
using System.Collections;
using TMPro;

public class ControladorRutinas : MonoBehaviour
{
    [Header("Arrastra aquí a tu Entrenador")]
    public Animator entrenadorAnim;

    [Header("Escribe el nombre del PRIMER cuadro")]
    public string primerEjerSuperior;
    public string primerEjerInferior;
    public string primerEjerCalentamiento;

    [Header("Arrastra aquí el PanelSeleccion")]
    public GameObject panelMenu;

    [Header("UI del Contador")]
    public TextMeshProUGUI textoContador;
    private int rutinasCompletadas = 0;

    [Header("Botones de Información")]
    public GameObject btnInfoBrazo;
    public GameObject btnInfoPierna;
    public GameObject btnInfoTorso;
    public GameObject btnCerrarInfo;

    [Header("Equipo de Entrenamiento")]
    public GameObject mancuernaDer;
    public GameObject mancuernaIzq;

    // --- LA LISTA MÁGICA ---
    [Header("Nombres de los cuadros que SÍ usan pesas")]
    public string[] animacionesConPesas;

    void Start()
    {
        rutinasCompletadas = PlayerPrefs.GetInt("RutinasGuardadas", 0);
        ActualizarTextoUI();
    }

    public void PlaySuperior()
    {
        if (entrenadorAnim != null)
        {
            entrenadorAnim.Play(primerEjerSuperior);
            ConfigurarBotonesInfo(true, false, false);
            StartCoroutine(RadarFinRutina());
        }
    }

    public void PlayInferior()
    {
        if (entrenadorAnim != null)
        {
            entrenadorAnim.Play(primerEjerInferior);
            ConfigurarBotonesInfo(false, true, true);
            StartCoroutine(RadarFinRutina());
        }
    }

    public void PlayCalentamiento()
    {
        if (entrenadorAnim != null)
        {
            entrenadorAnim.Play(primerEjerCalentamiento);
            ConfigurarBotonesInfo(false, false, false);
            StartCoroutine(RadarFinRutina());
        }
    }

    public void DetenerEntrenadorYApagarBotonesAnatomia()
    {
        if (entrenadorAnim != null) entrenadorAnim.Play("Offensive Idle");
        ConfigurarBotonesInfo(false, false, false);
        ControlarMancuernas(false);
    }

    IEnumerator RadarFinRutina()
    {
        yield return new WaitForSeconds(0.5f);

        while (!entrenadorAnim.GetCurrentAnimatorStateInfo(0).IsName("Offensive Idle") &&
               !entrenadorAnim.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Offensive Idle"))
        {
            // --- EL RADAR REVISA EN TIEMPO REAL ---
            AnimatorStateInfo estadoActual = entrenadorAnim.GetCurrentAnimatorStateInfo(0);
            bool necesitaPesas = false;

            foreach (string nombre in animacionesConPesas)
            {
                if (estadoActual.IsName(nombre))
                {
                    necesitaPesas = true;
                    break;
                }
            }

            ControlarMancuernas(necesitaPesas);
            // --------------------------------------

            yield return null;
        }

        rutinasCompletadas++;
        PlayerPrefs.SetInt("RutinasGuardadas", rutinasCompletadas);
        PlayerPrefs.Save();
        ActualizarTextoUI();

        ConfigurarBotonesInfo(false, false, false);
        ControlarMancuernas(false);

        if (panelMenu != null) panelMenu.SetActive(true);
    }

    // Funciones auxiliares para mantener todo en orden
    void ActualizarTextoUI()
    {
        if (textoContador != null) textoContador.text = "Rutinas: " + rutinasCompletadas;
    }

    void ConfigurarBotonesInfo(bool brazo, bool pierna, bool torso)
    {
        if (btnInfoBrazo != null) btnInfoBrazo.SetActive(brazo);
        if (btnInfoPierna != null) btnInfoPierna.SetActive(pierna);
        if (btnInfoTorso != null) btnInfoTorso.SetActive(torso);
        if (btnCerrarInfo != null) btnCerrarInfo.SetActive(brazo || pierna || torso);
    }

    void ControlarMancuernas(bool estado)
    {
        if (mancuernaDer != null) mancuernaDer.SetActive(estado);
        if (mancuernaIzq != null) mancuernaIzq.SetActive(estado);
    }
}