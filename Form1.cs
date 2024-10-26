using System;
// using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;


namespace winFormApp
{
    public partial class Form1 : Form
    {
        public Button button1;
        public Button button2;
        public Button button3;


        public TextBox textBox1;
        public TextBox textBox2;
        public TextBox textBox3;
        public TextBox textBox4;
        public TextBox textBox5;

        public Label label1;
        public Label label2;
        public Label label3;
        public Label label4;
        public Label label5;

        SqlConnection connection = new SqlConnection("Data source=DESKTOP-FJ1PLK6; Initial Catalog=registration; Integrated Security=True");
        SqlCommand? cmd = null; 

        private DataGridView dataGridView;
        public Form1()
        {
            InitializeComponent();
            dataGridView = new DataGridView();
            dataGridView.Size = new Size(400, 250);
            dataGridView.Location = new Point(350, 30);
            dataGridView.CellClick += DataGridView_CellClick;

            this.Controls.Add(dataGridView);
            LoadData(); 

            //  labels
            label1 = new Label { Size = new Size(40, 20), Location = new Point(50, 30), Text = "Id" };
            this.Controls.Add(label1);

            label2 = new Label { Size = new Size(50, 20), Location = new Point(50, 60), Text = "Name" };
            this.Controls.Add(label2);

            label3 = new Label { Size = new Size(40, 20), Location = new Point(50, 90), Text = "Age" };
            this.Controls.Add(label3);

            label4 = new Label { Size = new Size(50, 20), Location = new Point(50, 120), Text = "Address" };
            this.Controls.Add(label4);

            label5 = new Label { Size = new Size(50, 20), Location = new Point(50, 150), Text = "Faculty" };
            this.Controls.Add(label5);

            //  text boxes
            textBox1 = CreateNumericTextBox(100, 30);
            this.Controls.Add(textBox1);

            textBox2 = new TextBox { Size = new Size(200, 20), Location = new Point(100, 60) };
            this.Controls.Add(textBox2);

            textBox3 = CreateNumericTextBox(100, 90);;
            this.Controls.Add(textBox3);

            textBox4 = new TextBox { Size = new Size(200, 20), Location = new Point(100, 120) };
            this.Controls.Add(textBox4);

            textBox5 = new TextBox { Size = new Size(200, 20), Location = new Point(100, 150) };
            this.Controls.Add(textBox5);

            //  buttons
            button1 = new Button { Size = new Size(50, 25), Location = new Point(60, 220), Text = "Create" };
            button1.Click += Button1_Click;
            this.Controls.Add(button1);

            button2 = new Button { Size = new Size(60, 25), Location = new Point(140, 220), Text = "Update" };
              button2.Click += Button2_Click;
            this.Controls.Add(button2);


             button3 = new Button { Size = new Size(60, 25), Location = new Point(220, 220), Text = "Delete" };
              button3.Click += Button3_Click;
            this.Controls.Add(button3);
        }

       
           private void LoadData()
        {
            try
            {
                connection.Open();
                cmd = new SqlCommand("SELECT * FROM student", connection);
                DataTable dataTable = new DataTable();
                SqlDataAdapter  adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dataTable);
                dataGridView.DataSource = dataTable;
                       }

            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        private void DataGridView_CellClick(object? sender, DataGridViewCellEventArgs e)
{
    if (e.RowIndex >= 0)
    {
        DataGridViewRow row = dataGridView.Rows[e.RowIndex];

        string id = row.Cells[0].Value?.ToString(); 
        string name = row.Cells[1].Value?.ToString(); 
        string age = row.Cells[2].Value?.ToString(); 
        string address = row.Cells[3].Value?.ToString(); 
        string faculty = row.Cells[4].Value?.ToString(); 

        textBox1.Text = id;
        textBox1.ReadOnly = true;
        textBox2.Text = name;
        textBox3.Text = age;
        textBox4.Text = address;
        textBox5.Text = faculty;

    }
}


        private TextBox CreateNumericTextBox(int x, int y)
        {
            TextBox textBox = new TextBox { Size = new Size(200, 20), Location = new Point(x, y) };
            textBox.KeyPress += NumericTextBox_KeyPress;
            return textBox;
        }

        private void NumericTextBox_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; 
            }
        }

        private void Button1_Click(object? sender, EventArgs e)
        {
            try
            {
                connection.Open();
                cmd = new SqlCommand("INSERT INTO student (name, address, age, faculty) VALUES (@name, @address, @age, @faculty)", connection);
                
                cmd.Parameters.AddWithValue("@name", textBox2.Text);
                cmd.Parameters.AddWithValue("@age", textBox3.Text); 
                cmd.Parameters.AddWithValue("@address", textBox4.Text);
                cmd.Parameters.AddWithValue("@faculty", textBox5.Text);
                cmd.ExecuteNonQuery();

                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                

                MessageBox.Show("Registration Complete");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
            finally
            {
                connection.Close(); 
                LoadData();

            }
        }
          private void Button2_Click(object? sender, EventArgs e)
        {
            try{
                    connection.Open();
                cmd = new SqlCommand("UPDATE student SET name = @name, address = @address, age = @age, faculty = @faculty WHERE id = @id", connection);
                cmd.Parameters.AddWithValue("@id", int.Parse(textBox1.Text));
                cmd.Parameters.AddWithValue("@name", textBox2.Text);
                cmd.Parameters.AddWithValue("@age", textBox3.Text); 
                cmd.Parameters.AddWithValue("@address", textBox4.Text);
                cmd.Parameters.AddWithValue("@faculty", textBox5.Text);
                cmd.ExecuteNonQuery();

                
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                 textBox1.ReadOnly = false;

                MessageBox.Show("Update Successfull");

              
            }
                  catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
            finally
            {
                connection.Close(); 
                LoadData();

            }
            
      

        }

        private void Button3_Click(object? sender, EventArgs e)
        {
            try{
                    connection.Open();
                cmd = new SqlCommand("delete from student WHERE id = @id", connection);
                cmd.Parameters.AddWithValue("@id", int.Parse(textBox1.Text));
                cmd.ExecuteNonQuery();

                
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                 textBox1.ReadOnly = false;

                MessageBox.Show("Delete Successfull");

              
            }
                  catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
            finally
            {
                connection.Close(); 
                LoadData();

            }
            
      

        }
    }
}
