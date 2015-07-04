using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle( "Insert Statement Generator" )]
[assembly: AssemblyDescription( "Allows the user to generate INSERT statements for the data in existing database tables." )]
#if DEBUG
[assembly : AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration( "Release" )]
#endif
[assembly: AssemblyCompany( "Craig Wagner" )]
[assembly: AssemblyProduct( "Insert Statement Generator" )]
[assembly: AssemblyCopyright( "Copyright  Craig Wagner 2006-2007" )]
[assembly: AssemblyTrademark( "" )]
[assembly: AssemblyCulture( "" )]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible( false )]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid( "b61d64c8-999a-489a-8ec8-c606a4301bd9" )]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
[assembly: AssemblyVersion( "2.1.2.0" )]
[assembly: AssemblyFileVersion( "2.1.2.0" )]
