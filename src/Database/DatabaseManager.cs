using Bogus;
using MySql.Data.MySqlClient;
using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Database
{
    public class DatabaseManager
    {
        private readonly string connectionString;
        private static readonly string[] items = new[] { "Islam", "Kristen", "Katholik", "Hindu", "Buddha", "Konghucu" };
        private static readonly string[] itemsArray = new[] { "Belum Menikah", "Menikah", "Cerai" };

        public DatabaseManager(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public string GetConnectionString()
        {
            return connectionString;
        }

        public void PopulateSidikJari(int numberOfRecords, string imageDirectory)
        {
            var faker = new Faker("id_ID");

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                for (int i = 1; i <= numberOfRecords; i++)
                {
                    var prefix = i.ToString() + "__";
                    List<string> imagePaths = FindImagesWithPrefix(imageDirectory, prefix);

                    if (imagePaths.Count == 0)
                    {
                        Console.WriteLine($"Gambar dengan prefix {prefix} tidak ditemukan.");
                        continue;
                    }

                    var name = faker.Name.FullName();

                    foreach (var imagePath in imagePaths)
                    {
                        var sidikJari = new
                        {
                            BerkasCitra = imagePath, // menyimpan path dari gambar
                            Nama = name
                        };

                        InsertSidikJariData(conn, sidikJari.Nama, sidikJari.BerkasCitra);
                    }
                }

                conn.Close();
            }
        }

        public void PopulateBiodata()
        {
            var faker = new Faker("id_ID");

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                List<string> names = GetNamesFromSidikJari(conn);
                HashSet<string> uniqueSet = new HashSet<string>(names);
                List<string> uniquelist = new List<string>(uniqueSet);

                foreach (var name in uniquelist)
                {
                    var biodata = new
                    {
                        NIK = faker.Random.String2(16, "0123456789"),
                        Nama = ApplyAlayVariations(name),
                        TempatLahir = faker.Address.City(),
                        TanggalLahir = faker.Date.Past(30, DateTime.Now.AddYears(-18)),
                        JenisKelamin = faker.PickRandom(new[] { "Laki-Laki", "Perempuan" }),
                        GolonganDarah = faker.PickRandom(new[] { "A", "B", "AB", "O" }),
                        Alamat = faker.Address.FullAddress(),
                        Agama = faker.PickRandom(items),
                        StatusPerkawinan = faker.PickRandom(itemsArray),
                        Pekerjaan = faker.Name.JobTitle(),
                        Kewarganegaraan = "Indonesia"
                    };

                    InsertBiodata(conn, biodata);
                }

                conn.Close();
            }
        }

        private string ConvertImageToAscii(string imagePath)
        {
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"File {imagePath} tidak ditemukan.");
                return string.Empty;
            }

            try
            {
                using (Image<Rgba32> image = Image.Load<Rgba32>(imagePath))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        image.SaveAsBmp(ms);
                        byte[] imageBytes = ms.ToArray();

                        // Konversi ke ASCII 8-bits
                        StringBuilder asciiStringBuilder = new StringBuilder();
                        foreach (byte b in imageBytes)
                        {
                            asciiStringBuilder.Append(Convert.ToString(b, 2).PadLeft(8, '0'));
                        }

                        Console.Write(asciiStringBuilder.ToString().Length);
                        return asciiStringBuilder.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error mengkonversi gambar: {ex.Message}");
                return string.Empty;
            }
        }

        private void InsertSidikJariData(MySqlConnection conn, string nama, string berkasCitra)
        {
            string query = "INSERT INTO sidik_jari (nama, berkas_citra) VALUES (@nama, @berkasCitra)";
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@nama", nama);
                cmd.Parameters.AddWithValue("@berkasCitra", berkasCitra);
                cmd.ExecuteNonQuery();
            }
        }

        private List<string> GetNamesFromSidikJari(MySqlConnection conn)
        {
            string query = "SELECT nama FROM sidik_jari";
            List<string> names = new List<string>();

            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        names.Add(reader.GetString("nama"));
                    }
                }
            }

            return names;
        }

        private void InsertBiodata(MySqlConnection conn, dynamic biodata)
        {
            string query = @"INSERT INTO biodata (NIK, nama, tempat_lahir, tanggal_lahir, jenis_kelamin, golongan_darah, alamat, agama, status_perkawinan, pekerjaan, kewarganegaraan) 
                            VALUES (@NIK, @Nama, @TempatLahir, @TanggalLahir, @JenisKelamin, @GolonganDarah, @Alamat, @Agama, @StatusPerkawinan, @Pekerjaan, @Kewarganegaraan)";
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@NIK", biodata.NIK);
                cmd.Parameters.AddWithValue("@Nama", biodata.Nama);
                cmd.Parameters.AddWithValue("@TempatLahir", biodata.TempatLahir);
                cmd.Parameters.AddWithValue("@TanggalLahir", biodata.TanggalLahir);
                cmd.Parameters.AddWithValue("@JenisKelamin", biodata.JenisKelamin);
                cmd.Parameters.AddWithValue("@GolonganDarah", biodata.GolonganDarah);
                cmd.Parameters.AddWithValue("@Alamat", biodata.Alamat);
                cmd.Parameters.AddWithValue("@Agama", biodata.Agama);
                cmd.Parameters.AddWithValue("@StatusPerkawinan", biodata.StatusPerkawinan);
                cmd.Parameters.AddWithValue("@Pekerjaan", biodata.Pekerjaan);
                cmd.Parameters.AddWithValue("@Kewarganegaraan", biodata.Kewarganegaraan);
                cmd.ExecuteNonQuery();
            }
        }

        private string ApplyAlayVariations(string name)
        {
            Random rnd = new Random();
            int variation = rnd.Next(1, 5);

            switch (variation)
            {
                case 1:
                    return CombineCase(name);
                case 2:
                    return UseNumbers(name);
                case 3:
                    return Shorten(name);
                case 4:
                    return CombineAll(name);
                default:
                    return name;
            }
        }

        private string CombineCase(string input)
        {
            StringBuilder sb = new StringBuilder();
            bool toUpper = true;
            foreach (char c in input)
            {
                sb.Append(toUpper ? char.ToUpper(c) : char.ToLower(c));
                if (char.IsLetter(c))
                {
                    toUpper = !toUpper;
                }
            }
            return sb.ToString();
        }

        private string UseNumbers(string input)
        {
            Dictionary<char, char> replacements = new Dictionary<char, char>
            {
                {'a', '4'}, {'e', '3'}, {'i', '1'}, {'o', '0'}, {'s', '5'}, {'g', '9'}
            };
            StringBuilder sb = new StringBuilder();
            foreach (char c in input)
            {
                sb.Append(replacements.ContainsKey(char.ToLower(c)) ? replacements[char.ToLower(c)] : c);
            }
            return sb.ToString();
        }

        private string Shorten(string input)
        {
            string[] words = input.Split(' ');
            StringBuilder sb = new StringBuilder();
            foreach (string word in words)
            {
                sb.Append(word.Length > 2 ? word.Substring(0, 3) : word);
                sb.Append(" ");
            }

            return sb.ToString().Trim();
        }

        private string CombineAll(string input)
        {
            string combinedCase = CombineCase(input);
            string withNumbers = UseNumbers(combinedCase);
            return Shorten(withNumbers);
        }

        private string FindImageWithPrefix(string directory, string prefix)
        {
            var files = Directory.GetFiles(directory, $"{prefix}*.bmp");
            return files.FirstOrDefault() ?? string.Empty;
        }

        private List<string> FindImagesWithPrefix(string directory, string prefix)
        {
            var files = Directory.GetFiles(directory, $"{prefix}*.bmp").ToList();
            return files;
        }
    }
}
