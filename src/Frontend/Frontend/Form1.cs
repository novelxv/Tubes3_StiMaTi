using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Database;
using algo;

namespace Frontend
{
    public partial class Form1 : Form
    {

        public string ImageLocation { get; private set; }
        public Form1()
        {
            InitializeComponent();
            bioPanel.BackColor = Color.FromArgb(0, Color.Black);
            tempPanel.BackColor = Color.FromArgb(0, Color.Black);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "jpg files (*.jpg)|*.jpg|PNG files (*.png)|*.png|All files (*.*)|*.*";

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    ImageLocation = dialog.FileName;
                    InputImage.ImageLocation = ImageLocation;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("An Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Event handler untuk event Click
        private void KMP_Click(object sender, EventArgs e)
        {
        // Tambahkan logika pemrosesan KMP di sini, menggunakan ImageLocation
        if (!string.IsNullOrEmpty(ImageLocation))
        {
            List<string?> databaseFingerprints = DataProcessor.GetAllSidikJari().Where(s => s != null).ToList();
            string? name;
            double executionTime;
            double bestMatchPercentage;
            (name, executionTime, bestMatchPercentage) = FingerprintsProcessor.ProcessFingerprints(ImageLocation, databaseFingerprints, true);
            if (name != null){
                Biodata? biodata = BiodataProcessor.GetBiodata(name, DataProcessor.BiodataList);
                if (biodata != null)
                {
                    // convert TanggalLahir DateTime to string
                    string tanggalLahirString = biodata.TanggalLahir.ToString();
                    UpdateBioPanelLabels(biodata.Nama[0], biodata.TempatLahir, tanggalLahirString, biodata.JenisKelamin, biodata.GolonganDarah, biodata.Alamat, biodata.Agama, biodata.StatusPerkawinan, biodata.Pekerjaan, biodata.Kewarganegaraan);
                }
            }
            // convert executionTime to string
            string executionTimeString = executionTime.ToString();
            string bestMatchPercentageString = bestMatchPercentage.ToString();
            UpdateTempPanelLabels(executionTimeString, bestMatchPercentageString);
        }
        }

        // Event handler untuk event Click
        private void BM_Click(object sender, EventArgs e)
        {
        // Tambahkan logika pemrosesan BM di sini, menggunakan ImageLocation
        if (!string.IsNullOrEmpty(ImageLocation))
        {
            List<string?> databaseFingerprints = DataProcessor.GetAllSidikJari().Where(s => s != null).ToList();
            string? name;
            double executionTime;
            double bestMatchPercentage;
            (name, executionTime, bestMatchPercentage) = FingerprintsProcessor.ProcessFingerprints(ImageLocation, databaseFingerprints, false);
            if (name != null){
            Biodata? biodata = BiodataProcessor.GetBiodata(name, DataProcessor.BiodataList);
                if (biodata != null)
                {
                    // convert TanggalLahir DateTime to string
                    string tanggalLahirString = biodata.TanggalLahir.ToString();
                    UpdateBioPanelLabels(biodata.Nama[0], biodata.TempatLahir, tanggalLahirString, biodata.JenisKelamin, biodata.GolonganDarah, biodata.Alamat, biodata.Agama, biodata.StatusPerkawinan, biodata.Pekerjaan, biodata.Kewarganegaraan);
                }
            }
            // convert executionTime to string
            string executionTimeString = executionTime.ToString();
            string bestMatchPercentageString = bestMatchPercentage.ToString();
            UpdateTempPanelLabels(executionTimeString, bestMatchPercentageString);
        }
        }

        private void FingerprintForm_Load(object sender, EventArgs e)
        {
            Console.WriteLine("Fingerprint Matching Program");

            // Konfigurasi DatabaseManager
            string conn = "server=localhost;user=tubes3;database=tubes3;port=3306;password=stimati";
            DatabaseManager dbManager = new(conn);

            // Inisialisasi DataProcessor dengan DatabaseManager
            DataProcessor.Initialize(dbManager);
        }

        private void UpdateBioPanelLabels(string nama, string tempatLahir, string tanggalLahir, string jenisKelamin, string golonganDarah, string alamat, string agama, string statusPerkawinan, string pekerjaan, string kewarganegaraan)
        {

            labelNama.Text = $"Nama: {nama}";
            labelTempatLahir.Text = $"Tempat Lahir: {tempatLahir}";
            labelTanggalLahir.Text = $"Tanggal Lahir: {tanggalLahir}";
            labelJenisKelamin.Text = $"Jenis Kelamin: {jenisKelamin}";
            labelGolonganDarah.Text = $"Golongan Darah: {golonganDarah}";
            labelAlamat.Text = $"Alamat: {alamat}";
            labelAgama.Text = $"Agama: {agama}";
            labelStatusPerkawinan.Text = $"Status Perkawinan: {statusPerkawinan}";
            labelPekerjaan.Text = $"Pekerjaan: {pekerjaan}";
            labelKewarganegaraan.Text = $"Kewarganegaraan: {kewarganegaraan}";
        }

        private void UpdateTempPanelLabels(string waktuPencarian, string persentaseKecocokan)
        {
            labelWaktuPencarian.Text = $"{waktuPencarian} ms";
            labelPersentaseKecocokan.Text = $"{persentaseKecocokan}%";
        }
    }
}
