using System;
using System.Collections.Generic;
using Database;

namespace algo {
    public class BiodataProcessor {
        /* Get Biodata from Name */
        public static Biodata? GetBiodata(string name, List<Biodata> biodatas){
            foreach (var biodata in biodatas){
                if (IsAMatch(name, biodata.Nama)){
                    biodata.Nama = [name];
                    return biodata;
                }
            }

            // if no exact match, use Levenshtein
            double maxSimilarity = 0;
            Biodata? bestMatch = null;
            foreach (var biodata in biodatas){
                double similarity = UseLevenshtein(name, biodata.Nama);
                if (similarity > maxSimilarity){
                    maxSimilarity = similarity;
                    bestMatch = biodata;
                    bestMatch.Nama = [name];
                }
            }

            return maxSimilarity > 30.0 ? bestMatch : null;
        }

        /* Check if name is a match */
        private static bool IsAMatch(string name, List<string>? names){
            if (names == null){
                return false;
            }

            // exact match using KMP
            foreach (var n in names){
                if (KMP.KMPMatch(name, n)){
                    return true;
                }
            }

            // exact match using BM
            foreach (var n in names){
                if (BoyerMoore.BMMatch(name, n)){
                    return true;
                }
            }

            return false;
        }

        /* Use Levenshtein */
        private static double UseLevenshtein(string name, List<string> names){
            // return highest similarity
            double maxSimilarity = 0;
            foreach (var n in names){
                double similarity = Levenshtein.ComputeSimilarity(name, n);
                if (similarity > maxSimilarity){
                    maxSimilarity = similarity;
                }
            }
            return maxSimilarity;
        }
    }
}