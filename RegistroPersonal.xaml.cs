using Microsoft.Win32; // Para abrir archivos (Foto)
using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using prueva1; // <--- AGREGA ESTO

namespace prueba1 // <--- REVISA QUE TU NAMESPACE SEA 'prueba1' o 'prueva1'
{
    public partial class RegistroPersonal : Window
    {
        private byte[] fotoBytes = null; // Aquí guardaremos la foto en memoria

        public RegistroPersonal()
        {
            InitializeComponent();
        }

        // BOTÓN: SELECCIONAR FOTO
        private void BtnFoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Seleccionar imagen";
            op.Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png";
            
            if (op.ShowDialog() == true)
            {
                // 1. Mostrar la imagen en pantalla
                imgFoto.Source = new BitmapImage(new Uri(op.FileName));

                // 2. Convertir imagen a Bytes para guardarla en SQL
                fotoBytes = File.ReadAllBytes(op.FileName);
            }
        }

        // BOTÓN: GUARDAR
        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            // Validaciones básicas
            if (string.IsNullOrWhiteSpace(txtMatricula.Text) || 
                string.IsNullOrWhiteSpace(txtNombres.Text) ||
                cmbGrado.SelectedItem == null || 
                cmbJefatura.SelectedItem == null)
            {
                MessageBox.Show("Faltan datos obligatorios.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                using (SqlConnection conexion = ConexionDB.ObtenerConexion())
                {
                    string query = @"INSERT INTO Personal_Naval 
                                     (Matricula, Nombres, Apellidos, IdGrado, IdJefatura, FotoPerfil) 
                                     VALUES 
                                     (@mat, @nom, @ape, @idGrado, @idJefa, @foto)";

                    SqlCommand cmd = new SqlCommand(query, conexion);
                    
                    cmd.Parameters.AddWithValue("@mat", txtMatricula.Text);
                    cmd.Parameters.AddWithValue("@nom", txtNombres.Text);
                    cmd.Parameters.AddWithValue("@ape", txtApellidos.Text);
                    
                    // Obtenemos el ID oculto en la propiedad 'Tag' del ComboBoxItem
                    int idGrado = int.Parse(((ComboBoxItem)cmbGrado.SelectedItem).Tag.ToString());
                    int idJefa = int.Parse(((ComboBoxItem)cmbJefatura.SelectedItem).Tag.ToString());
                    
                    cmd.Parameters.AddWithValue("@idGrado", idGrado);
                    cmd.Parameters.AddWithValue("@idJefa", idJefa);

                    // Si no hay foto, enviamos NULL a la base de datos
                    if (fotoBytes != null)
                        cmd.Parameters.AddWithValue("@foto", fotoBytes);
                    else
                        cmd.Parameters.AddWithValue("@foto", DBNull.Value);

                    int filas = cmd.ExecuteNonQuery();

                    if (filas > 0)
                    {
                        MessageBox.Show("Personal registrado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close(); // Cerramos la ventana
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar en BD: " + ex.Message);
            }
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // --- CONTROLES DE IMAGEN ---

private void BtnRotarIzq_Click(object sender, RoutedEventArgs e)
{
    rtRotacion.Angle -= 90;
}

private void BtnRotarDer_Click(object sender, RoutedEventArgs e)
{
    rtRotacion.Angle += 90;
}

private void BtnZoomIn_Click(object sender, RoutedEventArgs e)
{
    // Aumentamos el zoom un 10%
    stEscala.ScaleX += 0.1;
    stEscala.ScaleY += 0.1;
}

private void BtnZoomOut_Click(object sender, RoutedEventArgs e)
{
    // Reducimos, pero evitamos que sea menor a 0.1 para que no desaparezca
    if (stEscala.ScaleX > 0.2)
    {
        stEscala.ScaleX -= 0.1;
        stEscala.ScaleY -= 0.1;
    }
}
    }
}