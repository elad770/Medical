using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;

namespace LibraryDiagnosis
{

    enum DISEASES { Anemia, Diet, Bleeding, Hyperlipidemia, Blood_Disorder, Hematological_Disorder, Iron_Poisoning, Dehydration, Infection, Vitamin, Viral, Bile_Duct, Heart, Blood, Liver, Kidney, Iron_Deficiency, Muscle, Smoking, Lung, Overactive_Thyroid_Gland, Adult_Diabetes, Cancer, Increased_Consumption_Meat, Use_Various_Medications, Malnutrition,Pregnancy };

    public class Diagnostic
    {
        //All information of the tests relevant to the patient
        Patient Pat;

        //All normal ranges of all tests
        Dictionary<string, double[][]> ranges;

        //A dictionary containing the keys as diseases and the values as treatments for them
        Dictionary<string, string> AllTreatment;

        //An index set for all diseases
        int[] IndTreatment;

        //Auxiliary array of indexes By high/low values
        List<int>[] DisByIndexes;
        

        public Diagnostic(Patient pat,string path)
        {
            Pat = pat;
            CreateAllRanges();
            try
            {
                AllTreatment = File.ReadLines(path)
                .Select(line => line.Split(':'))
                .ToDictionary(split => split[0], split => split[1]);
                IndTreatment = new int[AllTreatment.Count];
            }
            catch
            {
            }
        }

        private void CreateAllRanges()
        {
            //Create all standard test ranges
            ranges = new Dictionary<string, double[][]>();
            ranges.Add("Ages WBC", new double[3][] { new double[] { 4500, 11000 }, new double[] { 5500, 15500 }, new double[] { 6000, 17500 } });
            ranges.Add("Percent Neut", new double[1][] { new double[] { 28, 54 } });
            ranges.Add("Percent Lymph", new double[1][] { new double[] { 36, 52 } });
            ranges.Add("RBC", new double[1][] { new double[] { 4.5, 6 } });
            ranges.Add("Percent HCT", new double[2][] { new double[] { 37, 54 }, new double[] { 33, 47 } });
            ranges.Add("Urea", new double[1][] { new double[] { 17, 43 } });
            ranges.Add("Hemoglobin", new double[3][] { new double[] { 12, 18 }, new double[] { 12, 16 }, new double[] { 11.5, 15.5 } });
            ranges.Add("Creatinine", new double[4][] { new double[] { 0.6, 1.2 } , new double[] { 0.6, 1 }, new double[] { 0.5, 1 }, new double[] { 0.2, 0.5 } });
            ranges.Add("Iron", new double[2][] { new double[] { 60, 160 }, new double[] { 48, 128 } });
            ranges.Add("HDL", new double[2][] { new double[] { 29, 62 }, new double[] { 34, 82 } });
            ranges.Add("AP", new double[2][] { new double[] { 60, 120 }, new double[] { 30, 90 } });
        }


        public List<string[]> AllTests()
        {
            //All test for patient
            DiagnsticWBC();
            DiagnsticNeut();
            DiagnsticLymph();
            DiagnsticRBC();
            DiagnsticHCT();
            DiagnsticUrea();
            DiagnsticHemoglobin();
            DiagnsticCreatinine();
            DiagnsticIron();
            DiagnsticHDL();
            DiagnsticAP();

            //Check max value in the array
            int max = IndTreatment.Max();
           // OrderedDictionary PatientTreatment = null;
            List<string[]> PatientTreatment =null;
           // OrderedDictionary<string, string> PatientTreatment = null;
            if (max != 0)
            {
                PatientTreatment = new List<string[]>() {};
                int count = 1;
                //PatientTreatment = new Dictionary<string, string>();
                for (int i = 0; i < this.IndTreatment.Length; i++)
                {
                    string key = AllTreatment.ElementAt(i).Key;
                    
                    if (IndTreatment[i] > 1)
                    {
                        PatientTreatment.Insert(0,new string[] { key, AllTreatment[key],"0" });
                    }
                    else if (IndTreatment[i] != 0)
                    {
                        PatientTreatment.Add(new string[] { key, AllTreatment[key],(count++).ToString()});
                    }
                    
                }
            }

            return PatientTreatment;
            //return PatientTreatment.Cast<DictionaryEntry>().ToDictionary(k => (string)k.Key, v => (string)v.Value);
        }
        

