using System;
using System.Linq;
using System.Collections.Generic;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using MySql.Data.MySqlClient;
using System.Text;
using SixLabors.ImageSharp.Processing;
using System.Text.RegularExpressions;
using System.Globalization;

namespace Database {
    /* Biodata */
    public class Biodata {
        public string? NIK { get; set; }
        public List<string>? Nama { get; set; }
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
    public static class DataProcessor {
        public static List<Biodata> BiodataList { get; set; } = new List<Biodata>();
        public static List<SidikJari> SidikJariList { get; set; } = new List<SidikJari>();
        private static DatabaseManager _dbManager;

        /* Initialize */
        public static void Initialize(DatabaseManager manager){
            _dbManager = manager;
            LoadData();
        }

        /* Load Data */
        private static void LoadData(){
            LoadBiodata();
            LoadSidikJari();
        }

        /* Load Biodata */
        private static void LoadBiodata(){
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
        private static void LoadSidikJari(){
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
        public static string ConvertImageToAscii(string? path){
            if (path == null) return "";
            using Image<Rgba32> image = LoadBitmap(path);
            return ConvertToAscii(image);
        }

        /* Load Bitmap from Path */
        public static Image<Rgba32> LoadBitmap(string path){
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
        public static List<string> ConvertAlayToNormal(string? alayText)
        {
            var results = new List<string>();
            if (alayText == null) return results;

            // Handle penggunaan angka
            var replacements = new (string pattern, string replacement)[]
            {
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
            foreach (var (pattern, replacement) in replacements)
            {
                normalText = Regex.Replace(normalText, pattern, replacement, RegexOptions.IgnoreCase);
            }
            // to title case
            normalText = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(normalText);
            results.Add(normalText);

            string regexPattern = @"[a-z][aiueo]{0,2}([^aiueo][aiueo]{0,2}[^aiueo])+[aiueo]{0,2}";
            
            string consonantPattern1 = @"^[^aiueo]*$";
            string consonantPattern2 = @"^[aiueo][^aiueo]+$";
            
            normalText = normalText.ToLower();
            if (Regex.IsMatch(normalText, consonantPattern1) || Regex.IsMatch(normalText, consonantPattern2))
            {
                string[] parts = normalText.Split(' ');
                if (parts.Length > 0)
                {
                    results.Clear();
                    results = GeneratePossibleNames(parts[0], regexPattern);
                }
            }
            return results;
        }

        private static List<string> GeneratePossibleNames(string word, string pattern)
        {
            var results = new List<string>();
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            var vowels = "aiueo".ToCharArray();

            var queue = new Queue<(string current, int index)>();
            queue.Enqueue((string.Empty, 0));

            while (queue.Count > 0)
            {
                var (current, index) = queue.Dequeue();

                if (index >= word.Length)
                {
                    if (regex.IsMatch(current))
                    {
                        current = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(current);
                        results.Add(current);
                    }
                    continue;
                }

                queue.Enqueue((current + word[index], index + 1));

                if ("aiueo".IndexOf(word[index]) == -1)
                {
                    foreach (var v1 in vowels)
                    {
                        queue.Enqueue((current + word[index] + v1, index + 1));
                        foreach (var v2 in vowels)
                        {
                            queue.Enqueue((current + word[index] + v1 + v2, index + 1));
                        }
                    }
                }
            }

            return results;
        }

        /* *** Get Data *** */

        /* Get Nama from Sidik Jari */
        public static string? GetNamaFromSidikJari(string berkasCitra){
            return SidikJariList.Find(s => s.BerkasCitra == berkasCitra)?.Nama;
        }

        /* Get All Sidik Jari ASCII */
        public static List<string?> GetAllSidikJari(){
            return SidikJariList.Select(s => s.BerkasCitra).ToList();
        }
    }
}