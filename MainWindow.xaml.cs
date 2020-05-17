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
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace Calculator
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {


        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private Dictionary<string, object> _propertyValues = new Dictionary<string, object>();

        public T GetValue<T>([CallerMemberName] string propertyName = null)
        {
            if (_propertyValues.ContainsKey(propertyName))
                return (T)_propertyValues[propertyName];
            return default(T);
        }
        public bool SetValue<T>(T newValue, [CallerMemberName] string propertyName = null)
        {
            var currentValue = GetValue<T>(propertyName);
            if (currentValue == null && newValue != null
             || currentValue != null && !currentValue.Equals(newValue))
            {
                _propertyValues[propertyName] = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }
            return false;
        }

        #endregion

        Model m = new Model();
        public MainWindow()
        {
            this.DataContext = this;
            InitializeComponent();
        }

        public string Saisie
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string Resultat
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        private ObservableCollection<Historique> _historique;
        public ObservableCollection<Historique> MonHistorique
        {
            get
            {
                if (_historique == null) _historique = new ObservableCollection<Historique>();
                return _historique;
            }

            set
            {
                SetValue(value);
            }
        }



        private void Button_Click_Op(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            Saisie += b.Content.ToString();
        }

        //Gestion des touches du clavier
        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                try
                {
                    Resultat = "=" + m.calcul(textb.Text).ToString();
                    Historique newHisto = new Historique() { ResultatHisto = Resultat, SaisieHisto = Saisie };
                    MonHistorique.Add(newHisto);
                }
                catch (Exception ex)
                {
                    Saisie = "Error!";
                }
            }
            else 
            {
                // Saisie += e.Key;
            }
            
        }

        //Gestion exception au click du bouton
        private void Result_click(object sender, RoutedEventArgs e)
        {
            try
            {
                Resultat = "=" + m.calcul(textb.Text).ToString();
                Historique newHisto = new Historique() { ResultatHisto = Resultat, SaisieHisto = Saisie };
                MonHistorique.Add(newHisto);
            }
            catch (Exception ex)
            {
                Saisie = "Error!";
            }
        }

     
     
        
       

     

      
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
            if (Saisie.Length > 0)
            {
                Saisie = Saisie.Substring(0, Saisie.Length - 1);
            }
        }

        private void onClearClick(object sender, RoutedEventArgs e)
        {
            MonHistorique.Clear();
        }
    }
}


