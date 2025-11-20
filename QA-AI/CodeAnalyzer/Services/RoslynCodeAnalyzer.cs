using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using CodeAnalyzer.Models;

namespace CodeAnalyzer.Services
{
    /// <summary>
    /// Analyzes C# code using Roslyn
    /// </summary>
    public class RoslynCodeAnalyzer
    {
        /// <summary>
        /// Analyzes a C# file and extracts code context
        /// </summary>
        public CodeContext AnalyzeFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File not found: {filePath}");
            }

            var sourceCode = File.ReadAllText(filePath);
            return AnalyzeSourceCode(sourceCode, filePath);
        }

        /// <summary>
        /// Analyzes C# source code and extracts code context
        /// </summary>
        public CodeContext AnalyzeSourceCode(string sourceCode, string filePath = null)
        {
            var context = new CodeContext
            {
                FilePath = filePath,
                SourceCode = sourceCode,
                IsDesignerFile = filePath != null && filePath.EndsWith(".Designer.cs", StringComparison.OrdinalIgnoreCase)
            };

            try
            {
                var syntaxTree = CSharpSyntaxTree.ParseText(sourceCode);
                var root = syntaxTree.GetRoot();

                // Extract namespace
                var namespaceDeclaration = root.DescendantNodes().OfType<NamespaceDeclarationSyntax>().FirstOrDefault();
                if (namespaceDeclaration != null)
                {
                    context.Namespace = namespaceDeclaration.Name.ToString();
                }

                // Extract using statements
                var usings = root.DescendantNodes().OfType<UsingDirectiveSyntax>();
                context.Usings = usings.Select(u => u.Name.ToString()).ToList();

                // Extract classes
                var classDeclarations = root.DescendantNodes().OfType<ClassDeclarationSyntax>();
                foreach (var classDecl in classDeclarations)
                {
                    var classInfo = ExtractClassInfo(classDecl, root);
                    context.Classes.Add(classInfo);
                }

                // Extract interfaces
                var interfaceDeclarations = root.DescendantNodes().OfType<InterfaceDeclarationSyntax>();
                foreach (var interfaceDecl in interfaceDeclarations)
                {
                    var interfaceInfo = ExtractInterfaceInfo(interfaceDecl, root);
                    context.Interfaces.Add(interfaceInfo);
                }

                // Check if it's a Windows Forms file
                context.IsWindowsFormsFile = IsWindowsFormsFile(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error analyzing code: {ex.Message}");
                // Return partial context if analysis fails
            }

            return context;
        }

        /// <summary>
        /// Extracts information about a class
        /// </summary>
        private ClassInfo ExtractClassInfo(ClassDeclarationSyntax classDecl, SyntaxNode root)
        {
            var classInfo = new ClassInfo
            {
                Name = classDecl.Identifier.ValueText,
                FullName = classDecl.Identifier.ValueText,
                IsPublic = classDecl.Modifiers.Any(m => m.IsKind(SyntaxKind.PublicKeyword)),
                IsStatic = classDecl.Modifiers.Any(m => m.IsKind(SyntaxKind.StaticKeyword)),
                IsAbstract = classDecl.Modifiers.Any(m => m.IsKind(SyntaxKind.AbstractKeyword)),
                IsPartial = classDecl.Modifiers.Any(m => m.IsKind(SyntaxKind.PartialKeyword)),
                StartLine = classDecl.GetLocation().GetLineSpan().StartLinePosition.Line + 1,
                EndLine = classDecl.GetLocation().GetLineSpan().EndLinePosition.Line + 1
            };

            // Extract base type
            if (classDecl.BaseList != null)
            {
                var baseType = classDecl.BaseList.Types.FirstOrDefault();
                if (baseType != null)
                {
                    classInfo.BaseType = baseType.Type.ToString();
                }
            }

            // Extract methods
            var methods = classDecl.DescendantNodes().OfType<MethodDeclarationSyntax>();
            foreach (var method in methods)
            {
                var methodInfo = ExtractMethodInfo(method);
                classInfo.Methods.Add(methodInfo);
            }

            // Extract properties
            var properties = classDecl.DescendantNodes().OfType<PropertyDeclarationSyntax>();
            foreach (var property in properties)
            {
                var propertyInfo = ExtractPropertyInfo(property);
                classInfo.Properties.Add(propertyInfo);
            }

            // Extract fields
            var fields = classDecl.DescendantNodes().OfType<FieldDeclarationSyntax>();
            foreach (var field in fields)
            {
                foreach (var variable in field.Declaration.Variables)
                {
                    classInfo.Fields.Add($"{field.Declaration.Type} {variable.Identifier.ValueText}");
                }
            }

            // Extract dependencies (simplified - look for type references)
            var typeReferences = classDecl.DescendantNodes().OfType<IdentifierNameSyntax>()
                .Select(t => t.Identifier.ValueText)
                .Distinct()
                .Where(t => char.IsUpper(t[0])) // Likely a type name
                .ToList();
            classInfo.Dependencies.AddRange(typeReferences);

            // Extract source code
            classInfo.SourceCode = classDecl.ToFullString();

            return classInfo;
        }

        /// <summary>
        /// Extracts information about an interface
        /// </summary>
        private ClassInfo ExtractInterfaceInfo(InterfaceDeclarationSyntax interfaceDecl, SyntaxNode root)
        {
            var interfaceInfo = new ClassInfo
            {
                Name = interfaceDecl.Identifier.ValueText,
                FullName = interfaceDecl.Identifier.ValueText,
                IsPublic = interfaceDecl.Modifiers.Any(m => m.IsKind(SyntaxKind.PublicKeyword)),
                StartLine = interfaceDecl.GetLocation().GetLineSpan().StartLinePosition.Line + 1,
                EndLine = interfaceDecl.GetLocation().GetLineSpan().EndLinePosition.Line + 1
            };

            // Extract methods
            var methods = interfaceDecl.DescendantNodes().OfType<MethodDeclarationSyntax>();
            foreach (var method in methods)
            {
                var methodInfo = ExtractMethodInfo(method);
                interfaceInfo.Methods.Add(methodInfo);
            }

            // Extract properties
            var properties = interfaceDecl.DescendantNodes().OfType<PropertyDeclarationSyntax>();
            foreach (var property in properties)
            {
                var propertyInfo = ExtractPropertyInfo(property);
                interfaceInfo.Properties.Add(propertyInfo);
            }

            return interfaceInfo;
        }

        /// <summary>
        /// Extracts information about a method
        /// </summary>
        private MethodInfo ExtractMethodInfo(MethodDeclarationSyntax method)
        {
            var methodInfo = new MethodInfo
            {
                Name = method.Identifier.ValueText,
                ReturnType = method.ReturnType?.ToString() ?? "void",
                IsPublic = method.Modifiers.Any(m => m.IsKind(SyntaxKind.PublicKeyword)),
                IsStatic = method.Modifiers.Any(m => m.IsKind(SyntaxKind.StaticKeyword)),
                IsAsync = method.Modifiers.Any(m => m.IsKind(SyntaxKind.AsyncKeyword)),
                IsVirtual = method.Modifiers.Any(m => m.IsKind(SyntaxKind.VirtualKeyword)),
                IsAbstract = method.Modifiers.Any(m => m.IsKind(SyntaxKind.AbstractKeyword)),
                IsOverride = method.Modifiers.Any(m => m.IsKind(SyntaxKind.OverrideKeyword)),
                StartLine = method.GetLocation().GetLineSpan().StartLinePosition.Line + 1,
                EndLine = method.GetLocation().GetLineSpan().EndLinePosition.Line + 1
            };

            // Extract parameters
            if (method.ParameterList != null)
            {
                foreach (var parameter in method.ParameterList.Parameters)
                {
                    var paramInfo = new ParameterInfo
                    {
                        Name = parameter.Identifier.ValueText,
                        Type = parameter.Type?.ToString() ?? "object",
                        HasDefaultValue = parameter.Default != null
                    };

                    if (paramInfo.HasDefaultValue)
                    {
                        paramInfo.DefaultValue = parameter.Default.Value.ToString();
                    }

                    methodInfo.Parameters.Add(paramInfo);
                }
            }

            // Extract body
            if (method.Body != null)
            {
                methodInfo.Body = method.Body.ToFullString();
            }
            else if (method.ExpressionBody != null)
            {
                methodInfo.Body = method.ExpressionBody.ToFullString();
            }

            // Extract XML documentation
            var leadingTrivia = method.GetLeadingTrivia();
            var docComment = leadingTrivia
                .Where(t => t.IsKind(SyntaxKind.SingleLineDocumentationCommentTrivia) ||
                           t.IsKind(SyntaxKind.MultiLineDocumentationCommentTrivia))
                .FirstOrDefault();

            if (docComment != null)
            {
                methodInfo.Documentation = docComment.ToFullString();
            }

            return methodInfo;
        }

        /// <summary>
        /// Extracts information about a property
        /// </summary>
        private PropertyInfo ExtractPropertyInfo(PropertyDeclarationSyntax property)
        {
            var propertyInfo = new PropertyInfo
            {
                Name = property.Identifier.ValueText,
                Type = property.Type?.ToString() ?? "object",
                IsPublic = property.Modifiers.Any(m => m.IsKind(SyntaxKind.PublicKeyword)),
                IsStatic = property.Modifiers.Any(m => m.IsKind(SyntaxKind.StaticKeyword))
            };

            if (property.AccessorList != null)
            {
                propertyInfo.HasGetter = property.AccessorList.Accessors.Any(a => a.IsKind(SyntaxKind.GetAccessorDeclaration));
                propertyInfo.HasSetter = property.AccessorList.Accessors.Any(a => a.IsKind(SyntaxKind.SetAccessorDeclaration));
            }

            // Extract XML documentation
            var leadingTrivia = property.GetLeadingTrivia();
            var docComment = leadingTrivia
                .Where(t => t.IsKind(SyntaxKind.SingleLineDocumentationCommentTrivia) ||
                           t.IsKind(SyntaxKind.MultiLineDocumentationCommentTrivia))
                .FirstOrDefault();

            if (docComment != null)
            {
                propertyInfo.Documentation = docComment.ToFullString();
            }

            return propertyInfo;
        }

        /// <summary>
        /// Checks if the file is a Windows Forms file
        /// </summary>
        private bool IsWindowsFormsFile(CodeContext context)
        {
            // Check for Windows Forms namespaces
            if (context.Usings.Any(u => u.Contains("System.Windows.Forms") || u.Contains("Windows.Forms")))
            {
                return true;
            }

            // Check if any class inherits from Form
            if (context.Classes.Any(c => c.BaseType != null && 
                (c.BaseType.Contains("Form") || c.BaseType.Contains("UserControl") || c.BaseType.Contains("Control"))))
            {
                return true;
            }

            return false;
        }
    }
}

