using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProjetoTeatro
{
    public class Form1 : Form
    {
        int[,] teatro = new int[15, 40]
        Label lblResultado;

        public Form1()
        {
            this.Text = "Projeto Teatro";
            this.Width = 1300;
            this.Height = 600;

            CriarMapaPoltronas();
            CriarBotaoFaturamento();
            CriarLabelResultado();
        }

        private void CriarMapaPoltronas()
        {
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 40; j++)
                {
                    Button btn = new Button();
                    btn.Width = 25;
                    btn.Height = 25;
                    btn.Left = j * 30 + 10;
                    btn.Top = i * 30 + 10;
                    btn.BackColor = Color.LightGreen;
                    btn.Tag = new int[] { i, j };
                    btn.Click += Btn_Click;
                    this.Controls.Add(btn);
                }
            }
        }

        private void CriarBotaoFaturamento()
        {
            Button btnFat = new Button();
            btnFat.Text = "Faturamento";
            btnFat.Width = 120;
            btnFat.Height = 40;
            btnFat.Left = 10;
            btnFat.Top = 500;
            btnFat.Click += BtnFaturamento_Click;
            this.Controls.Add(btnFat);
        }

        private void CriarLabelResultado()
        {
            lblResultado = new Label();
            lblResultado.Width = 400;
            lblResultado.Height = 60;
            lblResultado.Left = 150;
            lblResultado.Top = 500;
            lblResultado.Text = "Qtde de lugares ocupados: 0\nValor da bilheteria: R$ 0,00";
            this.Controls.Add(lblResultado);
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int[] pos = (int[])btn.Tag;
            int fileira = pos[0];
            int coluna = pos[1];

            if (teatro[fileira, coluna] == 0)
            {
                var escolha = MessageBox.Show("Deseja reservar como Inteira?", "Reserva",
                    MessageBoxButtons.YesNo);

                teatro[fileira, coluna] = escolha == DialogResult.Yes ? 1 : 2;
                btn.BackColor = escolha == DialogResult.Yes ? Color.Red : Color.Yellow;
            }
            else
            {
                MessageBox.Show("Poltrona já ocupada!");
            }
        }

        private void BtnFaturamento_Click(object sender, EventArgs e)
        {
            int ocupadas = 0;
            decimal total = 0;

            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 40; j++)
                {
                    if (teatro[i, j] != 0)
                    {
                        ocupadas++;
                        total += PrecoPoltrona(i + 1, teatro[i, j]);
                    }
                }
            }

            lblResultado.Text = $"Qtde de lugares ocupados: {ocupadas}\n" +
                                $"Valor da bilheteria: R$ {total:F2}";
        }

        private decimal PrecoPoltrona(int fileira, int tipoReserva)
        {
            decimal valorBase = fileira <= 5 ? 50 :
                                fileira <= 10 ? 30 : 15;

            return tipoReserva == 2 ? valorBase / 2 : valorBase;
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new Form1());
        }
    }
}
