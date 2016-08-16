using System;

namespace GR.Core.Extensions
{
    public static class DateTimeExtension
    {
        /// <summary>
        /// 生成Unix时间戳格式(timestamp)
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long ToUnixEpochDate(this DateTime dateTime) => (long)Math.Round((dateTime.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

        /// <summary>
        /// Unix时间戳 转换成时间
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string timeStamp)
        {
            var dateTimeStart = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero).UtcDateTime;
            var ticks = long.Parse(timeStamp);//long.Parse(timeStamp+ "0000000")
            var timeSpan = new TimeSpan(ticks);
            return dateTimeStart.Add(timeSpan);
        }

        /// <summary>
        /// 计算两个时间的时间间隔返回秒
        /// </summary>
        /// <param name="endTime"></param>
        /// <param name="beginTime"></param>
        /// <returns></returns>
        public static int GetTimeDiff(this DateTime endTime, DateTime beginTime)
        {
            var ticks = endTime.Subtract(beginTime).Ticks;
            return (int)(ticks / 10000000);
        }
    }
}
