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
                throw new ArgumentException("Bad email");

            int numerator = 1;
            int denominator = 1;

            for (int i = n; i > n - k; i--)
                numerator *= i;
            for (int i = 1; i <= k; i++)
                denominator *= i;

            sum += (numerator / denominator);
        }
        return sum + 1;
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

    static void WriteInFile(string pathToSave, List<string> nameEmail, string domainEmail, string password)
    {
        using (StreamWriter sw = new StreamWriter(File.Create(pathToSave + "Emails.txt")))
        {
            for (int i = 0; i < nameEmail.Count; i++)
            {
                if (password != "")
                    sw.Write(nameEmail[i] + domainEmail + ":" + password);
                else
                    sw.Write(nameEmail[i] + domainEmail);

                if (nameEmail.Count - i != 1)
                    sw.Write("\n");
            }
        }
    }

    static void Main(string[] args)
    {
        string email = "";
        string password = "";
        bool cmdUsage = false;
        if (args.Length > 0)
        {
            cmdUsage = true;
            if (args.Length > 0)
                email = args[0];
            if (args.Length == 2)
                password = args[1];
        }
        try
        {
            if (!cmdUsage)
            {
                Console.WriteLine("Generator subemails by Ryaza");
                Console.Write("Enter email: ");
                email = Console.ReadLine();
                Console.WriteLine("Add password after emails(Yes/DEFAULT NO): ");
                string answer = Console.ReadLine();
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
                    throw new Exception();
            }
            List<string> result = SumCombinations(email.Substring(0, email.IndexOf('@')));
            WriteInFile(AppDomain.CurrentDomain.BaseDirectory, result, email.Substring(email.IndexOf('@')), password);
            Console.WriteLine("Subemails generated in Email.txt file!");
            if (!cmdUsage)
                Console.ReadKey();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}