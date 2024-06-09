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
            return null;
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

            // approximate match using Levenshtein
            const double threshold = 0.8;
            foreach (var n in names){
                double similarity = Levenshtein.ComputeSimilarity(name, n);
                if (similarity >= threshold){
                    return true;
                }
            }

            return false;
        }
    }
}