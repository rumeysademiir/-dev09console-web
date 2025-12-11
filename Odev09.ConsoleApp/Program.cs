using System;
using System.Reflection;

namespace Odev09.ConsoleApp
{
    internal class Program
    {
        // Student isminde bir struct oluşturdum.
        // Bu yapıda Id, Name ve Gpa tutuyorum.
        public struct Student
        {
            public int Id;
            public string Name;
            public double Gpa;
        }

        // Bu attribute, sınıflara ve metotlara açıklama eklememi sağlıyor.
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
        public class InfoAttribute : Attribute
        {
            public string Author { get; set; }
            public string Message { get; set; }

            public InfoAttribute(string author, string message)
            {
                Author = author;
                Message = message;
            }
        }

        // Bu sınıfa attribute ekledim. Yani metadata ekliyorum.
        [Info("Rümeysa", "Bu sınıf öğrenci işlemlerini yapar.")]
        public class StudentManager
        {
            // Bu metoda da ayrı bir attribute ekledim.
            [Info("Sistem", "Öğrenci yazdırma metodu.")]
            public void Print(Student s)
            {
                Console.WriteLine($"{s.Id} - {s.Name} - {s.Gpa}");
            }
        }

        // Kendi özel exception sınıfımı oluşturdum.
        // Öğrencinin ortalaması düşükse bu hatayı fırlatacağım.
        public class LowGpaException : Exception
        {
            public LowGpaException(string message) : base(message)
            {
            }
        }

        // === OBSOLETE ATTRIBUTE METOTLARI ===

        // Sadece uyarı veren eski metot (warning)
        [Obsolete("Bu metot artık kullanılmamalı! Yeni versiyonunu kullanınız.", false)]
        public static void OldMethodWarning()
        {
            Console.WriteLine("OldMethodWarning çalıştı (uyarı veriyor).");
        }

        // Derleme hatası veren metot (error)
        // NOT: Bu satır çalıştırılırsa derleme hatası verir.
        // Bu nedenle yorum satırına aldım.
        [Obsolete("Bu metot tamamen kaldırıldı! Kullanmanız yasak.", true)]
        public static void OldMethodError()
        {
            Console.WriteLine("OldMethodError çalıştı (hata vermeli).");
        }

        static void Main(string[] args)
        {
            // 3 tane öğrenci oluşturdum. Değerleri tek tek verdim.
            var s1 = new Student { Id = 1, Name = "rümeysa", Gpa = 3.0 };
            var s2 = new Student { Id = 2, Name = "emine", Gpa = 2.6 };
            var s3 = new Student { Id = 3, Name = "nisa", Gpa = 3.5 };

            Console.WriteLine("=== STUDENT LIST ===");
            Console.WriteLine($"{s1.Id} - {s1.Name} - {s1.Gpa}");
            Console.WriteLine($"{s2.Id} - {s2.Name} - {s2.Gpa}");
            Console.WriteLine($"{s3.Id} - {s3.Name} - {s3.Gpa}");

            Console.WriteLine("\n=== EXCEPTION HANDLING ===");

            try
            {
                Console.Write("Bir sayı gir: ");
                int a = int.Parse(Console.ReadLine());

                Console.Write("Bölecek sayı gir: ");
                int b = int.Parse(Console.ReadLine());

                int sonuc = a / b;
                Console.WriteLine("Sonuç: " + sonuc);
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Hata: Sıfıra bölünemez!");
            }
            catch (FormatException)
            {
                Console.WriteLine("Hata: Lütfen sadece sayı giriniz!");
            }
            finally
            {
                Console.WriteLine("Finally bloğu çalıştı!");
            }

            Console.WriteLine("\n=== CUSTOM EXCEPTION (LowGpaException) ===");

            try
            {
                if (s2.Gpa < 2.8)
                {
                    throw new LowGpaException($"{s2.Name} öğrencisinin not ortalaması çok düşük!");
                }
            }
            catch (LowGpaException ex)
            {
                Console.WriteLine("Custom Exception Yakalandı: " + ex.Message);
            }

            Console.WriteLine("\n=== REFLECTION — ATTRIBUTES OKUMA ===");

            Type managerType = typeof(StudentManager);
            var classAttribute = managerType.GetCustomAttribute<InfoAttribute>();
            Console.WriteLine($"Sınıf Author: {classAttribute.Author}, Mesaj: {classAttribute.Message}");

            var printMethod = managerType.GetMethod("Print");
            var methodAttribute = printMethod.GetCustomAttribute<InfoAttribute>();
            Console.WriteLine($"Metod Author: {methodAttribute.Author}, Mesaj: {methodAttribute.Message}");

            Console.WriteLine("\n=== REFLECTION İLE METOD ÇAĞIRMA ===");
            var managerInstance = Activator.CreateInstance(managerType);
            printMethod.Invoke(managerInstance, new object[] { s1 });

            Console.WriteLine("\n=== OBSOLETE ATTRIBUTE TEST ===");

            // Uyarı veren metot → çalışır
            OldMethodWarning();

            // Derleme hatası veren metot → YORUMDA KALMAK ZORUNDA!
            // OldMethodError();

            Console.WriteLine("\nProgram bitti!");
        }
    }
}