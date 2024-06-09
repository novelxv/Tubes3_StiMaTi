using algo;
using Database;

class Program
{
    static void Main(string[] args)
    {   
        Console.WriteLine("Fingerprint Matching Program"); 

        // Konfigurasi DatabaseManager
        string conn = "server=localhost;user=tubes3;database=tubes3;port=3306;password=stimati";
        DatabaseManager dbManager = new(conn);

        // Membuat instance dari Data class
        DataProcessor data = new(dbManager);

        // Menampilkan data biodata
        // Console.WriteLine("Data Biodata:");
        // foreach (var biodata in data.BiodataList)
        // {
        //     Console.WriteLine($"NIK: {biodata.NIK}, Nama: {biodata.Nama}, Tempat Lahir: {biodata.TempatLahir}, Tanggal Lahir: {biodata.TanggalLahir?.ToString("yyyy-MM-dd")}, Jenis Kelamin: {biodata.JenisKelamin}");
        // }

        // Menampilkan data sidik jari
        Console.WriteLine("\nData Sidik Jari:");
        // foreach (var sidikJari in data.SidikJariList)
        // {
        //     Console.WriteLine($"Nama: {sidikJari.Nama}, Berkas Citra: {sidikJari.BerkasCitra}");
        // }
        List<string?> sidikJari = data.GetAllSidikJari();
        // foreach (var sidik in sidikJari)
        // {
        //     Console.WriteLine(sidik);
        // }

        string inputFingerprintImagePath = "..\\test\\1__M_Left_index_finger.BMP";
        List<string?> databaseFingerprints = data.GetAllSidikJari().Where(s => s != null).ToList();
        string? bestMatchFingerprint;
        double executionTime;
        double bestMatchPercentage;
        (bestMatchFingerprint, executionTime, bestMatchPercentage) = FingerprintsProcessor.ProcessFingerprints(inputFingerprintImagePath, databaseFingerprints, false);
        Console.WriteLine($"Best Match Fingerprint: {bestMatchFingerprint}, Execution Time: {executionTime}, Best Match Percentage: {bestMatchPercentage}");
        string? name = data.GetNamaFromSidikJari(bestMatchFingerprint);
        Console.WriteLine($"Best Match Name: {name}");
    }
}
