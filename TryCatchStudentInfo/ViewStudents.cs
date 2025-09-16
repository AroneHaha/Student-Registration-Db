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
    public partial class ViewStudents : Form
    {
        private StudentInformationClass info;
        public ViewStudents(StudentInformationClass studentInfo)
        {
            InitializeComponent();
            info = studentInfo;
        }

        private void frmConfirmation_Load(object sender, EventArgs e)
        {

        }

        private void SubmitBtn_Click(object sender, EventArgs e)
        {

        }
    }
}
