using System.Collections.Generic;

namespace CalculoMelhorVendaVergalhao.Classes
{
    public class Vergalhao
    {
        private Precos precos;
        public Vergalhao(Precos precos)
        {
            this.precos = precos;
        }

        public Precos Precos
        {
            get
            {
                return this.precos;
            }
        }
    }
}