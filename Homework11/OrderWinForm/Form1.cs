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
    public partial class Form1 : Form
    {
        public String searchText { get; set; }
        public static OrderService orderService;
        BindingSource bdsFields = new BindingSource();
        public Action<Form2> ShowEditForm { get; set; }
        public String Keyword { get; set; }
        public Form1()
        {
            InitializeComponent();
            orderService = new OrderService();
            ordersBindingSource.DataSource = orderService.Orders;
            //txtKeyword.DataBindings.Add("Text", this, "Keyword");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            String[] s = { "刷新订单", "订单号", "货物", "客户", "价格" };
            comboBox1.DataSource = s;
            textBox1.DataBindings.Add("Text", this, "searchText");

            /*Customer c1 = new Customer("小红");
            Customer c2 = new Customer("小蓝");
            Customer c3 = new Customer("小绿");

            Goods g1 = new Goods("苹果", 3.5);
            Goods g2 = new Goods("梨", 5.5);
            Goods g3 = new Goods("香蕉", 4.5);

            OrderDetail ode1 = new OrderDetail(01, g1, 9);
            OrderDetail ode2 = new OrderDetail(02, g2, 5);
            OrderDetail ode3 = new OrderDetail(03, g3, 7);
            OrderDetail ode4 = new OrderDetail(04, g2, 4);
            OrderDetail ode5 = new OrderDetail(05, g1, 6);

            List<OrderDetail> l1 = new List<OrderDetail>();
            l1.Add(ode1); l1.Add(ode3);
            List<OrderDetail> l2 = new List<OrderDetail>();
            l2.Add(ode2); l2.Add(ode4);
            List<OrderDetail> l3 = new List<OrderDetail>();
            l3.Add(ode1); l3.Add(ode2); l3.Add(ode5);

            Order o1 = new Order(02, c1, l1);
            Order o2 = new Order(01, c2, l2);
            Order o3 = new Order(03, c3, l3);

            List<Order> l4 = new List<Order>();
            l4.Add(o1); l4.Add(o2); l4.Add(o3);
            os = new OrderService(l4);
            /*using (var db = new Model1())
            {
                db.OrderServices ;
                db.SaveChanges();
            }*/
            
            orderServiceBindingSource.Add(orderService);
        }
        public void CloseChildForm()
        {
            foreach (Form form in this.MdiChildren)
            {
                form.Dispose();
            }
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            CloseChildForm();
            new Form2(orderService).ShowDialog();
        }
        private void buttonChange_Click(object sender, EventArgs e)
        {
            CloseChildForm();
            new Form2(orderService).ShowDialog();
        }
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                string ID = (string)dataGridView1.CurrentRow.Cells[0].Value;
                orderService.Delete(ID);
                ordersBindingSource.ResetBindings(false);
            }
        }
        private void buttonExport_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFile = new SaveFileDialog())
            {
                saveFile.Filter = "|*.xml";
                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    orderService.Export(saveFile.FileName);
                    MessageBox.Show("导出成功！");
                }
            }
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFile = new OpenFileDialog())
            {
                openFile.Filter = "|*.xml";
                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    orderService.Import(openFile.FileName);
                    MessageBox.Show("导入成功！");
                    ordersBindingSource.DataSource = orderService.Orders;
                }
            }

        }
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            String s1 = textBox1.Text;
            String s2 = comboBox1.Text;
            switch (s2)
            {
                case "刷新订单":
                    ordersBindingSource.DataSource = orderService.Orders;
                    break;
                case "订单号":
                    ordersBindingSource.DataSource = orderService.SearchByID(s1);
                    break;
                case "货物":
                    ordersBindingSource.DataSource = orderService.SerachByGoods(s1);
                    break;
                case "客户":
                    ordersBindingSource.DataSource = orderService.SerachByCustomer(s1);
                    break;
                case "价格":
                    ordersBindingSource.DataSource = orderService.SearchByPrice(Convert.ToDouble(s1));
                    break;
                default:
                    break;
            }
            ordersBindingSource.ResetBindings(false);
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint_2(object sender, PaintEventArgs e)
        {

        }

        private void ordersBindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }
    }
}
