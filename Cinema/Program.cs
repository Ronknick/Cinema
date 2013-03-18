using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Intestazione;
namespace ConsoleApplication24
{
    class Program
    {
        static int carattere = 65;
        static Random gen = new Random();
        static bool[,] occupato = new bool[30, 20];
        static List<int> omini = new List<int>();
        static string[] menuZone = { "galleria", "Platea, posti dietro", "Platea, posti davanti" };
        static string[] menuSiNo = { "SI", "NO" };
        static int guadagno = 0;
        static int velOmini = 0;
        static bool teatro = true;
        //Metodi
        static void costruisciPostiASedere()
        {

            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(0, 0);
            Console.Write("█████E");

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.SetCursorPosition(20, 0);
            Console.Write("  ");
            for (int j = 0; j < 20; j++)
            {
                if (j > 9)
                    Console.Write(j);
                else
                    Console.Write(" " + j);
            }
            Console.SetCursorPosition(20, 1);
            for (int i = 0; i < 10; i++)
            {
                Console.Write("G" + Convert.ToChar(carattere));
                Console.Write("║");
                for (int j = 0; j < 20; j++)
                {

                    Console.Write("█║");
                }
                Console.SetCursorPosition(20, Console.CursorTop + 2);
                carattere++;
            }
            carattere = 65;
            Console.SetCursorPosition(20, Console.CursorTop + 2);
            Console.ForegroundColor = ConsoleColor.Red;
            for (int i = 0; i < 20; i++)
            {
                Console.Write("P" + Convert.ToChar(carattere));
                Console.Write("║");
                for (int j = 0; j < 20; j++)
                {
                    Console.Write("█║");
                }
                Console.SetCursorPosition(20, Console.CursorTop + 2);
                carattere++;
            }



        }

        static void posizioniCasuali()
        {
            for (int i = 0; i < 30; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    if (gen.Next(0, 1) == 0)
                    {
                        occupato[i, j] = true;
                        creaOmini(i, j);
                        muoviOmino();
                        muoviOmino();
                    }
                }
            }
            while (omini.Count > 0)
            {
                muoviOmino();
            }

        }

        static void creaOmini(int lunghezza, int larghezza)
        {
            Console.SetCursorPosition(1, 1);
            Console.Write("O");
            if (lunghezza < 10)
            {

                lunghezza = lunghezza * 2 + 2;

            }
            else
            {

                lunghezza = lunghezza * 2 + 4;
            }

            omini.Add((larghezza * 2 + 23) * 1000000 + lunghezza * 10000 + 100 + 1);
        }

        static void muoviOmino()
        {
            bool entra;
            int posizioneX;
            int posizioneY;
            int posizioneXArrivo;
            int posizioneYArrivo;
            for (int i = 0; i < omini.Count; i++)
            {
                entra = true;
                posizioneY = omini[i] % 100;
                posizioneX = ((omini[i] - (omini[i] / 10000) * 10000)) / 100;
                posizioneYArrivo = (omini[i] - (omini[i] / 1000000 * 1000000)) / 10000;
                posizioneXArrivo = omini[i] / 1000000;

                if (((posizioneX > 18 && posizioneX < posizioneXArrivo) || (posizioneX < 18)) || (posizioneX == 18 && posizioneY == posizioneYArrivo))
                {

                    Console.MoveBufferArea(posizioneX, posizioneY, 1, 1, posizioneX + 1, posizioneY);
                    posizioneX++;
                }
                else if (posizioneX == 18 && posizioneY != posizioneYArrivo)
                {
                    Console.MoveBufferArea(posizioneX, posizioneY, 1, 1, posizioneX, posizioneY + 1);
                    posizioneY++;
                }
                else if (posizioneX == posizioneXArrivo && posizioneY == posizioneYArrivo)
                {
                    Console.MoveBufferArea(posizioneX, posizioneY, 1, 1, posizioneX, posizioneY - 1);
                    omini.RemoveAt(i);
                    i--;
                    entra = false;
                }
                if (entra)
                    omini[i] = posizioneXArrivo * 1000000 + posizioneYArrivo * 10000 + posizioneX * 100 + posizioneY;

            }
            System.Threading.Thread.Sleep(velOmini);


        }

