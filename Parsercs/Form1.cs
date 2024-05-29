﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Reflection.Emit;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace Parsercs
{
    public partial class Form1 : Form
    {
        
        
        private MySqlConnection connection;
        private string connectionString = "Server=sql7.freesqldatabase.com;Port=3306;Database=sql7710165;Uid=sql7710165;Pwd=kpl4jFAWA6;Charset=utf8;"; 

        Dictionary<string, List<string>> carModels = new Dictionary<string, List<string>>();

        public Form1()
        {
            InitializeComponent();

            carModels.Add("BMW", new List<string> { "X5", "3 Series", "5 Series" });
            carModels.Add("Mercedes", new List<string> { "C-Class", "E-Class", "S-Class" });
            carModels.Add("Audi", new List<string> { "A4", "A6", "Q7" });

            foreach (string carBrand in carModels.Keys)
            {
                comboBox2.Items.Add(carBrand);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Очищаем второй ComboBox
            comboBox3.Items.Clear();

            // Получаем выбранную марку машины из первого ComboBox
            string selectedBrand = comboBox2.SelectedItem.ToString();

            // Если для выбранной марки есть модели, добавляем их во второй ComboBox
            if (carModels.ContainsKey(selectedBrand))
            {
                foreach (string model in carModels[selectedBrand])
                {
                    comboBox3.Items.Add(model);
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "database1DataSet.Auto". При необходимости она может быть перемещена или удалена.
            
            
            connection = new MySqlConnection(connectionString);

            connection.Open();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool isValid = true; // Переменная для отслеживания выполнения всех условий

            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Сначала выберите Город!");
                isValid = false;
            }
      

            if (comboBox2.SelectedItem == null && comboBox3.SelectedItem == null)
            {
                MessageBox.Show("Сначала выберите Марку и модель");
                isValid = false;
            }
           

            if (textBox1.Text == "")
            {
                MessageBox.Show("Введите цену ");
                isValid = false;
            }
 
            if (textBox2.Text == "")
            {
                MessageBox.Show("Введите описание ");
                isValid = false;
            }
           

            if (comboBox4.SelectedItem == null)
            {
                MessageBox.Show("Введите год выпуска ");
                isValid = false;
            }
            

            if (textBox3.Text == "")
            {
                MessageBox.Show("Введите пробег ");
                isValid = false;
            }
            

            if (comboBox5.SelectedItem == null)
            {
                MessageBox.Show("Введите цвет");
                isValid = false;
            }
          

            // Проверяем, все ли условия выполнены
            if (isValid)
            {
                // Записываем значения во все label
                label1.Text = comboBox1.SelectedItem.ToString();
                label4.Text = comboBox2.SelectedItem.ToString() + " " + comboBox3.SelectedItem.ToString();
                label8.Text = textBox1.Text;
                label2.Text = textBox2.Text;
                label11.Text = comboBox4.SelectedItem.ToString();
                label13.Text = textBox3.Text;
                label15.Text = comboBox5.SelectedItem.ToString();
     

                string query = "INSERT INTO Aut (City, Mark, Model, Price, description, YearOfIssue, Mileage, Color) VALUES (@City, @Mark, @Model, @Price, @description, @YearOfIssue, @Mileage, @Color)";
                MySqlCommand command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("City", comboBox1.SelectedItem.ToString());
                command.Parameters.AddWithValue("Mark", comboBox2.SelectedItem.ToString());
                command.Parameters.AddWithValue("Model", comboBox3.SelectedItem.ToString());
                command.Parameters.AddWithValue("Price", textBox1.Text);
                command.Parameters.AddWithValue("description", textBox2.Text);
                command.Parameters.AddWithValue("YearOfIssue", comboBox4.SelectedItem.ToString());
                command.Parameters.AddWithValue("Mileage", textBox3.Text);
                command.Parameters.AddWithValue("Color", comboBox5.SelectedItem.ToString());


                MessageBox.Show(command.ExecuteNonQuery().ToString());
                MessageBox.Show("Данные успешно сохранены!");
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Проверяем, является ли нажатая клавиша цифрой или клавишей Backspace
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                // Если это не цифра и не Backspace, предотвращаем добавление символа
                e.Handled = true;
             
            }
            if (textBox1.Text.Length >= 8 && e.KeyChar != (char)Keys.Back)
            {
                // Если длина текста превышает 8 символов и это не Backspace, предотвращаем добавление символа
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Проверяем, является ли нажатая клавиша цифрой или клавишей Backspace
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                // Если это не цифра и не Backspace, предотвращаем добавление символа
                e.Handled = true;

            }
            if (textBox3.Text.Length >= 6 && e.KeyChar != (char)Keys.Back)
            {
                // Если длина текста превышает 8 символов и это не Backspace, предотвращаем добавление символа
                e.Handled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
             this.Close();

                // Создаем новый экземпляр формы 2
                Form3 form3 = new Form3();

                // Отображаем форму 2
                form3.Show();
        }
    }
}



