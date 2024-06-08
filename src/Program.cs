using algo;
using Database;

class Program
{
    static void Main(string[] args)
    {   
        Console.WriteLine("Fingerprint Matching Program"); 

        // Konfigurasi DatabaseManager
        string conn = "server=localhost;user=tubes3;database=tubes3;port=3306;password=stimati";
        DatabaseManager dbManager = new DatabaseManager(conn);

        // Membuat instance dari Data class
        DataProcessor data = new(dbManager);

        // Menampilkan data biodata
        Console.WriteLine("Data Biodata:");
        foreach (var biodata in data.BiodataList)
        {
            Console.WriteLine($"NIK: {biodata.NIK}, Nama: {biodata.Nama}, Tempat Lahir: {biodata.TempatLahir}, Tanggal Lahir: {biodata.TanggalLahir?.ToString("yyyy-MM-dd")}, Jenis Kelamin: {biodata.JenisKelamin}");
        }

        // Menampilkan data sidik jari
        Console.WriteLine("\nData Sidik Jari:");
        foreach (var sidikJari in data.SidikJariList)
        {
            Console.WriteLine($"Nama: {sidikJari.Nama}, Berkas Citra: {sidikJari.BerkasCitra}");
        }


        // Contoh input sidik jari
        string inputFingerprint = "11001001"; // Mendefinisikan sidik jari input sebagai string
        List<string> databaseFingerprints = new List<string> // Mendefinisikan list sidik jari dalam database
        {
            "11001010",
            "1298372180011093712110010008789707897987",
            "11001001",
            "01010101"
        };

        // Pencarian dengan KMP dan Boyer-Moore
        bool matchFound = false; // Inisialisasi variabel untuk menyimpan status kecocokan
        foreach (var dbFingerprint in databaseFingerprints) // Loop melalui setiap sidik jari dalam database
        {
            if (BoyerMoore.BMMatch(inputFingerprint, dbFingerprint)) // Memeriksa kecocokan menggunakan algoritma KMP atau Boyer-Moore
            {
                Console.WriteLine("Exact match found with fingerprint: " + dbFingerprint); // Mencetak pesan jika ditemukan kecocokan
                matchFound = true; // Mengatur variabel status kecocokan menjadi true
                break; // Keluar dari loop setelah menemukan kecocokan
            }
        }

        if (!matchFound) // Jika tidak ditemukan kecocokan
        {
            // Menghitung kemiripan menggunakan Hamming Distance
            int threshold = 75; // Nilai threshold dalam persen
            string mostSimilarFingerprint = Similarity.FindMostSim(inputFingerprint, databaseFingerprints, threshold); // Mencari sidik jari yang paling mirip
            if (mostSimilarFingerprint != null) // Jika ditemukan sidik jari yang mirip
            {
                Console.WriteLine("Most similar fingerprint: " + mostSimilarFingerprint); // Mencetak pesan sidik jari yang paling mirip
            }
            else
            {
                Console.WriteLine("No similar fingerprint found above threshold."); // Mencetak pesan jika tidak ada sidik jari yang mirip
            }
        }
    }
}