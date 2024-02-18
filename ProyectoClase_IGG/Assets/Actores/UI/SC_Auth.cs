using System.Threading.Tasks;
using Firebase.Auth;
using Firebase.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SC_Auth : MonoBehaviour
{
    private FirebaseAuth auth;

    public TMP_InputField inputEmail;
    public TMP_InputField inputPassword;
    public TMP_InputField inputVerificaContraseña;

    public GameObject canvasLogin;
    public GameObject canvasRegistro;
    public GameObject canvasPrincipal;

    void Start()
    {
        // Inicializa la instancia de Firebase Auth.
        auth = FirebaseAuth.DefaultInstance;


}

    public void RegistrarUsuario(string email, string password)
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("RegistrarUsuario fue cancelado.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("RegistrarUsuario encontró un error: " + task.Exception);
                return;
            }

            
            if (task.IsCompletedSuccessfully)
            {
                // El usuario de Firebase ha sido creado exitosamente.
                Firebase.Auth.AuthResult result = task.Result;
                Debug.LogFormat("Usuario de Firebase creado exitosamente.");

                canvasLogin.SetActive(true);
                canvasRegistro.SetActive(false);
            }
            
        });
    }

    public void IniciarSesion(string email, string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("IniciarSesionUsuario fue cancelado.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("IniciarSesionUsuario encontró un error: " + task.Exception);
                return;
            }

            if(task.IsCompletedSuccessfully)
            {
                Firebase.Auth.AuthResult result = task.Result;
                Debug.LogFormat("Usuario ha iniciado sesión exitosamente. ");

                canvasLogin.SetActive(false);
                canvasRegistro.SetActive(false);
                canvasPrincipal.SetActive(true);
            }
            
        });
    }

    public void RegistrarUsuario()
    {
        string email = inputEmail.text;
        string password = inputPassword.text;
        string verifica = inputVerificaContraseña.text;

        if (password.Length < 6)
        {
            Debug.LogError("La contraseña debe tener al menos 6 caracteres.");
            return;
        } 
        else if (password != verifica)
        {
            Debug.Log("Corrige tu contraseña, deben coincidir.");
        }
        else if (password == verifica)
        {
            RegistrarUsuario(email, password);
            Debug.Log("Usuario: " + email);
        }
        
    }

    public void IniciarSesion()
    {
        string email = inputEmail.text;
        string password = inputPassword.text;
        IniciarSesion(email, password);
        Debug.Log("Pass: " + password);
    }
}
