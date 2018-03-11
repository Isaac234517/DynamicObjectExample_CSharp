using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Dynamic;
using Newtonsoft.Json.Linq;

namespace DynamicJsonObjectExample
{
    public class DynamicJsonObject: DynamicObject
    {
        private readonly JObject node;
        private readonly JArray arr;
        public static dynamic parseJSONString(string jsonStr) {
            return new DynamicJsonObject(jsonStr);
        }

        private DynamicJsonObject(string jsonStr) {
            this.node = JObject.Parse(jsonStr);
        }

        private DynamicJsonObject(JObject node) {
            this.node = node;
        }

        private DynamicJsonObject(JArray arr) {
            this.arr = arr;
        }

        public override string ToString()
        {
            return node != null ? node.ToString() : arr.ToString();
        }

        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
        {
            int index = (int)indexes[0];
            if (arr != null) {
                Type tt = arr[index].GetType();
                if (tt.Name.Equals("JObject"))
                {
                    result = new DynamicJsonObject(JObject.FromObject(arr[index]));
                }

                else if (tt.Name.Equals("JArray"))
                {
                    result = new DynamicJsonObject(JArray.FromObject(arr[index]));
                }

                else {
                    result = (JValue)arr[index];
                }
                return true;
            }
            return base.TryGetIndex(binder, indexes, out result);
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (node != null && node.SelectToken(binder.Name) != null) {
                Type tt = node.GetValue(binder.Name).GetType();
                if (tt.Name.Equals("JObject"))
                {
                    result = new DynamicJsonObject(JObject.FromObject(node.Value<JObject>(binder.Name)));
                }
                else if (tt.Name.Equals("JArray"))
                {
                    result = new DynamicJsonObject(JArray.FromObject(node.Value<JArray>(binder.Name)));
                }

                else
                    result = node.Value<JValue>(binder.Name);
                return true;
            }
            else
                return base.TryGetMember(binder, out result);
        }

    }
}
