using ikvm.@internal;
using IKVM.Runtime;
using java.io;
using java.lang;
using sun.misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static com.sun.org.apache.bcel.@internal.generic.InstructionConstants;

namespace Mixins
{
    public abstract class class121mod : class121
    {

        [MixinSettings(TargetClass = "class121", TargetMethod = "RunException_sendStackTrace", Replace = true)]
        public static void RunException_sendStackTrace(string str, System.Exception t)
        {
            try
            {
                string text = "";
                if (t != null)
                {
                    System.Exception ex = t;
                    string text2;
                    if (ex is RunException)
                    {
                        RunException ex2 = (RunException)ex;
                        text2 = new java.lang.StringBuilder().append(ex2.field4131).append(" | ").toString();
                        ex = ex2.field4132;
                    }
                    else
                    {
                        text2 = "";
                    }
                    StringWriter stringWriter = new StringWriter();
                    PrintWriter printWriter = new PrintWriter(stringWriter);
                    Throwable.instancehelper_printStackTrace(ex, printWriter);
                    printWriter.close();
                    string s = stringWriter.toString();
                    BufferedReader bufferedReader = new BufferedReader(new StringReader(s));
                    string str2 = bufferedReader.readLine();
                    for (; ; )
                    {
                        string text3 = bufferedReader.readLine();
                        if (text3 == null)
                        {
                            break;
                        }
                        int num = java.lang.String.instancehelper_indexOf(text3, 40);
                        int num2 = java.lang.String.instancehelper_indexOf(text3, 41, num + 1);
                        if (num >= 0 && num2 >= 0)
                        {
                            string text4 = java.lang.String.instancehelper_substring(text3, num + 1, num2);
                            int num3 = java.lang.String.instancehelper_indexOf(text4, ".java:");
                            if (num3 >= 0)
                            {
                                text4 = new java.lang.StringBuilder().append(java.lang.String.instancehelper_substring(text4, 0, num3)).append(java.lang.String.instancehelper_substring(text4, num3 + 5)).toString();
                                text2 = new java.lang.StringBuilder().append(text2).append(text4).append(' ').toString();
                                continue;
                            }
                            text3 = java.lang.String.instancehelper_substring(text3, 0, num);
                        }
                        text3 = java.lang.String.instancehelper_trim(text3);
                        text3 = java.lang.String.instancehelper_substring(text3, java.lang.String.instancehelper_lastIndexOf(text3, 32) + 1);
                        text3 = java.lang.String.instancehelper_substring(text3, java.lang.String.instancehelper_lastIndexOf(text3, 9) + 1);
                        text2 = new java.lang.StringBuilder().append(text2).append(text3).append(' ').toString();
                    }
                    text2 = new java.lang.StringBuilder().append(text2).append("| ").append(str2).toString();
                    string text5 = text2;
                    text = text5;
                }
                if (str != null)
                {
                    if (t != null)
                    {
                        text = new java.lang.StringBuilder().append(text).append(" | ").toString();
                    }
                    text = new java.lang.StringBuilder().append(text).append(str).toString();
                }
                System.Console.WriteLine(new java.lang.StringBuilder().append("Error: ").append(text).toString());
                text = java.lang.String.instancehelper_replace(text, ':', '.');
                text = java.lang.String.instancehelper_replace(text, '@', '_');
                text = java.lang.String.instancehelper_replace(text, '&', '_');
                text = java.lang.String.instancehelper_replace(text, '#', '_');
            }
            catch (System.Exception x)
            {
                if (ByteCodeHelper.MapException<java.lang.Exception>(x, ByteCodeHelper.MapFlags.Unused) == null)
                {
                    throw;
                }
                return;
            }
        }
    }
}
