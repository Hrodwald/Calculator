using System.Windows.Documents;

namespace Calculator
{
    public class Historique : BaseNotifyPropertyChanged
    {
        public string SaisieHisto
        {
            get { return (string)GetField(); }
            set { SetField(value); }
        }
        public string ResultatHisto
        {
            get { return (string)GetField(); }
            set { SetField(value); }
        }

        }
}