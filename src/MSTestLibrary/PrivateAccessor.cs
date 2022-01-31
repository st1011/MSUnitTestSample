using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Concurrent;

namespace MSTestLibrary
{
    /// <summary>
    /// Privateなメソッドやプロパティへのアクセサファクトリ
    /// </summary>
    public static class PrivateAccessorFactory
    {
        private static ConcurrentDictionary<object, IPrivateAccessor> Cache { get; }

        static PrivateAccessorFactory()
        {
            Cache = new ConcurrentDictionary<object, IPrivateAccessor>();
        }

        /// <summary>
        /// アクセサの作成
        /// </summary>
        /// <param name="target">対象のインスタンスか<see cref="Type"/></param>
        /// <returns>アクセサ</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IPrivateAccessor Create(object target)
        {
            if (target is null) { throw new ArgumentNullException(nameof(target)); }
            if (Cache.TryGetValue(target, out var cachedAccessor))
            {
                return cachedAccessor;
            }

            IPrivateAccessor accessor;
            if (target is Type type) { accessor = new PrivateTypeAccessor(type); }
            else { accessor = new PrivateObjectAccessor(target); }

            // 排他処理していないが、上書きしても問題ない
            Cache[target] = accessor;

            return accessor;
        }
    }

    internal class PrivateObjectAccessor : IPrivateAccessor
    {
        private PrivateObject PrivateObject { get; }

        public PrivateObjectAccessor(object target)
        {
            PrivateObject = new PrivateObject(target);
        }

        /// <inheritdoc/>
        public T DoMethod<T>(string name, params object[] args) => (T)PrivateObject.Invoke(name, args);

        /// <inheritdoc/>
        public void DoMethod(string name, params object[] args) => PrivateObject.Invoke(name, args);

        /// <inheritdoc/>
        public T GetMember<T>(string name) => (T)PrivateObject.GetFieldOrProperty(name);

        /// <inheritdoc/>
        public void SetMember(string name, object value) => PrivateObject.SetFieldOrProperty(name, value);
    }

    internal class PrivateTypeAccessor : IPrivateAccessor
    {
        private PrivateType PrivateType { get; }

        public PrivateTypeAccessor(Type target)
        {
            PrivateType = new PrivateType(target);
        }

        /// <inheritdoc/>
        public T DoMethod<T>(string name, params object[] args) => (T)PrivateType.InvokeStatic(name, args);

        /// <inheritdoc/>
        public void DoMethod(string name, params object[] args) => PrivateType.InvokeStatic(name, args);

        /// <inheritdoc/>
        public T GetMember<T>(string name) => (T)PrivateType.GetStaticFieldOrProperty(name);

        /// <inheritdoc/>
        public void SetMember(string name, object value) => PrivateType.SetStaticFieldOrProperty(name, value);
    }
}
