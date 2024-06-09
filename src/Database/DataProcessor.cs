using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using MySql.Data.MySqlClient;
using Image = SixLabors.ImageSharp.Image;
using System.Text;
using SixLabors.ImageSharp.Processing;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Globalization;

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
                    Nama = ConvertAlayToNormal(reader["nama"].ToString()),
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
            if (path == null) return "";
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

        /* *** Text Processing *** */

        /* Convert Alay to Normal */
        public static string ConvertAlayToNormal(string alayText){
            if (alayText == null) return "";
            // handle penggunaan angka
            var replacements = new (string pattern, string replacement)[]{
                (@"4", "a"), // 4 -> a
                (@"3", "e"), // 3 -> e
                (@"1", "i"), // 1 -> i
                (@"0", "o"), // 0 -> o
                (@"5", "s"), // 5 -> s
                (@"2", "z"), // 2 -> z
                (@"6", "g"), // 6 -> g
                (@"7", "t"), // 7 -> t
                (@"8", "b"), // 8 -> b
                (@"9", "g")  // 9 -> g
            };
            string normalText = alayText;
            foreach (var (pattern, replacement) in replacements){
                normalText = Regex.Replace(normalText, pattern, replacement, RegexOptions.IgnoreCase);
            }
            normalText = normalText.ToLower();
            
            // handle penyingkatan
            var abbrevations = new (string pattern, string replacement)[]{
                // abbreviations for first names
                (@"\bEmp\b", "Empluk"),  // Emp -> Empluk
                (@"\bInd\b", "Indra"),  // Ind -> Indra
                (@"\bRat\b", "Ratna"),  // Rat -> Ratna
                (@"\bNar\b", "Nardi"),  // Nar -> Nardi
                (@"\bCin\b", "Cinthia"),  // Cin -> Cinthia
                (@"\bNas\b", "Nasir"),  // Nas -> Nasir
                (@"\bGam\b", "Gamani"),  // Gam -> Gamani
                (@"\bHer\b", "Heryanto"),  // Her -> Heryanto
                (@"\bJan\b", "Januar"),  // Jan -> Januar
                (@"\bLas\b", "Lasmono"),  // Las -> Lasmono
                (@"\bSaf\b", "Safitri"),  // Saf -> Safitri
                (@"\bKui\b", "Kunthara"),  // Kui -> Kunthara
                (@"\bDar\b", "Darma"),  // Dar -> Darma
                (@"\bMuH\b", "Muhammad"),  // MuH -> Muhammad
                (@"\bRai\b", "Raisa"),  // Rai -> Raisa
                (@"\bSar\b", "Sari"),  // Sar -> Sari
                (@"\bCin\b", "Cinthia"),  // Cin -> Cinthia
                (@"\bMaR\b", "Maria"),  // MaR -> Maria
                (@"\bWiNdA\b", "Winda"),  // WiNdA -> Winda
                (@"\bV3r0\b", "Vero"),  // V3r0 -> Vero
                (@"\bYuL14\b", "Yulia"),  // YuL14 -> Yulia
                (@"\bKuNtHaRa\b", "Kunthara"),  // KuNtHaRa -> Kunthara
                (@"\b4m4\b", "Amalia"),  // 4m4 -> Amalia
                (@"\bPuPut\b", "Puput"),  // PuPut -> Puput
                (@"\bWiNDa\b", "Winda"),  // WiNDa -> Winda
                (@"\bNaRdI\b", "Nardi"),  // NaRdI -> Nardi
                (@"\bNaS\b", "Nasir"),  // NaS -> Nasir
                // abbreviations for last names
                (@"\bSuw\b", "Suwandi"),  // Suw -> Suwandi
                (@"\bRah\b", "Rahman"),  // Rah -> Rahman
                (@"\bYul\b", "Yulianto"),  // Yul -> Yulianto
                (@"\bLat\b", "Latif"),  // Lat -> Latif
                (@"\bTha\b", "Thamrin"),  // Tha -> Thamrin
                (@"\bPra\b", "Prasetyo"),  // Pra -> Prasetyo
                (@"\bSim\b", "Simanjuntak"),  // Sim -> Simanjuntak
                (@"\bNur\b", "Nurhadi"),  // Nur -> Nurhadi
                (@"\bDam\b", "Damanik"),  // Dam -> Damanik
                (@"\bBud\b", "Budiman"),  // Bud -> Budiman
                (@"\bSul\b", "Sulistyo"),  // Sul -> Sulistyo
                (@"\bWan\b", "Wandira"),  // Wan -> Wandira
                (@"\bWir\b", "Wirawan"),  // Wir -> Wirawan
                (@"\bPut\b", "Putri"),  // Put -> Putri
                (@"\bMal\b", "Mala"),  // Mal -> Mala
                (@"\bHid\b", "Hidayat"),  // Hid -> Hidayat
                (@"\bRam\b", "Ramadhan"),  // Ram -> Ramadhan
                (@"\bNab\b", "Nababan"),  // Nab -> Nababan
                (@"\bKal\b", "Kalim"),  // Kal -> Kalim
                (@"\bSid\b", "Sidharta"),  // Sid -> Sidharta
                (@"\bKus\b", "Kuswandi"),  // Kus -> Kuswandi
                (@"\bMan\b", "Mangunsong")  // Man -> Mangunsong
            };
            foreach (var (pattern, replacement) in abbrevations){
                normalText = Regex.Replace(normalText, pattern, replacement, RegexOptions.IgnoreCase);
            }

            // handle kombinasi huruf besar-kecil
            normalText = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(normalText);
            return normalText;
        }

    }
}
