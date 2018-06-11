using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel.Design;
using System.Data.Common;
using System.Globalization;
using System.Runtime.CompilerServices;
using lab2;

namespace lab2
{
    abstract class Element
    {
        protected int ident;
        protected ArrayList idents;
        protected int num_of_inputs = 0;
        protected string[] inputs = new string[10];
        protected string output = "";
        abstract public string calculate();
        abstract public void set(int identify, ArrayList idents, int num_of_in, string[] values);
    }

    class AND : Element
    {
        public AND()
        {
        }

        public int ident_get()
        {
            return this.ident;
        }

        public ArrayList idents_get()
        {
            return this.idents;
        }

        public string[] inputs_get()
        {
            return this.inputs;
        }

        public string output_get()
        {
            return this.output;
        }

        public override void set(int identify, ArrayList idents, int num_of_in, string[] values)
        {
            this.ident = identify;
            this.idents = idents;
            this.num_of_inputs = num_of_in;
            this.inputs = values;
        }

        public AND(int identify, ArrayList idents, int num_of_in, string[] values)
        {
            this.ident = identify;
            this.idents = idents;
            this.num_of_inputs = num_of_in;
            this.inputs = values;
        }

        public override string calculate()
        {
            this.output = this.inputs[0];
            for (int i = 0; i < num_of_inputs; i++)
            {
                this.output = this & this.inputs[i];
            }

            return output;
        }

        private string and(string s1, string s2)
        {
            int size = Math.Max(s1.Length, s2.Length);
            string res = "";
            if (s1.Length > s2.Length)
            {
                string buff = "";
                for (int i = 0; i < s1.Length - s2.Length; i++)
                {
                    buff += "0";
                }

                buff += s2;
                for (int i = 0; i < size; i++)
                {
                    char val;
                    if (buff[i] == '1' && s1[i] == '1')
                    {
                        val = '1';
                    }
                    else
                    {
                        val = '0';
                    }

                    res += val;
                }
            }
            else
            {
                string buff = "";
                for (int i = 0; i < s2.Length - s1.Length; i++)
                {
                    buff += "0";
                }

                buff += s1;
                for (int i = 0; i < size; i++)
                {
                    char val;
                    if (buff[i] == '1' && s2[i] == '1')
                    {
                        val = '1';
                    }
                    else
                    {
                        val = '0';
                    }

                    res += val;
                }
            }

            return res;
        }

        public static string operator &(AND self, string s2)
        {
            return self.and(self.output, s2);
        }
    }

    class OR : Element
    {
        public OR()
        {
        }

        public int ident_get()
        {
            return this.ident;
        }

        public ArrayList idents_get()
        {
            return this.idents;
        }

        public string[] inputs_get()
        {
            return this.inputs;
        }

        public string output_get()
        {
            return this.output;
        }

        public override void set(int identify, ArrayList idents, int num_of_in, string[] values)
        {
            this.ident = identify;
            this.idents = idents;
            this.num_of_inputs = num_of_in;
            this.inputs = values;
        }

        public OR(int identify, ArrayList idents, int num_of_in, string[] values)
        {
            this.ident = identify;
            this.idents = idents;
            this.num_of_inputs = num_of_in;
            this.inputs = values;
        }

        public override string calculate()
        {
            this.output = this.inputs[0];
            for (int i = 0; i < num_of_inputs; i++)
            {
                this.output = this | this.inputs[i];
            }

            return output;
        }

        private string and(string s1, string s2)
        {
            int size = Math.Max(s1.Length, s2.Length);
            string res = "";
            if (s1.Length > s2.Length)
            {
                string buff = "";
                for (int i = 0; i < s1.Length - s2.Length; i++)
                {
                    buff += "0";
                }

                buff += s2;
                for (int i = 0; i < size; i++)
                {
                    char val;
                    if (buff[i] == '1' || s1[i] == '1')
                    {
                        val = '1';
                    }
                    else
                    {
                        val = '0';
                    }

                    res += val;
                }
            }
            else
            {
                string buff = "";
                for (int i = 0; i < s2.Length - s1.Length; i++)
                {
                    buff += "0";
                }

                buff += s1;
                for (int i = 0; i < size; i++)
                {
                    char val;
                    if (buff[i] == '1' || s2[i] == '1')
                    {
                        val = '1';
                    }
                    else
                    {
                        val = '0';
                    }

                    res += val;
                }
            }

            return res;
        }

        public static string operator |(OR self, string s2)
        {
            return self.and(self.output, s2);
        }
    }

    class Schema
    {
        private Element[] elements;
        private string[] values;

        public Schema(Element[] el, string[] inputs)
        {
            elements = el;
            values = inputs;
        }

        public string calculate()
        {
            ArrayList array = new ArrayList(2);
            string[] els_and = {values[0], values[1]};
            elements[0].set(1, array, 2, els_and);
            string and_calc = elements[0].calculate();
            string[] els_or = {values[2], and_calc};
            elements[1].set(2, array, 2, els_or);
            string result = elements[1].calculate();
            return result;
        }
    }

    internal class Program
    {
        public static void Main(string[] args)
        {
            string[] values = {"1101", "0101", "0001"};
            Element[] elements = {new AND(), new OR()};

            Schema schema = new Schema(elements, values);
            Console.WriteLine("Result of ((1101 & 0101) | 0001) = " + schema.calculate());
        }
    }
}