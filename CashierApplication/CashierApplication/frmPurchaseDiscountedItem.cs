using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ItemNamespace;

namespace CashierApplication
{
    public partial class frmPurchaseDiscountedItem : Form
    {
        public frmPurchaseDiscountedItem()
        {
            
            InitializeComponent();
        }

        private DiscountedItem currentItem;

        private void btnCompute_Click(object sender, EventArgs e)
        {
                
            string name = txtItem.Text;
            double price = Convert.ToDouble(txtPrice.Text);
            int quantity = Convert.ToInt32(txtQuantity.Text);
            double discount = Convert.ToDouble(txtDiscount.Text);


            currentItem = new DiscountedItem(name, price, quantity, discount);
            double totalPrice = currentItem.GetTotalPrice();
            lblTotalAmount.Text = totalPrice.ToString("C");
           

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if(currentItem == null)
            {
                MessageBox.Show("Wala yung item mo baliw");
                return;
            }
            double payment = Convert.ToDouble(txtPaymentReceived.Text);

            currentItem.SetPayment(payment);

            lblChange.Text = currentItem.GetChange().ToString("C");
        }
    }
}

namespace ItemNamespace
{
    public abstract class Item
    {
        protected string item_name;
        protected double item_price;
        protected int item_quantity;
        private double total_price;

        public Item(string name, double price, int quantity)
        {
            item_name = name;
            item_price = price;
            item_quantity = quantity;
        }

        public abstract double GetTotalPrice(); 
        

        public abstract void SetPayment(double payment);
       
    }

    public class DiscountedItem : Item
    {
        private double item_discount;
        private double discounted_price;
        private double payment_amount;
        private double change;

        public DiscountedItem(string name, double price, int quantity, double discount) : base(name, price, quantity)
        {
            item_discount = discount;
           
        }

        public override double GetTotalPrice()
        {
            double total_price = item_price * item_quantity;
            discounted_price = total_price - (total_price * (item_discount * 0.01));
            return discounted_price;
        }

        public override void SetPayment(double payment)
        {
            payment_amount = payment;
            change = payment_amount - discounted_price;
          
        }

        public double GetChange()
        {
            return change;
        }
    }
}