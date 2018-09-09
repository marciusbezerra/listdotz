using System;

namespace ListDotz
{
    public class Rootobject
    {
        public string took { get; set; }
        public bool timed_out { get; set; }
        public _Shards _shards { get; set; }
        public Hits hits { get; set; }
        public Aggregations aggregations { get; set; }
    }

    public class _Shards
    {
        public string total { get; set; }
        public string successful { get; set; }
        public string failed { get; set; }
    }

    public class Hits
    {
        public string total { get; set; }
        public object max_score { get; set; }
        public Hit[] hits { get; set; }
    }

    public class Hit
    {
        public string _index { get; set; }
        public string _type { get; set; }
        public string _id { get; set; }
        public string _score { get; set; }
        public _Source _source { get; set; }
        public string[] sort { get; set; }
    }

    public class _Source
    {
        public string id { get; set; }
        public string idPai { get; set; }
        public string produtoIdFornecedor { get; set; }
        public string nomeReduzido { get; set; }
        public string nomeReduzidoNormalizado { get; set; }
        public string nomeCompleto { get; set; }
        public string nomeCompletoNormalizado { get; set; }
        public string descricao { get; set; }
        public string tipoTrocaId { get; set; }
        public string tipoTroca { get; set; }
        public string tipoProdutoId { get; set; }
        public string tipoProdutoNome { get; set; }
        public string fornecedorId { get; set; }
        public string fornecedorNome { get; set; }
        public Detalhe[] detalhes { get; set; }
        public string condicaoEntrega { get; set; }
        public Categoria[] categorias { get; set; }
        public Imagen[] imagens { get; set; }
        public string prazoEntrega { get; set; }
        public Canai[] canais { get; set; }
        public Precos precos { get; set; }
        public bool habilitadoDotzMaisReais { get; set; }
        public bool disponivelParaTroca { get; set; }
        public string urlVendaParceiroSite { get; set; }
        public string tipoProdutoOperacao { get; set; }
        public Avaliacaoproduto avaliacaoProduto { get; set; }
        public Catalogo[] catalogos { get; set; }
        public bool aprovado { get; set; }
        public bool disponivel { get; set; }
        public string status { get; set; }
        public string tipoEnvioReciboTroca { get; set; }
        public bool visivel { get; set; }
        public string produtoPaiIdEquivalente { get; set; }
        public DateTime dataRequest { get; set; }
        public string marca { get; set; }
        public string popularidade { get; set; }
        public float precoVendaFornecedor { get; set; }
        public string precoVendaFornecedorDe { get; set; }
        public bool fornecedorAtivo { get; set; }
        public string codigoEan { get; set; }
        public bool _virtual { get; set; }
    }

    public class Precos
    {
        public Preco[] preco { get; set; }
    }

    public class Preco
    {
        public string pontosFinal { get; set; }
        public string pontosSemOferta { get; set; }
        public string porcentagemOferta { get; set; }
        public float precoVenda { get; set; }
        public string precoVendaSemDesconto { get; set; }
        public string precoVendaPorcentagemOferta { get; set; }
        public Grupo grupo { get; set; }
        public string unidadeFederal { get; set; }
    }

    public class Grupo
    {
        public DateTime dataCriacao { get; set; }
        public bool naoRecebeComunicacao { get; set; }
        public bool validarTodos { get; set; }
        public string codigoOrigemCadastro { get; set; }
        public string codigoUsuarioParceiro { get; set; }
        public string codigoUsuarioCriacao { get; set; }
        public string codigoMecanica { get; set; }
        public string codigoTipoGrupo { get; set; }
        public string codigoPrograma { get; set; }
        public string codigoGrupo { get; set; }
        public string codigoGrupoStatus { get; set; }
        public string codigoAplicacao { get; set; }
    }

    public class Avaliacaoproduto
    {
        public string quantidade1Estrela { get; set; }
        public string quantidade2Estrela { get; set; }
        public string quantidade3Estrela { get; set; }
        public string quantidade4Estrela { get; set; }
        public string quantidade5Estrela { get; set; }
        public string mediaAvaliacao { get; set; }
    }

    public class Detalhe
    {
        public string id { get; set; }
        public string nome { get; set; }
        public string valor { get; set; }
        public bool obrigatorio { get; set; }
        public Propriedade[] propriedade { get; set; }
        public bool exibir { get; set; }
    }

    public class Propriedade
    {
        public string chave { get; set; }
        public string valor { get; set; }
    }

    public class Categoria
    {
        public string id { get; set; }
        public string nome { get; set; }
        public string produtoOrdemExibicao { get; set; }
        public bool exibir { get; set; }
    }

    public class Imagen
    {
        public string url { get; set; }
        public string urlImagemFornecedor { get; set; }
        public bool principal { get; set; }
        public string imagemDimensao { get; set; }
        public string textoAlternativo { get; set; }
        public string ordemExibicao { get; set; }
    }

    public class Canai
    {
        public string canalId { get; set; }
        public string nome { get; set; }
        public bool status { get; set; }
        public bool origemDefault { get; set; }
    }

    public class Catalogo
    {
        public string id { get; set; }
        public string nome { get; set; }
    }

    public class Aggregations
    {
        public Tamanho tamanho { get; set; }
        public Voltagem voltagem { get; set; }
        public Cor cor { get; set; }
        public Marca marca { get; set; }
    }

    public class Tamanho
    {
        public string doc_count_error_upper_bound { get; set; }
        public string sum_other_doc_count { get; set; }
        public Bucket[] buckets { get; set; }
    }

    public class Bucket
    {
        public string key { get; set; }
        public string doc_count { get; set; }
    }

    public class Voltagem
    {
        public string doc_count_error_upper_bound { get; set; }
        public string sum_other_doc_count { get; set; }
        public Bucket1[] buckets { get; set; }
    }

    public class Bucket1
    {
        public string key { get; set; }
        public string doc_count { get; set; }
    }

    public class Cor
    {
        public string doc_count_error_upper_bound { get; set; }
        public string sum_other_doc_count { get; set; }
        public Bucket2[] buckets { get; set; }
    }

    public class Bucket2
    {
        public string key { get; set; }
        public string doc_count { get; set; }
    }

    public class Marca
    {
        public string doc_count_error_upper_bound { get; set; }
        public string sum_other_doc_count { get; set; }
        public Bucket3[] buckets { get; set; }
    }

    public class Bucket3
    {
        public string key { get; set; }
        public string doc_count { get; set; }
    }
}
