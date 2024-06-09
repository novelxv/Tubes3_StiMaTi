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

        // Inisialisasi DataProcessor dengan DatabaseManager
        DataProcessor.Initialize(dbManager);

        // Menampilkan data sidik jari
        Console.WriteLine("\nData Sidik Jari:");
        List<string?> sidikJari = DataProcessor.GetAllSidikJari();

        string inputFingerprintImagePath = "..\\test\\test.BMP";
        List<string?> databaseFingerprints = DataProcessor.GetAllSidikJari().Where(s => s != null).ToList();
        string? name;
        double executionTime;
        double bestMatchPercentage;
        (name, executionTime, bestMatchPercentage) = FingerprintsProcessor.ProcessFingerprints(inputFingerprintImagePath, databaseFingerprints, true);
        Console.WriteLine($"Best Match Fingerprint: {name}, Execution Time: {executionTime}, Best Match Percentage: {bestMatchPercentage}");

        if (name != null){
            Biodata? biodata = BiodataProcessor.GetBiodata(name, DataProcessor.BiodataList);
            Console.WriteLine($"NIK: {biodata.NIK}, Nama: {biodata.Nama[0]}");
        }
    }
}
