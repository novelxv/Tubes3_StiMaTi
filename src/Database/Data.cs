using MySql.Data.MySqlClient;

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

    /* Data */
    public class Data {
        public List<Biodata> BiodataList { get; set; }
        public List<SidikJari> SidikJariList { get; set; }
        private readonly DatabaseManager _dbManager;

        /* Constructor */
        public Data(DatabaseManager manager){
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
                    BerkasCitra = reader["berkas_citra"].ToString(),
                    Nama = reader["nama"].ToString()
                });
            }
        }
    }
}