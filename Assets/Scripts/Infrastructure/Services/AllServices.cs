using System;
using System.Collections.Generic;

using UnityEngine;
public class AllServices
{
    private static AllServices _instance;
    public static AllServices Container => _instance ?? (_instance = new AllServices());
    private Dictionary<Type, IService> _services = new Dictionary<Type, IService>();


    public void RegisterSingle<TService>(TService implementation) where TService : IService
    {
        //Debug.Log(typeof(TService));
        _services.Add(typeof(TService), implementation);
        
    }

    public TService Single<TService>() where TService : class, IService
    {
        //TODO проверка?
        return _services[typeof(TService)] as TService; //TODO подумать над изменением даункаста
    }


    /*    private static AllServices _instance;
        public static AllServices Container => _instance ?? (_instance = new AllServices());

        public void RegisterSingle<TService>(TService implementation) where TService : IService =>
          Implementation<TService>.ServiceInstance = implementation;

        public TService Single<TService>() where TService : IService =>
          Implementation<TService>.ServiceInstance;

        private class Implementation<TService> where TService : IService
        {
            public static TService ServiceInstance;
        }*/
}
