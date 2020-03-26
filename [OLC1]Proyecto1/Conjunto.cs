using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _OLC1_Proyecto1
{
    class Conjunto
    {
        public String Id;
        public String conjunto;
        public List<char> listaCaracteres = new List<char>();
        public List<int> listaEnteros = new List<int>();
        public List<char> listaDeComas = new List<char>();


        public Conjunto(String id, String conjunto)
        {
            this.Id = id;
            this.conjunto = conjunto;
            RevisarConjunto(this.conjunto);
        }

        public void RevisarConjunto(String conjunto1)
        {
            for (int i=0;i<conjunto1.Length;i++)
            {
                if (conjunto[i].Equals('~'))
                {
                    char aux = conjunto[i - 1];
                    if (Char.IsLetter(aux))
                    {
                        for (char j= conjunto[i - 1];j<=conjunto[i + 1];j++)
                        {
                            listaCaracteres.Add(j);
                        }


                    }else if (Char.IsDigit(aux))
                    {
                        String aux2 = conjunto[i - 1].ToString();
                        int aux3 = int.Parse(aux2);

                        String aux4 = conjunto[i + 1].ToString();
                        int aux5 = int.Parse(aux4);
                        for (int j = aux3;j<=aux5 ;j++)
                        {
                            listaEnteros.Add(j);
                        }

                    }
                }
               
            }


            if (listaCaracteres.Count==0 && listaEnteros.Count==0)
            {
                
               
                for (int i = 0; i < conjunto1.Length; i++)
                {
                    if (!conjunto1.ElementAt(i).Equals(','))
                    {
                        if (!conjunto1.ElementAt(i).Equals(""))
                        {
                            listaDeComas.Add(conjunto1[i]);
                        }
                    }
                }

            }
        }
    }
}
