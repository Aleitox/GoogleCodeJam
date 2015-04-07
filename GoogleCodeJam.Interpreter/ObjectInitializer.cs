using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

namespace GoogleCodeJam.Interpreter
{
    public class ObjectInitializer
    {
        public List<InfoToInitializeProperty> Info { get; set; }

        public ObjectInitializer(object objectToCreateMap) 
        {
            Info = new List<InfoToInitializeProperty>();

            PropertyInfo[] propertyInfos = objectToCreateMap.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var propertyInfo in propertyInfos)
            {
                object[] attributes = propertyInfo.GetCustomAttributes(true);
                foreach (object attribute in attributes)
                {
                    InterpreterAttribute interpreterAttribute = attribute as InterpreterAttribute;
                    if (interpreterAttribute != null)
                    {
                        var mapperProperty = new InfoToInitializeProperty();
                        mapperProperty.PropertyName = propertyInfo.Name;
                        mapperProperty.Order = interpreterAttribute.Order;
                        mapperProperty.ItitializeAttibutes = GetItitializeAttibutes(interpreterAttribute.ItitializeAttibutes);
                        mapperProperty.MethodForInitialize = GetMethodInfo(propertyInfo.PropertyType, interpreterAttribute.InitializeMethod);

                        Info.Add(mapperProperty);
                    }
                }
            }

            Info.OrderBy(x => x.Order);
        }

        public InputProblems<T> InitializeObject<T>(T aProblem, List<List<string>> input) where T : new()
        {
            var problems = new InputProblems<T>();

            var iterator = new ListListIterator<string>(input);
            problems.Cases = Int32.Parse(iterator.Read());

            while(!iterator.IsDone())
            {
                var problem = new T();
                foreach(var infoToInitializeProperty in Info)
                {
                    var parameters = new object[] { problem, infoToInitializeProperty.PropertyName, iterator, infoToInitializeProperty.ItitializeAttibutes};
                    infoToInitializeProperty.MethodForInitialize.Invoke(this, parameters);
                }
                problems.Problems.Add(problem);
            }

            return problems;
        }

        private Dictionary<Type, Func<string, string>> MethodsName = new Dictionary<Type, Func<string, string>>() 
        {
            {typeof(int), x => x != string.Empty ? x : "SetIntProperty"},
            {typeof(string), x => x != string.Empty ? x : "SetStringProperty"},
            {typeof(List<int>), x => x != string.Empty ? x : "SetListIntProperty"}
        };

        private MethodInfo GetMethodInfo(Type type, string InitializeMethod)
        {
            var methodName = MethodsName[type].Invoke(InitializeMethod);
            var methodInfo = typeof(ObjectInitializer).GetMethod(methodName);
            return methodInfo;
        }

        private List<ItitializeAttibute> GetItitializeAttibutes(string[] ititializeAttibutesArray)
        {
            var ititializeAttibutes = new List<ItitializeAttibute>();

            if (ititializeAttibutesArray == null) return ititializeAttibutes;

            for (var index = 0; index < ititializeAttibutesArray.Length; index = index + 2)
            {
                ititializeAttibutes.Add(new ItitializeAttibute() 
                    {
                        OtherPropertyName = ititializeAttibutesArray[index],
                        ThisPropertyAttribute = (PropertyAttribute)Enum.Parse(typeof(PropertyAttribute), ititializeAttibutesArray[index + 1]) 
                    });
            }
            return ititializeAttibutes;            
        }

        public void SetIntProperty(ref object objectToInitialize, string propertyNameToSet, Iterator<string> iterator, List<ItitializeAttibute> initializeAttributes) 
        {
            PropertyInfo prop = objectToInitialize.GetType().GetProperty(propertyNameToSet, BindingFlags.Public | BindingFlags.Instance);
            if (null != prop && prop.CanWrite)
            {
                prop.SetValue(objectToInitialize, Int32.Parse(iterator.Read()), null);
            }
        }

        public void SetStringProperty(ref object objectToInitialize, string propertyNameToSet, Iterator<string> iterator, List<ItitializeAttibute> initializeAttributes)
        {
            PropertyInfo prop = objectToInitialize.GetType().GetProperty(propertyNameToSet, BindingFlags.Public | BindingFlags.Instance);
            if (null != prop && prop.CanWrite)
            {
                prop.SetValue(objectToInitialize, string.Join(" ", iterator.ReadLine()), null);
            }
        }

        public void SetListIntProperty(ref object objectToInitialize, string propertyNameToSet, Iterator<string> iterator, List<ItitializeAttibute> initializeAttributes)
        {
            PropertyInfo prop = objectToInitialize.GetType().GetProperty(propertyNameToSet, BindingFlags.Public | BindingFlags.Instance);
            if (null != prop && prop.CanWrite)
            {
                var juan = iterator.ReadLine();
                var pedro = new List<int>();
                foreach (var item in juan)
                {
                    pedro.Add(Int32.Parse(item));
                }
                prop.SetValue(objectToInitialize, pedro, null);
            }
        }
    }
}
