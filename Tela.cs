using System.Collections.Generic;
using XadrezConsole.tabuleiro;

using XadrezConsole.xadrez;
using XadrezConsole.tabuleiro;

namespace XadrezConsole
{
    class Tela
    {

        public static void imprimirPartida(PartidaDeXadrez partida)
        {
            tabuleiroStart(partida.tab);

            Console.WriteLine();
            imprimirPecasCapturadas(partida);
            Console.WriteLine();

            Console.WriteLine("Turno: " + partida.turno);

            if (!partida.terminada)
            {
                Console.WriteLine("Aguardando jogada: " + partida.jogadorAtual);

                if (partida.xeque)
                {
                    Console.WriteLine("XEQUE!!");
                }
            }
            else
            {
                Console.WriteLine("XEQUE MATE !!");
                Console.WriteLine("Vencedor: " + partida.jogadorAtual);
            }

        }

        public static void imprimirPecasCapturadas(PartidaDeXadrez partida)
        {
            Console.WriteLine("Peca Capturadas: ");
            Console.WriteLine();
            Console.Write("Brancas: ");
            imprimirConjunto(partida.pecasCapturadas(Cor.Branca));
            Console.WriteLine();
            Console.Write("Pretas: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            imprimirConjunto(partida.pecasCapturadas(Cor.Preta));
            Console.ForegroundColor = aux;
            Console.WriteLine();
        }

        public static void imprimirConjunto(HashSet<Peca> conjunto)
        {
            Console.Write("[");
            foreach (Peca x in conjunto)
            {
                Console.Write(x + " ");
            }

            Console.Write("]");
        }
        public static void tabuleiroStart(Tabuleiro tab)
        {
            Console.WriteLine("    _______________");
            Console.WriteLine();
            for (int i = 0; i < tab.linhas; i++)
            {
                Console.Write(8 - i + " ");
                Console.Write("| ");
                for (int j = 0; j < tab.colunas; j++)
                {
                    impromirPeca(tab.peca(i, j));
                }
                Console.Write("| ");
                Console.WriteLine();
            }
            Console.WriteLine("    _______________");
            Console.WriteLine("    A B C D E F G H");
        }

        public static PosicaoXadrez lerPosicaoXadrez()
        {
            string s = Console.ReadLine();

            if (string.IsNullOrEmpty(s))
            {
                throw new TabuleiroException("Erro de entrada vazia");
            }

            if (s.Length == 2)
            {
                char coluna = s[0];
                int linha;

                if (char.IsLetter(coluna) && int.TryParse(s[1] + "", out linha))
                {
                    return new PosicaoXadrez(coluna, linha);
                }
                else
                {
                    throw new TabuleiroException("Entrada Invertida !! Primeiro Letra depois Numero");
                }
            }
            else
            {
                throw new TabuleiroException("Entrada deve conter exatamente dois caracteres.");
            }
        }


        public static void impromirPeca(Peca peca)
        {
            if (peca == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (peca.cor == Cor.Branca)
                {
                    Console.Write(peca);
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(peca);
                    Console.ForegroundColor = aux;

                }
                Console.Write(" ");
            }
        }

        public static void tabuleiroStart(Tabuleiro tab, bool[,] posicoesPossiveis)
        {

            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;
            Console.WriteLine("    _______________");
            Console.WriteLine();
            for (int i = 0; i < tab.linhas; i++)
            {
                Console.Write(8 - i + " ");
                Console.Write("* ");
                for (int j = 0; j < tab.colunas; j++)
                {
                    if (posicoesPossiveis[i, j])
                    {
                        Console.BackgroundColor = fundoAlterado;
                    }
                    else
                    {
                        Console.BackgroundColor = fundoOriginal;
                    }

                    impromirPeca(tab.peca(i, j));
                    Console.BackgroundColor = fundoOriginal;
                }
                Console.Write("* ");
                Console.WriteLine();
            }
            Console.WriteLine("    _______________");
            Console.WriteLine("    A B C D E F G H");
            Console.BackgroundColor = fundoOriginal;
        }
    }
}
