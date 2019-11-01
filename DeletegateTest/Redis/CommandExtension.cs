using StackExchange.Redis;
using System.Collections.Generic;

namespace DeletegateTest.Redis
{
    public static class CommandExtension
    {
        public static string[] RedisValueToString(this RedisValue[] values)
        {
            if (values == null)
                return null;
            var result = new string[values.Length];
            for (int i = 0; i < values.Length; i++)
                result[i] = values[i];
            return result;
        }

        public static byte[][] RedisValueToByte(this RedisValue[] values)
        {
            if (values == null)
                return null;
            var result = new byte[values.Length][];
            for (int i = 0; i < values.Length; i++)
                result[i] = values[i];
            return result;
        }

        public static RedisValue[] StringToRedisValue(this string[] values)
        {
            if (values == null)
                return null;
            var result = new RedisValue[values.Length];
            for (int i = 0; i < values.Length; i++)
                result[i] = values[i];
            return result;
        }

        public static RedisValue[] ByteToRedisValue(this byte[][] values)
        {
            if (values == null)
                return null;
            var result = new RedisValue[values.Length];
            for (int i = 0; i < values.Length; i++)
                result[i] = values[i];
            return result;
        }

        public static RedisKey[] StringToRedisKey(this string[] keys)
        {
            if (keys == null)
                return null;
            var result = new RedisKey[keys.Length];
            for (int i = 0; i < keys.Length; i++)
                result[i] = keys[i];
            return result;
        }

        public static string[] RedisKeyToString(this RedisKey[] keys)
        {
            if (keys == null)
                return null;
            var result = new string[keys.Length];
            for (int i = 0; i < keys.Length; i++)
                result[i] = keys[i];
            return result;
        }

        public static Dictionary<string, byte[]> HashEntryToDicByte(this HashEntry[] entryList)
        {
            if (entryList == null)
                return null;
            var dic = new Dictionary<string, byte[]>();
            foreach (var entry in entryList)
            {
                dic.Add(entry.Name, entry.Value);
            }
            return dic;
        }

        public static Dictionary<string, string> HashEntryToDicString(this HashEntry[] entryList)
        {
            if (entryList == null)
                return null;
            var dic = new Dictionary<string, string>();
            foreach (var entry in entryList)
            {
                dic.Add(entry.Name, entry.Value);
            }
            return dic;
        }

        public static HashEntry[] DicStringToHashEntry(this Dictionary<string, string> dic)
        {
            if (dic == null)
                return null;
            var entryList = new HashEntry[dic.Count];
            int index = 0;
            foreach (var pair in dic)
            {
                entryList[index] = new HashEntry(pair.Key, pair.Value);
                index++;
            }
            return entryList;
        }

        public static HashEntry[] DicByteToHashEntry(this Dictionary<string, byte[]> dic)
        {
            if (dic == null)
                return null;
            var entryList = new HashEntry[dic.Count];
            int index = 0;
            foreach (var pair in dic)
            {
                entryList[index] = new HashEntry(pair.Key, pair.Value);
                index++;
            }
            return entryList;
        }

        public static SortedSetEntry[] DicByteToZSetEntry(this Dictionary<byte[], double> dic)
        {
            if (dic == null)
                return null;
            var entryList = new SortedSetEntry[dic.Count];
            int index = 0;
            foreach (var pair in dic)
            {
                entryList[index] = new SortedSetEntry(pair.Key, pair.Value);
                index++;
            }
            return entryList;
        }

        public static SortedSetEntry[] DicStringToZSetEntry(this Dictionary<string, double> dic)
        {
            if (dic == null)
                return null;
            var entryList = new SortedSetEntry[dic.Count];
            int index = 0;
            foreach (var pair in dic)
            {
                entryList[index] = new SortedSetEntry(pair.Key, pair.Value);
                index++;
            }
            return entryList;
        }

        public static Dictionary<byte[], double> ZSetEntryToDicByte(this SortedSetEntry[] entryList)
        {
            if (entryList == null)
                return null;
            var dic = new Dictionary<byte[], double>();
            foreach (var entry in entryList)
            {
                dic.Add(entry.Element, entry.Score);
            }
            return dic;
        }

        public static Dictionary<string, double> ZSetEntryToDicString(this SortedSetEntry[] entryList)
        {
            if (entryList == null)
                return null;
            var dic = new Dictionary<string, double>();
            foreach (var entry in entryList)
            {
                dic.Add(entry.Element, entry.Score);
            }
            return dic;
        }
    }
}
