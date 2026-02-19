using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using prueva1;
using prueba1;

namespace prueba1
{
    public partial class PanelAdminWindow : Window
    {
        public PanelAdminWindow()
        {
            InitializeComponent();
        }

        private void BtnNuevoRegistro_Click(object sender, RoutedEventArgs e)
        {
            RegistroPersonal ventana = new RegistroPersonal();
            ventana.ShowDialog();
            
            if (PanelDirectorio.Visibility == Visibility.Visible)
            {
                CargarDirectorio();
            }
        }

        private void BtnDirectorio_Click(object sender, RoutedEventArgs e)
        {
            PanelBienvenida.Visibility = Visibility.Collapsed;
            PanelDirectorio.Visibility = Visibility.Visible;
            CargarDirectorio();
        }

        private void BtnCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Sesión cerrada correctamente.", "Cerrar Sesión", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        private void BtnEditarRegistro_Click(object sender, RoutedEventArgs e)
        {
            Button boton = sender as Button;
            if (boton != null && boton.CommandParameter != null)
            {
                string matriculaSeleccionada = boton.CommandParameter.ToString();
                MessageBox.Show($"Abriendo panel de edición y novedades para la matrícula: {matriculaSeleccionada}", "Editar Personal");
            }
        }

       private void CargarDirectorio()
{
    try
    {
        using (SqlConnection conexion = ConexionDB.ObtenerConexion())
        {
            // VERSIÓN SIMPLIFICADA: Solo busca en Personal_Naval sin usar JOINs
            string query = @"
                SELECT 
                    Matricula, 
                    IdGrado AS Grado, 
                    Nombres AS Nombre, 
                    Apellidos, 
                    IdJefatura AS Jefatura,
                    'SIN REGISTRO' AS Novedad
                FROM Personal_Naval"; 

            SqlCommand cmd = new SqlCommand(query, conexion);
            SqlDataAdapter adaptador = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adaptador.Fill(dt);

            dgPersonal.ItemsSource = dt.DefaultView;
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show("Error al cargar personal: " + ex.Message);
    }
}
    }
}