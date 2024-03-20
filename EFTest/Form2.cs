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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            using (var classicContext = new classicmodelsEntities())
            {
                //select all employee number
                var allEmpNum = from list in classicContext.employees
                                select list.employeeNumber; //select employeeNumber from employees
                                                            //var emp = classContext.employees.Select(i => i.employeeNumber);


                foreach (var empno in allEmpNum.ToList())
                {
                    listBox1.Items.Add(empno);
                }

                //new codes – filtering by Email and Employee Number
                var filterEmpNum = classicContext.employees.Where(i => i.email.Contains("ab") || i.employeeNumber == 1501).Select(i => i.employeeNumber);

                foreach (var empno in filterEmpNum.ToList())
                {
                    listBox2.Items.Add(empno);
                }

                //select all first name
                var allFirstLastName = from list in classicContext.employees
                                       select new { list.firstName, list.lastName };
                //select firstName as a, LastName as b from employees
                foreach (var empno in allFirstLastName.ToList())
                {
                    listBox3.Items.Add(empno.firstName + " " + empno.lastName);
                }

                //select filter first and last name
                var filterFirstLastName = from list in classicContext.employees
                                       where (list.firstName.Contains("Mary") || list.firstName.Contains("Barry"))
                                       select new { list.firstName, list.lastName };
                foreach (var empno in allFirstLastName.ToList())
                {
                    listBox4.Items.Add(empno.firstName + " " + empno.lastName);
                }


            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
