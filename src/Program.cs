using algo;

class Program
{
    static void Main(string[] args)
    {
        // Contoh input sidik jari
        string inputFingerprint = "11001010"; // Mendefinisikan sidik jari input sebagai string
        List<string> databaseFingerprints = new List<string> // Mendefinisikan list sidik jari dalam database
        {
            "11001010",
            "11001000",
            "11001001",
            "01010101"
        };

        // Pencarian dengan KMP dan Boyer-Moore
        bool matchFound = false; // Inisialisasi variabel untuk menyimpan status kecocokan
        foreach (var dbFingerprint in databaseFingerprints) // Loop melalui setiap sidik jari dalam database
        {
            if (KMP.IsAMatch(inputFingerprint, dbFingerprint) || BoyerMoore.IsAMatch(inputFingerprint, dbFingerprint)) // Memeriksa kecocokan menggunakan algoritma KMP atau Boyer-Moore
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