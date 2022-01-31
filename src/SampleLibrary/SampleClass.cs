using System;

namespace SampleLibrary
{
    public class SampleClass
    {
        private static readonly object s_locker = new object();

        public static int NextId { get => s_id; }
        private static int s_id = 0;

        public SampleClass(string name, int age)
        {
            lock (s_locker)
            {
                Id = s_id++;
            }
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Age = age;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get => m_age; set => m_age = value; }
        private int m_age;

        public string GetName() => GetNameInternal();

        private string GetNameInternal()
        {
            return $"{Name} {Age}";
        }

        public int GetCryptedAge(int seed) => GetCryptedAgeInternal(seed);

        private int GetCryptedAgeInternal(int seed)
        {
            return Age ^ seed;
        }

        public static int GetNextId() => GetNextIdInternal();

        private static int GetNextIdInternal()
        {
            return s_id;
        }

        public static int GetCryptedNextId(int seed) => GetCryptedNextIdInternal(seed);

        private static int GetCryptedNextIdInternal(int seed)
        {
            return s_id ^ seed;
        }

        public void DummyWrite(string text)
        {
            _ = text ?? throw new ArgumentNullException(nameof(text));
        }
    }
}
