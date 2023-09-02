using AutomationPipeline.Helper.Infrastructure;
using System.Linq;

namespace AutomationPipeline.Helper.Common
{
    public class GenericHandler
    {
        public static T? TryExecute<T>(Func<T?> action) where T : IError
        {
            T? result;
            try
            {
                return action();
            }
            catch (Exception ex)
            {
                result = Activator.CreateInstance<T>();
                result.ErrorMessage = ex.Message;
                return result;
            }
        }

        public static T[] InputBuilder<T>(List<ConsoleUserInterface> prompts)
        {

            T[] inputs = new T[prompts.Count];

            for (int i = 0; i < prompts.Count; i++)
            {
                Console.WriteLine(prompts[i].PromptMessage);
                string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    inputs[i] = default(T); 
                    continue;
                }

                try
                {
                    inputs[i] = (T)Convert.ChangeType(input, typeof(T));
                }
                catch (Exception e)
                {
                    Console.WriteLine("Invalid input: " + e.Message);
                }
            }
            return inputs;
        }

    }

    public class ConsoleUserInterface
    {
        public string PromptMessage { get; set; }
    }
}
