using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace OśrodekDomena
{
    /// <summary>
    /// Repozytorium operatów.
    /// </summary>
    public class RepozytoriumOperatów : IEnumerable<Operat>
    {
        public Operat this[int index] => _operaty[index];

        /// <summary>
        /// Szukaj operatu w repozytorium.
        /// </summary>
        /// <param name="idZasobu"></param>
        /// <returns></returns>
        public Operat this[string idZasobu] => _indeksOperatów[idZasobu];

        /// <summary>
        /// Lista operatów znalezionych w bazie danych Ośrodka.
        /// </summary>
        public IEnumerable<Operat> ZnalezioneOperaty => Operaty.Where(op => op.Id.HasValue);

        /// <summary>
        /// Lista operatów nieznalezionych w bazie danych Ośrodka.
        /// </summary>
        public IEnumerable<Operat> NieznalezioneOperaty => Operaty.Where(op => op.Id.HasValue == false);

        /// <summary>
        /// Lista operatów w repozytorium.
        /// </summary>
        public IEnumerable<Operat> Operaty => _operaty;
        List<Operat> _operaty = new List<Operat>();
        Dictionary<string, Operat> _indeksOperatów = new Dictionary<string, Operat>();

        public bool Zawiera(string idZasobu)
        {
            return _indeksOperatów.ContainsKey(idZasobu);
        }

        public void Dodaj(Operat operat)
        {
            if (operat == null)
                throw new ArgumentNullException(
                    paramName: "operat",
                    message: "Operat jest null");
            var idZasobu = operat.IdZasobu;
            if (idZasobu == null)
                throw new ArgumentNullException(
                    paramName: "operat.IdZasobu",
                    message: "Id zasobu jest null");
            if (_indeksOperatów.ContainsKey(idZasobu))
                throw new InvalidOperationException(
                    message: "Repozytorium zawiera już operat z danym id zasobu: " + idZasobu);
            _indeksOperatów.Add(idZasobu, operat);
            _operaty.Add(operat);

        }

        public void Usuń(Operat operat)
        {
            var id = operat.IdZasobu;
            _indeksOperatów.Remove(id);
            _operaty.Remove(operat);
        }

        public IEnumerator<Operat> GetEnumerator() { return Operaty.GetEnumerator(); }
        IEnumerator IEnumerable.GetEnumerator() { return Operaty.GetEnumerator(); }
    }
}
