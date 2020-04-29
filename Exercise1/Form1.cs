using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exercise1
{
    public partial class Form1 : Form
    {
        public delegate bool CheckOddOrEven(int number);
        StringBuilder builder;
        int[] intArr;
        char[] charArr;
        double[] doubArr;
        public Form1()
        {
            InitializeComponent();
        }

        private async void btnCalc_Click(object sender, EventArgs e)
        {
            try
            {
                int number = Convert.ToInt32(txtFactorial.Text);
                Task<double> task1 = Task.Run(() => GetFactorial(number));
                lblfactorial.Text = "Calculating...";
                await task1;
                lblfactorial.Text = task1.Result.ToString();

            }

            catch (Exception)
            {
                MessageBox.Show("Please provide number only");
            }

        }

        private double GetFactorial(int number)

        {


            System.Threading.Thread.Sleep(4000);

            if (number == 1)
                return 1;
            else
                return number * GetFactorial(number - 1);

        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            CheckOddOrEven evenPredicate = IsEven;
            CheckOddOrEven oddPredicate = IsOdd;
            try
            {
                int number = Convert.ToInt32(txtNumber.Text);
                if (number == 0)
                    throw new Exception();

                if (evenPredicate(number))
                {
                    lblNum.Text = "The number is even";
                }

                else if (oddPredicate(number))
                {
                    lblNum.Text = "The number is odd";
                }


            }
            catch (Exception)
            {

                MessageBox.Show("Please provide number bigger than zero only");
            }

        }

        public void RandomString()
        {
            builder = new StringBuilder();
            Random random = new Random();

            
    
            
            int i = 0;
            if (rbDouble.Checked)
            {
                doubArr = new double[10];
                double c;
                while (i < 10)
                {
                    c = random.NextDouble() * (99 - 10) + 10 ;
                    if (!doubArr.Any(a => a == c))
                    {
                        doubArr[i] = Math.Round(c,1);
                      builder.Append($"{ doubArr[i++]}\r\n");

                    }
                    
                }
            }

            if (rbChar.Checked )
            {
               charArr = new char[10];
                char c;
              
               while (i < 10)
                {
                    c = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                    if (!charArr.Any(a => a == c))
                    {
                        charArr[i] = c;
                  
                        builder.Append($"{charArr[i++]}\r\n");
                    }
                }
            }

            if (rbInteger.Checked)
            {
                 intArr = new int[10];
                int c;
                while (i < 10)
                {
                    c = random.Next(10,99);


                    if (!intArr.Any(a => a == c))
                    {
                        intArr[i] = c;
                        builder.Append($"{intArr[i++]}\r\n");

                    }
                }
            }
            txtValues.Text = builder.ToString();
     
        }


        private static bool IsEven(int number) => number % 2 == 0;
        private static bool IsOdd(int number) => number % 2 == 1;

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            RandomString();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            bool res=false;
            try
            {

                if (rbDouble.Checked)
                    res = Search(doubArr,double.Parse (txtSearch.Text));
                if (rbChar.Checked)
                    res = Search(charArr, char.Parse(txtSearch.Text.ToUpper()));
                if (rbInteger.Checked)
                    res = Search(intArr, int.Parse(txtSearch.Text));
                if (res)
                    MessageBox.Show($"\"{txtSearch.Text}\" is found");
            }
            catch (Exception )
            {

                MessageBox.Show("Input a valid Search value");
            }
               
        }

        private  bool Search<T>(T[] arr , T searchKey) where T : IComparable<T> 
        {
            bool result = false;
            try
            {
                if ( searchKey == null)
                    throw new Exception("Input Search value");


                for (int i = 0; i < arr.Length; i++)
                {

                    if (arr[i].CompareTo(searchKey)==0)
                    {
                        result = true;
                        break;
                    }

                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                
            }
            return result;
        }


        private void Display<T>(T[] arr)
        { 
            
            for (int i = int.Parse(txtLowIndex.Text); i <= int.Parse(txtHighIndex.Text); i++)
            {
                txtOut.Text += $"{arr[i]}\t";
            }
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            try
            {
                int low = int.Parse(txtLowIndex.Text);
                int high = int.Parse(txtHighIndex.Text);
                if (low > high)
                    throw new Exception("Low index should not be  higher than high index ");


                txtOut.Text = "";
                if (rbDouble.Checked)
                    Display(doubArr);
                if (rbChar.Checked)
                    Display(charArr);
                if (rbInteger.Checked)
                    Display(intArr);
            }
            catch (Exception ee)
            {

                MessageBox.Show(ee.Message);
            }
           
        }
    }
}
   
