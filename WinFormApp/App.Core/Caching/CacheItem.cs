using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Caching
{
    public class CacheItem
    {
        /// <summary>
        /// 缓存名称
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 缓存内容
        /// </summary>
        public object Data { get; set; }
        /// <summary>
        /// 缓存时间，单位分钟
        /// </summary>
        public int CacheTimes { get; set; }

        public DateTime Expir { get; set; }


    }
}
