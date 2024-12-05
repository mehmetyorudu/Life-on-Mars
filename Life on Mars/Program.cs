using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life_On_Mars
{
    internal class Program
    {
        static string operation3(string gnd)
        {
            string[] codons = new string[] {"GCT","GCG","GCA","GCC","CGT","CGC","CGA","CGG","AGA","AGG",
                                            "AAT","AAC","GAT","GAC","TGT","TGC","CAA","CAG","GAA","GAG",
                                            "GGT","GGC","GGA","GGG","CAT","CAC","ATT","ATA","ATC","CTT",
                                            "CTC","CTA","CTG","TTA","TTG","AAA","AAG","TTT","TTC","CCT",
                                            "CCC","CCA","CCG","TCT","TCC","TCA","TCG","AGT","AGC","ACT",
                                            "ACC","ACA","ACG","TGG","TAT","TAC","GTT","GTC","GTA","GTG"};
            string[] gender = new string[] { "GGG", "CCC", "AAA", "TTT" };
            string[] stopcodon = new string[] { "TAA", "TGA", "TAG" };
            Random random = new Random();
            int gnd1 = random.Next(gender.Length);
            int gnd2 = random.Next(gender.Length);
            int genumber = random.Next(1, 7);
            if (gnd == "male")
            {
                if (gender[gnd1] == "GGG" || gender[gnd1] == "CCC")
                {
                    while (gender[gnd2] == "GGG" || gender[gnd2] == "CCC")
                    {
                        gnd2 = random.Next(gender.Length);
                    }
                }
                else if (gender[gnd1] == "AAA" || gender[gnd1] == "TTT")
                {
                    while (gender[gnd2] != "GGG" && gender[gnd2] != "CCC")
                    {
                        gnd2 = random.Next(gender.Length);
                    }
                }
            }
            else if (gnd == "female")
            {
                if (gender[gnd1] == "AAA" || gender[gnd1] == "TTT")
                {
                    while (gender[gnd2] == "GGG" || gender[gnd2] == "CCC")
                    {
                        gnd2 = random.Next(gender.Length);
                    }
                }
                while (gender[gnd1] == "GGG" || gender[gnd1] == "CCC")
                {
                    gnd1 = random.Next(gender.Length);
                }
            }
            else
            {
                if (gender[gnd1] == "GGG" || gender[gnd1] == "CCC")
                {
                    while (gender[gnd2] == "GGG" || gender[gnd2] == "CCC")
                    {
                        gnd2 = random.Next(gender.Length);
                    }
                }
            }
            string[] firstgen = { "ATG", gender[gnd1], gender[gnd2], "TAG" };
            string[] totalgen = new string[99];
            string[] randomgen = new string[50];
            string temp1 = "";
            for (int i = 0; i < firstgen.Length; i++)
            {
                temp1 += firstgen[i];
            }
            totalgen[0] = temp1;
            for (int i = 1; i <= genumber; i++)
            {
                int codonumber = random.Next(1, 6);
                for (int j = 1; j <= codonumber; j++)
                {
                    int rndgen = random.Next(60);
                    randomgen[0] = "ATG";
                    randomgen[j] = codons[rndgen];
                }
                int stp = random.Next(stopcodon.Length);
                randomgen[codonumber + 1] = stopcodon[stp];
                string temp2 = "";
                for (int m = 0; m < codonumber + 2; m++)
                {
                    temp2 += randomgen[m];
                }
                totalgen[i] = temp2;
            }
            string dna = "";
            for (int i = 0; i <= genumber; i++)
            {
                dna += totalgen[i];
            }
            return dna;
        }
        static void operation4(string dna)
        {
            int kontrol1 = 1;
            bool error = false;
            Console.Write("DNA strand: ");
            string gen;
            void writing_dna()
            {
                while (kontrol1 <= dna.Length)
                {
                    Console.Write(dna[kontrol1 - 1]);
                    if (kontrol1 % 3 == 0)
                        Console.Write(' ');
                    kontrol1 += 1;
                }
                Console.WriteLine();
            }
            void Codon()
            {
                Console.WriteLine("Codon structure error.");
            }
            void Gen1()
            {
                gen = "";

                for (int i = dna.Length - 3; i <= dna.Length - 1; i++)
                    gen += dna[i];

                if (!(gen == "TAA" || gen == "TGA" || gen == "TAG"))
                    Console.WriteLine("Gene structure error.");
            }
            void Gen2()
            {
                gen = "";

                for (int i = 0; i < 3; i++)
                    gen += dna[i];

                if (gen != "ATG")
                    Console.WriteLine("Gene structure error.");
            }
            void Gen3()
            {
                gen = "";
                int start_nuc = 0;
                int stop_nuc = 5;

                for (int i = stop_nuc; i <= dna.Length - 1; i += 3)
                {
                    for (int j = start_nuc; j <= i; j++)
                        gen += dna[j];

                    if (gen == "ATGTAA" || gen == "ATGTGA" || gen == "ATGTAG")
                    {
                        Console.WriteLine("Gene structure is OK. (Not a BLOB DNA, but OK)");
                        break;
                    }

                    if (!(gen == "ATGTAA" || gen == "ATGTGA" || gen == "ATGTAG"))
                    {
                        Console.WriteLine("Gene structure is OK.");
                        break;
                    }
                }
            }
            void Gen4()
            {
                gen = "";

                int kontrol2 = 0;

                for (int i = 0; i < dna.Length; i++)
                {
                    gen += dna[i];
                    kontrol2++;
                    if (kontrol2 % 3 == 0)
                    {
                        if (gen == "ATG")
                        {
                            int kontrol3 = 0;
                            gen = "";
                            for (int j = kontrol2; j < dna.Length; j++)
                            {
                                gen += dna[j];
                                kontrol3++;
                                if (kontrol3 % 3 == 0)
                                {
                                    if (gen == "TAA" || gen == "TGA" || gen == "TAG")
                                    {
                                        gen = "";
                                        break;
                                    }
                                    else if (gen == "ATG")
                                    {
                                        Console.WriteLine("Gene structure error.");
                                        error = true;
                                        break;
                                    }
                                    gen = "";
                                }
                            }
                        }
                        if (gen == "TAA" || gen == "TGA" || gen == "TAG")
                        {
                            int kontrol4 = 0;
                            gen = "";
                            for (int j = kontrol2; j < dna.Length; j++)
                            {
                                gen += dna[j];
                                kontrol4++;

                                if (kontrol4 % 3 == 0)
                                {
                                    if (gen == "ATG")
                                    {
                                        gen = "";
                                        break;
                                    }
                                    else if (gen == "TAA" || gen == "TGA" || gen == "TAG")
                                    {
                                        Console.WriteLine("Gene structure error.");
                                        error = true;
                                        break;
                                    }
                                    gen = "";
                                }
                            }
                        }
                    }
                    if (error == true)
                    {
                        break;
                    }
                    if (kontrol2 % 3 == 0)
                    {
                        gen = "";
                    }
                }
            }
            writing_dna();

            if (dna.Length % 3 != 0)
            {
                Codon();
            }
            else
            {
                Gen1();

                if (gen == "TAA" || gen == "TGA" || gen == "TAG")
                {
                    Gen2();

                    if (gen == "ATG")
                    {
                        Gen4();

                        if (error == false)
                        {
                            Gen3();
                        }
                    }
                }
            }
        }
        static void operation5(string dna)
        {
            Console.Write("DNA Strand:");
            for (int i = 0; i < dna.Length; i += 3)
            {
                Console.Write($"{dna[i]}{dna[i + 1]}{dna[i + 2]} ");
            }
            Console.WriteLine();
            string[] bc = new string[dna.Length / 3];//bc = blob control
            string remake = "";
            //Checking if dna valid or not 
            if (dna.Length % 3 != 0)
            {
                Console.WriteLine("Codon structure error.");
            }
            else
            {
                //This for is converting dna string to Codons
                for (int k = 0; k < dna.Length; k += 3)
                {
                    remake += $"{dna[k]}{dna[k + 1]}{dna[k + 2]}";
                    bc[k / 3] = remake;
                    remake = "";
                }
                int gc = 0;// gc = gender control 
                           //checking blob gender
                for (int k = 0; k < bc.Length; k++)
                {
                    if (bc[k] == "AAA" && bc[k + 1] == "TTT")
                        gc = 1;
                }
                for (int k = 0; k < bc.Length; k++)
                {
                    if (bc[k] == "TTT" && bc[k + 1] == "AAA")
                        gc = 1;
                }
                for (int k = 0; k < bc.Length; k++)
                {
                    if (bc[k] == "GGG" && bc[k + 1] == "CCC")
                        gc = 0;
                }
                for (int k = 0; k < bc.Length; k++)
                {
                    if (bc[k] == "CCC" && bc[k + 1] == "GGG")
                        gc = 0;
                }
                for (int k = 0; k < bc.Length; k++)
                {
                    if (bc[k] == "TTT" && bc[k + 1] == "TTT")
                        gc = 1;
                }
                for (int k = 0; k < bc.Length; k++)
                {
                    if (bc[k] == "TTT" && bc[k + 1] == "CCC")
                        gc = 1;
                }
                for (int k = 0; k < bc.Length; k++)
                {
                    if (bc[k] == "CCC" && bc[k + 1] == "TTT")
                        gc = 0;
                }
                for (int k = 0; k < bc.Length; k++)
                {
                    if (bc[k] == "GGG" && bc[k + 1] == "AAA")
                        gc = 1;
                }
                for (int k = 0; k < bc.Length; k++)
                {
                    if (bc[k] == "AAA" && bc[k + 1] == "GGG")
                        gc = 0;
                }
                for (int k = 0; k < bc.Length; k++)
                {
                    if (bc[k] == "GGG" && bc[k + 1] == "TTT")
                        gc = 1;
                }
                for (int k = 0; k < bc.Length; k++)
                {
                    if (bc[k] == "TTT" && bc[k + 1] == "GGG")
                        gc = 0;
                }
                for (int k = 0; k < bc.Length; k++)
                {
                    if (bc[k] == "AAA" && bc[k + 1] == "AAA")
                        gc = 1;
                }
                //checking number of genes error 
                for (int k = 0; k < bc.Length; k++)
                {
                    if (bc[k] == "ATG" && bc[k + 1] == "AAA" && bc[k + 2] == "TTT" && (bc[k + 3] == "TAG" || bc[k + 3] == "TGA" || bc[k + 3] == "TAA"))
                        gc = 2;
                    else
                        gc = 1;
                }
                for (int k = 0; k < bc.Length; k++)
                {
                    if (bc[k] == "ATG" && bc[k + 1] == "TTT" && bc[k + 2] == "AAA" && (bc[k + 3] == "TAG" || bc[k + 3] == "TGA" || bc[k + 3] == "TAA"))
                        gc = 2;
                    else
                        gc = 1;
                }
                for (int k = 0; k < bc.Length; k++)
                {
                    if (bc[k] == "ATG" && bc[k + 1] == "TTT" && bc[k + 2] == "CCC" && (bc[k + 3] == "TAG" || bc[k + 3] == "TGA" || bc[k + 3] == "TAA"))
                        gc = 2;
                    else
                        gc = 1;
                }
                for (int k = 0; k < bc.Length; k++)
                {
                    if (bc[k] == "ATG" && bc[k + 1] == "CCC" && bc[k + 2] == "TTT" && (bc[k + 3] == "TAG" || bc[k + 3] == "TGA" || bc[k + 3] == "TAA"))
                        gc = 2;
                    else
                        gc = 1;
                }
                for (int k = 0; k < bc.Length; k++)
                {
                    if (bc[k] == "ATG" && bc[k + 1] == "TTT" && bc[k + 2] == "TTT" && (bc[k + 3] == "TAG" || bc[k + 3] == "TGA" || bc[k + 3] == "TAA"))
                        gc = 2;
                    else
                        gc = 1;
                }
                for (int k = 0; k < bc.Length; k++)
                {
                    if (bc[k] == "ATG" && bc[k + 1] == "GGG" && bc[k + 2] == "AAA" && (bc[k + 3] == "TAG" || bc[k + 3] == "TGA" || bc[k + 3] == "TAA"))
                        gc = 2;
                    else
                        gc = 1;
                }
                for (int k = 0; k < bc.Length; k++)
                {
                    if (bc[k] == "ATG" && bc[k + 1] == "AAA" && bc[k + 2] == "GGG" && (bc[k + 3] == "TAG" || bc[k + 3] == "TGA" || bc[k + 3] == "TAA"))
                        gc = 2;
                    else
                        gc = 1;
                }
                for (int k = 0; k < bc.Length; k++)
                {
                    if (bc[k] == "ATG" && bc[k + 1] == "GGG" && bc[k + 2] == "TTT" && (bc[k + 3] == "TAG" || bc[k + 3] == "TGA" || bc[k + 3] == "TAA"))
                        gc = 2;
                    else
                        gc = 1;
                }
                for (int k = 0; k < bc.Length; k++)
                {
                    if (bc[k] == "ATG" && bc[k + 1] == "TTT" && bc[k + 2] == "GGG" && (bc[k + 3] == "TAG" || bc[k + 3] == "TGA" || bc[k + 3] == "TAA"))
                        gc = 2;
                    else
                        gc = 1;
                }
                for (int k = 0; k < bc.Length; k++)
                {
                    if (bc[k] == "ATG" && bc[k + 1] == "AAA" && bc[k + 2] == "AAA" && (bc[k + 3] == "TAG" || bc[k + 3] == "TGA" || bc[k + 3] == "TAA"))
                        gc = 2;
                    else
                        gc = 1;
                }
                //checking number of codons error
                for (int k = 0; k < bc.Length - 1; k++)
                {
                    if (bc[k] == "ATG" && (bc[k + 1] == "TGA" || bc[k + 1] == "TAA" || bc[k + 1] == "TAG"))
                        gc = 3;
                }
                //writing the result
                if (gc == 1)
                    Console.WriteLine("Blob is OK.");
                else if (gc == 0)
                    Console.WriteLine("Gender error.");
                else if (gc == 2)
                    Console.WriteLine("Number of genes error.");
                else if (gc == 3)
                    Console.WriteLine("Number of codons error.");
            }
        }
        static string operation6(string dna)
        {
            Console.WriteLine("DNA strand: " + dna);
            Console.Write("Complement: ");
            for (int i = 0; i < dna.Length; i++)
            {
                if (dna[i] == 'A')
                    Console.Write('T');
                else if (dna[i] == 'T')
                    Console.Write('A');
                else if (dna[i] == 'G')
                    Console.Write('C');
                else if (dna[i] == 'C')
                    Console.Write('G');
            }
            Console.WriteLine("");
            return dna;
        }
        static void operation7(string dna)
        {
            Console.WriteLine("DNA strand: " + dna);
            Console.Write("Amino acids: ");
            //Adding strings 
            string[] dna_codons = new string[] { "GCT","GCC","GCA","GCG","CGT","CGC","CGA","CGG","AGA","AGG","AAT","AAC","GAT","GAC","TGT","TGC","CAA","CAG","GAA","GAG","GGT","GGC","GGA","GGG","CAT","CAC","ATT","ATC","ATA","CTT","CTC","CTA","CTG","TTA","TTG"
                                                   ,"AAG","AAA","ATG","TTT","TTC","CCT","CCC","CCA","CCG","TCT","TCC","TCA","TCG","AGT","AGC","ACT","ACC","ACA","ACG","TGG","TAT","TAC","GTT","GTC","GTA","GTG","TAA","TGA","TAG" };
            string[] amino_acids = new string[] { "Ala", "Ala", "Ala", "Ala","Arg", "Arg", "Arg", "Arg", "Arg", "Arg" ,"Asn","Asn","Asp","Asp","Cys","Cys","Gln","Gln","Glu","Glu" ,
                                                    "Gly","Gly","Gly","Gly","His","His","Ile","Ile","Ile","Leu","Leu","Leu","Leu","Leu","Leu","Lys","Lys","Met","Phe","Phe","Pro","Pro","Pro","Pro","Ser","Ser","Ser","Ser","Ser","Ser","Thr","Thr","Thr","Thr","Trp","Tyr","Tyr","Val","Val","Val","Val","Stop","Stop","Stop" };

            string[] dif = new string[dna.Length / 3];
            string convert = "";


            for (int j = 0; j < dna.Length; j += 3)//Burda ana dna yı 3 lü kodonlar haline bölüyoruz ki işimzi kolaylaşsın
            {
                convert += $"{dna[j]}{dna[j + 1]}{dna[j + 2]}";
                dif[j / 3] = convert;
                convert = "";
            }
            for (int j = 0; j < dif.Length; j++)
            { //Amino asitlere eşit DNA metni için (for döngüsü) kullanma
                for (int k = 0; k < amino_acids.Length; k++)
                {
                    if (dif[j] == dna_codons[k])
                        Console.Write(amino_acids[k] + " ");
                }

            }
            Console.WriteLine("");
        }
        static string operation8(string dna)
        {
            string[] dnarray = new string[dna.Length / 3];
            for (int i = 0; i < dna.Length; i += 3) //Burda ana dna yı 3 lü kodonlar haline bölüyoruz ki işimzi kolaylaşsın
            {
                string temp = "";
                temp = $"{dna[i]}{dna[i + 1]}{dna[i + 2]}";
                dnarray[i / 3] = temp;
            }
            for (int i = 0; i < dnarray.Length; i++)
            {
                Console.Write(dnarray[i] + " ");
            }
            int start = 0;
            int number = 0;
            Console.Write("\nStart from : ");
            while (!int.TryParse(Console.ReadLine(), out start) || start > dnarray.Length) //Tam sayı kontrolü
            {
                Console.Write("Please enter valid integer : ");
            }
            Console.Write("How many codons to delete : ");
            while (!int.TryParse(Console.ReadLine(), out number) || number + start > dnarray.Length)
            {
                Console.Write("Please enter valid integer : ");
            }
            string[] finaldna = new string[dnarray.Length - number];
            for (int i = 0; i < start - 1; i++) //Burada başlangıç noktasına kadar ana dna yı yazma
            {
                finaldna[i] = dnarray[i];
            }
            int tempcount = start - 1; //out of index hatası vermemesi için başlangıcın 1 eksiğini geçici sayıcıya atıyoruz
            for (int i = start + number - 1; i < dnarray.Length; i++) // Başlangıç ve kaç tane kodon olduğuna göre kalan dnaları finaldna ya atıyoruz
            {
                finaldna[tempcount] = dnarray[i];
                tempcount++;
            }
            Console.Write("Result : "); //konsola yazım
            for (int i = 0; i < finaldna.Length; i++)
            {
                Console.Write(finaldna[i] + " ");
            }
            Console.WriteLine();
            string dnafinal = "";
            for (int i = 0; i < finaldna.Length; i++) //Tekrardan finaldna yı stringe çevirme
            {
                dnafinal += finaldna[i];
            }
            return dnafinal;
        }
        static string operation9(string dna)
        {

            string[] dnarray = new string[dna.Length / 3];
            int count = 0;
            for (int i = 0; i < dna.Length; i += 3) //Burada dna yı 3 lü kodonlar haline bölüyoruz ki işimiz kolaylaşsın
            {
                dnarray[i / 3] = $"{dna[i]}{dna[i + 1]}{dna[i + 2]}";
            }
            Console.Write("Dna Strand (stage 1) : ");
            for (int i = 0; i < dnarray.Length; i++)
            {
                Console.Write(dnarray[i] + " ");
            }
            //input alımı (index 1 den başlar)
            Console.WriteLine();
            Console.Write("Codon sequence(without space) : ");
            string input = Console.ReadLine();
            Console.Write("Starting from : ");
            int number = Convert.ToInt32(Console.ReadLine());

            string[] inputarray = new string[input.Length / 3];
            string[] temparray = new string[dnarray.Length + inputarray.Length];


            for (int i = 0; i < input.Length; i += 3) //Girilen inputun da 3 lü kodonlara ayırma işlemi
            {
                inputarray[i / 3] = $"{input[i]}{input[i + 1]}{input[i + 2]}";
            }
            for (int i = 0; i < number - 1; i++) //Başlangıç noktasına kadar ana dna mızdan kodon alma işlemi
            {
                temparray[i] = dnarray[i];
            }
            for (int i = number - 1; i < number + inputarray.Length - 1; i++) //Başlangıç noktasından itibaren girdiğimiz kodon dizisinin eklenmesi
            {
                temparray[i] = inputarray[count];
                count++;
            }
            for (int i = number + inputarray.Length - 1; i < temparray.Length; i++) //Kodon eklenmesi bitince geriye kalan kodonların ana dna dan alınması
            {
                temparray[i] = dnarray[i - inputarray.Length];
            }
            Console.Write("Result : ");
            for (int i = 0; i < temparray.Length; i++) //Son kodonun konsolda yazımı
            {
                Console.Write(temparray[i] + " ");
            }
            Console.WriteLine();
            string dna1 = "";
            for (int i = 0; i < temparray.Length; i++) //Son kodonun tekrardan string haline dönüştürülmesi
            {
                dna1 += temparray[i];
            }
            return dna1;
        }
        static void operation10(string dna)
        {
            string temp = "";
            int count = 0;
            string[] dnarray = new string[dna.Length / 3];
            for (int i = 0; i < dna.Length; i += 3)
            {
                temp += $"{dna[i]}{dna[i + 1]}{dna[i + 2]}";
                dnarray[i / 3] = temp;
                temp = "";
            }
            for (int i = 0; i < dnarray.Length; i++)
            {
                Console.Write(dnarray[i] + " ");
            }

            Console.Write("\nInsert DNA : ");
            string input = Console.ReadLine();
            while (input.Length % 3 != 0 || int.TryParse(input, out int x))
            {
                Console.WriteLine("\nPlease insert in true format");
                Console.Write("\nInsert DNA : ");
                input = Console.ReadLine();
            }
            string[] inputarray = new string[input.Length / 3];
            for (int i = 0; i < input.Length; i += 3)
            {
                temp += $"{input[i]}{input[i + 1]}{input[i + 2]}";
                inputarray[i / 3] = temp;
                temp = "";
            }
            Console.Write("Insert Number : ");
            int start = Convert.ToInt32(Console.ReadLine());
            int result = 0;
            for (int i = 0; i < inputarray.Length - 1; i++)
            {
                for (int j = start - 1; j < dnarray.Length - 1; j++)
                {
                    if (dnarray[j] == inputarray[i])
                    {
                        if (dnarray[j + 1] == inputarray[i + 1])
                        {
                            count++;
                            result = j;
                        }
                        start = j + 1;
                        break;
                    }
                }
            }
            if (count + 1 == inputarray.Length)
            {
                if (inputarray.Length >= 3)
                {
                    Console.WriteLine("Result : " + (result + 3 - inputarray.Length));
                }
                else
                {
                    Console.WriteLine("Result : " + (result + 1));
                }
            }
            else
            {
                Console.WriteLine("Result : -1 (Not Found)");
            }

            Console.ReadLine();
        }
        static string operation11(string dna)
        {
            int n = 0, m = 0;
            string temp = "";
            string[] dnarray = new string[dna.Length / 3];
            for (int i = 0; i < dna.Length; i += 3)
            {
                temp += $"{dna[i]}{dna[i + 1]}{dna[i + 2]}";
                dnarray[i / 3] = temp;
                temp = "";
            }
            for (int i = 0; i < dnarray.Length; i++)
            {
                Console.Write(dnarray[i] + " ");
            }
            Console.Write("\nStart from : ");
            while (!int.TryParse(Console.ReadLine(), out n))
            {
                Console.Write("Please insert a valid integer : ");
            }
            Console.Write("How many codons : ");
            while (!int.TryParse(Console.ReadLine(), out m))
            {
                Console.Write("Please insert a valid integer : ");
            }
            string[] reversed = new string[m];
            for (int i = 0; i < m; i++)
            {
                reversed[i] = dnarray[n + m - 2 - i];
            }
            for (int i = 0; i < m; i++)
            {
                dnarray[n + i - 1] = reversed[i];
            }
            for (int i = 0; i < dnarray.Length; i++)
            {
                Console.Write(dnarray[i] + " ");
            }
            string dna1 = "";
            for (int i = 0; i < dnarray.Length; i++)
            {
                dna1 += dnarray[i];
            }
            return dna1;

        }
        static void operation12(string dna)
        {
            string temp = "";
            int dnacount = 0;
            string[] dnarray = new string[dna.Length / 3];
            for (int i = 0; i < dna.Length; i += 3)
            {
                temp += $"{dna[i]}{dna[i + 1]}{dna[i + 2]}";
                dnarray[i / 3] = temp;
                temp = "";
            }
            for (int i = 0; i < dnarray.Length; i++)
            {
                Console.Write(dnarray[i] + " ");
            }
            for (int i = 0; i < dnarray.Length; i++)
            {
                if (dnarray[i] == "ATG")
                {
                    for (int j = i; j < dnarray.Length; j++)
                    {
                        if (dnarray[j] == "TAA" || dnarray[j] == "TGA" || dnarray[j] == "TAG")
                        {
                            dnacount++;
                            i = j;
                            break;
                        }
                    }
                }
            }
            Console.WriteLine();
            Console.WriteLine("Result : " + dnacount);
        }
        static void operation13(string dna)
        {
            string enkucukGEN = "";
            string temp = "";
            int min_GEN = 0;
            int ATGmindeki = 0;
            int minimum_kodon = 40; // farazi bir değer. 0 koyasak daha küçüğünü bulamayıp değeri değiştiremezdik
            int kodon_sayısı = 0;
            int ATG_BASLANGIC = 0;
            int stop_bitis = 0;
            int GEN_SAYISI = 0; //counter 
            string[] stop = { "TAA", "TGA", "TAG", "ATG" };// 0 1 2 3 indexler

            string[] dnarray = new string[dna.Length / 3];
            for (int i = 0; i < dna.Length; i += 3)
            {
                temp += $"{dna[i]}{dna[i + 1]}{dna[i + 2]}"; //dnaları 3'lü yani kodon olarak bir değişkene atıyoruz
                dnarray[i / 3] = temp;
                temp = "";
            }
            for (int i = 0; i < dnarray.Length; i++)// kodonları boşluklu yazdırma
            {
                Console.Write(dnarray[i] + " ");
            }
            for (int i = 0; i < dnarray.Length; i++)
            {
                if (dnarray[i] == stop[3]) // ATG bulduğunda değişkene ata 
                {
                    ATG_BASLANGIC = i;


                }
                else if (dnarray[i] == stop[0] || dnarray[i] == stop[1] || dnarray[i] == stop[2])
                {
                    stop_bitis = i; // stop kodonu bulduğunda değişkene ata
                }
                if (ATG_BASLANGIC < stop_bitis)
                {
                    GEN_SAYISI++;
                    kodon_sayısı = stop_bitis - ATG_BASLANGIC + 1;// ATGyi eklemek için +1 yapıyoruz 
                    if (kodon_sayısı < minimum_kodon) // yeni bulduğumuz geni en küçükle değiştiriyoruz
                    {
                        min_GEN = GEN_SAYISI; // kaçıncı gen olduğunu burada eşitliyoruz
                        minimum_kodon = kodon_sayısı;
                        ATGmindeki = ATG_BASLANGIC;

                    }
                }
            }
            for (int k = 0; k < minimum_kodon; k++) // minimum kodonu yeni bir string değişkenin de toplayıp yazdırıyor
            {
                enkucukGEN += dnarray[k + ATGmindeki] + " "; //miniumum kodondaki atgnin başladğı yerden başlamayı sağlıyor
            }
            Console.WriteLine("\n\nShortest gene:" + enkucukGEN); // yazdırma işlemleri
            Console.WriteLine("Number of codons in the gene:" + minimum_kodon);
            Console.WriteLine("Position of the gene:" + min_GEN);
        }
        static void operation14(string dna)
        {
            string enbuyukGEN = "";
            int max_GEN = 0;
            string temp = "";
            int ATGmaxdaki = 0;
            int max_kodon = 0; // yine farazi bir değer .minde 40 tı burada 0 değişmesi için olabilecek en küçükten başlıyoruz
            int kodon_sayısı = 0;
            int ATG_BASLANGIC = 0;
            int stop_bitis = 0;
            int GEN_SAYISI = 0; //counter 
            string[] stop = { "TAA", "TGA", "TAG", "ATG" };// 0 1 2 3 indexler
            string[] dnarray = new string[dna.Length / 3];
            for (int i = 0; i < dna.Length; i += 3)
            {
                temp += $"{dna[i]}{dna[i + 1]}{dna[i + 2]}";
                dnarray[i / 3] = temp;
                temp = "";
            }
            for (int i = 0; i < dnarray.Length; i++) // ana kodonu boşluklu yazdırma 
            {
                Console.Write(dnarray[i] + " ");
            }
            for (int i = 0; i < dnarray.Length; i++)
            {
                if (dnarray[i] == stop[3])
                {
                    ATG_BASLANGIC = i; // ATG gelen yeri bir değişkene atıyoruz

                }
                else if (dnarray[i] == stop[0] || dnarray[i] == stop[1] || dnarray[i] == stop[2])
                {
                    stop_bitis = i;// STOPu gelen yeri bir değişekene atama

                }
                if (ATG_BASLANGIC < stop_bitis) // ATG VE STOP KODONU SIRASINI BOZMAMAK İÇİN
                {
                    GEN_SAYISI++;
                    kodon_sayısı = stop_bitis - ATG_BASLANGIC + 1;// ATGyi eklemek için +1 yapıyoruz 
                    if (kodon_sayısı > max_kodon) // yeni bulduğumu gen eğer öncekiden daha büyükse birbiriyle değiştiriyoruz
                    {
                        max_GEN = GEN_SAYISI - 1; // kaçıncı gen olduğunu burada eşitliyoruz
                        max_kodon = kodon_sayısı;
                        ATGmaxdaki = ATG_BASLANGIC;


                    }
                }
            }

            for (int k = 0; k < max_kodon; k++) // maxiumum kodonu yeni bir string değişkenin de toplayıp yazdırıyor
            {
                enbuyukGEN += dnarray[k + ATGmaxdaki] + " ";//maximum kodondaki atgnin başladğı yerden başlamayı sağlıyor
            }
            Console.WriteLine("\n\n Shortest gene:" + enbuyukGEN); // yazdırma işlemleri
            Console.WriteLine("Number of codons in the gene:" + max_kodon);
            Console.WriteLine("Position of the gene:" + max_GEN);
            Console.ReadLine();
        }
        static void operation15(string dna)
        {
            Console.WriteLine("please enter number of nucletide: ");
            int sayı = Convert.ToInt32(Console.ReadLine());


            char[] most_tekrar = new char[sayı];
            int biggest_frekans = 0;

            int uzunluk = dna.Length - sayı + 1; //girdiğimiz count değeri kadar ileriye bakacağımız için i'nin son değerini 
                                                 //toplam dna uzunluğunundan count değerini çıkararak buluyoruz

            for (int i = 0; i < uzunluk; ++i)
            {

                char[] codon1 = new char[sayı];//girilen uzunlukta bir char dizisi oluşturuyoruz

                for (int j = 0; j < sayı; ++j)
                {
                    codon1[j] = dna[i + j]; //i"ninci karakterden girdiğimiz sayıyaya kadar olan kısmı tarıyoruz
                }

                int frekans = 0;

                for (int t = 0; t < uzunluk; ++t)
                {

                    char[] codon2 = new char[sayı];

                    for (int j = 0; j < sayı; j++)
                    {
                        codon2[j] = dna[t + j];
                    }

                    if (new string(codon1) == new string(codon2))
                    {
                        frekans += 1;
                        t += sayı - 1; //GAG AGA GAC NORMALDE 3 TANE GAGA VAR AMA ÖRNEKTE 2 BUNA ÇÖZÜM OLARAK T'Yİ ARTTIYORUZ
                    }
                }

                if (frekans > biggest_frekans)//hangi sequence daha fazla tekrar ediyorsa onu ana değişkenimizle değiştiriyoruz

                {
                    most_tekrar = codon1;
                    biggest_frekans = frekans;
                }

            }
            Console.WriteLine("Enter number of nucletide:  " + sayı);
            Console.WriteLine("Frequency:  " + biggest_frekans);
            Console.Write("Most repeated:  ");
            Console.WriteLine(most_tekrar);
        }
        static void operation16(string dna)
        {
            int hbond2 = 0;
            int hbond3 = 0;
            for (int i = 0; i < dna.Length; i++)
            {
                char letter = dna[i];

                if (('G' == letter) || ('C' == letter))
                {
                    hbond3 = hbond3 + 3;
                }
                else if (('A' == letter) || ('T' == letter))
                {
                    hbond2 = hbond2 + 2;
                }
            }

            double hat = hbond2 / 2;
            double hgc = hbond3 / 3;

            Console.WriteLine("DNA strand: " + dna);
            Console.WriteLine("Number of pairing with 2-hydrogen bonds: " + hat);
            Console.WriteLine("Number of pairing with 3-hydrogen bonds: " + hgc);
            Console.WriteLine("Number of hydrogen bonds: " + (hbond3 + hbond2));
            Console.WriteLine("");
        }
        static string operation17(string blob1, string blob2)
        {
            int count = 1;
            while (count <= 10)
            {
                string temp = "";
                string[] b1array = new string[blob1.Length / 3];
                string[] b2array = new string[blob2.Length / 3];
                for (int i = 0; i < blob1.Length; i += 3)
                {
                    temp = $"{blob1[i]}{blob1[i + 1]}{blob1[i + 2]}";
                    b1array[i / 3] = temp;
                }

                for (int i = 0; i < blob2.Length; i += 3)
                {
                    temp = $"{blob2[i]}{blob2[i + 1]}{blob2[i + 2]}";
                    b2array[i / 3] = temp;
                }
                string[] b3gndarray = { "ATG", b1array[1], b2array[2], "TAG" };
                string b3gender = "";
                for (int i = 0; i < b3gndarray.Length; i++)
                {
                    b3gender += b3gndarray[i];
                }

                //Gen1 sayısı kontrolü
                int gen1count = 0;
                for (int i = 0; i < b1array.Length; i++)
                {
                    if (b1array[i] == "ATG")
                    {
                        for (int j = i; j < b1array.Length; j++)
                        {
                            if (b1array[j] == "TAA" || b1array[j] == "TGA" || b1array[j] == "TAG")
                            {
                                gen1count++;
                                i = j;
                                break;
                            }
                        }
                    }
                }
                //Gen2 sayısı kontrolü
                int gen2count = 0;
                for (int i = 0; i < b2array.Length; i++)
                {
                    if (b2array[i] == "ATG")
                    {
                        for (int j = i; j < b2array.Length; j++)
                        {
                            if (b2array[j] == "TAA" || b2array[j] == "TGA" || b2array[j] == "TAG")
                            {
                                gen2count++;
                                i = j;
                                break;
                            }
                        }
                    }
                }
                int m = 0;
                int b1_start = 0;
                int b1_end = 0;
                string[] b1genarray = new string[gen1count];
                for (int i = 0; i < b1array.Length; i++)
                {
                    if (b1array[i] == "ATG")
                    {
                        b1_start = i;
                    }
                    else if (b1array[i] == "TAG" || b1array[i] == "TGA" || b1array[i] == "TAA")
                    {
                        b1_end = i;
                    }
                    if (b1_end - b1_start > 0)
                    {
                        temp = "";
                        for (int j = b1_start; j <= b1_end; j++)
                        {

                            temp += b1array[j];
                        }

                        b1genarray[m] = temp;
                        m++;
                    }
                }
                m = 0;
                int b2_start = 0;
                int b2_end = 0;
                string[] b2genarray = new string[gen2count];
                for (int i = 0; i < b2array.Length; i++)
                {
                    if (b2array[i] == "ATG")
                    {
                        b2_start = i;
                    }
                    else if (b2array[i] == "TAG" || b2array[i] == "TGA" || b2array[i] == "TAA")
                    {
                        b2_end = i;
                    }

                    if (b2_end - b2_start > 0)
                    {
                        temp = "";
                        for (int j = b2_start; j <= b2_end; j++)
                        {
                            temp += b2array[j];
                        }
                        b2genarray[m] = temp;
                        m++;
                    }
                }
                string[] b3 = new string[gen2count + gen1count];
                b3[0] = b3gender;
                for (int i = 1; i < gen2count + gen1count - 2; i++)
                {
                    if ((i % 2 == 1 && b1genarray.Length > i) || (b2genarray.Length <= i && b1genarray.Length > i))
                    {
                        b3[i] = b1genarray[i];
                    }
                    else if ((i % 2 == 0 && b2genarray.Length > i) || (b1genarray.Length <= i && b2genarray.Length > i))
                    {
                        b3[i] = b2genarray[i];
                    }
                }
                string b3string = "";
                for (int i = 0; i < b3.Length; i++)
                {
                    b3string += b3[i];
                }
                string[] b3array = new string[b3string.Length / 3];
                for (int i = 0; i < b3string.Length; i += 3)
                {
                    temp = $"{b3string[i]}{b3string[i + 1]}{b3string[i + 2]}";
                    b3array[i / 3] = temp;
                }
                double riskpoint = 0;
                temp = "";
                for (int i = 0; i < b3array.Length; i++)
                {
                    while (b3array[i] == "CCC" || b3array[i] == "CCG" || b3array[i] == "CGC" || b3array[i] == "CGG" || b3array[i] == "GCC" || b3array[i] == "GGC" || b3array[i] == "GGG" || b3array[i] == "GCG" || i > b3array.Length - 1)
                    {
                        temp += b3array[i];
                        i++;
                    }

                    if (temp.Length / 3 >= 3)
                    {
                        riskpoint += temp.Length / 3;
                        temp = "";
                    }
                    else
                    {
                        temp = "";
                    }
                }
                Console.Write("Blob 1 : ");
                for (int i = 0; i < b1array.Length; i++)
                {
                    Console.Write(b1array[i] + " ");
                }
                Console.WriteLine();

                Console.Write("Blob 2 : ");
                for (int i = 0; i < b2array.Length; i++)
                {
                    Console.Write(b2array[i] + " ");
                }
                Console.WriteLine();

                Console.Write("Blob 3 : ");
                for (int i = 0; i < b3array.Length; i++)
                {
                    Console.Write(b3array[i] + " ");
                }
                Console.WriteLine();
                Console.WriteLine("Generations : " + count);
                Console.WriteLine();

                if (riskpoint / b3array.Length * 100 > 10)
                {
                    Console.WriteLine("Blob 3 codons ratio {0}/{1} = {2}%", riskpoint, b3array.Length, riskpoint / b3array.Length * 100);
                    Console.WriteLine("Newborn dies. Generations are over.");
                    break;
                }
                blob1 = b3string;
                if (b3array[1] == "AAA" || b3array[1] == "TTT")
                {
                    if (b3array[2] == "GGG" || b3array[2] == "CCC")
                    {
                        blob2 = operation3("female");
                    }
                    else
                    {
                        blob2 = operation3("male");
                    }
                }
                else
                {
                    blob2 = operation3("female");
                }
                count++;
            }
            return blob1;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Operation 1. Load a DNA sequence from a file\n" +
                "Operation 2. Load a DNA sequence from a string\n" +
                "Operation 3. Generate random DNA sequence of a BLOB\n");
            int select1;
            Console.Write("Select operation : ");
            while (!int.TryParse(Console.ReadLine(), out select1) || select1 > 3 || select1 < 1)
            {
                Console.Write("Please enter valid integer : ");
            }
            string dna = "";
            string dna2 = "";
            bool devam = true;
            switch (select1)
            {
                case 1:
                    StreamReader f = File.OpenText("dna1.txt");
                    dna = f.ReadLine();
                    break;
                case 2:
                    Console.Write("Please write your dna sequence(without space) : ");
                    dna = Console.ReadLine();
                    while (dna.Length % 3 != 0)
                    {
                        Console.Write("Please enter in right form : ");
                        dna = Console.ReadLine();
                    }
                    break;
                case 3:
                    dna = operation3("");
                    break;

            }
            Console.Clear();

            while (devam)
            {
                Console.WriteLine("\nOperation 1. Check DNA gene structure\n" +
                "Operation 2. Check DNA of BLOB organism\n" +
                "Operation 3. Produce complement of a DNA sequence\n" +
                "Operation 4. Determine amino acids\n" +
                "Operation 5. Delete codons (delete n codons, starting from mth codon)\n" +
                "Operation 6. Insert codons (insert a codon sequence, starting from mth codon)\n" +
                "Operation 7. Find codons (find a codon sequence, starting from mth codon)\n" +
                "Operation 8. Reverse codons (reverse n codons, starting from mth codon)\n" +
                "Operation 9. Find the number of genes in a DNA strand (BLOB or not)\n" +
                "Operation 10. Find the shortest gene in a DNA strand\n" +
                "Operation 11. Find the longest gene in a DNA strand\n" +
                "Operation 12. Find the most repeated n-nucleotide sequence in a DNA strand (STR - Short Tandem Repeat)\n" +
                "Operation 13. Hydrogen bond statistics for a DNA strand\n" +
                "Operation 14. Simulate BLOB generations using DNA strand 1 and 2 (DNA strand 3 is for the newborn)\n" +
                "Please enter y or Y to exit.");
                Console.Write("\nSelect operation : ");
                string select2 = Console.ReadLine();
                switch (select2)
                {
                    case "1":
                        operation4(dna);
                        break;
                    case "2":
                        operation5(dna);
                        break;
                    case "3":
                        dna = operation6(dna);
                        break;
                    case "4":
                        operation7(dna);
                        break;
                    case "5":
                        dna = operation8(dna);
                        break;
                    case "6":
                        dna = operation9(dna);
                        break;
                    case "7":
                        operation10(dna);
                        break;
                    case "8":
                        dna = operation11(dna);
                        break;
                    case "9":
                        operation12(dna);
                        break;
                    case "10":
                        operation13(dna);
                        break;
                    case "11":
                        operation14(dna);
                        break;
                    case "12":
                        operation15(dna);
                        break;
                    case "13":
                        operation16(dna);
                        break;
                    case "14":
                        Console.Clear();
                        Console.WriteLine("Operation 1. Load a DNA sequence from a file\n" +
                "Operation 2. Load a DNA sequence from a string\n" +
                "Operation 3. Generate random DNA sequence of a BLOB\n");
                        Console.Write("Please select how to choose dna 2 : ");
                        int select = Convert.ToInt32(Console.ReadLine());
                        switch (select)
                        {
                            case 1:
                                StreamReader f = File.OpenText("dna1.txt");
                                dna2 = f.ReadLine();
                                break;
                            case 2:
                                Console.Write("Please write your dna sequence(without space) : ");
                                dna2 = Console.ReadLine();
                                while (dna.Length % 3 != 0)
                                {
                                    Console.Write("Please enter in right form : ");
                                    dna2 = Console.ReadLine();
                                }
                                break;
                            case 3:
                                dna2 = operation3("");
                                break;
                        }
                        dna = operation17(dna, dna2);
                        break;
                    case "y":
                        devam = false;
                        break;
                    case "Y":
                        devam = false;
                        break;
                }
            }
        }
    }
}
