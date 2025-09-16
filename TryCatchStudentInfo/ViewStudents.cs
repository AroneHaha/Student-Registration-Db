using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TryCatchStudentInfo.Repositories;

namespace TryCatchStudentInfo
{
    public partial class ViewStudents : Form
    {

        public ViewStudents()
        {
            InitializeComponent();
            ReadStudents();

            //info = studentInfo;
        }

        public ViewStudents(StudentInformationClass studentInfo)
        {
            InitializeComponent();
            ReadStudents();

            //info = studentInfo;
        }


        private void frmConfirmation_Load(object sender, EventArgs e)
        {

        }

        private void SubmitBtn_Click(object sender, EventArgs e)
        {

        }

        private void ReadStudents()
        {
            DataTable Table = new DataTable();
            Table.Columns.Add("Student ID");
            Table.Columns.Add("First Name");
            Table.Columns.Add("Last Name");
            Table.Columns.Add("Middle Initial");
            Table.Columns.Add("Program");
            Table.Columns.Add("Birth Date");
            Table.Columns.Add("Age");
            Table.Columns.Add("Gender");
            Table.Columns.Add("Address");
            Table.Columns.Add("Contact Number");

            var repo = new StudentRepository();
            var students = repo.GetStudents();


            foreach (var student in students)
            {
                var row = Table.NewRow();
                row["Student ID"] = student.StudentId;
                row["First Name"] = student.FirstName;
                row["Last Name"] = student.LastName;
                row["Middle Initial"] = student.MiddleInitial;
                row["Program"] = student.Program;
                row["Birth Date"] = student.BirthDate.ToString("yyyy-MM-dd");
                row["Age"] = student.Age;
                row["Gender"] = student.Gender;
                row["Address"] = student.Address;
                row["Contact Number"] = student.ContactNum;

                Table.Rows.Add(row);
            }
            this.DataGrid.DataSource = Table;
        }

        private void AddStudentBtn_Click(object sender, EventArgs e)
        {
            ManageStudentForm RegForm = new ManageStudentForm();

            if (RegForm.ShowDialog() == DialogResult.OK)
            {
                ReadStudents();
            }

        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (this.DataGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a student to edit.");
                return;
            }

            string value = this.DataGrid.SelectedRows[0].Cells[0].Value?.ToString();
            if (string.IsNullOrEmpty(value))
            {
                MessageBox.Show("Invalid student ID.");
                return;
            }

            int studentId = int.Parse(value);
            var repo = new StudentRepository();
            var student = repo.GetStudent(studentId);

            if (student == null)
            {
                MessageBox.Show("Student not found.");
                return;
            }

            ManageStudentForm editForm = new ManageStudentForm();
            editForm.EditStudent(student);

            if (editForm.ShowDialog() == DialogResult.OK)
            {
                ReadStudents();
            }
        }

    }
}
