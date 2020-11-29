using System;

namespace SagaSample.Common
{
    public class QueueNames
    {
        private const string rabbitUri = "queue:";
        public static Uri GetMessageUri(string key)
        {
            return new Uri(rabbitUri + key.PascalToKebabCaseMessage());
        }
        public static Uri GetActivityUri(string key)
        {
            var kebabCase = key.PascalToKebabCaseActivity();
            if (kebabCase.EndsWith('-'))
            {
                kebabCase = kebabCase.Remove(kebabCase.Length - 1);
            }
            return new Uri(rabbitUri + kebabCase + '_' + "execute");
        }
    }
}
