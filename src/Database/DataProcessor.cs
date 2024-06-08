using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using MySql.Data.MySqlClient;
using Image = SixLabors.ImageSharp.Image;
using System.Text;
using SixLabors.ImageSharp.Processing;

namespace Database {
    /* Biodata */
    public class Biodata {
        public string? NIK { get; set; }
        public string? Nama { get; set; }
        public string? TempatLahir { get; set; }
        public DateTime? TanggalLahir { get; set; }
        public string? JenisKelamin { get; set; }
        public string? GolonganDarah { get; set; }
        public string? Alamat { get; set; }
        public string? Agama { get; set; }
        public string? StatusPerkawinan { get; set; }
        public string? Pekerjaan { get; set; }
        public string? Kewarganegaraan { get; set; }
    }

    /* Sidik Jari */
    public class SidikJari {
        public string? BerkasCitra { get; set; }
        public string? Nama { get; set; }
    }

    /* Data Processor */
    public class DataProcessor {
        public List<Biodata> BiodataList { get; set; }
        public List<SidikJari> SidikJariList { get; set; }
        private readonly DatabaseManager _dbManager;

        /* Constructor */
        public DataProcessor(DatabaseManager manager){
            _dbManager = manager;
            BiodataList = [];
            SidikJariList = [];
            LoadData();
        }

        /* Load Data */
        private void LoadData(){
            LoadBiodata();
            LoadSidikJari();
        }

        /* Load Biodata */
        private void LoadBiodata(){
            string query = "SELECT * FROM biodata";
            using MySqlConnection conn = new(_dbManager.GetConnectionString());
            MySqlCommand cmd = new(query, conn);
            conn.Open();
            using MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read()){
                BiodataList.Add(new Biodata {
                    NIK = reader["NIK"].ToString(),
                    Nama = reader["nama"].ToString(),
                    TempatLahir = reader["tempat_lahir"].ToString(),
                    TanggalLahir = reader.IsDBNull(reader.GetOrdinal("tanggal_lahir")) ? null : reader.GetDateTime("tanggal_lahir"),
                    JenisKelamin = reader["jenis_kelamin"].ToString(),
                    GolonganDarah = reader["golongan_darah"].ToString(),
                    Alamat = reader["alamat"].ToString(),
                    Agama = reader["agama"].ToString(),
                    StatusPerkawinan = reader["status_perkawinan"].ToString(),
                    Pekerjaan = reader["pekerjaan"].ToString(),
                    Kewarganegaraan = reader["kewarganegaraan"].ToString()
                });
            }
        }

        /* Load Sidik Jari */
        private void LoadSidikJari(){
            string query = "SELECT * FROM sidik_jari";
            using MySqlConnection conn = new(_dbManager.GetConnectionString());
            MySqlCommand cmd = new(query, conn);
            conn.Open();
            using MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read()) {
                SidikJariList.Add(new SidikJari {
                    BerkasCitra = ConvertImageToAscii(reader["berkas_citra"].ToString()),
                    Nama = reader["nama"].ToString()
                });
            }
        }

        /* *** Image Processing *** */

        /* Convert Image Path to ASCII */
        public static string ConvertImageToAscii(string path){
            using Image<Rgba32> image = LoadBitmap(path);
            return ConvertToAscii(image);
        }

        /* Load Bitmap from Path */
        private static Image<Rgba32> LoadBitmap(string path){
            return Image.Load<Rgba32>(path);
        }
        
        /* Convert Bipmap to ASCII */
        public static string ConvertToAscii(Image<Rgba32> image){
            if (image == null) return "";
            StringBuilder sb = new();

            image.Mutate(x => x.Resize(image.Width / 10, image.Height / 10));

            for (int y = 0; y < image.Height; y++){
                for (int x = 0; x < image.Width; x++){
                    Rgba32 pixelColor = image[x, y];
                    int grayScale = (int)((pixelColor.R * 0.299) + (pixelColor.G * 0.587) + (pixelColor.B * 0.114));
                    char asciiChar = GetAsciiCharacter(grayScale);
                    sb.Append(asciiChar);
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }

        /* Get ASCII Character */
        private static char GetAsciiCharacter(int grayScale){
            string asciiChars = "@%#*+=-:. ";
            int index = (int)(grayScale / 255.0 * (asciiChars.Length - 1));
            return asciiChars[index];
        }
    }
}