        private void DiagnsticWBC()
        {
            int[][] ageRanges = { new int[] { 18, 99 }, new int[] { 4, 17 }, new int[] { 0, 3 } };
            double[] properRanges = GetRangesForAge(Pat.Age, ranges["Ages WBC"], ageRanges, ageRanges.GetLength(0) - 1);
            DisByIndexes = new List<int>[2] { new List<int>(), new List<int>() };
            if (Pat.IsHeat)
            {
                DisByIndexes[0].Add((int)DISEASES.Infection);
            }
            DisByIndexes[0].AddRange(new int[] { (int)DISEASES.Blood, (int)DISEASES.Cancer });
            DisByIndexes[1].AddRange(new int[] { (int)DISEASES.Viral, (int)DISEASES.Cancer });
            RevaluationByRange(Pat.cWCB, properRanges, DisByIndexes);
        }

        private void DiagnsticNeut()
        {
            DisByIndexes = new List<int>[2] { new List<int>(), new List<int>() };
            DisByIndexes[0].Add((int)DISEASES.Infection);
            DisByIndexes[1].AddRange(new int[] { (int)DISEASES.Blood_Disorder, (int)DISEASES.Infection, (int)DISEASES.Cancer });
            RevaluationByRange(Pat.cNeut, ranges["Percent Neut"][0], DisByIndexes);
        }

        private void DiagnsticLymph()
        {
            DisByIndexes = new List<int>[2] { new List<int>(), new List<int>() };
            DisByIndexes[0].AddRange(new int[] { (int)DISEASES.Infection, (int)DISEASES.Cancer });
            DisByIndexes[1].Add((int)DISEASES.Blood_Disorder);
            RevaluationByRange(Pat.cLymph, ranges["Percent Lymph"][0], DisByIndexes);
        }

        private void DiagnsticRBC()
        {
            DisByIndexes = new List<int>[2] { new List<int>(), new List<int>() };
            DisByIndexes[0].AddRange(new int[] { (int)DISEASES.Blood_Disorder,(int)DISEASES.Lung });
            AddToSmoking();

            DisByIndexes[1].AddRange(new int[] { (int)DISEASES.Anemia, (int)DISEASES.Bleeding });
            RevaluationByRange(Pat.cRBC, ranges["RBC"][0], DisByIndexes);
        }

        private void DiagnsticHCT()
        {
            double[] ran = GetRangesForGender(Pat.Gender, ranges["Percent HCT"]);
            DisByIndexes = new List<int>[2] { new List<int>(), new List<int>() };
            AddToSmoking();
            DisByIndexes[1].AddRange(new int[] { (int)DISEASES.Anemia, (int)DISEASES.Bleeding });
            RevaluationByRange(Pat.cHCT, ran, DisByIndexes);
        }

        private void AddToSmoking()
        {
            if (Pat.IsSmoking)
            {
                DisByIndexes[0].Add((int)DISEASES.Smoking);
            }
        }


        private void DiagnsticUrea()
        {
            DisByIndexes = new List<int>[2] { new List<int>(), new List<int>() };
            DisByIndexes[0].AddRange(new int[] { (int)DISEASES.Kidney, (int)DISEASES.Diet, (int)DISEASES.Dehydration });
         
            //If eastern Testimony
            double[] rang = ranges["Urea"][0];
            if (Pat.Region.ToString() == "East")
            {
                rang[0] *= 1.1;
                rang[1] *= 1.1;
            }

            //If pregnancy
            //if (!(!Pat.Gender && Pat.IsPregnant == true)|| Pat.Gender)
            if(Pat.IsPregnant != true|| Pat.Gender)
            {
                DisByIndexes[1].AddRange(new int[] { (int)DISEASES.Diet, (int)DISEASES.Liver, (int)DISEASES.Malnutrition });
            }

            RevaluationByRange(Pat.cUrea, rang, DisByIndexes);
        }

        private void DiagnsticHemoglobin()
        {
            DisByIndexes = new List<int>[2] { new List<int>(), new List<int>() };
            DisByIndexes[1].AddRange(new int[] { (int)DISEASES.Anemia, (int)DISEASES.Bleeding, (int)DISEASES.Hematological_Disorder, (int)DISEASES.Iron_Deficiency });
            double[] temp = null;
            if (Pat.Age < 18)
            {
                temp = ranges["Hemoglobin"][2];
            }
            else
            {
                temp = GetRangesForGender(Pat.Gender, ranges["Hemoglobin"]);
            }
            RevaluationByRange(Pat.cHb, temp, DisByIndexes);
        }

