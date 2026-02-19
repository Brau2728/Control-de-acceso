using System.Windows;
using prueva1;
using prueba1;

namespace prueba1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is MainViewModel vm)
            {
                vm.ProcesarLectura();
            }
        }

        private void OpenLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow();
            
            if (login.ShowDialog() == true)
            {
                if (login.RolUsuario == "Administrador")
                {
                    PanelAdminWindow panelAdmin = new PanelAdminWindow();
                    panelAdmin.ShowDialog(); 
                }
                else
                {
                    MessageBox.Show("Sesión iniciada como Guardia. Sistema listo.", "Acceso", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void Test_Click(object sender, RoutedEventArgs e)
        {
            RegistroPersonal ventana = new RegistroPersonal();
            ventana.ShowDialog();
        }
    }
}