using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MSTestLibrary
{
    /// <summary>
    /// テストクラスのベース実装
    /// </summary>
    public class TestClassBase
    {
        /// <summary>
        /// コンテキスト
        /// </summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        /// テストコンテキストの取得用
        /// </summary>
        /// <typeparam name="T">取得した要素の型</typeparam>
        /// <param name="key">タイトル</param>
        /// <returns>テストデータ</returns>
        protected T TestField<T>(string key) => (T)TestContext.DataRow[key];

        /// <summary>
        /// 指定された型をthrowするかのテスト用へルパ
        /// </summary>
        /// <typeparam name="T">例外型</typeparam>
        /// <param name="action">実行する処理</param>
        /// <returns>発生した例外内容</returns>
        /// <exception cref="ArgumentNullException"></exception>
        protected T ThrowsException<T>(Action action)
            where T : Exception
        {
            if (action == null) { throw new ArgumentNullException(nameof(action)); }

            T throwedException = null;
            try
            {
                action();
                Assert.Fail($"ThrowsException failed. No exception thrown. {nameof(T)} exception was expected.");
            }
            catch (T ex)
            {
                throwedException = ex;
            }

            return throwedException;
        }
    }
}
