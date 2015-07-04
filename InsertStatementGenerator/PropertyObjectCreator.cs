#region using
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.CSharp;
using Microsoft.SqlServer.Management.Smo;
#endregion using

namespace Wagner.InsertStatementGenerator
{
    class PropertyObjectCreator
    {
        public static object CreatePropertyObjectFromTable( Table table )
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

            StringBuilder generatedCode = new StringBuilder( 50000 );

            foreach( Column column in table.Columns )
            {
                generatedCode.AppendFormat( "public {0} {1};", column.DataType.Name, column.Name );
                generatedCode.Append( Environment.NewLine );
            }

            string codeToCompile = codeTemplate.Replace( "<% code %>", generatedCode.ToString() );
            
            CodeDomProvider compiler = CodeDomProvider.CreateProvider( "CSharp" );
            CompilerParameters compilerParameters = new CompilerParameters();

            // Add any referenced assemblies
            compilerParameters.ReferencedAssemblies.Add( "System.dll" );
            // Load the resulting assembly into memory
            compilerParameters.GenerateInMemory = false;

            // Now compile the whole thing
            CompilerResults compilerResults =
                compiler.CompileAssemblyFromSource( compilerParameters, codeToCompile );

            if( compilerResults.Errors.HasErrors )
            {
                string errorMsg = compilerResults.Errors.Count.ToString() + " Errors:";
                for( int i = 0; i < compilerResults.Errors.Count; i++ )
                    errorMsg = errorMsg + "\r\nLine: " +
                                 compilerResults.Errors[i].Line.ToString() + " - " +
                                 compilerResults.Errors[i].ErrorText;

                throw new ApplicationException( errorMsg );
            }

            Assembly assembly = compilerResults.CompiledAssembly;

            // *** Retrieve an obj ref – generic type only
            
            return assembly.CreateInstance( "Wagner.InsertStatementGenerator.ColumnValues" );
        }
    }
}
