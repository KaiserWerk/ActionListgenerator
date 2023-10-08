using KaiserMVVMCore;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Xml.Linq;

namespace ActionlistGenerator
{
    public class ViewModel : ViewModelBase
    {
        private string generatedContent;

        public ObservableCollection<Action> SelectedActions { get; set; } = new();
        public Action SelectedAction { get; set; }
        public List<Action> AvailableActions { get; set; } = new();
        public Action SelectedAvailableAction { get; set; }
        public List<Preset> AvailablePresets { get; set; }
        public Preset SelectedPreset { get; set; }
        public string GeneratedContent { get => generatedContent; set => base.Set(ref this.generatedContent, value); }

        public RelayCommand AddActionCommand { get; }
        public RelayCommand RemoveActionCommand { get; }
        public RelayCommand GenerateCommand { get; }
        public RelayCommand GenerateFromPresetCommand { get; }
        public RelayCommand SaveToFileCommand { get; }

        public ViewModel()
        {
            this.AddActionCommand = new RelayCommand(this.AddActionExecute);
            this.RemoveActionCommand = new RelayCommand(this.RemoveActionExecute);
            this.GenerateCommand = new RelayCommand(this.GenerateExecute);
            this.GenerateFromPresetCommand = new RelayCommand(this.GenerateFromPresetExecute);
            this.SaveToFileCommand = new RelayCommand(this.SaveToFileExecute);

            this.AvailableActions = AllActions.ListOfAvailableActions;
            this.AvailablePresets = AllPresets.ListOfAvailablePresets;
        }

        private void AddActionExecute()
        {
            if (this.SelectedAvailableAction != null)
            {
                this.SelectedActions.Add(this.SelectedAvailableAction.HardCopy());
            }
        }

        private void RemoveActionExecute()
        {
            if (this.SelectedAction != null)
            {
                this.SelectedActions.Remove(this.SelectedAction);
            }
        }
        private void GenerateExecute()
        {
            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
            var document = new XDocument();
            var actionlist = new XElement("actionlist",
                new XAttribute("ns", "http://someaddr/schema.xslt"),
                new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
                new XAttribute(xsi + "schemaLocation", "http://schemas.foo.com http://schemas.foo.com/Current/xsd/Foo.xsd")
            );
            var actions = new XElement("actions");
            document.Add(actionlist);
            actionlist.Add(actions);

            foreach (var item in this.SelectedActions)
            {
                actions.Add(item.Element);
            }

            this.GeneratedContent = document.ToString();
        }

        private void GenerateFromPresetExecute()
        {
            if (this.SelectedPreset == null)
                return;

            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
            var document = new XDocument();
            var actionlist = new XElement("actionlist",
                new XAttribute("ns", "http://someaddr/schema.xslt"),
                new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
                new XAttribute(xsi + "schemaLocation", "http://schemas.foo.com http://schemas.foo.com/Current/xsd/Foo.xsd")
            );
            var actions = new XElement("actions");
            document.Add(actionlist);
            actionlist.Add(actions);

            foreach (var item in this.SelectedPreset.Elements)
            {
                actions.Add(item);
            }

            this.GeneratedContent = document.ToString();
        }

        private void SaveToFileExecute()
        {
            this.GenerateExecute();
            if (string.IsNullOrEmpty(this.GeneratedContent))
                return;
            var d = new SaveFileDialog() 
            {
                Filter = "XML-Dateien|*.xml"
            };
            if (d.ShowDialog() == true)
            {
                File.WriteAllText(d.FileName, this.GeneratedContent);
            }
        }
    }
}