        static void richiestaPosti()
        {
            bool esciTryCatch;
            bool esci = false;
            int uscire = 0;
            int numMinori = 0, numPersone = 0, zona = 0;
            Console.ForegroundColor = ConsoleColor.Black;
            do
            {
                Console.SetCursorPosition(0, 65);
                do
                {
                    try
                    {
                        esciTryCatch = true;
                        Console.Write("Numero di persone: ");
                        numPersone = Convert.ToInt32(Console.ReadLine());
                        esciTryCatch = richiestaZona(numPersone, ref zona);
                    }
                    catch (FormatException)
                    {

                        Console.WriteLine("Scrivi solo numeri!");
                        esciTryCatch = false;
                        Intestazione.Intestazione.aspetta(500);
                        pulisci(65, 67);
                        Console.SetCursorPosition(0, 65);
                    }
                    catch (OverflowException)
                    {
                        Console.WriteLine("Non scrivere numeri troppo alti!");
                        esciTryCatch = false;
                        Intestazione.Intestazione.aspetta(500);
                        pulisci(65, 67);
                        Console.SetCursorPosition(0, 65);

                    }
                } while (!esciTryCatch);


                if (Console.CursorTop == 66)
                    Console.SetCursorPosition(0, 67);
                else
                    Console.SetCursorPosition(0, 66);

                do
                {
                    try
                    {
                        esciTryCatch = true;
                        Console.Write("Quanti minori di 14 anni: ");
                        numMinori = Convert.ToInt32(Console.ReadLine());
                        if (numPersone < numMinori)
                        {
                            esciTryCatch = false;
                            Console.WriteLine("Non possono esserci più minori che persone!");
                            Intestazione.Intestazione.aspetta(800);
                            pulisci(66, 68);
                            Console.SetCursorPosition(0, 66);
                        }
                    }
                    catch (FormatException)
                    {

                        Console.WriteLine("Scrivi solo numeri!");
                        esciTryCatch = false;
                        Intestazione.Intestazione.aspetta(500);
                        pulisci(66, 68);
                        Console.SetCursorPosition(0, 66);

                    }
                    catch (OverflowException)
                    {
                        Console.WriteLine("Non scrivere numeri troppo alti!");
                        esciTryCatch = false;
                        Intestazione.Intestazione.aspetta(500);
                        pulisci(66, 68);
                        Console.SetCursorPosition(0, 66);

                    }
                } while (!esciTryCatch);
                getPrice(numPersone, numMinori, zona);
                Console.Write("Uscire: ");
                uscire = Intestazione.Intestazione.menuTabulazione(menuSiNo, "nero", "grigio scuro");

                if (uscire == 0)
                    esci = true;
                pulisci(65, Console.CursorTop + 1);
            } while (!esci);


        }

