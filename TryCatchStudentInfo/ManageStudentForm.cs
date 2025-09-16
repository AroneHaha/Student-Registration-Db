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
using TryCatchStudentInfo.Models;
using TryCatchStudentInfo.Repositories;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TryCatchStudentInfo
{
    public partial class ManageStudentForm : Form
    {
        private long _StudentNo;
        private int _Age;
        private long _ContactNo;
        private string _FullName;

        public ManageStudentForm()
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

        public void SetMode(string mode)
        {
            this.Text = "Edit Information"; // changes form title
            this.label1.Text = "Edit Student Information"; // change your label text (replace MainLabel with actual label name)
            this.label2.Text = "Edit Student's Profile for IT Organization";
        }


        private int StudentIdHolder = 0; 

        public void EditStudent(Students student)
        {
            this.StudentIdHolder = student.StudentId; 
            this.StudentNoTxt.Text = student.StudentId.ToString();
            this.FirstNameTxt.Text = student.FirstName;
            this.LastNameTxt.Text = student.LastName;
            this.MiddleInitialTxt.Text = student.MiddleInitial;
            this.ProgramCmb.Text = student.Program;
            this.BirthDatePicker.Value = student.BirthDate;
            this.AgeTxt.Text = student.Age.ToString();
            this.GenderCmb.Text = student.Gender;
            this.AddressTxt.Text = student.Address;
            this.ContactNoTxt.Text = student.ContactNum;
        }

        private void RegisterBtn_Click(object sender, EventArgs e)
        {
            try
            {
                Students student = new Students();
                student.StudentId = int.Parse(StudentNoTxt.Text); 
                student.FirstName = FirstNameTxt.Text;
                student.LastName = LastNameTxt.Text;
                student.MiddleInitial = MiddleInitialTxt.Text;
                student.Program = ProgramCmb.Text;
                student.BirthDate = BirthDatePicker.Value;
                student.Age = Age(AgeTxt.Text);
                student.Gender = GenderCmb.Text;
                student.Address = AddressTxt.Text;
                student.ContactNum = ContactNo(ContactNoTxt.Text).ToString();

                var repo = new StudentRepository();

                if (StudentIdHolder == 0)
                {
                    repo.CreateStudent(student);
                }
                else
                {
                    repo.UpdateStudents(StudentIdHolder, student);
                }

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred during registration: " + ex.Message);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
