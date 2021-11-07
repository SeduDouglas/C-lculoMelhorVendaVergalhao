using System;
using System.Collections.Generic;

namespace CalculoMelhorVendaVergalhao.Classes
{
    public class Precos
    {
        private readonly Precos _subPrecos;
        private readonly List<int> _precosTamanho;
        private int? melhorVenda;
        private int? precoInteiro;
        private string textoQuantidadesMelhorVenda;
        private string textoMelhorQuantidade;
        public Precos(List<int> precosTamanho)
        {
            _precosTamanho = precosTamanho;
            if (_precosTamanho.Count > 0)
                _subPrecos = new Precos(precosTamanho.GetRange(0, (precosTamanho.Count - 1)));
        }

        
        public int PrecoInteiro
        {
            get
            {
                if (_precosTamanho.Count == 0)
                    return 0;

                if (precoInteiro == null)
                    precoInteiro = _precosTamanho[_precosTamanho.Count - 1];

                return precoInteiro.Value;
            }
        }

        public int MelhorVenda
        {
            get
            {
                if (_precosTamanho.Count == 0)
                    return 0;

                if (melhorVenda == null)
                    melhorVenda = ResolveMelhorVenda();
              
                return melhorVenda.Value;
            }
        }

        private int ResolveMelhorVenda()
        {
            int melhorVendaAtual = 0;
            string textoMelhorQuantidadeAtual = string.Empty; 
            for(int i = 0; i < _precosTamanho.Count; i++)
            {
                int tamanhoMaximoAtual = _precosTamanho.Count / (i + 1);
                int resto = _precosTamanho.Count % (i + 1);
                int valorAtual = (_precosTamanho[i] * tamanhoMaximoAtual) + _subPrecos.MelhorVendaTamanho(resto);

                if(melhorVendaAtual < valorAtual)
                {
                    melhorVendaAtual =  valorAtual;
                    string textoResto = _subPrecos.TextoMelhorQuandidadeTamanho(resto) + (string.IsNullOrWhiteSpace(_subPrecos.TextoMelhorQuandidadeTamanho(resto)) ? string.Empty : " e ");
                    textoMelhorQuantidadeAtual = $"{textoResto}{tamanhoMaximoAtual} vergalhões de tamanho {i + 1}";
                }
            }
            this.textoMelhorQuantidade = textoMelhorQuantidadeAtual;

            return melhorVendaAtual;
        }

        protected int MelhorVendaTamanho(int tamanho)
        {
            if (_precosTamanho.Count == 0)
                return 0;

            if (_precosTamanho.Count == tamanho)
            {
                return this.MelhorVenda;
            }
            return _subPrecos.MelhorVendaTamanho(tamanho);
        }

        public string TextoQuantidadesMelhorVenda
        {
            get
            {
                if(string.IsNullOrEmpty(this.textoQuantidadesMelhorVenda))
                    this.textoQuantidadesMelhorVenda = ResolveTextoQuantidadesMelhorVenda();

                return this.textoQuantidadesMelhorVenda;
            }
        }

        protected string TextoMelhorQuantidade
        {
            get
            {
                return this.textoMelhorQuantidade;
            }
            private set
            {
                this.textoMelhorQuantidade = value;
            }
        }


        private string ResolveTextoQuantidadesMelhorVenda()
        {
            int melhorVenda = this.MelhorVenda;
            return $"Valor máximo de venda {melhorVenda}. {this.textoMelhorQuantidade}";
        }

        protected string TextoMelhorQuandidadeTamanho(int tamanho)
        {
            if (_precosTamanho.Count == 0)
                return string.Empty;

            if (_precosTamanho.Count == tamanho)
            {
                return this.TextoMelhorQuantidade;
            }
            return _subPrecos.TextoMelhorQuandidadeTamanho(tamanho);
        }
    }


}