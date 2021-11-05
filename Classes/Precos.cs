using System.Collections.Generic;

namespace CalculoMelhorVendaVergalhao.Classes
{
    public class Precos
    {
        private readonly Precos _subPrecos;
        private readonly List<int> _precosTamanho;
        private int? melhorVenda;
        private int? precoInteiro;
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
            for(int i = 0; i < _precosTamanho.Count; i++)
            {
                int tamanhoMaximoAtual = _precosTamanho.Count / (i + 1);
                int resto = _precosTamanho.Count % (i + 1);
                int valorAtual = (_precosTamanho[i] * tamanhoMaximoAtual) + _subPrecos.MelhorVendaTamanho(resto - 1);

                melhorVendaAtual = melhorVendaAtual < valorAtual ? valorAtual : melhorVendaAtual;
            }

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
        
    }


}