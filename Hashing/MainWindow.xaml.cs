using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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

namespace Hashing
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private byte[] _key;
        private byte[] _hashedKey;
        private byte[] _hmacHashed;

        private readonly HashingSupervisor _hashingSupervisor;

        public MainWindow()
        {
            InitializeComponent();
            _key = new byte[] { };
            _hashedKey = new byte[] { };
            _hashingSupervisor = new HashingSupervisor();
            //_hashingSupervisor.ConvertTestToSHA256();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(UserInputKeyText.Text))
            {
                MessageBox.Show("You must Create a Key!");
            }
            else
            {
                // This key is a byte array of the text input by the user
                _key = _hashingSupervisor.GetBytes(UserInputKeyText.Text);
                // go and get the key hashed after and returns it to the hashed key array
                _hashedKey = _hashingSupervisor.GetHashedKey(HashAlgorithmComboBox.SelectedIndex, _key);
                // reverts it to plain text and writes it to th Text field
                HashedKeyText.Text = _hashingSupervisor.GetBytesToPlainText(_hashedKey);
            }
        }

        private void Hvac_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(HashedKeyText.Text) || string.IsNullOrEmpty(UserInputMessageText.Text))
            {
                MessageBox.Show("You must create a key and type in a message");
            }
            else
            {
                byte[] message = _hashingSupervisor.GetBytes(UserInputMessageText.Text);
                _hmacHashed = _hashingSupervisor.GenerateHmac256Message(_key, message);
                HmacMessageText.Text = _hashingSupervisor.GetBytesToPlainText(_hmacHashed);
                HmacMessageHex.Text = _hashingSupervisor.GetByteArrayToHexString(_hmacHashed);
                
            }

            
        }

        private void Authenticate_OnClick(object sender, RoutedEventArgs e)
        {
            AuthenticatedText.Text = _hashingSupervisor.CheckAuthenticity(_key, _hmacHashed, UserInputMessageText.Text).ToString();
        }
    }
}