using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TryCatchStudentInfo
{
    public partial class OrganizationProfile : Form
    {
        private long _StudentNo;
        private int _Age;
        private long _ContactNo;
        private string _FullName;

        public OrganizationProfile()
        {
            InitializeComponent();
        }


        private void OrganizationProfile_Load(object sender, EventArgs e)
        {
            string[] ListOfProgram = new string[]{
             "BS Information Technology",
             "BS Computer Science",
             "BS Information Systems",
             "BS in Accountancy",
             "BS in Hospitality Management",
             "BS in Tourism Management"
             };

            for (int i = 0; i < 6; i++)
            {
                ProgramCmb.Items.Add(ListOfProgram[i].ToString());
            }
        }

        public long StudentNumber(string studNum)
        {

            _StudentNo = long.Parse(studNum);

            return _StudentNo;
        }

        public long ContactNo(string Contact)
        {
            if (Regex.IsMatch(Contact, @"^[0-9]{10,11}$"))
            {
                _ContactNo = long.Parse(Contact);
            }

            return _ContactNo;
        }

        public string FullName(string LastName, string FirstName, string MiddleInitial)
        {
            if (Regex.IsMatch(LastName, @"^[a-zA-Z]+$") || Regex.IsMatch(FirstName, @"^[a-zA-Z]+$") || Regex.IsMatch(MiddleInitial, @"^[a-zA-Z]+$"))
            {
                _FullName = LastName + ", " + FirstName + ", " + MiddleInitial;
            }

            return _FullName;
        }

        public int Age(string age)
        {
            if (Regex.IsMatch(age, @"^[0-9]{1,3}$"))
            {
                _Age = Int32.Parse(age);
            }

            return _Age;
        }

        private void RegisterBtn_Click(object sender, EventArgs e)
        {
            
            StudentInformationClass studentinfo = new StudentInformationClass();
            frmConfirmation confirmationform = new frmConfirmation(studentinfo);

            studentinfo.SetFullName = FullName(LastNameTxt.Text, FirstNameTxt.Text, MiddleInitialTxt.Text);
            studentinfo.SetStudentNo = (int)StudentNumber(StudentNoTxt.Text);
            studentinfo.SetProgram = ProgramCmb.Text;
            studentinfo.SetGender = GenderCmb.Text;
            studentinfo.SetContactNo = (int)ContactNo(ContactNoTxt.Text);
            studentinfo.SetAge = Age(AgeTxt.Text);
            studentinfo.SetBirthday = BirthDatePicker.Text;

            confirmationform.Show();
        }
    }
}
