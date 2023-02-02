using System;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace piedra_papel_tijera {
    
    class Program {

        static void Main(string[] args) {

            Menu();

        }


        static void Menu() {

            int rondas = 0;

            int empates = 0;

            int usuario_victorias = 0;
            int usuario_victorias_contador = 0;
            int usuario_victorias_consecutivas = 0;
            int usuario_piedra = 0;
            int usuario_papel = 0;
            int usuario_tijera = 0;

            int oponente_victorias = 0;
            int oponente_victorias_contador = 0;
            int oponente_victorias_consecutivas = 0;
            int oponente_piedra = 0;
            int oponente_papel = 0;
            int oponente_tijera = 0;

            int opcion = 9;

            List<string> usuario_movimientos;
            Dificultad dificultad = Dificultad.Facil;

            while (opcion != 4) {

                try {

                    Console.WriteLine("\n\t" + "Piedra, Papel, Tijera" + "\n");

                    Console.WriteLine("\t" + "1. Jugar");
                    Console.Write("\t" + "2. Escoger dificultad ");

                    switch (dificultad) {
                        case Dificultad.Facil: Console.ForegroundColor = ConsoleColor.Green; Console.Write("o"); Console.ResetColor(); break;
                        case Dificultad.Media: Console.ForegroundColor = ConsoleColor.DarkYellow; Console.Write("o"); Console.ResetColor(); break;
                        case Dificultad.Dificil: Console.ForegroundColor = ConsoleColor.Red; Console.Write("o"); Console.ResetColor(); break;
                    }

                    Console.WriteLine("\n\t" + "3. Mostrar estadísticas");
                    Console.WriteLine("\t" + "4. Salir");

                    Console.WriteLine("\n\t" + "¿Qué quieres hacer?");
                    Console.Write("\n\t" + ">> ");

                    opcion = int.Parse(Console.ReadLine());

                    switch (opcion) {

                        case 1:

                            Console.Clear();

                            Console.Write("\n\t" + "Dificultad: ");

                            switch (dificultad) {
                                case Dificultad.Facil: Console.ForegroundColor = ConsoleColor.Green; Console.Write("Fácil" + "\n"); Console.ResetColor(); break;
                                case Dificultad.Media: Console.ForegroundColor = ConsoleColor.DarkYellow; Console.Write("Media" + "\n"); Console.ResetColor(); break;
                                case Dificultad.Dificil: Console.ForegroundColor = ConsoleColor.Red; Console.Write("Difícil" + "\n"); Console.ResetColor(); break;
                            }

                            string entrada = "algo";

                            while (!entrada.Equals("")) {

                                try {

                                    Console.WriteLine("\n\t" + "Ingrese su jugada (piedra, papel, tijera): ");
                                    Console.Write("\n\t" + ">> ");

                                    string usuario_movimiento = Console.ReadLine().ToLower();

                                    usuario_movimiento = obtener_movimiento_usuario(usuario_movimiento);

                                    entrada = usuario_movimiento;

                                    if (usuario_movimiento.Equals("")) {

                                        Console.Clear();
                                        break;

                                    } else if (!usuario_movimiento.Equals("piedra") && !usuario_movimiento.Equals("papel") && !usuario_movimiento.Equals("tijera")) {

                                        Console.WriteLine("\n\t" + "Entrada incorrecta");

                                    } else {

                                        rondas++;

                                        usuario_movimientos = new List<string>();
                                        string oponente_movimiento = obtener_movimiento_computadora(dificultad, usuario_movimientos);
                                        Console.WriteLine("\n\t" + "Oponente juega " + oponente_movimiento);

                                        if (usuario_movimiento.Equals("piedra")) { usuario_piedra++; }
                                        if (usuario_movimiento.Equals("papel")) { usuario_papel++; }
                                        if (usuario_movimiento.Equals("tijera")) { usuario_tijera++; }

                                        if (oponente_movimiento.Equals("piedra")) { oponente_piedra++; }
                                        if (oponente_movimiento.Equals("papel")) { oponente_papel++; }
                                        if (oponente_movimiento.Equals("tijera")) { oponente_tijera++; }

                                        string resultado = obtener_resultado(usuario_movimiento, oponente_movimiento);
                                        Console.WriteLine(resultado + "\n\n\t" + "--------------------------------------------");

                                        if (resultado.Contains("!")) {

                                            usuario_victorias_contador++;

                                            if (usuario_victorias_contador > usuario_victorias_consecutivas) {

                                                usuario_victorias_consecutivas = usuario_victorias_contador;

                                            }

                                        } else {

                                            usuario_victorias_contador = 0;

                                        }

                                        if (resultado.Contains(",")) {

                                            oponente_victorias_contador++;

                                            if (oponente_victorias_contador > oponente_victorias_consecutivas) {

                                                oponente_victorias_consecutivas = oponente_victorias_contador;

                                            }

                                        } else {

                                            oponente_victorias_contador = 0;

                                        }

                                        actualizar_estadísticas(resultado, ref usuario_victorias, ref usuario_victorias_consecutivas, ref oponente_victorias, ref oponente_victorias_consecutivas, ref empates, ref rondas, ref usuario_piedra, ref usuario_papel, ref usuario_tijera, ref oponente_piedra, ref oponente_papel, ref oponente_tijera);

                                    }

                                } catch {

                                    Console.WriteLine("alo");
                                }

                            }

                            break;

                        case 2:

                            Console.Clear();

                            Console.WriteLine("\n\t" + "Elige la dificultad" + "\n");

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("\n\t" + "o ");
                            Console.ResetColor();

                            Console.Write("Fácil");

                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write("\n\t" + "o ");
                            Console.ResetColor();

                            Console.Write("Media");

                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("\n\t" + "o ");
                            Console.ResetColor();

                            Console.Write("Difícil");

                            Console.Write("\n" + "\n" + "\t" + ">> ");

                            string nueva_dificultad = Console.ReadLine().ToLower();
                            dificultad = obtener_dificultad(nueva_dificultad);
                            Console.Clear();

                            break;

                        case 3:

                            Console.Clear();
                            mostrar_estadísticas(usuario_victorias, usuario_victorias_consecutivas, oponente_victorias, oponente_victorias_consecutivas, empates, rondas, usuario_piedra, usuario_papel, usuario_tijera, oponente_piedra, oponente_papel, oponente_tijera);
                            break;

                        case 4:

                            Console.Clear();
                            Console.Write("\n\t" + "Cerrando programa");

                            for (int i = 0; i < 3; i++) {

                                Console.Write(".");
                                Thread.Sleep(1000);

                            }

                            Console.Write("\n" + "\n" + "\n" + "\t");
                            Console.ReadKey();
                            break;

                        default:

                            Console.WriteLine("\n\t" + "Opción no válida, inténtalo de nuevo");
                            break;

                    }

                } catch (FormatException ex) {

                    Console.WriteLine("\n\t" + "Entrada inválida. Por favor, introduzca un número.");

                } catch (Exception ex) {

                    Console.WriteLine("\n\t" + "Ocurrió un error: " + ex.Message);

                }

            }

        }


        static string obtener_movimiento_computadora(Dificultad dificultad, List<string> usuario_movimientos) {

            Random aleatorio = new Random();
            string movimiento_computadora = "";

            switch (dificultad) {

                case Dificultad.Facil:

                    movimiento_computadora = obtener_numero_aleatorio();
                    break;

                case Dificultad.Media:

                    // Compruebe si el usuario tiene un patrón en sus movimientos.
                    if (usuario_movimientos.Count >= 3) {

                        if (usuario_movimientos[usuario_movimientos.Count - 1] == usuario_movimientos[usuario_movimientos.Count - 2] &&
                            usuario_movimientos[usuario_movimientos.Count - 2] == usuario_movimientos[usuario_movimientos.Count - 3]) {

                            // Si el usuario tiene un patrón, contrarrestelo.
                            movimiento_computadora = movida_oponente(usuario_movimientos[usuario_movimientos.Count - 1]);

                        } else {

                            //  De lo contrario, haz un movimiento aleatorio.
                            movimiento_computadora = obtener_numero_aleatorio();

                        }
                    } else {

                        // Si no hay suficientes datos, haga un movimiento aleatorio
                        movimiento_computadora = obtener_numero_aleatorio();

                    }

                    break;

                case Dificultad.Dificil:

                    // Realizar un seguimiento de los patrones en los movimientos del usuario
                    Dictionary<string, int> patrones = new Dictionary<string, int>();

                    for (int i = 0; i < usuario_movimientos.Count - 2; i++) {

                        string patron = usuario_movimientos[i] + usuario_movimientos[i + 1] + usuario_movimientos[i + 2];

                        if (patrones.ContainsKey(patron)) {

                            patrones[patron]++;

                        } else {

                            patrones.Add(patron, 1);

                        }

                    }

                    // Predecir el próximo movimiento del usuario
                    if (patrones.Count > 0) {

                        string patron_mas_frequente = patrones.OrderByDescending(x => x.Value).First().Key;
                        movimiento_computadora = movida_oponente(patron_mas_frequente[2].ToString());

                    } else {

                        movimiento_computadora = obtener_numero_aleatorio();

                    }

                    break;

                default:

                    movimiento_computadora = obtener_numero_aleatorio();
                    break;

            }

            return movimiento_computadora;

        }

        static string obtener_numero_aleatorio() {

            Random aleatorio = new Random();
            int movimiento = aleatorio.Next(1, 4);

            switch (movimiento) {

                case 1: return "piedra";

                case 2: return "papel";

                case 3: return "tijera";

                default: return "piedra";

            }

        }

        static string movida_oponente(string movimiento) {

            switch (movimiento) {

                case "piedra": return "papel";

                case "papel": return "tijera";

                case "tijera": return "piedra";

                default: return "piedra";

            }

        }

        static string obtener_movimiento_usuario(string input) {

            switch (input.ToLower()) {

                case "piedra":
                case "rock":
                case "1":

                    return "piedra";

                case "papel":
                case "paper":
                case "2":

                    return "papel";

                case "tijera":
                case "scissors":
                case "3":

                    return "tijera";

                default:

                    return input;
            }

        }

        static Dificultad obtener_dificultad(string dificultad) {

            switch (dificultad.ToLower()) {

                case "facil":
                case "easy":
                case "1":

                    Console.Write("\n\t" + "Configurando dificultad a fácil");

                    for (int i = 0; i < 3; i++) {

                        Console.Write(".");
                        Thread.Sleep(1000);

                    }

                    return Dificultad.Facil;

                case "media":
                case "medium":
                case "2":

                    Console.Write("\n\t" + "Configurando dificultad a media");

                    for (int i = 0; i < 3; i++) {

                        Console.Write(".");
                        Thread.Sleep(1000);

                    }

                    return Dificultad.Media;

                case "difícil":
                case "hard":
                case "3":

                    Console.Write("\n\t" + "Configurando dificultad a difícil");

                    for (int i = 0; i < 3; i++) {

                        Console.Write(".");
                        Thread.Sleep(1000);

                    }

                    return Dificultad.Dificil;

                default:

                    Console.Write("\n\t" + "Dificultad inválida, configurando a fácil");

                    for (int i = 0; i < 3; i++) {

                        Console.Write(".");
                        Thread.Sleep(1000);

                    }

                    return Dificultad.Facil;

            }

        }

        static string obtener_resultado(string usuario_movimiento, string oponente_movimiento) {

            if (usuario_movimiento == oponente_movimiento) {

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("\n\t" + "Empate");
                Console.ResetColor();

                return "...";

            } else if (usuario_movimiento == "piedra" && oponente_movimiento == "tijera") {

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("\n\t" + "Ganaste");
                Console.ResetColor();

                return "! Piedra vence tijeras.";

            } else if (usuario_movimiento == "tijera" && oponente_movimiento == "papel") {

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("\n\t" + "Ganaste");
                Console.ResetColor();

                return "! Tijeras vence papel.";

            } else if (usuario_movimiento == "papel" && oponente_movimiento == "piedra") {

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("\n\t" + "Ganaste");
                Console.ResetColor();

                return "! Papel vence a piedra.";

            } else {

                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write("\n\t" + "Perdiste");
                Console.ResetColor();

                return ", " + oponente_movimiento + " vence " + usuario_movimiento + ".";

            }

        }
        static void actualizar_estadísticas(string resultado, ref int usuario_gana, ref int usuario_gana_consecutivamente, ref int oponente_gana, ref int oponente_gana_consecutivamente, ref int empate, ref int rondas, ref int usuario_piedra, ref int usuario_papel, ref int usuario_tijera, ref int oponente_piedra, ref int oponente_papel, ref int oponente_tijera) {

            if (resultado.Contains("!")) {

                usuario_gana++;

            } else if (resultado.Contains(",")) {

                oponente_gana++;

            } else {

                empate++;

            }

        }

        static void mostrar_estadísticas(int usuario_victorias, int usuario_victorias_consecutivas, int oponente_victorias, int oponente_victorias_consecutivas, int empates, int rondas, int usuario_piedra, int usuario_papel, int usuario_tijera, int oponente_piedra, int oponente_papel, int oponente_tijera) {

            string titulo_1 = "", titulo_2 = "Usuario", titulo_3 = "Máquina";
            int ancho_tabla = 66;

            Console.WriteLine("\n" + "\n" + "\n" + "\n" + "\t" + new string('-', ancho_tabla));

            Console.WriteLine("\t" + "{0,-22} {1,-22} {2, -22}", titulo_1, titulo_2, titulo_3);

            Console.WriteLine("\t" + new string('-', ancho_tabla));

            Console.WriteLine("\t" + "{0,-22} {1,-22} {2, -22}", "Victorias", usuario_victorias, oponente_victorias);
            Console.WriteLine("\t" + "{0,-22} {1,-22} {2, -22}", "Porcentaje victorias", Math.Round((usuario_victorias * 100.0 / (usuario_victorias + oponente_victorias))) + "%", Math.Round((oponente_victorias * 100.0 / (usuario_victorias + oponente_victorias))) + "%");
            Console.WriteLine("\t" + "{0,-22} {1,-22} {2, -22}", "Victorias consecutivas", usuario_victorias_consecutivas, oponente_victorias_consecutivas);
            Console.WriteLine("\t" + "{0,-22} {1,-22} {2, -22}", "Frecuencia piedra", Math.Round((double)usuario_piedra / rondas * 100) + "%", Math.Round((double)oponente_piedra / rondas * 100) + "%");
            Console.WriteLine("\t" + "{0,-22} {1,-22} {2, -22}", "Frecuencia papel", Math.Round((double)usuario_papel / rondas * 100) + "%", Math.Round((double)oponente_papel / rondas * 100) + "%");
            Console.WriteLine("\t" + "{0,-22} {1,-22} {2, -22}", "Frecuencia tijera", Math.Round((double)usuario_tijera / rondas * 100) + "%", Math.Round((double)oponente_tijera / rondas * 100) + "%");


            Console.WriteLine("\t" + new string('-', ancho_tabla));



            Console.WriteLine("\n\t" + "Rondas jugadas: " + rondas);

            Console.WriteLine("\n\t" + "Empates: " + empates);

            Console.ReadKey();
            Console.Clear();

        }

    }

    enum Dificultad {

        Facil,
        Media,
        Dificil

    }

}
