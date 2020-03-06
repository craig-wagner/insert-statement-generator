#region using

using System;
using System.CodeDom.Compiler;
using System.Text;
using Microsoft.SqlServer.Management.Smo;

#endregion using

namespace Wagner.InsertStatementGenerator
{
    internal class PropertyObjectCreator
    {
        public static object CreatePropertyObjectFromTable(Table table)
        {
            const string codeTemplate = @"
                using System;

                namespace Wagner.InsertStatementGenerator
                {
                    public class ColumnValues
                    {
                        <% code %>
                    }
                }";

            var generatedCode = new StringBuilder(50000);

            foreach (Column column in table.Columns)
            {
                generatedCode.AppendFormat("public {0} {1};", column.DataType.Name, column.Name);
                generatedCode.Append(Environment.NewLine);
            }

            var codeToCompile = codeTemplate.Replace("<% code %>", generatedCode.ToString());

            var compiler = CodeDomProvider.CreateProvider("CSharp");
            var compilerParameters = new CompilerParameters();

            // Add any referenced assemblies
            compilerParameters.ReferencedAssemblies.Add("System.dll");

            // Load the resulting assembly into memory
            compilerParameters.GenerateInMemory = false;

            // Now compile the whole thing
            var compilerResults = compiler.CompileAssemblyFromSource(compilerParameters, codeToCompile);

            if (compilerResults.Errors.HasErrors)
            {
                string errorMsg = compilerResults.Errors.Count.ToString() + " Errors:";
                for (var i = 0; i < compilerResults.Errors.Count; i++)
                {
                    errorMsg = errorMsg + "\r\nLine: " +
                        compilerResults.Errors[i].Line.ToString() + " - " +
                        compilerResults.Errors[i].ErrorText;
                }

                throw new ApplicationException(errorMsg);
            }

            var assembly = compilerResults.CompiledAssembly;

            // *** Retrieve an obj ref – generic type only

            return assembly.CreateInstance("Wagner.InsertStatementGenerator.ColumnValues");
        }
    }
}