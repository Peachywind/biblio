using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using Lib;

namespace WpfLib
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                string fileExtension = Path.GetExtension(filePath);

                if (fileExtension == ".txt")
                {
                    OpenTextFile(filePath);
                }
                else if (fileExtension == ".json")
                {
                    DeserializeJsonFile(filePath);
                }
            }
        }

        private void OpenTextFile(string filePath)
        {
            string fileContent = File.ReadAllText(filePath);
            TextBox.Text = fileContent;
        }

        private void DeserializeJsonFile(string filePath)
        {
            string jsonContent = File.ReadAllText(filePath);
            try
            {
                dynamic deserializedObject = ImmitationWork.Deserialize<dynamic>(jsonContent);
                TextBox.Text = deserializedObject.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deserializing JSON file: " + ex.Message);
            }
        }

        private void SaveJsonButton_Click(object sender, RoutedEventArgs e)
        {
            
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON files (*.json)|*.json";
            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                string jsonContent = TextBox.Text;

                
                    ImmitationWork.Serialize(jsonContent, filePath);
                    MessageBox.Show("JSON file saved successfully.");
                
                
            }
        }
    }
}
