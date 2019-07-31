using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using app01_consultarCEP.Servico.Modelo;
using app01_consultarCEP.Servico;


namespace app01_consultarCEP
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private bool valida;

        public MainPage()
        {
            InitializeComponent();

            Botao.Clicked += BuscarCEP;
        }

        private void BuscarCEP(object sender, EventArgs args)
        {
            string cep = CEP.Text.Trim();

            if (validaCep(cep))
            {
                try
                {
                    endereco end = viaCepServico.BuscarEnderecoViaCep(cep);

                    if (end == null)
                    {
                        DisplayAlert("Aviso", "Endereço não encontrado para o CEP " + cep, "OK");
                        return;
                    }

                    Resultado.Text = string.Format("Endereço: {2},{3},{0},{1}", end.localidade,end.uf,end.logradouro,end.bairro);
                }
                catch(Exception e)
                {
                    DisplayAlert("Erro","Erro crítico - "+e.Message,"OK");
                }
            }
        }

        public bool validaCep(string cep)
        {
            bool validado = true;

            if (cep.Length != 8)
            {
                //Erro
                DisplayAlert("Erro", "Número de caracteres inválido", "OK");
                validado = false;
                return false;
            }

            int novoCep = 0;
            if (!int.TryParse(cep, out novoCep))
            {
                //Erro
                DisplayAlert("Erro", "O cep deve conter somente números", "OK");
                validado = false;
                return false;
            }


            return validado;
        }
    }
}
