using System.Drawing;

namespace Frontend
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.InputImage = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.kmp = new System.Windows.Forms.PictureBox();
            this.bm = new System.Windows.Forms.PictureBox();
            this.bioPanel = new System.Windows.Forms.Panel();
            this.tempPanel = new System.Windows.Forms.Panel();
            this.labelNama = new System.Windows.Forms.Label();
            this.labelTempatLahir = new System.Windows.Forms.Label();
            this.labelTanggalLahir = new System.Windows.Forms.Label();
            this.labelJenisKelamin = new System.Windows.Forms.Label();
            this.labelGolonganDarah = new System.Windows.Forms.Label();
            this.labelAlamat = new System.Windows.Forms.Label();
            this.labelAgama = new System.Windows.Forms.Label();
            this.labelStatusPerkawinan = new System.Windows.Forms.Label();
            this.labelPekerjaan = new System.Windows.Forms.Label();
            this.labelKewarganegaraan = new System.Windows.Forms.Label();
            this.labelWaktuPencarian = new System.Windows.Forms.Label();
            this.labelPersentaseKecocokan = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.InputImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kmp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bm)).BeginInit();
            this.bioPanel.SuspendLayout();
            this.tempPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(277, 396);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(148, 38);
            this.button1.TabIndex = 0;
            this.button1.Text = "Select Image";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // InputImage
            // 
            this.InputImage.BackColor = System.Drawing.SystemColors.Window;
            this.InputImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.InputImage.Location = new System.Drawing.Point(58, 117);
            this.InputImage.Name = "InputImage";
            this.InputImage.Size = new System.Drawing.Size(269, 243);
            this.InputImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.InputImage.TabIndex = 1;
            this.InputImage.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(370, 113);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(259, 249);
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // BM
            // 
            this.bm.Image = global::Frontend.Properties.Resources.bm;
            this.bm.Location = new System.Drawing.Point(200, 447);
            this.bm.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.bm.Size = new System.Drawing.Size(146, 148);
            this.bm.BackColor = System.Drawing.Color.Transparent;
            this.bm.Click += new System.EventHandler(this.BM_Click);
            // 
            // KMP
            // 
            this.kmp.Image = global::Frontend.Properties.Resources.kmp;
            this.kmp.Location = new System.Drawing.Point(351, 447);
            this.kmp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.kmp.Size = new System.Drawing.Size(152, 154);
            this.kmp.BackColor = System.Drawing.Color.Transparent;
            this.kmp.Click += new System.EventHandler(this.KMP_Click);
            // 
            // bioPanel
            // 
            this.bioPanel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.bioPanel.Location = new System.Drawing.Point(675, 140);
            this.bioPanel.Name = "bioPanel";
            this.bioPanel.Size = new System.Drawing.Size(320, 380);
            this.bioPanel.TabIndex = 5;
            // 
            // tempPanel
            // 
            this.tempPanel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tempPanel.Location = new System.Drawing.Point(675, 520);
            this.tempPanel.Name = "timeandPercentagePanel";
            this.tempPanel.Size = new System.Drawing.Size(320, 80);
            this.tempPanel.TabIndex = 5;
            // 
            // labelNama
            // 
            this.labelNama.AutoSize = true;
            this.labelNama.Location = new System.Drawing.Point(10, 10);
            this.labelNama.Name = "labelNama";
            this.labelNama.Size = new System.Drawing.Size(180, 20);
            this.labelNama.TabIndex = 0;
            this.labelNama.Text = "Nama : ";
            // 
            // labelTempatLahir
            // 
            this.labelTempatLahir.AutoSize = true;
            this.labelTempatLahir.Location = new System.Drawing.Point(10, 40);
            this.labelTempatLahir.Name = "labelTempatLahir";
            this.labelTempatLahir.Size = new System.Drawing.Size(180, 20);
            this.labelTempatLahir.TabIndex = 0;
            this.labelTempatLahir.Text = "Tempat Lahir : ";
            // 
            // labelTanggalLahir
            // 
            this.labelTanggalLahir.AutoSize = true;
            this.labelTanggalLahir.Location = new System.Drawing.Point(10, 70);
            this.labelTanggalLahir.Name = "labelTanggalLahir";
            this.labelTanggalLahir.Size = new System.Drawing.Size(180, 20);
            this.labelTanggalLahir.TabIndex = 0;
            this.labelTanggalLahir.Text = "Tanggal Lahir : ";
            // 
            // labelJenisKelamin
            // 
            this.labelJenisKelamin.AutoSize = true;
            this.labelJenisKelamin.Location = new System.Drawing.Point(10, 100);
            this.labelJenisKelamin.Name = "labelJenisKelamin";
            this.labelJenisKelamin.Size = new System.Drawing.Size(180, 20);
            this.labelJenisKelamin.TabIndex = 0;
            this.labelJenisKelamin.Text = "Jenis Kelamin : ";
            // 
            // labelGolonganDarah
            // 
            this.labelGolonganDarah.AutoSize = true;
            this.labelGolonganDarah.Location = new System.Drawing.Point(10, 130);
            this.labelGolonganDarah.Name = "labelGolonganDarah";
            this.labelGolonganDarah.Size = new System.Drawing.Size(180, 20);
            this.labelGolonganDarah.TabIndex = 0;
            this.labelGolonganDarah.Text = "Golongan Darah : ";
            // 
            // labelAlamat
            // 
            this.labelAlamat.AutoSize = true;
            this.labelAlamat.Location = new System.Drawing.Point(10, 160);
            this.labelAlamat.Name = "labelAlamat";
            this.labelAlamat.Size = new System.Drawing.Size(180, 20);
            this.labelAlamat.TabIndex = 0;
            this.labelAlamat.Text = "Alamat : ";
            // 
            // labelAgama
            // 
            this.labelAgama.AutoSize = true;
            this.labelAgama.Location = new System.Drawing.Point(10, 260);
            this.labelAgama.Name = "labelAgama";
            this.labelAgama.Size = new System.Drawing.Size(180, 20);
            this.labelAgama.TabIndex = 0;
            this.labelAgama.Text = "Agama : ";
            // 
            // labelStatusPerkawinan
            // 
            this.labelStatusPerkawinan.AutoSize = true;
            this.labelStatusPerkawinan.Location = new System.Drawing.Point(10, 290);
            this.labelStatusPerkawinan.Name = "labelStatusPerkawinan";
            this.labelStatusPerkawinan.Size = new System.Drawing.Size(180, 20);
            this.labelStatusPerkawinan.TabIndex = 0;
            this.labelStatusPerkawinan.Text = "Status Perkawinan : ";
            // 
            // labelPekerjaan
            // 
            this.labelPekerjaan.AutoSize = true;
            this.labelPekerjaan.Location = new System.Drawing.Point(10, 320);
            this.labelPekerjaan.Name = "labelStatusPerkawinan";
            this.labelPekerjaan.Size = new System.Drawing.Size(180, 20);
            this.labelPekerjaan.TabIndex = 0;
            this.labelPekerjaan.Text = "Pekerjaan : ";
            // 
            // labelKewarganegaraan
            // 
            this.labelKewarganegaraan.AutoSize = true;
            this.labelKewarganegaraan.Location = new System.Drawing.Point(10, 350);
            this.labelKewarganegaraan.Name = "labelKewarganegaraan";
            this.labelKewarganegaraan.Size = new System.Drawing.Size(180, 20);
            this.labelKewarganegaraan.TabIndex = 0;
            this.labelKewarganegaraan.Text = "Kewarganegaraan : ";
            // 
            // labelWaktuPencarian
            // 
            this.labelWaktuPencarian.Visible = true;
            this.labelWaktuPencarian.ForeColor = Color.Black;
            this.labelWaktuPencarian.AutoSize = true;
            this.labelWaktuPencarian.Location = new System.Drawing.Point(200, 23);
            this.labelWaktuPencarian.Name = "labelWaktuPencarian";
            this.labelWaktuPencarian.Size = new System.Drawing.Size(100, 20);
            this.labelWaktuPencarian.TabIndex = 0;
            this.labelWaktuPencarian.Text = "9999.999 ms";
            // 
            // labelPersentaseKecocokan
            // 
            this.labelPersentaseKecocokan.Visible = true;
            this.labelPersentaseKecocokan.ForeColor = Color.Black;
            this.labelPersentaseKecocokan.AutoSize = true;
            this.labelPersentaseKecocokan.Location = new System.Drawing.Point(230, 45);
            this.labelPersentaseKecocokan.Name = "labelKewarganegaraan";
            this.labelPersentaseKecocokan.Size = new System.Drawing.Size(100, 20);
            this.labelPersentaseKecocokan.TabIndex = 0;
            this.labelPersentaseKecocokan.Text = "99.999%";
            // 
            // Tambahkan Label ke Panel
            this.bioPanel.Controls.Add(this.labelNama);
            this.bioPanel.Controls.Add(this.labelTempatLahir);
            this.bioPanel.Controls.Add(this.labelTanggalLahir);
            this.bioPanel.Controls.Add(this.labelJenisKelamin);
            this.bioPanel.Controls.Add(this.labelGolonganDarah);
            this.bioPanel.Controls.Add(this.labelAlamat);
            this.bioPanel.Controls.Add(this.labelAgama);
            this.bioPanel.Controls.Add(this.labelStatusPerkawinan);
            this.bioPanel.Controls.Add(this.labelPekerjaan);
            this.bioPanel.Controls.Add(this.labelKewarganegaraan);
            this.tempPanel.Controls.Add(this.labelWaktuPencarian);
            this.tempPanel.Controls.Add(this.labelPersentaseKecocokan);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Frontend.Properties.Resources.bg_blocking;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1064, 668);
            this.Controls.Add(this.bioPanel);
            this.Controls.Add(this.tempPanel);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.InputImage);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.kmp);
            this.Controls.Add(this.bm);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "StiMaTi";
            ((System.ComponentModel.ISupportInitialize)(this.InputImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kmp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bm)).EndInit();
            this.bioPanel.ResumeLayout(false);
            this.bioPanel.PerformLayout();
            this.tempPanel.ResumeLayout(false);
            this.tempPanel.PerformLayout();
            this.ResumeLayout(false);

            this.Controls.SetChildIndex(this.kmp, 0);
            this.Controls.SetChildIndex(this.bm, 1);
            this.Controls.SetChildIndex(this.InputImage, 2);
        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox InputImage;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox kmp;
        private System.Windows.Forms.PictureBox bm;
        private System.Windows.Forms.Panel bioPanel;
        private System.Windows.Forms.Panel tempPanel;
        private System.Windows.Forms.Label labelNama;
        private System.Windows.Forms.Label labelTempatLahir;
        private System.Windows.Forms.Label labelTanggalLahir;
        private System.Windows.Forms.Label labelJenisKelamin;
        private System.Windows.Forms.Label labelGolonganDarah;
        private System.Windows.Forms.Label labelAlamat;
        private System.Windows.Forms.Label labelAgama;
        private System.Windows.Forms.Label labelStatusPerkawinan;
        private System.Windows.Forms.Label labelPekerjaan;
        private System.Windows.Forms.Label labelKewarganegaraan;
        private System.Windows.Forms.Label labelWaktuPencarian;
        private System.Windows.Forms.Label labelPersentaseKecocokan;
    }
}

