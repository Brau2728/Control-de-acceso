using System.Windows;

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
            bool? result = login.ShowDialog();

            if (result == true)
            {
        // Lógica para cambiar la pantalla actual a la de Dashboard
        // Esto lo haremos con el ContentControl que te mencioné antes
            }
        }

        private void Test_Click(object sender, RoutedEventArgs e)
{
    RegistroPersonal ventana = new RegistroPersonal();
    ventana.ShowDialog();
}
    }
}