        private void DiagnsticCreatinine()
        {
            int[][] ageRanges = { new int[] { 60, 99 }, new int[] { 18, 59 }, new int[] { 3, 17 }, new int[] { 0, 2 } };
            double[] properRanges = GetRangesForAge(Pat.Age, ranges["Creatinine"], ageRanges, ageRanges.GetLength(0) - 1);
            DisByIndexes = new List<int>[2] { new List<int>(), new List<int>() };

            //check if diarrhea 
            if (!Pat.IsDiarrhea)
            {
                DisByIndexes[0].AddRange(new int[] { (int)DISEASES.Muscle, (int)DISEASES.Increased_Consumption_Meat, (int)DISEASES.Kidney });
            }
            DisByIndexes[1].AddRange(new int[] { (int)DISEASES.Muscle, (int)DISEASES.Malnutrition });
            RevaluationByRange(Pat.cCreatinine, properRanges, DisByIndexes);
        }

        private void DiagnsticIron()
        {
            double[] rang = GetRangesForGender(Pat.Gender, ranges["Iron"]);
            DisByIndexes = new List<int>[2] { new List<int>(), new List<int>() };
            DisByIndexes[0].Add((int)DISEASES.Iron_Poisoning);
            
            if (!Pat.Gender)
            {
               
                if (Pat.IsPregnant == false)
                {
                    DisByIndexes[1].Add((int)DISEASES.Pregnancy);
                }
                else
                {
                    return;
                }
              
            }
           // else
            //{
                DisByIndexes[1].AddRange(new int[] {(int)DISEASES.Bleeding, (int)DISEASES.Malnutrition });
            //}
            RevaluationByRange(Pat.cIron, rang, DisByIndexes);
        }

        private void DiagnsticHDL()
        {
            double[] rang = GetRangesForGender(Pat.Gender, ranges["HDL"]);
            if (Pat.Region.ToString() == "Ethiopia")
            {
                rang[0] *= 1.2;
                rang[1] *= 1.2;
            }
            DisByIndexes = new List<int>[2] { new List<int>(), new List<int>() };
            DisByIndexes[1].AddRange(new int[] { (int)DISEASES.Hyperlipidemia, (int)DISEASES.Heart, (int)DISEASES.Adult_Diabetes });
            RevaluationByRange(Pat.cHDL, rang, DisByIndexes);
        }

        private void DiagnsticAP()
        {
            double[] rang = Pat.Region.ToString() == "East" ? ranges["AP"][0] : ranges["AP"][1];
            DisByIndexes = new List<int>[2] { new List<int>(), new List<int>() };
    
            if (Pat.IsPregnant == false)
            {
                 DisByIndexes[0].Add((int)DISEASES.Pregnancy);
            }
          
            DisByIndexes[0].AddRange(new int[] { (int)DISEASES.Liver, (int)DISEASES.Bile_Duct, (int)DISEASES.Overactive_Thyroid_Gland, (int)DISEASES.Use_Various_Medications });
            DisByIndexes[1].AddRange(new int[]{ (int)DISEASES.Vitamin, (int)DISEASES.Malnutrition});
            RevaluationByRange(Pat.cAP, rang, DisByIndexes);
        }

        private void RevaluationByRange(double value, double[] rang, List<int>[] allIndexes)
        {
            if (value <Math.Round(rang[0], 6))
            {
                EncounterIndexes(allIndexes[1]);
            }
            else if (value >Math.Round(rang[1], 6))
            {
                EncounterIndexes(allIndexes[0]);
            }
        }

        private void EncounterIndexes(List<int> indexes)
        {
            for (int i = 0; i < indexes.Count; i++)
            {
                this.IndTreatment[indexes[i]]++;
            }
        }

        private double[] GetRangesForAge(int age, double[][] ranges, int[][] ageRanges, int size)
        {
            if (size == 0 || ageRanges[size][0] <= age && age <= ageRanges[size][1])
            {
                return ranges[size];
            }

            return GetRangesForAge(age, ranges, ageRanges, --size);
        }

        private double[] GetRangesForGender(bool gender, double[][] ranges)
        {
            return gender ? ranges[0] : ranges[1];
        }
    }
}