using System;
using System.Data.SqlClient;
using System.Windows;
using prueva1; // Tu conexión a BD

namespace prueba1
{
    public partial class LoginWindow : Window
    {
        // 1. CREAMOS ESTA PROPIEDAD PARA GUARDAR EL ROL Y QUE LA PANTALLA PRINCIPAL LO LEA
        public string RolUsuario { get; private set; } = "";

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string usuario = txtUser.Text;
            string password = txtPass.Password;

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Por favor ingresa usuario y contraseña.");
                return;
            }

            try
            {
                using (SqlConnection conexion = ConexionDB.ObtenerConexion())
                {
                    string query = "SELECT Rol FROM Usuarios_Sistema WHERE Username=@user AND PasswordHash=@pass";
                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@user", usuario);
                    cmd.Parameters.AddWithValue("@pass", password);

                    object resultado = cmd.ExecuteScalar();

                    if (resultado != null)
                    {
                        // 2. GUARDAMOS EL ROL AQUÍ ANTES DE CERRAR
                        RolUsuario = resultado.ToString();
                        
                        // 3. INDICAMOS QUE EL LOGIN FUE EXITOSO
                        this.DialogResult = true; 
                    }
                    else
                    {
                        MessageBox.Show("Usuario o contraseña incorrectos.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}