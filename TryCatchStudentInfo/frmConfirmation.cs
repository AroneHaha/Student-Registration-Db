using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TryCatchStudentInfo
{
    public partial class frmConfirmation : Form
    {
        private StudentInformationClass info;
        public frmConfirmation(StudentInformationClass studentInfo)
        {
            InitializeComponent();
            info = studentInfo;
        }

        private void frmConfirmation_Load(object sender, EventArgs e)
        {
            StudentNoLbl.Text = info.SetStudentNo.ToString();
            ProgramLbl.Text = info.SetProgram;
            FullNameLbl.Text = info.SetFullName;
            ContactNoLbl.Text = info.SetContactNo.ToString();
            GenderLbl.Text = info.SetGender;
            AddressLbl.Text = info.SetAddress;
            BirthDateLbl.Text = info.SetBirthday;
            AgeLbl.Text = info.SetAge.ToString();



        }

        private void SubmitBtn_Click(object sender, EventArgs e)
        {

        }
    }
}