        static void getPrice(int persone, int minori, int zona)
        {
            bool esciTryCatch = true;
            int prezzo = 0, resto;
            int soldInseriti = 0; ;
            switch (zona)
            {
                case 1:
                    prezzo += (persone - minori) * 7;
                    prezzo += minori * 3;

                    break;
                case 2:
                    prezzo += (persone - minori) * 8;
                    prezzo += minori * 4;

                    break;
                case 3:
                    prezzo += (persone - minori) * 10;
                    prezzo += minori * 5;
                    break;
            }
            guadagno += prezzo;

            Console.WriteLine("Costo biglietti: " + prezzo + " Euro.");
            do
            {
                try
                {
                    esciTryCatch = true;
                    Console.Write("Soldi consegnati: ");
                    soldInseriti = Convert.ToInt32(Console.ReadLine());
                    if (prezzo > soldInseriti)
                    {
                        esciTryCatch = false;
                        Console.WriteLine("Devi inserire abbastanza soldi per pagere i biglietti!");
                        Intestazione.Intestazione.aspetta(800);
                        pulisci(68, 70);
                        Console.SetCursorPosition(0, 68);
                    }
                }
                catch (FormatException)
                {

                    Console.WriteLine("Scrivi solo numeri!");
                    esciTryCatch = false;
                    Intestazione.Intestazione.aspetta(500);
                    pulisci(68, 70);
                    Console.SetCursorPosition(0, 68);

                }
                catch (OverflowException)
                {
                    Console.WriteLine("Non scrivere numeri troppo alti!");
                    esciTryCatch = false;
                    Intestazione.Intestazione.aspetta(500);
                    pulisci(68, 70);
                    Console.SetCursorPosition(0, 68);

                }
            } while (!esciTryCatch);
            resto = soldInseriti - prezzo;
            Console.Write("Resto:");
            if (resto / 500 > 0)
            {
                Console.Write(resto / 500 + " 500 Euro,");
                resto = resto % 500;
            }
            if (resto / 200 > 0)
            {
                Console.Write(" " + resto / 200 + " 200 Euro,");
                resto = resto % 200;
            }
            if (resto / 100 > 0)
            {
                Console.Write(" " + resto / 100 + " 100 Euro,");
                resto = resto % 100;
            }
            if (resto / 50 > 0)
            {
                Console.Write(" " + resto / 50 + " 50 Euro,");
                resto = resto % 50;
            }
            if (resto / 20 > 0)
            {
                Console.Write(" " + resto / 20 + " 20 Euro,");
                resto = resto % 20;
            }
            if (resto / 10 > 0)
            {
                Console.Write(" " + resto / 10 + " 10 Euro,");
                resto = resto % 10;
            }
            if (resto / 5 > 0)
            {
                Console.Write(" " + resto / 5 + " 5 Euro,");
                resto = resto % 10;
            }
            if (resto / 2 > 0)
            {
                Console.Write(" " + resto / 2 + " 2 Euro,");
                resto = resto % 2;
            }
            if (resto / 1 > 0)
            {
                Console.Write(" " + resto / 1 + " 1 Euro ");
                resto = resto % 1;
            }
            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
            Console.Write(".");
            Console.SetCursorPosition(0, Console.CursorTop + 1);
        }
        static void pulisci(int primaRiga, int secondaRiga)
        {
            Console.SetCursorPosition(0, primaRiga);
            string spazi = "";
            for (int i = primaRiga; i < secondaRiga; i++)
            {
                for (int j = 0; j < Console.BufferWidth; j++)
                {
                    spazi += " ";
                }
            }
            Console.Write(spazi);
        }

        static bool richiestaZona(int persone, ref int punto)
        {
            punto = Intestazione.Intestazione.menuTabulazione(menuZone, "nero", "grigio scuro") + 1;
            return postiLiberi(persone, punto);

        }

