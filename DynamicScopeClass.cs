using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Dynamic;

namespace DynamicJsonObjectExample
{
    public class DynamicScopeClass
    {
        public static dynamic staticField;
        public dynamic instanceField {
            get;
            set;
        }

        public DynamicScopeClass(){
            this.instanceField = "testing";
        }

        public void passDynamicParameter(dynamic param) {
            Console.WriteLine(param);
        }

        public dynamic returnDynamicType(dynamic param) {
            return param;
        }
    }
}
