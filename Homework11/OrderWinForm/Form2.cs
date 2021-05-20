using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrderWinForm
{
    public partial class Form2 : Form
    {
        public OrderService os;
        public Form2()
        {
            InitializeComponent();
        }
        public Form2(OrderService os)
        {
            InitializeComponent();
            this.os = os;
        }

        private void Form2_Load(object sender, EventArgs e){}

        private void label1_Click(object sender, EventArgs e){}

        private void label2_Click(object sender, EventArgs e){}

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string id = textBox1.Text;
            Customer c1 = new Customer(textBox2.Text, textBox3.Text, textBox4.Text);
            Goods g1 = new Goods(textBox6.Text, double.Parse(textBox5.Text));
            OrderDetail ode1 = new OrderDetail(01, g1, int.Parse(textBox8.Text));
            List<OrderDetail> l1 = new List<OrderDetail>();
            l1.Add(ode1);
            Order o1 = new Order(id ,c1, l1);
            try {
                os.Add(o1);
            }
            catch (ApplicationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Form1.orderService = this.os;
            this.Close();
        }

        private void buttonChange_Click(object sender, EventArgs e)
        {
            string id = textBox2.Text;
            Customer c1 = new Customer(textBox2.Text, textBox3.Text, textBox4.Text);
            Goods g1 = new Goods(textBox6.Text, double.Parse(textBox5.Text));
            OrderDetail ode1 = new OrderDetail(01, g1, int.Parse(textBox8.Text));
            List<OrderDetail> l1 = new List<OrderDetail>();
            l1.Add(ode1);
            Order o1 = new Order(id, c1, l1);
            try
            {
                os.Change(textBox9.Text,o1);
            }
            catch (ApplicationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Form1.orderService = this.os;
            this.Close();
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
