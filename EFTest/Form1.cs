using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EFTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using (var classicContext = new classicmodelsEntities())
            {
                //select all
                var emp = (from list in classicContext.employees
                           select list);    // select * from employees

                foreach (var emp2 in emp.ToList())
                {
                    dgvLab1.Rows.Add(emp2.employeeNumber, emp2.lastName, emp2.firstName, emp2.extension, emp2.email, emp2.officeCode, emp2.jobTitle);
                }
                //select filter
                var empQuery = from list in classicContext.employees
                                where list.lastName == "Bow"    
                                select list;

                var empName = empQuery.FirstOrDefault<employees>();
                txtLab1.Text = empName.firstName;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            using (var classicContext = new classicmodelsEntities())
            { //keyword search
                dgvLab1.Rows.Clear();
                string keyword = textBox1.Text;
                var resultSet = from list in classicContext.employees
                                where list.lastName.Contains(keyword)
                                select list;
                foreach (var emp2 in resultSet.ToList())
                {
                    dgvLab1.Rows.Add(emp2.employeeNumber, emp2.lastName, emp2.firstName, emp2.extension, emp2.email, emp2.officeCode, emp2.jobTitle);
                }
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            using (var classicContext = new classicmodelsEntities())
            {
                var result = classicContext.employees.SingleOrDefault(emp => emp.lastName == "Bow");
                if (result != null)
                {
                    result.firstName = txtLab1.Text;
                    classicContext.SaveChanges();
                }
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            using (var classicContext = new classicmodelsEntities())
            {
                employees emp = new employees();

                emp.employeeNumber = 5555;
                emp.lastName = "testLastName";
                emp.firstName = "testFirstName";
                emp.jobTitle = "TestJobTitle";
                emp.extension = "10Char";
                emp.email = "testEmail";
                emp.officeCode = "2";

                classicContext.employees.Add(emp);
                classicContext.SaveChanges();
            }
        }
    }
}
