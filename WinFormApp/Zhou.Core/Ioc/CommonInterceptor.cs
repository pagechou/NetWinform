using Castle.DynamicProxy;
using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhou.Core.Attributes;

namespace Zhou.Core.Ioc
{
    internal class CommonInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            //before... do something
            //在被拦截的方法执行完毕后 继续执行
            //invocation.Proceed();
            //after... do somthing
            try
            {
                //调用数据库链接
                var db = (DataConnection)invocation.TargetType.BaseType.InvokeMember("GetDBConnection",
                    System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public
                    , null, null, new object[] { null });
                //特性校验
                bool needTrans = invocation.Method.GetCustomAttributes(typeof(StartTrans), false).Count() >= 1;//开启事务特性
                using (db)
                {
                    if (needTrans)
                    {
                        try
                        {
                            db.BeginTransaction();
                            //事务
                            invocation.Proceed();
                            db.CommitTransaction();
                        }
                        catch (Exception ex)
                        {
                            db.RollbackTransaction();
                            throw ex;
                        }
                    }
                    else
                    {
                        //不开启事务
                        invocation.Proceed();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// <para>DAO层异常时，不throw，返回设定的值.</para>
        /// <para>1. 返回复杂类型，使用Type，复杂类型需要有无参的构造函数</para>
        /// <para>2. 返回简单类型，使用value</para>
        /// </summary>
        [AttributeUsage(AttributeTargets.Method)]
        public class ExceptionReturnAttribute : System.Attribute
        {
            /// <summary>
            /// 返回复杂类型，使用Type，复杂类型需要有无参的构造函数
            /// </summary>
            public Type Type { get; set; }

            /// <summary>
            /// 返回简单类型，使用value
            /// </summary>
            public object Value { get; set; }
        }
    }
}
