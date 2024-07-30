using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    { 
        public string nome;
        public string endereco;
        public string rg;
        public string telefone;
        public string automovel;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string strcon = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\aula\clientes.mdb";

/*            string comando = "INSERT INTO clientes (nome,endereco,rg, telefone, automovel )values('" +
nomet.Text + "', '" + enderecot.Text + "','" + rgt.Text + "','" + telefonet.Text + "', '" +
automovelt.Text + "')";
*/
            string comando = "INSERT INTO clientes (nome,endereco,automovel )values('" +
nomet.Text + "', '" + enderecot.Text + "', '" + automovelt.Text + "')";

            OleDbConnection con = new OleDbConnection(strcon);
            con.Open();
            OleDbCommand com = new OleDbCommand(comando, con);
            com.Parameters.Add(@nome, OleDbType.VarChar).Value = nomet.Text;
            com.Parameters.Add(@endereco, OleDbType.VarChar).Value = enderecot.Text;
           /* com.Parameters.Add(@rg, OleDbType.VarChar).Value = rgt.Text;
            com.Parameters.Add(@telefone, OleDbType.VarChar).Value = telefonet.Text; */
            com.Parameters.Add(@automovel, OleDbType.VarChar).Value = automovelt.Text;
            try
            {
                //con.Open();
                com.ExecuteNonQuery(); // aqui não retorna nada se deu certo.
                MessageBox.Show("deu certo...");
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
                string strcon = @"Provider=Microsoft.Jet.OLEDB.4.0;DataSource=c:\aula\clientes.mdb";
                string comando = "SELECT * from clientes where nome=@nome";
                OleDbConnection con = new OleDbConnection(strcon);

                OleDbCommand com = new OleDbCommand(comando, con);
                com.Parameters.Add(@nome, OleDbType.VarChar).Value = pesquisat.Text;
                try
                {
                    if (pesquisat.Text == "")
                    {
                        throw new Exception("digite um nome....");
                    }
                    con.Open();
                    OleDbDataReader cs = com.ExecuteReader();
                    if (cs.HasRows == false)
                    {
                        throw new Exception("nao achei nada...");
                    }
                    else
                    {
                        MessageBox.Show("Registro encontrado");
                        cs.Read();
                        enderecot.Text = Convert.ToString(cs["endereco"]);
                        rgt.Text = Convert.ToString(cs["rg"]);
                        telefonet.Text = Convert.ToString(cs["telefone"]);
                        automovelt.Text = Convert.ToString(cs["automovel"]);
                        nomet.Text = Convert.ToString(cs["nome"]);
                    }
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message);
                }
                finally
                {
                    con.Close();
                }
            }

        private void button4_Click(object sender, EventArgs e)
        {
            string strcon = @"Provider=Microsoft.Jet.OLEDB.4.0;Data
Source=c:\aula\BD_Clientes2.mdb";
            if (MessageBox.Show("deseja excluir?", "Cuidado", MessageBoxButtons.YesNo,
           MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                MessageBox.Show("Cancelado");
            }
            else
            {
                string comando = "delete from clientes where nome=@nome";
                OleDbConnection con = new OleDbConnection(strcon);
                OleDbCommand com = new OleDbCommand(comando, con);
                com.Parameters.Add(@nome, OleDbType.VarChar).Value = pesquisat.Text;
                try
                {
                    con.Open();
                    com.ExecuteNonQuery();

                    MessageBox.Show("apagado");
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message);
                }
                finally
                {
                    con.Close();
                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string strcon = @"Provider=Microsoft.Jet.OLEDB.4.0;Data
Source=c:\aula\BD_Clientes2.mdb";
            string comando = "update Clientes set nome=@nome,endereco=@endereco,rg=@rg,telefone=@telefone, automovel=@automovel where nome = @nome";
 OleDbConnection con = new OleDbConnection(strcon);
            OleDbCommand com = new OleDbCommand(comando, con);
            com.Parameters.Add(@nome, OleDbType.VarChar).Value = nomet.Text;
            com.Parameters.Add(@endereco, OleDbType.VarChar).Value = enderecot.Text;
            com.Parameters.Add(@rg, OleDbType.VarChar).Value = rgt.Text;
            com.Parameters.Add(@telefone, OleDbType.VarChar).Value = telefonet.Text;
            com.Parameters.Add(@automovel, OleDbType.VarChar).Value = automovelt.Text;
            try
            {
                con.Open();
                com.ExecuteNonQuery();
                MessageBox.Show("Dados alterados");
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
            finally
            {
                con.Close();
            }
        }
    }
}
