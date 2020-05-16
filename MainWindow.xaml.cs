using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Model m = new Model();
        public MainWindow()
        {
            //m.calcul("(12,2/3+5*(2,3+(3+7)))-3+5,5*2+(2+(-2+-2))");
            InitializeComponent();
        }

        private void Button_Click_Op(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            textb.Text += b.Content.ToString();
        }

        //Gestion des touches du clavier
        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                try
                {
                    textb.Text += "=" + m.calcul(textb.Text).ToString();
                }
                catch (Exception ex)
                {
                    textb.Text = "Error!";
                }
            }
            /*else
            {
                textb.Text = "Caractère non reconnu";
            }*/
        }

        //Gestion exception au click du bouton
        private void Result_click(object sender, RoutedEventArgs e)
        {
            try
            {
                textb.Text += "=" + m.calcul(textb.Text).ToString();
            }
            catch (Exception ex)
            {
                textb.Text = "Error!";
            }
        }

/*        //Permet la suppression de l'historique
        private void Clear_History()
        {

        }*/

/*        private void Clear_Histo_Click(object sender, RoutedEventArgs e)
        {
            //Supprime l'historique
            listbhisto.Clear
        }*/

        /*
        #region listbhisto dependency property
        public static readonly DependencyProperty ListbHProperty = DependencyProperty.Register("Listbhisto", typeof(ObservableCollection<String>), typeof(Window));

        public ObservableCollection<String> Listbhisto
        {
            get { return (ObservableCollection<String>)GetValue(ListbHProperty); }
            set { SetValue(ListbHProperty, value); }
        }
        #endregion */

      
        private void Off_Click_1(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            textb.Text = "";
        }

        private void R_Click(object sender, RoutedEventArgs e)
        {
            if (textb.Text.Length > 0)
            {
                textb.Text = textb.Text.Substring(0, textb.Text.Length - 1);
            }
        }

        private void textb_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void textr_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Listbhisto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}


