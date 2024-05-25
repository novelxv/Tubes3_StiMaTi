public static class Similarity {
    /* Function to find the most similar pattern */
    public static string FindMostSim(string pattern, string[] patterns, int threshold){
        string mostSim = "";
        int minHamming = int.MaxValue;

        foreach (string p in patterns){
            // hitung jarak hamming antara 2 pattern
            int hamming = HammingDistance(pattern, p);
            double sim = 100.0 * (pattern.length - hamming) / pattern.length;
            // jika similarity lebih besar dari threshold dan jarak hamming lebih kecil
            if (sim >= threshold && hamming < minHamming){
                minHamming = hamming;
                mostSim = p;
            }
        }
        return mostSim;
    }

    /* Function to calculate the Hamming Distance */
    private static int HammingDistance(string pattern1, string pattern2){
        if (pattern1.Length != pattern2.Length){
            return -1;
        }
        int distance = 0;
        // iterasi sepanjang pattern
        for (int i = 0; i < pattern1.Length; i++){
            // jika karakter tidak sama
            if (pattern1[i] != pattern2[i]){
                distance++;
            }
        }
        return distance;
    }
}