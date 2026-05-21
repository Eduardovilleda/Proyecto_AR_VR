using UnityEngine;
using System.IO;
using TMPro; // Necesario para el texto

// Esta pequeña clase es el "molde" de lo que vamos a guardar
[System.Serializable]
public class DatosGuardados
{
    public int rutinasCompletadas = 0;
}

public class GestorPerfil : MonoBehaviour
{
    public DatosGuardados datosActuales;
    public TextMeshProUGUI textoContador; // Aquí arrastrarás tu nuevo texto
    private string rutaArchivo;

    void Start()
    {
        // Define la ruta oculta dentro del celular para guardar el archivo JSON
        rutaArchivo = Application.persistentDataPath + "/perfil_gym.json";
        CargarDatos();
    }

    public void CargarDatos()
    {
        // Si el archivo ya existe (no es la primera vez que abre la app), lo lee
        if (File.Exists(rutaArchivo))
        {
            string contenido = File.ReadAllText(rutaArchivo);
            datosActuales = JsonUtility.FromJson<DatosGuardados>(contenido);
        }
        else
        {
            // Si es la primera vez, crea datos nuevos en cero
            datosActuales = new DatosGuardados();
        }
        ActualizarUI();
    }

    public void GuardarDatos()
    {
        // Traduce los datos a texto JSON y lo guarda en el celular
        string json = JsonUtility.ToJson(datosActuales);
        File.WriteAllText(rutaArchivo, json);
        ActualizarUI();
    }

    public void SumarRutina()
    {
        // Agrega 1 al contador y guarda inmediatamente
        datosActuales.rutinasCompletadas++;
        GuardarDatos();
    }

    void ActualizarUI()
    {
        // Actualiza la pantalla para que el usuario vea su progreso
        if (textoContador != null)
        {
            textoContador.text = "Rutinas Terminadas: " + datosActuales.rutinasCompletadas;
        }
    }
}