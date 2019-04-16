using System.IO;
using System.Text;
using Google.Protobuf;

namespace SSD.ATLASti.Common.Text.Internal
{
    public static class ProtobufExtensions
    {
        public static string PrettyPrint(this string str)
        {
            var indent        = 0;
            var quoted        = false;
            var stringBuilder = new StringBuilder(str.Length);
            for (var i = 0; i < str.Length; i++)
            {
                var ch = str[i];
                switch (ch)
                {
                    case '{':
                    case '[':
                        stringBuilder.Append(ch);
                        if (!quoted)
                        {
                            stringBuilder.AppendLine();
                            stringBuilder.Append(' ', ++indent);
                        }

                        break;
                    case '}':
                    case ']':
                        if (!quoted)
                        {
                            stringBuilder.AppendLine();
                            stringBuilder.Append(' ', --indent);
                        }

                        stringBuilder.Append(ch);
                        break;
                    case '"':
                        stringBuilder.Append(ch);
                        var escaped = false;
                        var index   = i;
                        while (index > 0 && str[--index] == '\\')
                        {
                            escaped = !escaped;
                        }

                        if (!escaped)
                        {
                            quoted = !quoted;
                        }

                        break;
                    case ',':
                        stringBuilder.Append(ch);
                        if (!quoted)
                        {
                            stringBuilder.AppendLine();
                            stringBuilder.Append(' ', indent);
                        }

                        break;
                    case ':':
                        stringBuilder.Append(ch);
                        if (!quoted)
                        {
                            stringBuilder.Append(" ");
                        }

                        break;
                    default:
                        stringBuilder.Append(ch);
                        break;
                }
            }

            return stringBuilder.ToString();
        }

        public static void WriteJsonFile(this IMessage message, string jsonFilePath)
        {
            var jsonFormatter = new JsonFormatter(JsonFormatter.Settings.Default);
            var json          = jsonFormatter.Format(message);
            var formattedJson = json.PrettyPrint();
            File.WriteAllText(jsonFilePath, formattedJson);
        }
    }
}
