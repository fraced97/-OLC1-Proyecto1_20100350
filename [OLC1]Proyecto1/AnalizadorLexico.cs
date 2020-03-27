using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _OLC1_Proyecto1
{
    class AnalizadorLexico
    {
        public static int indiceReporte = 0;
        LinkedList<Token> listaTokens = new LinkedList<Token>();
        LinkedList<ErroresLexicos> listaErrores = new LinkedList<ErroresLexicos>();
        LinkedList<TokenER> listaTokenER = new LinkedList<TokenER>();
        //LinkedList<LinkedList<Trancisiones>> listaEstados = new LinkedList<LinkedList<Trancisiones>>();
       // LinkedList<Estados> listaEstados = new LinkedList<Estados>();
        
        int indice = 0;
         
        public List<Imagen> listaImagenesThompson2 = new List<Imagen>();
        public List<Imagen> listaImagenesAfd2 = new List<Imagen>();
        public List<Imagen> listaImagenesTabla2 = new List<Imagen>();


        public List<Conjunto> listaConjuntos = new List<Conjunto>();
        public List<EjExpresionR> listaIdExpresionR = new List<EjExpresionR>();
        String auxConjunto = "";
        String auxIdExpresionR = "";

        String IdUtilizar = "";
        LinkedList<Estados> listaEstadosAFD = new LinkedList<Estados>();
        String capturar = "";

        public void LimpiarLista()
        {
            listaTokens.Clear();
            listaTokenER.Clear();
           // listaEstados.Clear(); 

        }
        public void Analizar(String x)
        {
            String aux = "";
            char variable;
            String lexema = "";
            int fila = 1;
            int columna = 0;
            int estadoActual = 0;
            LimpiarLista();

            for (int i=0; i<x.Length;i++)
            {
                variable = x[i];
                columna++;
                switch (estadoActual)
                {
                    case 0:
                        if (variable=='/')
                        {
                            estadoActual = 4;
                            lexema = lexema + variable;
                        }else if (variable=='<')
                        {
                            estadoActual = 6;
                            lexema = lexema + variable;
                            listaTokens.AddLast(new Token(Token.Tipo.MENOR_QUE, lexema, "Menor_Que", fila, columna));
                            lexema = "";
                        }
                        else if (Char.IsLetter(variable))
                        {
                            estadoActual = 10;

                            lexema = lexema + variable;
                        }else if (variable=='\n')
                        {
                            columna = 0;
                            estadoActual = 0;
                            fila++;
                        }else if (variable == ' ' || variable == '\t')
                        {
                            estadoActual = 0;
                        }
                        else
                        {
                            lexema = lexema + variable;
                            estadoActual = 0;
                            listaErrores.AddLast(new ErroresLexicos(lexema, "Lexico", "Elemento desconocido", fila, columna));
                            lexema = "";
                        }
                        break;
                    case 4:
                        if (variable == '/')
                        {
                            estadoActual = 5;
                            lexema = lexema + variable;
                            //System.out.println(variable + "   ... ENTRO EN OTRO SLASH");
                        }
                        else
                        {
                            lexema = lexema + variable;
                            estadoActual = 0;
                            listaErrores.AddLast(new ErroresLexicos(lexema, "Lexico", "Elemento desconocido", fila, columna));
                            lexema = "";
                        }


                        break;
                    case 5:
                        if (variable == '\n')
                        {
                            //System.out.println("ENTRO AQUI EN SALTO DE LINEA");

                            estadoActual = 0;
                            lexema = lexema + variable;
                            listaTokens.AddLast(new Token(Token.Tipo.COMENTARIO, lexema, "Comentario", fila, columna));
                            //listaTokens.add(new Token(Token.Tipo.COMENTARIO, lexema, "Comentario", fila, columna));
                            //System.out.println(lexema);
                            lexema = "";
                            fila++;
                            columna = 0;
                        }
                        else
                        {
                            //System.out.println("CONCATENANDO LETRAS EN COMENTARIO DE LINEA");

                            estadoActual = 5;
                            lexema = lexema + variable;
                            //System.out.println(lexema);
                            //listaTokens.add(new Token(Token.Tipo.PORCENTAJE,lexema,"Llave Cerrada",fila,columna));
                            //lexema ="";
                        }
                        break;
                    case 6:
                        if (variable == '!')
                        {
                            estadoActual = 7;
                            lexema = lexema + variable;
                            listaTokens.AddLast(new Token(Token.Tipo.EXCLAMACION, lexema, "Exclamacion", fila, columna));
                            lexema = "";
                        }
                        else
                        {
                            lexema = lexema + variable;
                            estadoActual = 0;
                            listaErrores.AddLast(new ErroresLexicos(lexema, "Lexico", "Elemento desconocido", fila, columna));
                            lexema = "";
                        }


                        break;
                    case 7:
                        if (variable == '!')
                        {
                            estadoActual = 8;
                            listaTokens.AddLast(new Token(Token.Tipo.COMENTARIO, lexema, "Comentario", fila, columna));
                            lexema = "";
                            lexema = lexema + variable;
                            listaTokens.AddLast(new Token(Token.Tipo.EXCLAMACION, lexema, "Exclamacion", fila, columna));
                            lexema = "";
                        }
                        else if (variable == '\n')
                        {
                            estadoActual = 7;
                            //columna = 0;
                            fila++;
                            //lexema = lexema + variable;
                        }else if (variable == ' ' || variable == '\t')
                        {
                            lexema = lexema + variable; //UN CAMBIO AQUI
                            estadoActual = 7;
                        }
                        else
                        {
                            estadoActual = 7;
                            lexema = lexema + variable;
                        }
                        break;
                    case 8:
                        if (variable == '>')
                        {
                            estadoActual = 0;
                            lexema = lexema + variable;
                            listaTokens.AddLast(new Token(Token.Tipo.MAYOR_QUE, lexema, "Mayor_Que", fila, columna));
                            lexema = "";
                        }
                        else
                        {
                            lexema = lexema + variable;
                            estadoActual = 0;
                            listaErrores.AddLast(new ErroresLexicos(lexema, "Lexico", "Elemento desconocido", fila, columna));
                            lexema = "";
                        }


                        break;
                    case 10:
                        if (Char.IsDigit(variable))
                        {
                            estadoActual = 10;
                            lexema = lexema + variable;
                        }
                        else if (Char.IsLetter(variable))
                        {
                            lexema = lexema + variable;
                            if (buscarPR(lexema, fila, columna))
                            {
                                estadoActual = 20;
                                //lexema=lexema+variable;
                            }
                            else
                            {
                                estadoActual = 10;
                            }

                            //lexema = lexema + variable;
                        }
                        else if (variable == '_')
                        {
                            estadoActual = 10;
                            lexema = lexema + variable;
                        }
                        else if (variable == ':')
                        {
                            auxIdExpresionR = lexema;
                            listaTokens.AddLast(new Token(Token.Tipo.IDENTIFICADOR, lexema, "Identificador", fila, columna - 1));
                            lexema = "";
                            estadoActual = 14;
                            lexema = lexema + variable;
                            listaTokens.AddLast(new Token(Token.Tipo.DOS_PUNTOS, lexema, "Dos puntos", fila, columna));
                            lexema = "";
                        }
                        else if (variable == '-')
                        {
                            listaTokens.AddLast(new Token(Token.Tipo.IDENTIFICADOR, lexema, "Identificador", fila, columna - 1));
                            lexema = "";
                            estadoActual = 11;
                            lexema = lexema + variable;
                            listaTokens.AddLast(new Token(Token.Tipo.GUION, lexema, "Guion", fila, columna));
                            lexema = "";

                        }
                        
                        break;
                    case 11:
                        if (variable == '>')
                        {
                            estadoActual = 12;
                            lexema = lexema + variable;
                            listaTokens.AddLast(new Token(Token.Tipo.MAYOR_QUE, lexema, "Mayor Que", fila, columna));
                            lexema = "";

                        }
                        else
                        {
                            lexema = lexema + variable;
                            estadoActual = 0;
                            listaErrores.AddLast(new ErroresLexicos(lexema, "Lexico", "Elemento desconocido", fila, columna));
                            lexema = "";
                        }



                        break;
                    case 12:
                        if (variable == ';')
                        {
                            listaTokens.AddLast(new Token(Token.Tipo.EXPRESION_REGULAR, lexema, "Expresion Regular", fila, columna - 1));
                            lexema = "";
                            estadoActual = 0;
                            lexema = lexema + variable;
                            listaTokens.AddLast(new Token(Token.Tipo.PUNTO_COMA, lexema, "Punto y Coma", fila, columna));
                            lexema = "";
                        }
                        else
                        {
                            estadoActual = 12;
                            lexema = lexema + variable;
                        }
                        break;
                    case 14:
                        if (variable == '"')
                        {
                            estadoActual = 15;
                            lexema = lexema + variable;
                            listaTokens.AddLast(new Token(Token.Tipo.COMILLAS, lexema, "Comillas", fila, columna));
                            lexema = "";
                        }else if (variable == ' ' || variable == '\t')
                        {
                            //lexema = lexema + variable; //UN CAMBIO AQUI
                            estadoActual = 14;
                        }
                        ////////////////-///////////////////////
                        else
                        {
                            lexema = lexema + variable;
                            estadoActual = 0;
                            listaErrores.AddLast(new ErroresLexicos(lexema, "Lexico", "Elemento desconocido", fila, columna));
                            lexema = "";
                        }


                        /*else if(Character.isLetter(variable)){
                    estadoActual=10;
                    lexema=lexema+variable;
                }*/
                        break;
                    case 15:
                        if (variable == '"')
                        {
                            listaIdExpresionR.Add(new EjExpresionR(auxIdExpresionR, lexema));
                            auxIdExpresionR = "";

                            listaTokens.AddLast(new Token(Token.Tipo.EJEMPLO_EXPRESION, lexema, "Ejemplo Expresion", fila, columna - 1));
                            lexema = "";
                            estadoActual = 22;
                            lexema = lexema + variable;
                            listaTokens.AddLast(new Token(Token.Tipo.COMILLAS, lexema, "Comillas", fila, columna));
                            lexema = "";
                            //lexema=lexema+variable;
                        }
                        else
                        {
                            estadoActual = 15;
                            lexema = lexema + variable;
                        }
                        break;
                    case 18:
                        if (variable == '>')
                        {
                            lexema = lexema + variable;
                            listaTokens.AddLast(new Token(Token.Tipo.MAYOR_QUE, lexema, "Mayor que", fila, columna));
                            lexema = "";
                            estadoActual = 19;
                        }
                        
                        
                        break;

                    case 19:
                        if (variable == ';')
                        {
                            listaConjuntos.Add(new Conjunto(auxConjunto,lexema));
                            auxConjunto = "";
                            listaTokens.AddLast(new Token(Token.Tipo.CONJUNTO, lexema, "Conjunto", fila, columna - 1));
                            lexema = "";
                            lexema = lexema + variable;
                            listaTokens.AddLast(new Token(Token.Tipo.PUNTO_COMA, lexema, "Punto y coma", fila, columna));
                            lexema = "";
                            estadoActual = 0;
                        }
                        else
                        {
                            lexema = lexema + variable;
                            estadoActual = 19;
                        }
                        break;
                    case 20:
                        if (variable == ':')
                        {
                            lexema = "";
                            lexema = lexema + variable;
                            listaTokens.AddLast(new Token(Token.Tipo.DOS_PUNTOS, lexema, "Dos puntos", fila, columna));
                            lexema = "";
                            estadoActual = 21;
                        }
                       
                        
                        break;
                    case 21:
                        if (Char.IsLetter(variable))
                        {
                            estadoActual = 21;
                            lexema = lexema + variable;
                        }
                        else if (Char.IsDigit(variable))
                        {
                            estadoActual = 21;
                            lexema = lexema + variable;
                        }
                        else if (variable == '_')
                        {
                            estadoActual = 21;
                            lexema = lexema + variable;
                        }
                        else if (variable == '-')
                        {
                            auxConjunto = lexema;
                            listaTokens.AddLast(new Token(Token.Tipo.IDENTIFICADOR_CONJ, lexema, "Identificador Conj", fila, columna - 1));
                            lexema = "";
                            lexema = lexema + variable;
                            listaTokens.AddLast(new Token(Token.Tipo.GUION, lexema, "Guion", fila, columna));
                            lexema = "";
                            estadoActual = 18;
                        }
                        
                        break;
                    case 22:
                        if (variable == ';')
                        {
                            lexema = lexema + variable;
                            listaTokens.AddLast(new Token(Token.Tipo.PUNTO_COMA, lexema, "Punto y coma", fila, columna));
                            lexema = "";
                            estadoActual =0;
                        }
                        
                        break;
                }

            }
            foreach (Token i in listaTokens)
            {
                //Console.WriteLine(i);
                Console.WriteLine(i.lexema + "   #-#-#-#-#  " + i.tipo1 + "   #-#-#-#  " +i.fila.ToString() + "   #-#-#-#  " + i.columna.ToString() + "   #-#-#-#  " + i.token.ToString() + "\n");
            }

        }



        public bool buscarPR(String lexema, int fila, int columna)
        {
            bool devolver = false;
            switch (lexema)
            {
                case "CONJ":
                    listaTokens.AddLast(new Token(Token.Tipo.RESERVADA_CONJ, lexema, "Reservada", fila, columna));
                    devolver = true;
                    break;
            }
            return devolver;
        }



        //////////////////////
        public void AnalizarER() 
        {
            String lexemaLetra="";
            String lexema="";
            String lexemaAux="";
        int nExpresion = 0;
        int estado = 0;
        int auxContador = 0;
        int contador = 0;
        //Token auxContador;
        foreach (Token i in listaTokens) {
                if (i.token.ToString().Equals("IDENTIFICADOR"))
                {
                    IdUtilizar = i.lexema;
                }
            if(i.token.ToString().Equals("EXPRESION_REGULAR")){
                
                nExpresion++;
                lexemaAux=i.lexema+"#";
                //lexemaAux="."+i.lexema;
                for (int j = 0; j<lexemaAux.Length; j++) {
                    auxContador=j;
                    char c = lexemaAux[j];
                    switch(estado){
                        case 0:
                                if (c == '*') {
                                    lexema = lexema + c;
                                    listaTokenER.AddLast(new TokenER(TokenER.TipoER.ASTERISCO, lexema, "Expresion " + (nExpresion.ToString()), false));
                                    lexema = "";
                                    estado = 0;
                                } else if (c == '+') {
                                    lexema = lexema + c;
                                    listaTokenER.AddLast(new TokenER(TokenER.TipoER.MAS, lexema, "Expresion " + (nExpresion.ToString()), false));
                                    lexema = "";
                                    estado = 0;
                                } else if (c == '|') {
                                    lexema = lexema + c;
                                    listaTokenER.AddLast(new TokenER(TokenER.TipoER.OR, lexema, "Expresion " + (nExpresion.ToString()), false));
                                    lexema = "";
                                    estado = 0;
                                } else if (c == '?') {
                                    lexema = lexema + c;
                                    listaTokenER.AddLast(new TokenER(TokenER.TipoER.INTERROGACION, lexema, "Expresion " + (nExpresion.ToString()), false));
                                    lexema = "";
                                    estado = 0;
                                } else if (c == '.') {
                                    lexema = lexema + c;
                                    listaTokenER.AddLast(new TokenER(TokenER.TipoER.PUNTO, lexema, "Expresion " + (nExpresion.ToString()), false));
                                    lexema = "";
                                    estado = 0;

                                } else if (c == '{') {
                                    estado = 2;
                                } else if (c == ' ' || c == '\t') {
                                    estado = 0;
                                } else if (c == '"') {
                                    estado = 1;
                                }  else if (c=='[') {
                                    estado= 3;

                                } else if (c == '#') {

                                    //lexema=lexema+c;
                                    //listaTokenER.AddFirst(new TokenER(TokenER.TipoER.NUMERAL, lexema,"Expresion "+(nExpresion.ToString())));
                                    //lexema="";
                                    //OrdenarExR(listaTokenER);
                                    MetodoThompson metodoT = new MetodoThompson(listaTokenER);

                                    //listaEstadosAFD = metodoT.MandarListaAFD();
                                    for (int p = 0; p < listaIdExpresionR.Count; p++)
                                    {
                                        if (IdUtilizar.Equals(listaIdExpresionR.ElementAt(p).Id))
                                        {


                                           // listaIdExpresionR.ElementAt(p).listaEstadosAfd = metodoT.MandarListaAFD();
                                        }
                                        
                                    }
                                    VerificarLexema();
                                    listaImagenesThompson2 = metodoT.crearImagen();
                                    listaImagenesAfd2 = metodoT.crearImagenAfd();
                                    listaImagenesTabla2 = metodoT.crearImagenTabla();

                                    //listaIdExpresionR.Clear();

                                    listaTokenER.Clear();

                                    estado = 0;


                                    try {
                                        // Arbol arbol = new Arbol(analizar.listaTokensER);
                                        //int numero = 0;
                                        //contador++;
                                        //numero = (int) (Math.random() * 100) + 1;
                                        //if((numero%2)==0){
                                        //  JOptionPane.showMessageDialog(null, "Expresion "+String.valueOf(contador)+" Correcta");
                                        //}else{
                                        //  JOptionPane.showMessageDialog(null, "Expresion "+String.valueOf(contador)+" InCorrecta");
                                        //}
                                        //VentanaPrincipal ventana = new VentanaPrincipal();
                                        //ventana.obtenerEjemploLexema(numero, contador);
                                        //ventana.setVisible(true);

                                        //arbol.tablaSiguientes();
                                        //analizar.listaTokensER.clear();
                                    } catch (Exception ex) {
                                        //Logger.getLogger(AnalizadoLexico.class.getName()).log(Level.SEVERE, null, ex);
                                    }
                                } else {
                                    lexema = lexema + c;
                                    listaTokenER.AddLast(new TokenER(TokenER.TipoER.SIMBOLO, lexema, "Expresion " + nExpresion.ToString(), false));
                                    lexema = "";
                                    estado = 0;
                                }
                        break;
                        case 1:
                            if(c=='"'){
                                //lexema=lexema+c;
                                listaTokenER.AddLast(new TokenER(TokenER.TipoER.CADENA, lexema,"Expresion "+(nExpresion.ToString()),true));
                                lexema="";
                                estado=0;
                            }else{
                                lexema=lexema+c;
                                estado=1;
                            }
                            break;
                        case 2:
                            if(c=='}'){
                                //lexema=lexema+c;
                                listaTokenER.AddLast(new TokenER(TokenER.TipoER.IDENTIFICADOR, lexema,"Expresion "+(nExpresion.ToString()),true));
                                lexema="";
                                estado=0;
                            }else{
                                lexema=lexema+c;
                                estado=2;
                            }
                            break;
                        case 3:
                                if (c == ':')
                                {
                                    estado = 4;   
                                }
                                
                             break;
                         case 4:
                                if (c==':')
                                {
                                    listaTokenER.AddLast(new TokenER(TokenER.TipoER.CADENA, lexema, "Expresion " + (nExpresion.ToString()), true));
                                    lexema = "";
                                    estado = 5;
                                }
                                else
                                {
                                    lexema = lexema + c;
                                    estado = 4;
                                }

                              break;
                          case 5:
                                if (c == ']')
                                {
                                    estado = 0;
                                }
                              break;

                    }
                    ////////ME QUEDE AQUI
                    
                    
                }
            }
        }
            //Console.WriteLine("----------------------LISTA EXPRESION REGULARES ----------------------");

            /* foreach (TokenER i in listaTokenER) {
                 Console.WriteLine(i.lexema+"  ===============   "+(i.tipo.ToString()) +"    ====================    "+i.nExpresion+"\n");
             }*/
           // OrdenarExR();


    }


     public List<EjExpresionR> mandarListaExpresionesR()
        {
            return listaIdExpresionR;
        }




        public void VerificarLexema()
        {
            String auxPosicionEstado = "";
            int auxAfd = 0;
            int auxAf2 = 0;
            Boolean encontroTrancision = false;
            for (int i=0;i<listaIdExpresionR.Count;i++)
            {
                auxAfd = 0;
                if (IdUtilizar.Equals(listaIdExpresionR.ElementAt(i).Id))
                {
                    auxAfd = 0;
                   
                    for (int j=0;j<listaIdExpresionR.ElementAt(i).ejemploExpresion.Length;j++)
                    {
                        for (int k = auxAf2; k < listaIdExpresionR.ElementAt(i).listaEstadosAfd.Count;k++)
                        {
                            encontroTrancision = false;
                            auxAf2 = 0; 
                            for (int p=0;p< listaIdExpresionR.ElementAt(i).listaEstadosAfd.ElementAt(k).listaTrancisiones.Count;p++)
                            {
                                
                                if (listaIdExpresionR.ElementAt(i).listaEstadosAfd.ElementAt(k).listaTrancisiones.ElementAt(p).NombreTrans.Length==1)
                                {
                                    //cadena
                                    if (listaIdExpresionR.ElementAt(i).listaEstadosAfd.ElementAt(k).listaTrancisiones.ElementAt(p).NombreTrans.Equals(listaIdExpresionR.ElementAt(i).ejemploExpresion[j]))
                                    {
                                        while (true)
                                        {
                                            if (listaIdExpresionR.ElementAt(i).listaEstadosAfd.ElementAt(auxAf2).nombre.Equals(listaIdExpresionR.ElementAt(i).listaEstadosAfd.ElementAt(k).listaTrancisiones.ElementAt(p).terminal))
                                            {
                                                encontroTrancision = true;
                                                break;
                                            }
                                            auxAf2++;
                                        }
                                        break;
                                    }
                                    else
                                    {
                                        if ((listaIdExpresionR.ElementAt(i).listaEstadosAfd.ElementAt(k).listaTrancisiones.Count - 1) == p)
                                        {
                                            Console.WriteLine("MANDO UN ERROR FATAL");
                                        }
                                    }

                                }
                                else if (listaIdExpresionR.ElementAt(i).listaEstadosAfd.ElementAt(k).listaTrancisiones.ElementAt(p).NombreTrans.Length>1)
                                {
                                    //identificador

                                    for (int y=0;y<listaConjuntos.Count;y++)
                                    {
                                        if (listaConjuntos.ElementAt(y).Id.Equals(listaIdExpresionR.ElementAt(i).listaEstadosAfd.ElementAt(k).listaTrancisiones.ElementAt(p).NombreTrans))
                                        {
                                            if (listaConjuntos.ElementAt(y).listaCaracteres.Count != 0)
                                            {
                                                for (int r = 0; r < listaConjuntos.ElementAt(y).listaCaracteres.Count; r++)
                                                {
                                                    if (listaConjuntos.ElementAt(y).listaCaracteres.ElementAt(r).Equals(listaIdExpresionR.ElementAt(i).ejemploExpresion[j]))
                                                    {

                                                        while (true)
                                                        {
                                                            if (listaIdExpresionR.ElementAt(i).listaEstadosAfd.ElementAt(auxAf2).nombre.Equals(listaIdExpresionR.ElementAt(i).listaEstadosAfd.ElementAt(k).listaTrancisiones.ElementAt(p).terminal))
                                                            {
                                                                encontroTrancision = true;
                                                                break;
                                                            }
                                                            auxAf2++;
                                                        }

                                                        break;
                                                    }
                                                    else
                                                    {
                                                        if ((listaConjuntos.ElementAt(y).listaCaracteres.Count-1)==r)
                                                        {
                                                            Console.WriteLine();
                                                            Console.WriteLine("Error Fatal");
                                                        }
                                                    }

                                                }
                                                break;

                                            }
                                            if (listaConjuntos.ElementAt(y).listaEnteros.Count != 0)
                                            {
                                                for (int r = 0; r < listaConjuntos.ElementAt(y).listaEnteros.Count; r++)
                                                {
                                                    if (listaConjuntos.ElementAt(y).listaEnteros.ElementAt(r).ToString().Equals(listaIdExpresionR.ElementAt(i).ejemploExpresion[j]))
                                                    {
                                                        while (true)
                                                        {
                                                            if (listaIdExpresionR.ElementAt(i).listaEstadosAfd.ElementAt(auxAf2).nombre.Equals(listaIdExpresionR.ElementAt(i).listaEstadosAfd.ElementAt(k).listaTrancisiones.ElementAt(p).terminal))
                                                            {
                                                                encontroTrancision = true;
                                                                break;
                                                            }
                                                            auxAf2++;
                                                        }

                                                        break;
                                                    }
                                                    else
                                                    {
                                                        if ((listaConjuntos.ElementAt(y).listaEnteros.Count - 1) == r)
                                                        {
                                                            Console.WriteLine("Error Fatal");
                                                        }
                                                    }

                                                }
                                                 break;

                                            }
                                            if (listaConjuntos.ElementAt(y).listaDeComas.Count != 0)
                                            {
                                                for (int r = 0; r < listaConjuntos.ElementAt(y).listaDeComas.Count; r++)
                                                {
                                                    if (listaConjuntos.ElementAt(y).listaDeComas.ElementAt(r).Equals(listaIdExpresionR.ElementAt(i).ejemploExpresion[j]))
                                                    {
                                                        while (true)
                                                        {
                                                            if (listaIdExpresionR.ElementAt(i).listaEstadosAfd.ElementAt(auxAf2).nombre.Equals(listaIdExpresionR.ElementAt(i).listaEstadosAfd.ElementAt(k).listaTrancisiones.ElementAt(p).terminal))
                                                            {
                                                                encontroTrancision = true;
                                                                break;
                                                            }
                                                            auxAf2++;
                                                        }

                                                        break;
                                                    }
                                                    else
                                                    {
                                                        if ((listaConjuntos.ElementAt(y).listaDeComas.Count - 1) == r)
                                                        {
                                                            Console.WriteLine("Error Fatal");
                                                        }
                                                    }

                                                }
                                                break;

                                            }
                                            
                                        }
                                        
                                    }

                                }
                                if (encontroTrancision)
                                {
                                    break;
                                }
                                ///////////////AQUI
                            }
                            break;

                        }


                    }


                }

            }



        }





        /*public void crearTrancision(Estados estadoInicial, Estados estadoFinal)
        {
            Estados aux = null;
            foreach(Estados i in listaEstados)
            {
                if (i.indiceEstado==estadoInicial.indiceEstado)
                {
                    aux = i;

                }
            }

            if (aux==null)
            {
                aux = estadoInicial;
                listaEstados.AddLast(aux);
            }
        }*/


        /*public Estados metodoThompson(Estados estado,LinkedList<Estados> listaAuxEstado)
        {
            Estados auxiliar, estado1, estado2, estado3, estado4, estado5;
            
            switch (listaAuxEstado.ElementAt(indice))
            {

            }



            return estado;
        }*/

       // public void OrdenarExR(LinkedList<TokenER> listaTokenER2)
        //{
            /*bool condicion = true;
            int contador = 1;
            while (condicion)
            {
                contador--;
                foreach (TokenER i in listaTokenER)
                {
                    if(i.nExpresion.Equals("Expresion " + contador.ToString()))
                    {
                        Console.WriteLine("---------------"+"Esta imprimiendo la Expresion "+contador.ToString()+"-------------");
                        Console.WriteLine(i.lexema + "  ===============   " + (i.tipo.ToString()) + "    ====================    " + i.nExpresion + "\n");
                    }
                    else
                    {
                        contador++;
                        //break;
                    }
                    
                        
                }
            }*/
           /* String Expresion1="";
            String Expresion2 = "";
            int contador = 0;
            foreach (TokenER i in listaTokenER2)
            {
                contador = 0;
                if (i.lexema.Equals("|"))
                {

                    while (contador !=6)
                    {
                        contador++;
                        listaEstados.AddLast(new Estados(Expresion1 + Expresion2 + "_"+contador.ToString(),contador));
                    }
                    foreach (Estados j in listaEstados)
                    {
                        if (j.nombre.Equals(Expresion1 + Expresion2 + "_" + 1.ToString()))
                        {
                            foreach (Estados z in listaEstados)
                            {
                                
                                if (z.nombre.Equals(Expresion1 + Expresion2 + "_" + 3.ToString()))
                                {
                                    j.listaTrancisiones.AddLast(new Trancisiones("ε", z));
                                }
                                else if (z.nombre.Equals(Expresion1 + Expresion2 + "_" + 5.ToString()))
                                {
                                    j.listaTrancisiones.AddLast(new Trancisiones("ε", z));
                                }
                            }
                            
                        }
                        else if(j.nombre.Equals(Expresion1 + Expresion2 + "_" + 2.ToString()))
                        {

                        }
                        else if (j.nombre.Equals(Expresion1 + Expresion2 + "_" + 3.ToString()))
                        {
                            foreach (Estados z in listaEstados)
                            {

                                if (z.nombre.Equals(Expresion1 + Expresion2 + "_" + 4.ToString()))
                                {
                                    j.listaTrancisiones.AddLast(new Trancisiones(Expresion1, z));
                                }
                                
                            }
                        }
                        else if (j.nombre.Equals(Expresion1 + Expresion2 + "_" + 4.ToString()))
                        {
                            foreach (Estados z in listaEstados)
                            {

                                if (z.nombre.Equals(Expresion1 + Expresion2 + "_" + 2.ToString()))
                                {
                                    j.listaTrancisiones.AddLast(new Trancisiones("ε", z));
                                }

                            }
                        }
                        else if (j.nombre.Equals(Expresion1 + Expresion2 + "_" + 5.ToString()))
                        {
                            foreach (Estados z in listaEstados)
                            {

                                if (z.nombre.Equals(Expresion1 + Expresion2 + "_" + 6.ToString()))
                                {
                                    j.listaTrancisiones.AddLast(new Trancisiones(Expresion2, z));
                                }

                            }
                        }
                        else if (j.nombre.Equals(Expresion1 + Expresion2 + "_" + 6.ToString()))
                        {
                            foreach (Estados z in listaEstados)
                            {

                                if (z.nombre.Equals(Expresion1 + Expresion2 + "_" + 2.ToString()))
                                {
                                    j.listaTrancisiones.AddLast(new Trancisiones("ε", z));
                                }

                            }
                        }
                    }


                }else if (i.lexema.Equals("."))
                {

                }
                else if (i.lexema.Equals("+"))
                {

                }
                else if (i.lexema.Equals("*"))
                {

                }
                else if (i.lexema.Equals("?"))
                {

                }
                else 
                {
                    if (Expresion1!="")
                    {
                        Expresion1 = i.lexema;

                    }else if (Expresion2!="")
                    {
                        Expresion2 = i.lexema;
                    }
                    
                }
            }

            Graficar();
        }*/

       /* public void GraficarOr(TextWriter escribir)
        {
            //String ruta2 = Application.StartupPath + "\\Thompson" + "\\" + "Thompson.dot";
            foreach (Estados i in listaEstados)
            {
                

            }



        }*/
        public void reporteErroresXML()
        {
            TextWriter escribir;
            String ruta = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Reporte" + "\\" + "ReporteErrores"+indiceReporte+".xml";
            String ruta2 = Application.StartupPath + "\\Reporte" + "\\" + "ReporteErrores"+indiceReporte+".xml";
            String ruta4 = Application.StartupPath + @"\Reporte";
            try
            {
                if (Directory.Exists(ruta4))
                {
                    MessageBox.Show("Carpeta Reporte Existe");
                }
                else
                {
                    MessageBox.Show("Carpeta Reporte No Existe, Creando....");
                    Directory.CreateDirectory(ruta4);
                }
            }
            catch (Exception e)
            {

            }
            escribir = new StreamWriter(ruta2);

            escribir.WriteLine("<ListaErrores>");


            for (int i = 0; i < listaErrores.Count; i++)
            {
                if (!listaErrores.ElementAt(i).error.Equals("<"))
                {
                    escribir.WriteLine("<Token>");
                    
                    escribir.WriteLine("<Valor>" + "\" " + listaErrores.ElementAt(i).error + " \"" + "</Valor>");
                    escribir.WriteLine("<Fila>" + "\" " + listaErrores.ElementAt(i).fila + " \"" + "</Fila>");
                    escribir.WriteLine("<Columna>" + "\" " + listaErrores.ElementAt(i).columna + " \"" + "</Columna>");

                    escribir.WriteLine("</Token>");

                }

            }
            escribir.WriteLine("</ListaErrores>");

            escribir.Close();
            Process.Start(ruta2);
        }
        public void reporteTokensXML()
        {
            TextWriter escribir;
            String ruta = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Reporte" + "\\" + "ReporteToken"+indiceReporte+".xml";
            String ruta2 = Application.StartupPath + "\\Reporte" + "\\" + "ReporteToken"+indiceReporte+".xml";
            String ruta4 = Application.StartupPath + @"\Reporte";
            try
            {
                if (Directory.Exists(ruta4))
                {
                    MessageBox.Show("Carpeta Reporte Existe");
                }
                else
                {
                    MessageBox.Show("Carpeta Reporte No Existe, Creando....");
                    Directory.CreateDirectory(ruta4);
                }
            }
            catch (Exception e)
            {

            }
            escribir = new StreamWriter(ruta2);

            escribir.WriteLine("<ListaTokens>");
            

            for (int i = 0; i < listaTokens.Count; i++)
            {
                if (!listaTokens.ElementAt(i).lexema.Equals("<"))
                {
                    escribir.WriteLine("<Token>");
                    escribir.WriteLine("<Nombre>" + listaTokens.ElementAt(i).lexema + "</Nombre>");
                    escribir.WriteLine("<Valor>" + "\" " + listaTokens.ElementAt(i).tipo1 + " \"" + "</Valor>");
                    escribir.WriteLine("<Fila>" + "\" " + listaTokens.ElementAt(i).fila + " \"" + "</Fila>");
                    escribir.WriteLine("<Columna>" + "\" " + listaTokens.ElementAt(i).columna + " \"" + "</Columna>");

                    escribir.WriteLine("</Token>");

                }
                
            }
            escribir.WriteLine("</ListaTokens>");
            
            escribir.Close();
            Process.Start(ruta2);

        }

       public void reporteHtml()
        {
            indiceReporte++;
            TextWriter escribir;
            String ruta = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Reporte" + "\\" + "ReporteToken"+indiceReporte+".html";
            String ruta2 = Application.StartupPath + "\\Reporte" + "\\" + "ReporteToken"+indiceReporte+".html";
            String ruta4 = Application.StartupPath + @"\Reporte";
            try
            {
                if (Directory.Exists(ruta4))
                {
                    MessageBox.Show("Carpeta Reporte Existe");
                }
                else
                {
                    MessageBox.Show("Carpeta Reporte No Existe, Creando....");
                    Directory.CreateDirectory(ruta4);
                }
            }
            catch (Exception e)
            {

            }
            escribir = new StreamWriter(ruta2);
            escribir.WriteLine("<html>");
            escribir.WriteLine("<head><title>REPORTE</title></head>");
            escribir.WriteLine("<body>");
            escribir.WriteLine("<h1 align=\"center\">TOKENS</h1>");
            escribir.WriteLine("\"<div align=\"center\">");
            escribir.WriteLine("\"<div align=\"center\">");
            escribir.WriteLine("<table border=\"2\" width=\"1000\" bordercolor=\"#EF1D21\" bgcolor=\"#86CED1\">");
            escribir.WriteLine("<tr height = \"50\" bgcolor=\"#3578CA\">");
            escribir.WriteLine("<th width = \"200\" > No. </th>");
            escribir.WriteLine("<th width = \"200\" > Lexema </th>");
            escribir.WriteLine("<th width = \"200\" > Tipo </th>");
            escribir.WriteLine("<th width = \"200\" > Columna </th>");
            escribir.WriteLine("<th width = \"200\" > Línea </th>");
            escribir.WriteLine("</tr>");
            
            for (int i=0;i< listaTokens.Count;i++)
            {
                escribir.WriteLine(@"<tr height=""30"" bgcolor=""#86CED1"" align=""center"">");
                escribir.WriteLine("<td>"+i+"</td>");
                escribir.WriteLine("<td>"+"\n" + "\n" + "\n" + listaTokens.ElementAt(i).lexema+ "\n" + "\n" + "\n"+"</td>");
                escribir.WriteLine("<td>" + listaTokens.ElementAt(i).tipo1 + "</td>");
                escribir.WriteLine("<td>" + listaTokens.ElementAt(i).columna + "</td>");
                escribir.WriteLine("<td>" + listaTokens.ElementAt(i).fila + "</td>");
                escribir.WriteLine("</tr>");
            }
            escribir.WriteLine("</body>");
            escribir.WriteLine("</html>");
            escribir.Close();
            Process.Start(ruta2);
        }

        public void ReporteErrores()
        {
            TextWriter escribir;
            String ruta = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Reporte" + "\\" + "ReporteErrores"+indiceReporte+".html";
            String ruta2 = Application.StartupPath + "\\Reporte" + "\\" + "ReporteErrores"+indiceReporte+".html";
            String ruta4 = Application.StartupPath + @"\Reporte";
            try
            {
                if (Directory.Exists(ruta4))
                {
                    MessageBox.Show("Carpeta Reporte Existe");
                }
                else
                {
                    MessageBox.Show("Carpeta Reporte No Existe, Creando....");
                    Directory.CreateDirectory(ruta4);
                }
            }
            catch (Exception e)
            {

            }
            escribir = new StreamWriter(ruta2);
            escribir.WriteLine("<html>");
            escribir.WriteLine("<head><title>REPORTE</title></head>");
            escribir.WriteLine("<body>");
            escribir.WriteLine("<h1 align=\"center\">ERRORES</h1>");
            escribir.WriteLine("\"<div align=\"center\">");
            escribir.WriteLine("\"<div align=\"center\">");
            escribir.WriteLine("<table border=\"2\" width=\"1000\" bordercolor=\"#EF1D21\" bgcolor=\"#86CED1\">");
            escribir.WriteLine("<tr height = \"50\" bgcolor=\"#3578CA\">");
            escribir.WriteLine("<th width = \"200\" > No. </th>");
            escribir.WriteLine("<th width = \"200\" > Lexema </th>");
            escribir.WriteLine("<th width = \"200\" > Tipo </th>");
            escribir.WriteLine("<th width = \"200\" > Descripcion</th>");
            escribir.WriteLine("<th width = \"200\" > Columna </th>");
            escribir.WriteLine("<th width = \"200\" > Línea </th>");
            escribir.WriteLine("</tr>");

            for (int i = 0; i < listaErrores.Count; i++)
            {
                escribir.WriteLine(@"<tr height=""30"" bgcolor=""#86CED1"" align=""center"">");
                escribir.WriteLine("<td>" + i + "</td>");
                escribir.WriteLine("<td>" + listaErrores.ElementAt(i).error+ "</td>");
                escribir.WriteLine("<td>" + listaErrores.ElementAt(i).tipo + "</td>");
                escribir.WriteLine("<td>" + listaErrores.ElementAt(i).descripcion + "</td>");
                escribir.WriteLine("<td>" + listaErrores.ElementAt(i).columna + "</td>");
                escribir.WriteLine("<td>" + listaErrores.ElementAt(i).fila + "</td>");
                escribir.WriteLine("</tr>");
            }
            escribir.WriteLine("</body>");
            escribir.WriteLine("</html>");
            escribir.Close();
            Process.Start(ruta2);
        }
        

        public void Graficar()
        {
            TextWriter escribir;
            String ruta = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)+"\\Thompson"+"\\"+"Thompson.dot";
            String ruta2 = Application.StartupPath+ "\\Thompson" + "\\" + "Thompson.dot";
            String ruta4 = Application.StartupPath + @"\Thompson";
            try
            {
                if (Directory.Exists(ruta4))
                {
                    MessageBox.Show("Carpeta Existe");
                }
                else
                {
                    MessageBox.Show("Carpeta No Existe, Creando....");
                    Directory.CreateDirectory(ruta4);
                }
            }
            catch (Exception e)
            {

            }
            escribir = new StreamWriter(ruta2);
            escribir.WriteLine("digraph G {\n");
            ///////////////////
            //GraficarOr(escribir);
            //escribir.WriteLine("\""+"Welcome"+"\""+"->" +"\""+"To"+ "\"" +"\n" +"\""+"To"+"\""+"->" +"\""+"Web"+"\"" +"\n" +"\""+"To"+"\""+"->" +"\""+"GraphViz!"+"\"");
            escribir.WriteLine("}");
            escribir.Close();

            String rutaImagen = Application.StartupPath + "//Thompson" + "//" + "ThompsonImagen.jpg";
            String rutaDot = Application.StartupPath + "//Thompson" + "//" + "Thompson.dot";
            //String rutaImagen2 = "C:/Users/USUARIO/source/repos/[OLC1]Proyecto1/[OLC1]Proyecto1/bin/Debug/Thompson/Thompsonimagen.jpg";
            //String rutaDot2 = "C:/Users/USUARIO/source/repos/[OLC1]Proyecto1/[OLC1]Proyecto1/bin/Debug/Thompson/Thompson.dot";
            //String rutaImagen3 = "C:\\Users\\USUARIO\\source\\repos\\[OLC1]Proyecto1\\[OLC1]Proyecto1\\bin\\Debug\\Thompson\\Thompsonimagen.jpg";
            //String rutaDot3 = "C:\\Users\\USUARIO\\source\\repos\\[OLC1]Proyecto1\\[OLC1]Proyecto1\\bin\\Debug\\Thompson\\Thompson.dot";

            /* try
             {
                 // create the ProcessStartInfo using "cmd" as the program to be run,
                 // and "/c " as the parameters.
                 // Incidentally, /c tells cmd that we want it to execute the command that follows,
                 // and then exit.
                 System.Diagnostics.ProcessStartInfo procStartInfo =
                     new System.Diagnostics.ProcessStartInfo("cmd", "/c " + " dot - Tjpg " + rutaDot2 + " - o " + rutaImagen2);

                 // The following commands are needed to redirect the standard output.
                 // This means that it will be redirected to the Process.StandardOutput StreamReader.
                 procStartInfo.RedirectStandardOutput = true;
                 procStartInfo.UseShellExecute = false;
                 // Do not create the black window.
                 procStartInfo.CreateNoWindow = false;
                 // Now we create a process, assign its ProcessStartInfo and start it
                 System.Diagnostics.Process proc = new System.Diagnostics.Process();
                 proc.StartInfo = procStartInfo;
                 proc.Start();
                 // Get the output into a string
                 string result = proc.StandardOutput.ReadToEnd();
                 // Display the command output.
                 Console.WriteLine(result);
             }
             catch (Exception objException)
             {
                 // Log the exception
             }*/
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.Start();
            process.StandardInput.WriteLine("dot -Tjpg " +rutaDot +" -o " +rutaImagen);
            process.StandardInput.Flush();
            process.StandardInput.Close();
            process.WaitForExit();
            Console.WriteLine(process.StandardOutput.ReadToEnd());
            //Console.ReadKey();



        }



    }
}
