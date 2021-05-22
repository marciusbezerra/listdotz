using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using static ListDotz.DataSetDotz;

namespace ListDotz
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
        }

        private static async Task<Rootobject> GetRootObjAsync(int from, int size, int maxDotz)
        {
            var client = new HttpClient { Timeout = TimeSpan.FromHours(1) };
            HttpContent httpContent =
                new StringContent(
                    "{\"from\":" + from + ",\"size\":" + size + ",\"aggs\":{\"cor\":{\"terms\":{\"field\":\"cor\",\"size\":10}},\"voltagem\":{\"terms\":{\"field\":\"voltagem\",\"size\":10}},\"tamanho\":{\"terms\":{\"field\":\"tamanho\",\"size\":10}},\"marca\":{\"terms\":{\"field\":\"marca\",\"size\":10}}},\"query\":{\"filtered\":{\"query\":{\"bool\":{\"must\":[{\"range\":{\"precos.preco.pontosFinal\":{\"gte\":\"1\",\"lte\":\"" + maxDotz + "\"}}}]}},\"filter\":{\"bool\":{\"must\":[{\"term\":{\"visivel\":true}},{\"term\":{\"disponivelParaTroca\":true}},{\"term\":{\"precos.preco.unidadeFederal\":\"6\"}}],\"must_not\":{\"term\":{\"tipoTrocaId\":9}},\"should\":[{\"range\":{\"produtoPaiIdEquivalente\":{\"lt\":1},\"precos.preco.pontosFinal\":{\"gte\":\"1\",\"lte\":\"" + maxDotz + "\"}}},{\"and\":{\"filters\":[{\"missing\":{\"field\":\"cor\"}},{\"missing\":{\"field\":\"voltagem\"}},{\"missing\":{\"field\":\"tamanho\"}}]}}]}}}},\"sort\":[{\"precos.preco.pontosFinal\":{\"order\":\"desc\",\"mode\":\"avg\"}},\"_score\"]}");
            //{"from":" + from + ",  "size":12,            "aggs":{"cor":{"terms":{"field":"cor","size":10}},            "voltagem":{"terms":{"field":"voltagem","size":10}},"tamanho":{"terms":{"field":"tamanho","size":10}},                    "marca":{"terms":{"field":"marca","size":10}}},          "query":{"filtered":{"filter":{"bool":{"must":[{"term":{"visivel":true}},{"term":{"disponivelParaTroca":true}},{"term":{"isOutlet":false}},{"term":{"precos.preco.unidadeFederal":"6"}}],"must_not":[{"term":{"tipoTrocaId":9}},{"term":{"id":3086888}},{"term":{"id":8561995}},{"term":{"id":8591411}},{"term":{"id":8317880}},{"term":{"id":8621965}},{"term":{"id":8589338}},{"term":{"id":919774}},{"term":{"id":7790266}},{"term":{"id":691482}},{"term":{"id":7790258}},{"term":{"id":8592408}},{"term":{"id":8606251}},{"term":{"id":3086888}},{"term":{"id":8561995}},{"term":{"id":8591411}},{"term":{"id":8317880}},{"term":{"id":8621965}},{"term":{"id":8589338}},{"term":{"id":919774}},{"term":{"id":7790266}},{"term":{"id":691482}},{"term":{"id":7790258}},{"term":{"id":8592408}},{"term":{"id":8606251}},{"term":{"id":7021600}},{"term":{"id":8773175}},{"term":{"id":7530036}},{"term":{"id":6919727}},{"term":{"id":5531125}},{"term":{"id":5498654}},{"term":{"id":1003458}},{"term":{"id":5049262}},{"term":{"id":7802529}},{"term":{"id":5309490}},{"term":{"id":5830010}},{"term":{"id":5718778}}],"should":[{"range":{"produtoPaiIdEquivalente":{"lt":1}}},{"and":{"filters":[{"missing":{"field":"cor"}},{"missing":{"field":"voltagem"}},{"missing":{"field":"tamanho"}}]}}]}}}},"sort":["_score"]}
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync("https://search.dotz.com.br/produtoview/_search", httpContent);
            var responseContent = response.Content;
            var responseStr = await responseContent.ReadAsStringAsync();

            var rootObj = JsonConvert.DeserializeObject<Rootobject>(responseStr);
            return rootObj;
        }

        private async void buttonLoadFromSite_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(@"Recuperar os dados do site Dotz?", @"Atenção",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.No)
                return;
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                var startTime = DateTime.Now;
                var rootObj = await GetRootObjAsync(1, 10000, Convert.ToInt32(numericUpDownMaxDotz.Value));

                dataSetDotz.Products.Clear();

                foreach (var hit in rootObj.hits.hits)
                {
                    dataSetDotz.Products.AddProductsRow(
                        hit._id,
                        hit._source?.nomeCompleto,
                        Convert.ToInt32(hit._source?.precos.preco[0]?.pontosFinal),
                        hit._source?.imagens.Length > 0 ? hit._source?.imagens[0]?.url : null,
                        hit._source?.imagens.Length > 1 ? hit._source?.imagens[1]?.url : null,
                        false);
                }

                var totalTime = DateTime.Now - startTime;
                MessageBox.Show("Finalizado em: " + totalTime.ToString("g"));
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void buttonLoadFromFile_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataSetDotz.Products.ReadXml("Data.xml");
            dataGridView1.DataSource = productsBindingSource;
        }

        private void buttonSaveToFile_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Deseja salvar a listagem?", "Atenção",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.No) return;
            dataSetDotz.Products.WriteXml("Data.xml");
            MessageBox.Show("Data.xml writed with success!");
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var productsRow = getCurrentProduct();
                Process.Start($"https://www.dotz.com.br/Produto?pid={productsRow?.Id}");
            }
        }

        private void checkBoxCheckeds_CheckedChanged(object sender, EventArgs e)
        {
            FilterData();
        }

        private void FilterData()
        {
            if (checkBoxCheckeds.Checked)
                productsBindingSource.Filter = $"Checked = True And Name LIKE '%{textBoxFilter.Text}%'";
            else
                productsBindingSource.Filter = $"Name LIKE '%{textBoxFilter.Text}%'";
        }

        private void buttonFilter_Click(object sender, EventArgs e)
        {
            FilterData();
        }

        private void productsBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = null;
            pictureBox2.ImageLocation = null;

            var productsRow = getCurrentProduct();

            pictureBox1.ImageLocation = productsRow?.Image1?.Replace("https://", "http://");
            pictureBox2.ImageLocation = productsRow?.Image2?.Replace("https://", "http://");
        }

        private ProductsRow getCurrentProduct()
        {
            var dataRowView = (productsBindingSource.Current as DataRowView);
            if (dataRowView == null) return null;
           return (dataRowView.Row as ProductsRow);
        }
    }
}
