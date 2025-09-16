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
    public partial class RegistrationForm : Form
    {
        private long _StudentNo;
        private int _Age;
        private long _ContactNo;
        private string _FullName;

        public RegistrationForm()
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

            string[] Genders = new string[]{
                "Male", "Female", "Prefer not to say"
            };

            for (int i = 0; i < 2; i++)
            {
                GenderCmb.Items.Add(Genders[i].ToString());
            }
        }

        public long StudentNumber(string studNum)
        {
            try
            {
                _StudentNo = long.Parse(studNum);
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid format for Student Number. Please enter numeric values only.");
            }
            catch (OverflowException)
            {
                MessageBox.Show("Student Number is too large.");
            }

            return _StudentNo;
        }

        public long ContactNo(string Contact)
        {
            try
            {
                if (Regex.IsMatch(Contact, @"^[0-9]{10,11}$"))
                {
                    _ContactNo = long.Parse(Contact);
                }
                else
                {
                    MessageBox.Show("Contact number must be 10 digits.");
                    //when input is 11 digits, it becomes negative (bug?)
                    //only 10 digits work
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid format for Contact Number. Please enter numeric values only.");
            }
            catch (OverflowException)
            {
                MessageBox.Show("Large value for Contact Number is Invalid.");
            }

            return _ContactNo;
        }

        public string FullName(string lastName, string firstName, string middleInitial)
        {
            try
            {
                if (Regex.IsMatch(lastName, @"^[a-zA-Z]+$") &&
                    Regex.IsMatch(firstName, @"^[a-zA-Z]+$") &&
                    Regex.IsMatch(middleInitial, @"^[a-zA-Z]+$"))
                {
                    return $"{lastName}, {firstName}, {middleInitial}";
                }
                else
                {
                    return "Invalid input.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while processing: " + ex.Message);
                return "Error";
            }
        }


        public int Age(string age)
        {
            try
            {
                if (Regex.IsMatch(age, @"^[0-9]{1,3}$"))
                {
                    _Age = Int32.Parse(age);
                }

            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid format for Age. Please enter numeric values only.");
            }
            catch (OverflowException)
            {
                MessageBox.Show("Age is too large, Invalid.");
            }

            return _Age;
        }

        private void RegisterBtn_Click(object sender, EventArgs e)
        {
            try
            {
                StudentInformationClass studentinfo = new StudentInformationClass();
                ViewStudents confirmationform = new ViewStudents(studentinfo);

                studentinfo.SetFullName = FullName(LastNameTxt.Text, FirstNameTxt.Text, MiddleInitialTxt.Text);
                studentinfo.SetStudentNo = (int)StudentNumber(StudentNoTxt.Text);
                studentinfo.SetProgram = ProgramCmb.Text;
                studentinfo.SetGender = GenderCmb.Text;
                studentinfo.SetContactNo = (int)ContactNo(ContactNoTxt.Text);
                studentinfo.SetAge = Age(AgeTxt.Text);
                studentinfo.SetBirthday = BirthDatePicker.Text;
                studentinfo.SetAddress = AddressTxt.Text;

                confirmationform.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred during registration: " + ex.Message);
            }
        }
    }
}
