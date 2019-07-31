using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using app01_consultarCEP.Servico.Modelo;
using Newtonsoft.Json;

namespace app01_consultarCEP.Servico
{
    public class viaCepServico
    {
        //Endereço padrão para a busca do cep com o parâmetro 0
        private static string enderecoURL = "http://viacep.com.br/ws/{0}/json/";   

        public static endereco BuscarEnderecoViaCep(string cep)
        {
            //atribui a variável novoEnderecoURL o endereço URL e monta o parâmetro
            string novoEnderecoURL = string.Format(enderecoURL, cep);

            //Cria uma comunicação web com o endereço digitado
            WebClient wc = new WebClient();
            //Faz o download do conteúdo que o site devolve
            string conteudo = wc.DownloadString(novoEnderecoURL);

            //interpreta o conteúdo enviado e armazena no objeto endereço
            endereco end = JsonConvert.DeserializeObject<endereco>(conteudo);

            //testa se o retorno foi nulo e retorna nulo ao invés de vazio
            if (end.cep == null)
            {
                return null;
            }

            return end;
        }
    }
}
