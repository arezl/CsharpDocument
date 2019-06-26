using AspectCore.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyHystrix1
{
    [AttributeUsage(AttributeTargets.Method)]
    class HystrixCommandAttribute : AbstractInterceptorAttribute
    {
        private string fallBackMethod;
        public HystrixCommandAttribute(string fallBackMethod)
        {
            this.fallBackMethod = fallBackMethod;
        }

        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            try
            {
                await next(context);
            }
            catch(Exception)
            {
                Console.WriteLine("出错");
                //调用降级方法
                //1、获得降级的方法(根据对象获取类，从类获取方法)
                //2、调用降级的方法
                //3、把降级方法的返回值返回

                //获取到降级方法
                MethodInfo fallbackMI = context.Implementation.GetType().GetMethod(fallBackMethod);
                object returnValue = fallbackMI.Invoke(context.Implementation, context.Parameters);
                context.ReturnValue = returnValue;//把降级方法的返回值作为我们的返回值

                //throw;
            }
        }
    }
}
