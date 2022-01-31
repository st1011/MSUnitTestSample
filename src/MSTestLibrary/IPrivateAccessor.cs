namespace MSTestLibrary
{
    /// <summary>
    /// Private要素へのアクセサI/F
    /// </summary>
    public interface IPrivateAccessor
    {
        /// <summary>
        /// メソッドの実行
        /// </summary>
        /// <typeparam name="T">呼び出すメソッドの戻り値の型</typeparam>
        /// <param name="name">呼び出すメソッド名</param>
        /// <param name="args">呼び出すメソッドの引数</param>
        /// <returns>呼び出すメソッドの戻り値</returns>
        T DoMethod<T>(string name, params object[] args);

        /// <summary>
        /// メソッドの実行
        /// </summary>
        /// <param name="name">呼び出すメソッド名</param>
        /// <param name="args">呼び出すメソッドの引数</param>
        void DoMethod(string name, params object[] args);

        /// <summary>
        /// プロパティ/メンバ変数の値の取得
        /// </summary>
        /// <typeparam name="T">取得する対象の型</typeparam>
        /// <param name="name">取得する対象の名前</param>
        /// <returns>取得した値</returns>
        T GetMember<T>(string name);

        /// <summary>
        /// プロパティ/メンバ変数への値の設定
        /// </summary>
        /// <param name="name">設定する対象の名前</param>
        /// <param name="value">設定する値</param>
        void SetMember(string name, object value);
    }
}
