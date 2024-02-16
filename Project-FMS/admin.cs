using MaterialSkin;
using MaterialSkin.Controls;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Project_LMS
{
    public partial class admin : MaterialForm
    {
        private string connectionString = "server=localhost;database=library-management-system;uid=root;password=Same2u;";
        private MySqlConnection connection;
        public admin()
        {
            InitializeComponent();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Blue900, Primary.Blue600,
            Primary.LightBlue100, Accent.LightBlue200, TextShade.WHITE);


            connection = new MySqlConnection(connectionString);
            connection.Open();

            string query = "SELECT * FROM roles";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader reader = cmd.ExecuteReader();

           
            reader.Close();

            query = "SELECT * FROM cities";
            cmd = new MySqlCommand(query, connection);
            reader = cmd.ExecuteReader();

           
            reader.Close();

            this.LoadBooks();
            this.LoadUser();
        }

        /*======================= Load Books Start =======================*/
        public void LoadBooks(String name = "")
        {
            try
            {
                materialListView1.Items.Clear();
                string connectionString = "server=localhost;database=library-management-system; uid = root; password = Same2u; ";
                connection = new MySqlConnection();
                connection.ConnectionString = connectionString;
                connection.Open();

                String query = "SELECT books.id, isAvailable, titles.title AS title, authors.name AS author\r\nFROM books \r\nINNER JOIN authors ON books.authors_id = authors.id\r\nINNER JOIN titles ON books.titles_id = titles.id WHERE title LIKE '%" + name + "%' AND isDeleted = '0' ORDER BY title ASC";

                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem(reader["id"].ToString());
                    item.SubItems.Add(reader["title"].ToString());
                    item.SubItems.Add(reader["author"].ToString());
                    item.SubItems.Add(reader["isAvailable"].ToString());
                    materialListView1.Items.Add(item);
                }

                reader.Close();
                connection.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error" + ex.Message);
            }
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            this.LoadBooks(materialTextBox1.Text);
        }

        private void materialButton6_Click(object sender, EventArgs e)
        {
            LoadBooks();
        }

        /*======================= Load Books End =======================*/

        /*======================= Load User Start =======================*/

        public void LoadUser(string name = "")
        {
            try
            {
                materialListView2.Items.Clear();
                string connectionString = "server=localhost;database=library-management-system; uid = root; password = Same2u; ";

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT users.*, genders.`type` AS gender, cities.name AS city, roles.type AS role FROM users " +
                    "INNER JOIN cities ON cities.id = users.city_id " +
                    "INNER JOIN genders ON genders.id = users.gender_id " +
                    "INNER JOIN roles ON roles.id = users.role_id " +
                    "WHERE users.name LIKE '%" + name + "%'";

                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ListViewItem item = new ListViewItem(reader["id"].ToString());
                            item.SubItems.Add(reader["name"].ToString());
                            item.SubItems.Add(reader["role"].ToString());
                            item.SubItems.Add(reader["gender"].ToString());
                            item.SubItems.Add(reader["city"].ToString());
                            materialListView2.Items.Add(item);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
       

        private void materialButton5_Click(object sender, EventArgs e)
        {
            LoadUser();
        }

        /*======================= Load User End =======================*/

        /*======================= Add Books Start =======================*/
        private void materialButton8_Click(object sender, EventArgs e)
        {
            
        }


        /*======================= Add Books End =======================*/

        private void materialButton9_Click(object sender, EventArgs e)
        {
        }


            private void materialTextBox6_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void materialButton2_Click(object sender, EventArgs e)
        {
            managestaff delete = new managestaff();
            delete.Show();
        }

        private void materialButton5_Click_1(object sender, EventArgs e)
        {
            userremove userremove = new userremove();
            userremove.Show();
        }

        private void materialButton3_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
