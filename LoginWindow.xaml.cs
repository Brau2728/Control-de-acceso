using System;
using System.Data.SqlClient; 
using System.Windows;
using System.Windows.Controls;
using prueva1; // <--- ESTO ES LA CLAVE: Importamos el namespace donde vive tu ConexionDB

namespace prueba1 
{
    public partial class LoginWindow : Window
    {
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
                // Ahora sí va a encontrar esta clase porque agregamos el 'using prueva1'
                using (SqlConnection conexion = ConexionDB.ObtenerConexion())
                {
                    string query = "SELECT Rol FROM Usuarios_Sistema WHERE Username=@user AND PasswordHash=@pass";
                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@user", usuario);
                    cmd.Parameters.AddWithValue("@pass", password);

                    object resultado = cmd.ExecuteScalar();

                    if (resultado != null)
                    {
                        string rol = resultado.ToString();
                        MessageBox.Show($"¡Bienvenido {usuario}! Rol: {rol}");
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
            this.Close();
        }
    }
}