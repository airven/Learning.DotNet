using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace DeletegateTest.Redis
{
    public class RedisDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private string _redisKey;
        private IDatabase _dataBase;
        public IDatabase RedisBase { get { return _dataBase; } }
        private bool _connectState = true;
        private static RedisSource redisClient;
        public string DataBase { get; set; }
        public RedisDictionary(string projectName, string dataBase, string env, string redisKey)
        {
            _redisKey = redisKey;
            DataBase = dataBase;
            redisClient = RedisSource.GetClient(env, projectName);
            if (redisClient.GetDatabase(dataBase) == null) ConnectState = false;
        }
        private string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
        private T Deserialize<T>(string serialized)
        {
            return JsonConvert.DeserializeObject<T>(serialized);
        }

        public bool ConnectState
        {
            get { return _connectState; }
            set { this._connectState = value; }
        }
        public void Add(TKey key, TValue value)
        {
            if (ConnectState)
            {
                _dataBase = redisClient.GetDatabase(DataBase) ?? null;
                _dataBase.HashSet(_redisKey, Serialize(key), Serialize(value));
            } 
        }
        public bool ContainsKey(TKey key)
        {
            if (ConnectState)
            {
                _dataBase = redisClient.GetDatabase(DataBase) ?? null;
                return _dataBase.HashExists(_redisKey, Serialize(key));
            }
            else
                return false;
        }
        public bool Remove(TKey key)
        {
            _dataBase = redisClient.GetDatabase(DataBase) ?? null;
            return ConnectState ? _dataBase.HashDelete(_redisKey, Serialize(key)) : false;
        }
        public bool TryGetValue(TKey key, out TValue value)
        {
            if (ConnectState)
            {
                _dataBase = redisClient.GetDatabase(DataBase) ?? null;
                var redisValue = _dataBase.HashGet(_redisKey, Serialize(key));
                if (redisValue.IsNull)
                {
                    value = default(TValue);
                    return false;
                }
                value = Deserialize<TValue>(redisValue.ToString());
                return true;
            }
            value = default(TValue);
            return false;

        }
        public ICollection<TValue> Values
        {
            get
            {
                if (ConnectState)
                {
                    _dataBase = redisClient.GetDatabase(DataBase) ?? null;
                    return new Collection<TValue>(_dataBase.HashValues(_redisKey).Select(h => Deserialize<TValue>(h.ToString())).ToList());
                }
                else
                    return null;
            }
        }
        public ICollection<TKey> Keys
        {
            get
            {
                if (ConnectState)
                {
                    _dataBase = redisClient.GetDatabase(DataBase) ?? null;
                    return new Collection<TKey>(_dataBase.HashKeys(_redisKey).Select(h => Deserialize<TKey>(h.ToString())).ToList());
                }
                else
                    return null;
            }
        }
        public TValue this[TKey key]
        {
            get
            {
                if (ConnectState)
                {
                    _dataBase = redisClient.GetDatabase(DataBase) ?? null;
                    var redisValue = _dataBase.HashGet(_redisKey, Serialize(key));
                    return redisValue.IsNull ? default(TValue) : Deserialize<TValue>(redisValue.ToString());
                }
                else
                {
                    return default(TValue);
                }
            }
            set
            {
                Add(key, value);
            }
        }
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }
        public void Clear()
        {
            if (ConnectState)
                _dataBase.KeyDelete(_redisKey);
        }
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            if (ConnectState)
            {
                _dataBase = redisClient.GetDatabase(DataBase) ?? null;
                return _dataBase.HashExists(_redisKey, Serialize(item.Key));
            }
            return false;
        }
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            if (ConnectState)
            {
                _dataBase = redisClient.GetDatabase(DataBase) ?? null;
                _dataBase.HashGetAll(_redisKey).CopyTo(array, arrayIndex);
            }
        }
        public int Count
        {
            get
            {
                if (ConnectState)
                {
                    _dataBase = redisClient.GetDatabase(DataBase) ?? null;
                    return (int)_dataBase.HashLength(_redisKey);
                }
                else
                    return 0;
            }
        }
        public bool IsReadOnly
        {
            get { return false; }
        }
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return Remove(item.Key);
        }
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            if (ConnectState)
            {
                var db = redisClient.GetDatabase(DataBase) ?? null;
                foreach (var hashKey in db.HashKeys(_redisKey))
                {
                    var redisValue = db.HashGet(_redisKey, hashKey);
                    yield return new KeyValuePair<TKey, TValue>(Deserialize<TKey>(hashKey.ToString()), Deserialize<TValue>(redisValue.ToString()));
                }
            }
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            yield return GetEnumerator();
        }
        public void AddMultiple(IEnumerable<KeyValuePair<TKey, TValue>> items)
        {
            if (ConnectState)
            {
                var _dataBase = redisClient.GetDatabase(DataBase) ?? null;
                _dataBase
                  .HashSet(_redisKey, items.Select(i => new HashEntry(Serialize(i.Key), Serialize(i.Value))).ToArray());
            }
        }
    }
}
