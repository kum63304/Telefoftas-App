using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TelesoftasApp.Models;
using TelesoftasApp.ViewModels;
using Unity.Attributes;


namespace TelesoftasApp
{

    public class MainWindowViewModel : DependencyObject
    {
        
        
        public static readonly DependencyProperty InputFilePathProperty = DependencyProperty.Register("InputFilePath", typeof(string), typeof(MainWindowViewModel), new
            PropertyMetadata("", new PropertyChangedCallback(OnInputFileChanged)));
        public static readonly DependencyProperty OutputFilePathProperty = DependencyProperty.Register("OutputFilePath", typeof(string), typeof(MainWindowViewModel), new
            PropertyMetadata("", new PropertyChangedCallback(OnOutputFileChanged)));

        public static readonly DependencyProperty ResultsProperty = DependencyProperty.Register("Results", typeof(string), typeof(MainWindowViewModel), new
            PropertyMetadata("", new PropertyChangedCallback(OnResultsChanged)));

        public static readonly DependencyProperty MaxLineLenghtProperty = DependencyProperty.Register("MaxLineLenght", typeof(int), typeof(MainWindowViewModel), new
            PropertyMetadata(20));

        public static void OnInputFileChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            MainWindowViewModel viewModel = dependencyObject as MainWindowViewModel;
            viewModel.OnInputFileChanged(e);
        }
        public static void OnOutputFileChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            MainWindowViewModel viewModel = dependencyObject as MainWindowViewModel;
            viewModel.OnOutputFileChanged(e);
        }

        public static void OnResultsChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            MainWindowViewModel viewModel = dependencyObject as MainWindowViewModel;
            viewModel.OnResultsChanged(e);
        }
        private IFileProcessingService _fileProcessingService;
        private ICommand _selectInputFileCommand;
        private ICommand _selectOutputFileCommand;
        private ICommand _minusCommand;
        private ICommand _pliusCommand;
        private Boolean? userChooseSaveResults= null;

        [Dependency]
        public IFileProcessingService FileProcessingService { set { _fileProcessingService = value; } }

        

        public int MaxLineLenght
        {
            get { return (int)GetValue(MaxLineLenghtProperty); }
            set { SetValue(MaxLineLenghtProperty, value); }
        }

        public string Results
        {
            get { return (string)GetValue(ResultsProperty); }
            set { SetValue(ResultsProperty, value); }
        }

        public string InputFilePath
        {
            get { return (string)GetValue(InputFilePathProperty); }
            set { SetValue(InputFilePathProperty, value); }
        }
        public string OutputFilePath
        {
            get { return (string)GetValue(OutputFilePathProperty); }
            set { SetValue(OutputFilePathProperty, value); }
        }
        public ICommand SelectInputFileCommand
        {
            get
            {
                return _selectInputFileCommand ?? (_selectInputFileCommand = new CommandHandler(() => OpenFileSelectionDialog(InputFilePathProperty), true));
            }
        }
        public ICommand SelectOutputFileCommand
        {
            get
            {
                return _selectOutputFileCommand ?? (_selectOutputFileCommand = new CommandHandler(() => OpenFileSelectionDialog(OutputFilePathProperty), true));
            }
        }
        
        public ICommand MinusCommand
        {
            get
            {
                return _minusCommand ?? (_minusCommand = new CommandHandler(() =>
                {
                    if ((MaxLineLenght - 1) > 0)
                        MaxLineLenght--;
                    Process();
                }, true));
            }
        }
        public ICommand PliusCommand
        {
            get
            {
                return _pliusCommand ?? (_pliusCommand = new CommandHandler(() =>
                {
                    MaxLineLenght++;
                    Process();
                }, true));
            }
        }




       
        
        private void Process()
        {
            try
            {
                String context = _fileProcessingService.LoadFileContext(InputFilePath);
                IEnumerable<string> words = _fileProcessingService.SplitToWords(context);
                Results = _fileProcessingService.WordsToLines(words, MaxLineLenght);
            }
            catch (Exception ex)
            {
                Results = ex.Message;
            }

        }


        private void OnInputFileChanged(DependencyPropertyChangedEventArgs e)
        {
            Process();
        }
        private void OnOutputFileChanged(DependencyPropertyChangedEventArgs e)
        {
            userChooseSaveResults = true;
            SaveResultToFile();
        }
        private void OnResultsChanged(DependencyPropertyChangedEventArgs e)
        {
            if (userChooseSaveResults == null)
            {
                MessageBoxResult result = MessageBox.Show("Would you like to save result to file?", "Telesoftas", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    OpenFileSelectionDialog(OutputFilePathProperty);
                }
                else
                {
                    userChooseSaveResults = false;
                }
            }
            else if (userChooseSaveResults==true)
            {
                SaveResultToFile();
            }
        }
        private void SaveResultToFile()
        {
            if (!String.IsNullOrEmpty(OutputFilePath))
            {
                _fileProcessingService.SaveResultsToFile(OutputFilePath, Results);
            }
        }

        private void OpenFileSelectionDialog(DependencyProperty filePathProperty)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                SetValue(filePathProperty, openFileDialog.FileName);
                
        }
    }

}
