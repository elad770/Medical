using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Xml.XPath;
using PropertyChanged;
namespace LibraryDiagnosis
{
    public enum Region { East, Europe, Ethiopia, Other };

   
    public class Patient: INotifyPropertyChanged
    {


        [DisplayName("Full Name")]
        public string Name { set; get; }

        [DisplayName("ID Number")]
        public string Id { set;get; }

        [DisplayName("White Blood Cells")]
        [Description("\n-White Blood Cells\n  Normal Values:\n  18+: 4500-11000\n  4-17: 5500-15500\n  0-3: 6000-17500")]
        public double cWCB { set; get; }

        [DisplayName("Neutrophil")]
        [Description("\n-Neutrophil\n  Normal Values:\n  28%-54% of all\n  white blood cells")]
        public double cNeut { set; get; }

        [DisplayName("Lymphocytes")]
        [Description("\n-Lymphocytes\n  Normal Values:\n  36%-52% of all\n  white blood cells")]
        public double cLymph { set; get; }

        [DisplayName("Red Blood Cells")]
        [Description("\n-Red Blood Cells\n  Normal Values:\n  4.5-6")]
        public double cRBC { set; get; }

        [DisplayName("Hemoglobin")]
        [Description("\n-Hemoglobin\n  Normal Values:\n  Women: 12-16\n  Men: 12-18\n  Children(0-17): 11.5-\n  15.5")]
        public double cHb { set; get; }

        [DisplayName("Creatinine")]
        [Description("\n-Creatinine\n  Normal Values:\n  0-2: 0.2-0.5\n  3-17: 0.5-1 12-18\n  18-59: 0.6-1\n  60+: 0.6-1.2")]
        public double cCreatinine { set; get; }

        [DisplayName("HCT")]
        [Description("\n-HCT\n  Normal Values:\n  Man: 37%-54%\n  Woman: 33%-47%")]
        public double cHCT { set; get; }

        [DisplayName("Urea")]
        [Description("\n-Urea\n  Normal Values:\n  17-43\n  Easterners: 10%\n  more")]
        public double cUrea {  set; get; }


        [DisplayName("Iron")]
        [Description("\n-Iron\n Normal Values:\n  60-160\n  Woman: 20% less")]
        public double cIron { set; get; }

        [DisplayName("HDL")]
        [Description("\n-High Density\n Lipoprotein\n Normal Values:\n Man: 29-62\n Woman: 34-82\n Ethiopian: 20% more")]
        public double cHDL { set; get; }

        [DisplayName("AP")]
        [Description("\n-Alkaline Phosphatase\n Normal Values:\n Easterners: 60-120\n Rest of the others:\n 30-90")]
        public double cAP { set; get; }

        public int Age { get; set; }
        public bool Gender { get; set; }
        public Region Region { get; set; }
        public bool IsSmoking { get; set; }
        public bool IsDiarrhea { get; set; }
        public bool IsHeat { get; set; }
        public bool? IsPregnant { get; set; }
        public string DateTest { get; }

        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };


        public Patient() { DateTest = DateTime.Now.ToString(); }


        private Patient(object[] args)
        {
            PropertyInfo[] prop = GetType().GetProperties();
            for (int i = 0; i < prop.Length; i++)
            {
                prop[i].SetValue(this,args[i]);
            }
          
        }

        public Patient(string id, string name, int age, bool gender, Region region, bool isSmoking,
            bool Isdiarrhea, bool? isPregnant, bool isHeat, double wcb, double neut, double lymph, double rbc,
            double hct, double urea, double hb, double creatinine, double iron, double hdl, double ap): 
            this(new object[] {name,id,wcb,neut,lymph,rbc,hb, creatinine,hct, urea,iron,hdl,ap,age,gender,region,isSmoking,Isdiarrhea,isPregnant,isHeat, DateTime.Now.ToString() })
        {

        }

        public void SaveToXML(List<string[]> dic)
        {
            XDocument doc = null;
            string path = "PatientDetails.xml";
            XElement addStep = null;
            if (File.Exists(path))
            {
                doc = XDocument.Load(path);

                addStep = doc.XPathSelectElements("Patients/Patient")
                 .Where(e => (string)e.Attribute("Id") == this.Id).FirstOrDefault();
                bool temp = addStep == null;
                CreateXML(dic, ref addStep);
                if (temp)
                    doc.Root.Add(addStep);
            }
            else
            {
                CreateXML(dic, ref addStep);
                doc = new XDocument(new XElement("Patients", addStep));
            }

            doc.Save("PatientDetails.xml");

        }
        private void CreateXML(List<string[]> dic, ref XElement xe)
        {

            string gen = "זכר";
            XElement temp = new XElement("Man", null);
            if (!this.Gender)
            {
                gen = "נקבה";
                temp = new XElement("IsPregnant", this.IsPregnant);
            }

            if (xe == null)
            {
                xe = new XElement("Patient",
                new XAttribute("Region", this.Region),
                new XAttribute("Gender", gen),
                new XAttribute("Age", this.Age),
                new XAttribute("Id", this.Id),
                new XAttribute("Name", this.Name));
            }

            if (dic == null)
            {
                dic = new List<string[]>();
            }
            XElement xeTest = new XElement("Tests");
            PropertyInfo[] prop = this.GetType().GetProperties().Where(x => x.Name != "Name" && x.Name != "Id").ToArray();

            foreach (PropertyInfo pi in prop)
            {
                DisplayNameAttribute dp = pi.GetCustomAttributes(typeof(DisplayNameAttribute), false).Cast<DisplayNameAttribute>().SingleOrDefault();
                if (dp == null)
                {
                    break;
                }
                xeTest.Add(new XElement(dp.DisplayName.Replace(" ", "_"), pi.GetValue(this)));
            }

            xe.Add(new XElement("ResultVisit" + (xe.Nodes().Count()), new XAttribute("Date", this.DateTest), new XElement("Questions", new XElement("IsSmoking", this.IsSmoking),
            new XElement("IsHeat", this.IsHeat), new XElement("IsDiarrhea", this.IsDiarrhea), temp), xeTest,
            //All diagnoses
            new XElement("Diagnostic", new XElement("Diseases", from it in dic
                                                                select new XElement("Disease",
                                                                 new XAttribute("NameDisease", it[0]), new XAttribute("Treatment", it[1]))))));
        }



    }
    
}
