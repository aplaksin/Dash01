using System;
using System.Collections.Generic;

public class AllServices
{
    private static AllServices _instance;
    public static AllServices Container => _instance ?? (_instance = new AllServices());
    private Dictionary<Type, IService> _services = new Dictionary<Type, IService>();


    public void RegisterSingle<TService>(TService implementation) where TService : IService
    {
        _services.Add(implementation.GetType(), implementation);
    }

    public TService Single<TService>() where TService : class, IService
    {
        //TODO проверка?
        return _services[typeof(TService)] as TService; //TODO подумать над изменением даункаста
    }
}