        static bool postiLiberi(int persone, int zona)
        {
            zona *= 10;
            int scratch;
            int punto;
            int numPostiLiberi;
            int numeroPosto = 0;
            string[] menu;
            List<int> posti = new List<int>();
            for (int i = zona - 10; i < zona; i++)
            {
                numPostiLiberi = 0;
                for (int j = 0; j < 20; j++)
                {
                    if (!occupato[i, j])
                        numPostiLiberi++;
                    else
                        numPostiLiberi = 0;
                    if (numPostiLiberi >= persone)
                    {
                        posti.Add(persone * 10000 + j * 100 + i);
                    }
                }


            }
            menu = new string[posti.Count];
            string outputMenu = "";
            for (int i = 0; i < posti.Count; i++)
            {
                scratch = (((posti[i] - posti[i] / 10000 * 10000) / 100));
                if (zona == 10)
                    outputMenu = "G";
                else
                    outputMenu = "P";
                if (zona == 10)
                    outputMenu += Convert.ToChar(65 + posti[i] % 100);
                else
                    outputMenu += Convert.ToChar(65 + posti[i] % 100 - 10);
                if (scratch - persone < 9)
                    outputMenu += "0" + (scratch - persone + 1);
                else
                    outputMenu += "" + (scratch - persone + 1);
                outputMenu += "-";
                outputMenu += outputMenu.Substring(0, 2);
                if (scratch < 10)
                    outputMenu += "0" + scratch;
                else
                    outputMenu += "" + scratch;
                menu[i] = outputMenu;
            }
            if (posti.Count > 0)
            {
                punto = Intestazione.Intestazione.menuTabulazione(menu, "nero", "grigio scuro");
                scratch = ((posti[punto] - posti[punto] / 10000 * 10000) / 100);
                for (int i = 0; i < posti[punto] / 10000; i++)
                {
                    creaOmini(posti[punto] % 100, scratch - i);
                    occupato[posti[punto] % 100, scratch - i] = true;
                    muoviOmino();
                    muoviOmino();
                }
                finisciAnimazione();
                return true;
            }
            else
            {

                if (persone > 1)
                    Console.WriteLine("Non ci sono abbastanza posti vicini");
                else
                    Console.WriteLine("Non ci sono più posti");
                Intestazione.Intestazione.aspetta(750);
                pulisci(65, 67);
                Console.SetCursorPosition(0, 65);
                return false;
            }
        }

