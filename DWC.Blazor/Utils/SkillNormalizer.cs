using System;
using System.Collections.Generic;
using System.Linq;

namespace DWC.Blazor.Utils
{
    public static class SkillNormalizer
    {
        private static Dictionary<string, string> _normalizationMap;
        
        private static Dictionary<string, string> NormalizationMap
        {
            get
            {
                if (_normalizationMap == null)
                {
                    _normalizationMap = new Dictionary<string, string>
                    {
                        { "javascript", "JavaScript" },
                        { "python", "Python" },
                        { "java", "Java" },
                        { "ruby", "Ruby" },
                        { "php", "PHP" },
                        { "go", "Go" },
                        { "swift", "Swift" },
                        { "kotlin", "Kotlin" },
                        { "elixir", "Elixir" },
                        
                        // Node
                        { "node", "Node.js" },
                        { "nodejs", "Node.js" },
                        { "node.js", "Node.js" },
                        
                        // Vue
                        { "vue", "Vue.js" },
                        { "vuejs", "Vue.js" },
                        { "vue.js", "Vue.js" },
                        { "vue js", "Vue.js" },
                        
                        // React
                        { "react", "React" },
                        { "reactjs", "React" },
                        { "react.js", "React" },
                        { "react js", "React" },
                        
                        // React Native
                        { "react native", "React Native" },
                        { "rn", "React Native" },
                        
                        // Angular
                        { "angular", "Angular" },
                        { "angularjs", "Angular" },
                        { "angular7", "Angular" },
                        
                        // TypeScript
                        { "typescript", "TypeScript" },
                        { "ts", "TypeScript" },
                        { "ts/js", "TypeScript" },
                        
                        // C#
                        { "c#", "C#" },
                        { "csharp", "C#" },
                        
                        // .NET
                        { ".net", ".NET" },
                        { "dotnet", ".NET" },
                        { ".net(c#)", ".NET" },
                        { ".net (c#)", ".NET" },
                        
                        // .NET Core
                        { ".net core", ".NET Core" },
                        { "dotnet core", ".NET Core" },
                        { ".net core c#", ".NET Core" },
                        
                        // ASP.NET
                        { "asp.net", "ASP.NET" },
                        { "aspnet", "ASP.NET" },
                        { "asp.net mvc", "ASP.NET" },
                        { "asp.net (c# + vb)", "ASP.NET" },
                        
                        // ASP.NET Core
                        { "asp.net core", "ASP.NET Core" },
                        
                        // Xamarin
                        { "xamarin", "Xamarin" },
                        { "xamarin.forms", "Xamarin" },
                        { "xamarin.android", "Xamarin" },
                        
                        // SQL
                        { "sql", "SQL Server" },
                        { "tsql", "T-SQL" },
                        { "t-sql", "T-SQL" },
                        { "sql server", "SQL Server" },
                        { "sql server/transact-sql", "SQL Server" },
                        
                        // Cloud
                        { "azure", "Azure" },
                        { "aws", "AWS" },
                        
                        // DevOps
                        { "docker", "Docker" },
                        { "kubernetes", "Kubernetes" },
                        { "k8s", "Kubernetes" },
                        { "terraform", "Terraform" },
                        
                        // Frameworks
                        { "laravel", "Laravel" },
                        { "laravel framework", "Laravel" },
                        { "php(laravel)", "Laravel" },
                        { "ruby on rails", "Ruby on Rails" },
                        
                        // Mobile
                        { "ios", "iOS" },
                        { "android", "Android" },
                        { "flutter", "Flutter" },
                        
                        // ML/AI
                        { "tensorflow", "TensorFlow" },
                        
                        // Game Dev
                        { "unity", "Unity" },
                        { "godot", "Godot" },
                        
                        // Blazor
                        { "blazor", "Blazor" },

                        // Bot Framework
                        { "bot framework", "Bot Framework" },

                        // Visual Studio App Center
                        { "appcenter", "Visual Studio App Center" },

                        // Continuous integration
                        { "ci", "Continuous integration" },

                        // Azure Cognitive Services
                        { "cognitive services", "Azure Cognitive Services" },

                        // CSS
                        { "css", "CSS" },

                        // The Elastic Stack (ELK)
                        { "elk", "ELK" },

                        // Google Cloud
                        { "gcp", "GCP" },

                        // Java 8
                        { "java 8+", "Java" },

                        // JavaScrpit/Js
                        { "javascrpit", "JavaScript" },
                        { "js", "JavaScript" },

                        // MVC
                        { "mvc", "MVC" },

                        // NativeScript
                        { "nativescript", "NativeScript" },

                        // NestJS
                        { "nestjs", "NestJS" },

                        // NoSQL
                        { "no/sql", "NoSQL" },

                        // OpenShift
                        { "openshift", "OpenShift" },

                        // Raspberry Pi
                        { "raspberry pi", "Raspberry Pi" },

                        // RESTFul APIs
                        { "restful apis", "RESTFul APIs" },

                        // Spring Boot
                        { "spring boot", "Spring Boot" },

                        // Spring Framework
                        { "spring framework", "Spring Framework" },
                        
                        // Umbraco
                        { "umbracocms", "Umbraco" },

                        //  Visual Basic.NET
                        { "vb.net", "Visual Basic .NET" },

                        // WordPress
                        { "wordpress (with seo)", "WordPress" },

                        // WPF
                        { "wpf", "WPF" },

                        // Oracle SQL
                        { "oracle sql", "Oracle SQL" },
                    };
                }
                return _normalizationMap;
            }
        }

        public static string Normalize(string skill)
        {
            if (string.IsNullOrWhiteSpace(skill))
                return string.Empty;

            var trimmedSkill = skill.Trim().ToLowerInvariant();

            if (NormalizationMap.TryGetValue(trimmedSkill, out var normalized))
                return normalized;

            return char.ToUpper(skill[0]) + skill.Substring(1).ToLower();
        }

        public static IEnumerable<string> NormalizeSkills(string skillsString)
        {
            if (string.IsNullOrWhiteSpace(skillsString))
                yield break;

            var skills = skillsString.Split(',', StringSplitOptions.RemoveEmptyEntries);
            
            foreach (var skill in skills)
            {
                var trimmedSkill = skill.Trim();
                
                if (trimmedSkill.Contains(" and ", StringComparison.OrdinalIgnoreCase))
                {
                    var parts = trimmedSkill.Split(new[] { " and " }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var part in parts)
                    {
                        var normalized = Normalize(part.Trim());
                        if (!string.IsNullOrWhiteSpace(normalized))
                        {
                            yield return normalized;
                        }
                    }
                }
                else if (trimmedSkill.Contains(" & ", StringComparison.OrdinalIgnoreCase))
                {
                    var parts = trimmedSkill.Split(new[] { " & " }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var part in parts)
                    {
                        var normalized = Normalize(part.Trim());
                        if (!string.IsNullOrWhiteSpace(normalized))
                        {
                            yield return normalized;
                        }
                    }
                }
                else
                {
                    var normalized = Normalize(trimmedSkill);
                    if (!string.IsNullOrWhiteSpace(normalized))
                    {
                        yield return normalized;
                    }
                }
            }
        }
    }
}
