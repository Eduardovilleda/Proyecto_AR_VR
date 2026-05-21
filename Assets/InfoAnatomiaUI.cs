using UnityEngine;
using TMPro;

public class InfoAnatomiaUI : MonoBehaviour
{
    [Header("Referencias de la Interfaz")]
    public GameObject panelInfoAR;        // Arrastra aquí tu 'CanvasInformacionAR' o el Panel de texto
    public TextMeshProUGUI textoInfo;     // Arrastra aquí el 'Text (TMP)' de adentro

    void Start()
    {
        // Aseguramos que empiece oculto
        if (panelInfoAR != null) panelInfoAR.SetActive(false);
    }

    public void MostrarBrazo()
    {
        if (panelInfoAR == null || textoInfo == null) return;
        panelInfoAR.SetActive(true);
        textoInfo.text = "<b>TREN SUPERIOR</b>\nFundamental para la fuerza de empuje, tracción y una postura correcta. Este entrenamiento integral activa pecho, espalda, hombros y brazos, mejorando tu soporte articular y capacidad de carga en el día a día.";
    }

    public void MostrarPierna()
    {
        if (panelInfoAR == null || textoInfo == null) return;
        panelInfoAR.SetActive(true);
        textoInfo.text = "<b>TREN INFERIOR</b>\nLa base de tu estabilidad, postura y potencia motriz. Al entrenar este grupo, activas cuádriceps, glúteos e isquiotibiales, maximizando además la quema calórica general.";
    }

    public void MostrarTorso()
    {
        if (panelInfoAR == null || textoInfo == null) return;
        panelInfoAR.SetActive(true);
        textoInfo.text = "<b>TORSO (ABDOMEN)</b>\nEl centro de gravedad y equilibrio de todo el cuerpo. Fortalecer el recto abdominal y los oblicuos protege tu columna vertebral, mejora la postura y optimiza la transferencia de fuerza.";
    }

    public void OcultarInfo()
    {
        if (panelInfoAR != null) panelInfoAR.SetActive(false);
    }
}