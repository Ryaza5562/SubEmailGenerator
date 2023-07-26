class Program
{
    static int CalculateAmountCombinations(string email)
    {
        string emailName = email.Substring(0, email.IndexOf('@'));
        int emailNameLength = emailName.Length;

        int n = emailName.Length - 1;
        int sum = 0;
        for (int k = 1; k <= n; k++)
        {
            if (k < 0 || k > n)
                throw new ArgumentException("Неверные аргументы");

            int numerator = 1;
            int denominator = 1;

            for (int i = n; i > n - k; i--)
                numerator *= i;
            for (int i = 1; i <= k; i++)
                denominator *= i;

            sum += (numerator / denominator);
        }
        return sum+1;
    }

    static List<string> SumCombinations(string emailName)
    {
        List<string> result = new List<string>();
        if (emailName.Length == 1)
        {
            result.Add(emailName);
            return result;
        }

        List<string> subCombinations = SumCombinations(emailName.Substring(1));
        foreach (string subCombination in subCombinations)
        {
            result.Add(emailName[0] + subCombination);
            result.Add(emailName[0] + "." + subCombination);
        }

        return result;
    }

    static void Main(string[] args)
    {
        try
        {
            Console.WriteLine("Generator subemails by Ryaza");
            Console.Write("Enter email: ");
            string email = Console.ReadLine();
            Console.WriteLine("Add password after emails(Yes/DEFAULT NO): ");
            string answer = Console.ReadLine();
            string password = "";
            if (answer.ToLower() == "yes")
            {
                Console.Write("Enter password: ");
                password = Console.ReadLine();
            }

            if (email != null || email != "")
            {
                int combinationResult = CalculateAmountCombinations(email);
                Console.WriteLine("Amount generating subemails: " + combinationResult);
            }
            else
                throw new ArgumentException();
            List<string> result = SumCombinations(email.Substring(0, email.IndexOf('@')));
            using (StreamWriter sw = new StreamWriter(File.Create("Emails.txt")))
            {
                for(int i = 0; i < result.Count; i++)
                {
                    if (answer.ToLower() == "yes")
                        sw.Write(result[i] + email.Substring(email.IndexOf('@')) + ":" + password);
                    else
                        sw.Write(result[i] + email.Substring(email.IndexOf('@')));

                    if (result.Count - i != 1)
                        sw.Write("\n");
                }
            }
            Console.WriteLine("Subemails generated in Email.txt file!");
            Console.ReadKey();
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}