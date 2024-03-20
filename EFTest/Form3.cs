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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (var classicContext = new classicmodelsEntities())
            {
                //select groub by
                var data = from list in classicContext.orderdetails
                           group list by list.orderNumber into g
                           select new { g.FirstOrDefault().orderNumber, priceSum = g.Sum(i => i.priceEach) };

                foreach (var orderNum in data.ToList())
                {
                    listBox1.Items.Add(orderNum.orderNumber + " " + orderNum.priceSum);
                }

                //select join table
                var orderDetail = (from list in classicContext.orderdetails
                                   select list).FirstOrDefault();
                listBox2.Items.Add("Order:" + orderDetail.orders.orderNumber + " / Status:" + orderDetail.orders.status);

                //join table with relationship, Take(2) - pick 2 records in orders table
                var data2 = classicContext.orders.Select(i => i).OrderBy(i => i.orderDate).Take(2);
                foreach (var order in data2.ToList())
                {
                    foreach (var orderdetail in order.orderdetails.ToList())
                    {
                        listBox3.Items.Add($"Orderline num:  { orderdetail.orderLineNumber} Date: {order.orderDate} ");
                    }
                }


            }
        }
    }
}
