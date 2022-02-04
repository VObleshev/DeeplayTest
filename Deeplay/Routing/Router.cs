using Newtonsoft.Json;
using DataLayer.Communication;
using Server.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Server.ConfigurationApp;

namespace Server.Routing
{
    public class Router
    {
        private readonly string _className = "Server.Controllers.";
        private readonly string _methodName = "";

        public Router(Route route)
        {
            _className += route.Controller + "Controller";
            _methodName = route.Action;
        }

        public IResponsible CallAction(object receiveData)
        {
            if (string.IsNullOrEmpty(_className))
                return new Response("Неправильно указаный путь", false, new ArgumentNullException("Route.Controller"));

            if (string.IsNullOrEmpty(_methodName))
                return new Response("Неправильно указаный путь", false, new ArgumentNullException("Route.Action"));

            // ищем контроллер
            var calledClass = Type.GetType(_className);
            if (calledClass == null)            
                return new Response("По заданному пути контроллер не найден", false, new ArgumentNullException("СalledClass"));
            
            // инициализируем объект контроллера  
            var instance = CreateInstance(calledClass);

            var calledMethod = calledClass.GetMethod(_methodName);
            var methodParamsCount = calledMethod.GetParameters().Length;
            object[] methodParams = methodParamsCount > 0 ? SetParams(calledMethod, receiveData) : null;

            if (methodParamsCount > 0 && methodParams?.Length != methodParamsCount)            
                return new Response("Недостаточно параметров для вызова метода", false, new ArgumentNullException("Action.Params"));
            
            var result = calledMethod.Invoke(instance, methodParams);

            var response = result as Response;

            if (response == null)
                return new Response("При обработке запроса произошла ошибка, ответ не получен", false, new ArgumentNullException("Method.Response"));

            return response;
        }

        // метод заполнения параметрического массива
        private object[] SetParams(MethodInfo method, dynamic data)
        {            
            var paramsCount = method.GetParameters().Length;
            object[] resultParams = new object[paramsCount];

            var paramType = method.GetParameters()[0].ParameterType;
            var jsonConvert = JsonConvert.DeserializeObject(data.ToString(), paramType);

            resultParams[0] = jsonConvert;
            return resultParams;
        }

        // инициализация объекта контроллера
        private object CreateInstance(Type calledClass)
        {            
            var ctorList = calledClass.GetConstructors();
            ConstructorInfo ctor = ctorList.OrderByDescending(c => c.GetParameters().Length).First();
            if (ctor == null)
            {
                throw new Exception("Ошибка при создании объекта");
            }

            if (ctor.GetParameters().Length == 0)
            {
                return ctor.Invoke(null);
            }

            var ctorParams = DIContainer.GetServices(ctor);
            return ctor.Invoke(ctorParams);
        }      
    }
}
