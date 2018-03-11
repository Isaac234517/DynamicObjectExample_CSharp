
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Dynamic;
using Newtonsoft.Json.Linq;
namespace DynamicJsonObjectExample
{
    class Program
    {
        static string jsonString = "{\"person\": { \"name\": \"tester\", \"age\": 20, \"saving\": 100.6,  \"creditCard\": [101, 102,301], \"valid\": true}, \"movies\": [{\"name\": \"Star War 8\", \"year\" :2017}, {\"name\": \"The shape of water\", \"year\": \"2017\"}]}";
        static void Main(string[] args)
        {
            string input = "";
            do
            {
                Console.WriteLine("1. Dynamic Type Example");
                Console.WriteLine("2. Dynamic Scope Class Example");
                Console.WriteLine("3. ExpandoObject Example");
                Console.WriteLine("4. Dynamic Object ReadFrom Json String");
                Console.WriteLine("N. Exit");
                input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        showDynamicTypeExample();
                        break;
                    case "2":
                        showDynamicScopeClassExample();
                        break;
                    case "3":
                        ExpandoObjectExample();
                        break;
                    case "4":
                        dynamicObjectFromJsonString();
                        break;
                    default:
                        break;
                }
                Console.WriteLine();
                Console.WriteLine();
            } while (!input.ToUpper().Equals("N"));
        }

        static void showDynamicTypeExample() {
            dynamic a = "abc";
            Console.WriteLine(a.GetType());
            a = 12;
            Console.WriteLine(a.GetType());
            a = 13.5;
            Console.WriteLine(a.GetType());
        }

        static void showDynamicScopeClassExample() {
            dynamic a = 13.5;
            DynamicScopeClass scopeClass = new DynamicScopeClass();
            DynamicScopeClass.staticField = "hello";
            Console.WriteLine("Static Field: {0}", DynamicScopeClass.staticField);
            DynamicScopeClass.staticField = new int[] { 1, 2 };
            Console.WriteLine("Static Field: {0}, {1}", DynamicScopeClass.staticField[0], DynamicScopeClass.staticField[1]);
            Console.WriteLine("Instance Field: {0}", scopeClass.instanceField);
            scopeClass.instanceField = 40;
            Console.WriteLine("Instance Field: {0}", scopeClass.instanceField);
            Console.WriteLine("Passing Dynamic Parameter a = 13.5 to function passDynamicParameter");
            scopeClass.passDynamicParameter(a);
            a = "change type to string";
            Console.WriteLine("Passing Dynamic Parameter a = change type to string to function passDynamicParameter");
            scopeClass.passDynamicParameter(a);
            Console.WriteLine("Return Dynamic Type from function returnDynamicType");
            dynamic b = scopeClass.returnDynamicType(a);
            Console.WriteLine(b);
            a = 12;
            b = scopeClass.returnDynamicType(a);
            Console.WriteLine(b);
        }

        static void ExpandoObjectExample() {
            dynamic expo = new ExpandoObject();
            expo.saving = 100.6;
            expo.age = 20;
            expo.name = "Tester";
            expo.showMember = new Action(() => {
                Console.WriteLine("ExpandoObject member: name:{0}, age:{1}, saving:{2}", expo.name, expo.age, expo.saving);
                ;
            });
            expo.showMember();
        }

        static void dynamicObjectFromJsonString() {
            dynamic obj = DynamicJsonObject.parseJSONString(jsonString);
            Console.WriteLine("Person.Name: {0}", obj.person.name);
            Console.WriteLine("Person:{0}", obj.person);
            Console.WriteLine("Movies[0]:{0}", obj.movies[0]);
            Console.WriteLine("Movies[1].name: {0}", obj.movies[1].name);
        }

    }
}
