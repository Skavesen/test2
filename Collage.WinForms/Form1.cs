using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Collage.Engine;

namespace Collage
{
    using System.IO;
    using System.Linq;

    public partial class Form1 : Form
    {
        private readonly List<FileInfo> imagesList = new List<FileInfo>();
        private string outputDirectory = "";

        private Generator collage;

        public Form1()
        {
            InitializeComponent();
        }

        private void bntChooseDir_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            imagesList.AddRange(openFileDialog1.FileNames.Select(name => new FileInfo(name)));
            ShowInformation(string.Format("Выбрано файлов {0}.", imagesList.Count));
            
        }

        private void btnSelectOutputDir_Click(object sender, EventArgs e)
        {
            this.folderBrowserDialog1.ShowDialog();
            this.outputDirectory = folderBrowserDialog1.SelectedPath;
            ShowInformation(string.Format("Фото сохранится в: {0}.", this.outputDirectory));
            btnCollage.Enabled = imagesList.Count > 0;
        }

        private void btnCollage_Click(object sender, EventArgs e)
        {
            if (imagesList == null || imagesList.Count == 0)
            {
                ShowInformation("Не выбраны фотографии.");
                return;
            }
            if (outputDirectory == null )
            {
                ShowInformation("Не выбрана дериктория для сохранения.");
                return;
            }
            ShowInformation("Процесс запущен...");
            DisableControls();

            var settings =
                new Settings(
                    new DimensionSettings
                        {
                            NumberOfColumns = Convert.ToInt32(nudColumns.Value),
                            NumberOfRows = Convert.ToInt32(nudRows.Value),
                            TileHeight = Convert.ToInt32(nudItemHeight.Value),
                            TileWidth = Convert.ToInt32(nudItemWidth.Value),
                            TileScalePercent = new Percentage(Convert.ToInt32(nudScalePercent.Value))
                        },
                        imagesList,
                        new DirectoryInfo(folderBrowserDialog1.SelectedPath)
                    );

            CreateCollage(settings);
        }

        private void ShowInformation(string message)
        {
            listBox1.Items.Add(message);
        }

        private void DisableControls()
        {
            btnChooseFiles.Enabled = false;
            btnSelectOutputDir.Enabled = false;
            btnCollage.Enabled = false;
            nudColumns.Enabled = false;
            nudItemHeight.Enabled = false;
            nudItemWidth.Enabled = false;
            nudRows.Enabled = false;
            nudScalePercent.Enabled = false;
        }

        private void EnableControls()
        {
            btnChooseFiles.Enabled = true;
            btnSelectOutputDir.Enabled = true;
            btnCollage.Enabled = true;
            nudColumns.Enabled = true;
            nudItemHeight.Enabled = true;
            nudItemWidth.Enabled = true;
            nudRows.Enabled = true;
            nudScalePercent.Enabled = true;
        }

        private void CreateCollage(Settings settings)
        {
            this.collage = new Generator(settings);
            
            collage.CreateCompleted += this.collage_CreateCompleted;
            collage.CreateProgressChanged += this.collage_CreateProgressChanged;

            collage.CreateAsync();
        }

        void collage_CreateProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        void collage_CreateCompleted(object sender, AsyncCompletedEventArgs e)
        {
            this.ShowInformation("Завершено");

            if (e.UserState != null)
            {
                ShowInformation(e.UserState.ToString());
            }
                
            progressBar1.Value = 0;
            EnableControls();
        }
    }
}