 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
namespace SalesApplication 
{
    public static class DataCache
    {
        static ObjectCache cache = new MemoryCache("Sales");
        static object locker = new object();

        static readonly string allstatemasterkey = "allstatemasterkey";
        static readonly string allCityMasters = "allCityMasters";
        static readonly string roleskey = "roleskey";
        static readonly string allEmployeeskey = "allEmployeeskey";
        static readonly string allEmployeeInRolekey = "allEmployeeInRolekey";
        static string mapPath;

        public static void Load()
        {
            var s = MapPath;
            var stateMasters = StateMasters;


            var cityMasters = CityMasters;
            var roles = Roles;
            var employees =Employees;
            var emoloyeeInRole = EmployeeInRoles;


        }

        public static string MapPath
        {
            get
            {
                if (string.IsNullOrEmpty(mapPath))
                {
                    mapPath = System.Web.HttpContext.Current.Server.MapPath("~");
                }
                return mapPath;
            }
        }


        #region States 
        public static List<StateMaster> StateMasters
        {
            get
            {
                var list = cache[allstatemasterkey] as List<StateMaster>;

                // ** Lazy Load

                if (list == null)
                {
                    using (SalesContainer db = new SalesContainer())
                    {
                        var states = from s in db.StateMasters where s.IsActive == true select s;
                        if(states!=null && states.Count()>0)
                        {
                            list = states.ToList();
                        }
                   
                        Add(allstatemasterkey, list, DateTime.Now.AddHours(6));
                    }
                }
                return list;
            }
        } 
        public static void RefreshStateMasters()
        {
            Clear(allstatemasterkey);
            var x = StateMasters;
        }

        #endregion

        #region Cities
        public static List<CityMaster> CityMasters
        {
            get
            {
                var list = cache[allCityMasters] as List<CityMaster>;

                // ** Lazy Load

                if (list == null)
                {
                    using (SalesContainer db = new SalesContainer())
                    {
                        var cities = from s in db.CityMasters where s.IsActive == true select s;
                        if (cities != null && cities.Count() > 0)
                        {
                            list = cities.ToList();
                        }

                        Add(allCityMasters, list, DateTime.Now.AddHours(6));
                    }
                }
                return list;
            }
        }
        public static void RefreshCityMasters()
        {
            Clear(allCityMasters);
            var x = CityMasters;
        }

        #endregion

        #region Role 
        public static List<Role> Roles
        {
            get
            {
                var list = cache[roleskey] as List<Role>;

                // ** Lazy Load

                if (list == null)
                {
                    using (SalesContainer db = new SalesContainer())
                    {
                        var roles = from s in db.Roles where s.IsActive == true select s;
                        if (roles != null && roles.Count() > 0)
                        {
                            list = roles.ToList();
                        }

                        Add(roleskey, list, DateTime.Now.AddHours(6));
                    }
                }
                return list;
            }
        }
        public static void RefreshRoles()
        {
            Clear(roleskey);
            var x = Roles;
        }

        #endregion

        #region Employees 
        public static List<Employee> Employees
        {
            get
            {
                var list = cache[allEmployeeskey] as List<Employee>;

                // ** Lazy Load

                if (list == null)
                {
                    using (SalesContainer db = new SalesContainer())
                    {
                        var employees = from s in db.Employees where s.IsActive == true select s;
                        if (employees != null && employees.Count() > 0)
                        {
                            list = employees.ToList();
                        }

                        Add(allEmployeeskey, list, DateTime.Now.AddHours(6));
                    }
                }
                return list;
            }
        }
        public static void RefreshEmployees()
        {
            Clear(allEmployeeskey);
            var x = Employees;
        }

        #endregion

        #region EmployeeInRole
        public static List<EmployeeInRole> EmployeeInRoles
        {
            get
            {
                var list = cache[allEmployeeInRolekey] as List<EmployeeInRole>;

                // ** Lazy Load

                if (list == null)
                {
                    using (SalesContainer db = new SalesContainer())
                    {
                        var employees = from s in db.EmployeeInRoles where s.IsActive == true select s;
                        if (employees != null && employees.Count() > 0)
                        {
                            list = employees.ToList();
                        }

                        Add(allEmployeeInRolekey, list, DateTime.Now.AddHours(6));
                    }
                }
                return list;
            }
        }
        public static void RefreshEmployeeInRoless()
        {
            Clear(allEmployeeInRolekey);
            var x = Employees;
        }

        #endregion


        static void Clear(string key)
        {
            cache.Remove(key);
        } 
        static void Add(string key, object value, DateTimeOffset expiration, CacheItemPriority priority = CacheItemPriority.Default)
        {
            var policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = expiration;
            policy.Priority = priority;
            var item = new CacheItem(key, value);
            cache.Add(item, policy);
        }
    }
}