        static void finisciAnimazione()
        {
            while (omini.Count > 0)
            {
                muoviOmino();
            }
        }
        static void richiestaPreferenze()
        {
            string[] menuCinemaTeatro = { "Cinema", "Teatro" };
            int punto;
            Intestazione.Intestazione.centra("Avere effetti grafici: ", 3, "nero", "bianco");

            punto = Intestazione.Intestazione.menuTabulazione(menuSiNo, "nero", "grigio scuro");

            if (punto == 1)
                velOmini = 0;
            else
            {
                velOmini = 5;
                pulisci(62, 64);
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Intestazione.Intestazione.centra("Dove siamo: ", 7, "nero", "bianco");
                punto = Intestazione.Intestazione.menuTabulazione(menuCinemaTeatro, "nero", "grigio scuro");
                if (punto == 0)
                    teatro = false;
            }
            pulisci(62, 64);
        }
        static void fine()
        {

            if (velOmini > 0)
            {
                if (teatro)
                {
                 //Mancava giusto ora di lavore per fare anche questo ma sono troppo stanco, dopo quasi 4 ore consecutive son distrutto e vado a riposarmi :Ds
                 //   palcoscenico();
                }
                else
                {
                    cinema();
                }

            }
            Console.SetCursorPosition(0, 67);
            for (int i = 67; i < 82; i++)
            {
                Intestazione.Intestazione.centra("                                           \n", -3, "", "nero");

                if (i == 74)
                    Intestazione.Intestazione.centra("             Made by Ronknick©             \n", -3, "rosso scuro", "nero");

            }
            Intestazione.Intestazione.aspetta(3000);


        }
        static void cinema()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
            for (int i = 0; i < 50; i++)
            {
                Console.SetCursorPosition(21, 67);
                for (int j = 0; j < 15; j++)
                {
                    for (int k = 0; k < 43; k++)
                    {
                        //schermo[j, k] = 
                        Console.Write(Convert.ToChar(gen.Next(30, 110)));
                    }

                    Console.SetCursorPosition(21, Console.CursorTop + 1);

                }
                Intestazione.Intestazione.aspetta(250);

            }


        }
     /*   static void palcoscenico()
        {
            List<int> persone = new List<int>();
            persone.Add(gen.Next(7, 16) * 100);
            persone.Add(gen.Next(0, 8) * 100);
            bool girarsi = false;
            bool destra = true, sopra = true, sotto = true, sinistra = true;
            int xAttore1 = 0, yAttore1 = 0, xAttore2 = 0, yAttore2 = 0;
            int direzione = 0;
            int scratch = 0;
            bool salta = false;
            /*Console.SetCursorPosition(20,66);
             Console.Write("Γ")
            for(int i=0;i<41;i++){
                Console.Write()

            }
            for(){
                Console.SetCursorPosition(20,Console.CursorTop+1);

            }*/
            do
            {
                for (int i = 0; i < 50; i++)
                {

                    for (int j = 0; j < persone.Count; j++)
                    {
                        yAttore1 = persone[j] % 100;
                        xAttore1 = (persone[j] - yAttore1) % 10000;
                        for (int k = 0; k < persone.Count; k++)
                        {
                            yAttore2 = persone[k] % 100;
                            xAttore2 = (persone[k] - yAttore2) % 10000;

                            if (yAttore2 == yAttore1 + 1)
                                sopra = false;
                            if (yAttore2 == yAttore1 - 1)
                                sotto = false;
                            if (xAttore2 == xAttore1 + 1)
                                destra = false;
                            if (xAttore2 == xAttore1 - 1)
                                sinistra = false;
                        }

                        switch (persone[j] / 10000)
                        {
                            case 0:
                                if(yAttore1==14){
                                    sopra=false;
                                girarsi=true;
                                }
                                else{
                                    persone[j] += 100;
                                }
                                break;
                            case 1:
                                if (xAttore1 == 42)
                                {
                                    destra = false;
                                    girarsi = true;
                                }
                                else
                                {
                                    persone[j] += 1;
                                }
                                break;
                            case 2:
                                if(yAttore1==0){
                                    sotto=false;
                                girarsi=true;
                                }
                                else{
                                    persone[j]-=100;
                                }
                                break;
                            case 3:
                                if (xAttore1 == 0)
                                {
                                    sinistra = false;
                                    girarsi = true;
                                }
                                else
                                {
                                    persone[j] -= 1;
                                }
                                break;

                        }
                        if (girarsi)
                        {
                            
                            direzione = gen.Next(0, 4); 
                            do{
                                if (persone[j] / 10000 == direzione)
                                    salta = true;

                                if (salta)
                                {
                                    switch (direzione)
                                    {
                                        case 0:
                                            if (sopra)
                                                persone[j] = persone[j] % 10000;
                                            break;
                                        case 1:
                                            if (destra)
                                                persone[j] = 10000+persone[j] % 10000;
                                            break;
                                        case 2:
                                            if (sotto)
                                                persone[j] = 20000 + persone[j] % 10000;
                                            break;
                                        case 3:
                                            if (sotto)
                                                persone[j] = 30000 + persone[j] % 10000;
                                            break;

                                    }
                                }
                                direzione++;
                                if (direzione == 4)
                                    direzione = 0;
                                scratch++;
                            } while(!(scratch==4));

                        }
                        

                    }
                    if (gen.Next(0, 100) < 5)
                        persone.Add(gen.Next(0, 15) * 100);
                    stampAttore( persone);
                }


            } while (persone.Count == 0);



        }

        static void stampAttore(List<int> attori)
        {
            for (int j = 0; j < attori.Count; j++)
            {
                Console.SetCursorPosition(((attori[j] - attori[j]%100) % 10000)/100+20, 15-(attori[j]%100)+67);
                Console.Write("O");
                Intestazione.Intestazione.aspetta(250);
            }


        }
        */
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();
            Console.SetWindowSize(80, 68);
            Console.SetBufferSize(80, 102);

            costruisciPostiASedere();
            richiestaPreferenze();
            richiestaPosti();
            fine();
            Console.ReadLine();
        }
    }
}
