using App.Core.Init;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.UseAge
{
    /// <summary>
	/// 缓存对象
	/// </summary>
    public interface ICache: IInitIoc
    {
        /// <summary>
        /// 清空所有的缓存
        /// </summary> 
        void Clear(string key = null);

        /// <summary>
        /// 获取一个缓存对象
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="factory">初始化缓存表达式</param>
        /// <param name="seconds">默认缓存时间，单位秒</param>
        /// <returns>缓存值</returns>
        object Get(string key, Func<string, object> factory = null, int seconds = 7200);

        /// <summary>
        /// 获取所有的缓存键
        /// </summary> 
        List<string> GetKeys();

        /// <summary>
        /// 移除指定的缓存对象
        /// </summary> 
        void Remove(string key);

        /// <summary>
        /// 按秒设置缓存对象
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        /// <param name="slidingExpireSeconds">多少秒后过期</param>
        void SetWithSeconds(string key, object value, int slidingExpireSeconds = 28800);
        /// <summary>
        /// 按分钟设置缓存对象
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="slidingExpireMins"></param>
        void SetWithMins(string key, object value, int slidingExpireMins = 60);

        /// <summary>
        /// 根据名称类型获取对象
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">名称</param>
        /// <returns></returns>
        T Get<T>(string key);

        /// <summary>
        /// 根据名称获取对象
        /// </summary>
        /// <param name="key">名称</param>
        /// <returns></returns>
        object GetItem(string key);


    }
}