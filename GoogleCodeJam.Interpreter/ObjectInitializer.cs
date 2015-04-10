using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using GoogleCodeJam.Model;

namespace GoogleCodeJam.Interpreter
{
    public class ObjectInitializer
    {
        public List<InfoToInitializeProperty> Info { get; set; }

        public List<InfoToInitializeProperty> InfoForStatic { get; set; }

        public ObjectInitializer(object objectToCreateMap) 
        {
            Info = new List<InfoToInitializeProperty>();

            var propertyInfos = objectToCreateMap.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            Info = GetInfoFor(propertyInfos);

            propertyInfos = objectToCreateMap.GetType().GetProperties(BindingFlags.Public | BindingFlags.Static);
            InfoForStatic = GetInfoFor(propertyInfos);

        }

        public List<InfoToInitializeProperty> GetInfoFor(PropertyInfo[] propertyInfos)
        {
            var info = new List<InfoToInitializeProperty>();
            foreach (var propertyInfo in propertyInfos)
            {
                object[] attributes = propertyInfo.GetCustomAttributes(true);
                foreach (object attribute in attributes)
                {
                    var interpreterAttribute = attribute as InterpreterAttribute;
                    if (interpreterAttribute != null)
                    {
                        var mapperProperty = new InfoToInitializeProperty
                        {
                            PropertyName = propertyInfo.Name,
                            Order = interpreterAttribute.Order,
                            ItitializeAttibutes = GetItitializeAttibutes(interpreterAttribute.ItitializeAttibutes),
                            MethodForInitialize = GetMethodInfo(propertyInfo.PropertyType, interpreterAttribute.InitializeMethod)
                        };

                        info.Add(mapperProperty);
                    }
                }
            }
            return info.OrderBy(x => x.Order).ToList();
        }

        public InputProblems<T> InitializeObject<T>(T aProblem, List<List<string>> input) where T : new()
        {
            var problems = new InputProblems<T>();

            var iterator = new ListListIterator<string>(input);
            problems.Cases = Int32.Parse(iterator.Read());

            var problem = new T();
            foreach (var infoToInitializeProperty in InfoForStatic)
            {
                var parameters = new object[] { problem, infoToInitializeProperty.PropertyName, iterator, infoToInitializeProperty.ItitializeAttibutes };
                infoToInitializeProperty.MethodForInitialize.Invoke(this, parameters);
            }

            while(!iterator.IsDone())
            {
                problem = new T();
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
            {typeof(List<int>), x => x != string.Empty ? x : "SetListIntProperty"},
            {typeof(Matrix<string>), x => x != string.Empty ? x : "SetMatrixStringProperty"}
        };

        private MethodInfo GetMethodInfo(Type type, string initializeMethod)
        {
            var methodName = MethodsName[type].Invoke(initializeMethod);
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
            PropertyInfo prop = objectToInitialize.GetType().GetProperty(propertyNameToSet);
            if (null != prop && prop.CanWrite)
            {
                prop.SetValue(objectToInitialize, Int32.Parse(iterator.Read()), null);
            }
        }

        public void SetStringProperty(ref object objectToInitialize, string propertyNameToSet, Iterator<string> iterator, List<ItitializeAttibute> initializeAttributes)
        {
            PropertyInfo prop = objectToInitialize.GetType().GetProperty(propertyNameToSet);
            if (null != prop && prop.CanWrite)
            {
                prop.SetValue(objectToInitialize, string.Join(" ", iterator.ReadLine()), null);
            }
        }

        public void SetListIntProperty(ref object objectToInitialize, string propertyNameToSet, Iterator<string> iterator, List<ItitializeAttibute> initializeAttributes)
        {
            PropertyInfo prop = objectToInitialize.GetType().GetProperty(propertyNameToSet);
            if (null != prop && prop.CanWrite)
            {
                var items = iterator.ReadLine();
                var intList = new List<int>();
                foreach (var item in items)
                {
                    intList.Add(Int32.Parse(item));
                }
                prop.SetValue(objectToInitialize, intList, null);
            }
        }

        public void SetMatrixStringProperty(ref object objectToInitialize, string propertyNameToSet, Iterator<string> iterator, List<ItitializeAttibute> initializeAttributes)
        {
            PropertyInfo prop = objectToInitialize.GetType().GetProperty(propertyNameToSet);

            var rows = (int)objectToInitialize.GetType().GetProperty(initializeAttributes.Single(x => x.ThisPropertyAttribute == PropertyAttribute.Rows).OtherPropertyName).GetValue(objectToInitialize);
            var columns = (int)objectToInitialize.GetType().GetProperty(initializeAttributes.Single(x => x.ThisPropertyAttribute == PropertyAttribute.Columns).OtherPropertyName).GetValue(objectToInitialize);
            
            if (null != prop && prop.CanWrite)
            {
                var matrix = new Matrix<string>(rows, columns);

                for (var r = 0; r < rows; r++)
                {
                    var line = iterator.ReadLine().First();
                    for(var c = 0; c < line.Length; c++)
                        matrix.Set(r, c, line[c].ToString());
                }
                prop.SetValue(objectToInitialize, matrix, null);
            }
        }
    }
}
