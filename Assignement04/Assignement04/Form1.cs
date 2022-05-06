using System;
using System.Drawing;
using System.Windows.Forms;
using RestSharp;
using System.Net;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading;


namespace Assignement04
{
    public partial class Form1 : Form
    {

        private static Datum[] employees;
        private static int counter = 0;
        private static int empTotal;

        public Form1()
        {
            InitializeComponent();
            GetEmployeeData();
        }

        private static void GetEmployeeData()
        {
    
            try
            {
                var client = new RestClient("http://dummy.restapiexample.com/api/v1/");
                var request = new RestRequest("employees");
                var response = client.Execute(request);

                if(response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string rawResponse = response.Content;
                    Rootobject result = JsonConvert.DeserializeObject<Rootobject>(rawResponse);
                    employees = result.data;
                    empTotal = employees.Length;

                }

            }
            catch (Exception)
            {
                MessageBox.Show("Fail to retireve JSON data", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void InitializeInputBoxes(Datum datum)
        {
            
            IdTextBox.Text = (datum.id).ToString();
            nameTextBox.Text = (datum.employee_name).ToString();
            salaryTextBox.Text = (datum.employee_salary).ToString("C");
            ageTextBox.Text = (datum.employee_age).ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Datum currentEmp = employees[0];
            InitializeInputBoxes(currentEmp);
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            if (counter >= empTotal - 1)
            {
                counter = -1;
            }
            try
            {
                Datum currentEmp = employees[++counter];
                InitializeInputBoxes(currentEmp);
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid Record.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void previousButton_Click(object sender, EventArgs e)
        {
            if (counter <= 0)
            {
                counter = empTotal;
            }
            try
            {
                Datum currentEmp = employees[--counter];
                InitializeInputBoxes(currentEmp);
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid Record.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }


    // Classes provided by Postman.
    public class Rootobject
    {
        public string status { get; set; }
        public Datum[] data { get; set; }
        public string message { get; set; }
    }

    public class Datum
    {
        public int id { get; set; }
        public string employee_name { get; set; }
        public int employee_salary { get; set; }
        public int employee_age { get; set; }

    }

}
