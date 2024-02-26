using System.Threading.Tasks;
using Firebase.Auth;
using Firebase.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SC_Auth : MonoBehaviour
{
    private FirebaseAuth auth;

    public TMP_InputField inputEmailRegistro;
    public TMP_InputField inputPasswordRegistro;
    public TMP_InputField inputEmailLogin;
    public TMP_InputField inputPasswordLogin;
    public TMP_InputField inputVerificaContraseña;

    public TMP_Text outputText;

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
                // El usuario ha sido creado exitosamente.
                Firebase.Auth.AuthResult result = task.Result;
                Debug.LogFormat("Usuario de Firebase creado exitosamente.");

                canvasLogin.SetActive(true);
                canvasRegistro.SetActive(false);
            }
            
        });
    }

    public void IniciarSesion(string email, string password)
    {
        outputText.text = email + password ;
        Debug.Log("Email: " + email);
        Debug.Log(" Pass: " + password);
        
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            Firebase.FirebaseException firebaseEX = task.Exception.GetBaseException() as Firebase.FirebaseException;
            outputText.text = "Principio: " + firebaseEX.Message;
            if (task.IsCanceled)
            {
                Debug.LogError("IniciarSesionUsuario cancelado.");
                return;
            }
            if (task.IsFaulted)
            {
                outputText.text ="Faulted: " +  firebaseEX.Message;
                Debug.LogError("IniciarSesionUsuario encontro error: " + task.Exception);
                return;
               
            }

            if(task.IsCompletedSuccessfully)
            {
                Firebase.Auth.AuthResult result = task.Result;
                Debug.LogFormat("Usuario ha iniciado sesion exitosamente. ");

                canvasLogin.SetActive(false);
                canvasRegistro.SetActive(false);
                canvasPrincipal.SetActive(true);
            }
            outputText.text = "Fin: " + firebaseEX.Message;
        });
    }

    public void RegistrarUsuario()
    {
        string email = inputEmailRegistro.text;
        string password = inputPasswordRegistro.text;
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
        string email = inputEmailLogin.text;
        string password = inputPasswordLogin.text;
        IniciarSesion(email, password);
        Debug.Log("Pass en metodo IniciarSesion y email: " + password + ",   " + email);
    }
}
