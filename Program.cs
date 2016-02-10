using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CashCalculatorTool
{
    class Program
    {
        #region Format console
        public static void MakeBgRed()
        {
            Console.Write("    ");
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Yellow;
        }
        public static void MakeBgDefault()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }
        public static void Error()
        {

            Console.WriteLine();
            MakeBgRed();
            Console.WriteLine("                                                                ");
            MakeBgDefault();
            MakeBgRed();
            Console.WriteLine("   Check entered values as some may be incorrect and were       ");
            MakeBgDefault();
            MakeBgRed();
            Console.WriteLine("   removed from the list. Accepted values should:               ");
            MakeBgDefault();
            MakeBgRed();
            Console.WriteLine("   - not include any letters                                    ");
            MakeBgDefault();
            MakeBgRed();
            Console.WriteLine("   - be separated from each other by at least one space         ");
            MakeBgDefault();
            MakeBgRed();
            Console.WriteLine("   - use forward slash to indicate no VAT e.g. /45.5 or 5,6/    ");
            MakeBgDefault();
            MakeBgRed();
            Console.WriteLine("   - have decimal values separated with either comma or dot     ");
            MakeBgDefault();
            MakeBgRed();
            Console.WriteLine("                                                                ");
            MakeBgDefault();

        }
        #endregion

        static void Main(string[] args)
        {
            #region Some initial definitions - create lists and string builder
            Console.Title = "Petty Cash Calculator";
            StringBuilder sb = new StringBuilder();
            List<double> paidList = new List<double>();
            List<double> netList = new List<double>();
            List<double> vatList = new List<double>();
            #endregion

            #region Calculations
            if (args.Length != 0)
            {
                int count = 0;
                string argCopy = "";
                string line = "      -------------------------------------------------------------";
                sb.AppendLine("\n" + line);
                sb.AppendLine("\n\tRECEIPT #  \tPAID\t\tNET\t\tVAT");
                sb.AppendLine("\n" + line);


                foreach (string arg in args)
                {
                    double vat, net, paid;

                    //   "/" - set vat to zero and replace "," with "."
                    if (arg.Contains("/") == true && arg.Contains(",") == true)
                    {
                        argCopy = arg.Replace(",", ".").Replace("/", "");

                        try
                        {
                            paid = Convert.ToDouble(argCopy);
                            vat = 0;
                            net = paid - vat;
                            string row = "\n\t\t" + (++count) + ".\t" + String.Format("{0:0.00}", paid) + "\t\t" + String.Format("{0:0.00}", net) + "\t\t" + String.Format("{0:0.00}", vat);
                            sb.AppendLine(row);
                            paidList.Add(paid);
                            netList.Add(net);
                            vatList.Add(vat);
                        }
                        catch (Exception)
                        {
                            Error();
                        }
                    }
                    //   "/" - set vat to zero
                    else if (arg.Contains("/") == true)
                    {
                        argCopy = arg.Replace("/", "");

                        try
                        {
                            paid = Convert.ToDouble(argCopy);
                            vat = 0;
                            net = paid - vat;
                            string row = "\n\t\t" + (++count) + ".\t" + String.Format("{0:0.00}", paid) + "\t\t" + String.Format("{0:0.00}", net) + "\t\t" + String.Format("{0:0.00}", vat);
                            sb.AppendLine(row);
                            paidList.Add(paid);
                            netList.Add(net);
                            vatList.Add(vat);
                        }
                        catch (Exception)
                        {
                            Error();
                        }
                    }
                    //   replace "," with "."
                    else if (arg.Contains(",") == true)
                    {

                        argCopy = arg.Replace(",", ".");
                        try
                        {
                            paid = Convert.ToDouble(argCopy);
                            vat = Math.Round((paid / 6), 2);
                            net = paid - vat;
                            string row = "\n\t\t" + (++count) + ".\t" + String.Format("{0:0.00}", paid) + "\t\t" + String.Format("{0:0.00}", net) + "\t\t" + String.Format("{0:0.00}", vat);
                            sb.AppendLine(row);
                            paidList.Add(paid);
                            netList.Add(net);
                            vatList.Add(vat);
                        }
                        catch (Exception)
                        {
                            Error();
                        }
                    }

                    else
                    {
                        argCopy = arg;
                        try
                        {
                            paid = Convert.ToDouble(argCopy);
                            vat = Math.Round((paid / 6), 2);
                            net = paid - vat;
                            string row = "\n\t\t" + (++count) + ".\t" + String.Format("{0:0.00}", paid) + "\t\t" + String.Format("{0:0.00}", net) + "\t\t" + String.Format("{0:0.00}", vat);
                            sb.AppendLine(row);
                            paidList.Add(paid);
                            netList.Add(net);
                            vatList.Add(vat);
                        }
                        catch (Exception)
                        {
                            Error();
                        }
                    }

                }
                sb.AppendLine("\n" + line);
                sb.AppendLine("\n\tTOTALS :\t" + String.Format("{0:0.00}", paidList.Sum().ToString()) + "\t\t" + String.Format("{0:0.00}", netList.Sum().ToString()) + "\t\t" + String.Format("{0:0.00}", vatList.Sum().ToString()));
                sb.AppendLine("\n" + line);
            }
            else
            {
                Console.WriteLine("\n\tNo data input specified...");
            }

            #endregion

            #region Save to file
            Console.WriteLine(sb.ToString());
            using (StreamWriter sw = new StreamWriter(("petty-cash-form-" + DateTime.Today.ToShortDateString() + ".txt").Replace("/", "-")))
            {
                try
                {
                    sw.Write(sb);
                }
                catch (Exception)
                {
                    MakeBgRed();
                    Console.WriteLine(" Unable to save it as it is currently used by other application ");
                    MakeBgDefault();
                }
            }
            #endregion

            #region The end - clearing lists
            Console.Write("\tPress Enter key to start again...");
            Console.Read();
            paidList.Clear();
            netList.Clear();
            vatList.Clear();
            Console.Clear();
            #endregion
        }
    }
}

