using System;
using System.Collections.Generic;
using AgentSmith.NamingConventions;
using AgentSmith.Options;

namespace AgentSmith
{
    public class SampleNameSettingsFactory
    {
        public static NamingConventionSettings GetSampleNameSettings()
        {
            NamingConventionSettings settings = new NamingConventionSettings();
            List<NamingConventionRule> rules = new List<NamingConventionRule>();
            
            NamingConventionRule r3 = new NamingConventionRule();
            r3.Description = "Classes should be named in Pascal";
            r3.Matches = new Match[] { new Match(Declaration.Class) };
            r3.Rule = RuleKind.Pascal;
            rules.Add(r3);

            NamingConventionRule r1 = new NamingConventionRule();
            r1.Description = "Class should not have underscore character";
            r1.Matches = new Match[] { new Match(Declaration.Class) };
            r1.Rule = RuleKind.NotMatchesRegex;
            r1.Regex = @"(?<remove>_)";
            rules.Add(r1);
           
            NamingConventionRule r31 = new NamingConventionRule();
            r31.Description = "Acronyms should not have more than 3 characters.";
            r31.Matches =new Match[]{new Match(Declaration.Any)};
            r31.NotMatches = new Match[] { new Match(Declaration.Constant) };
            r31.Rule = RuleKind.MatchesRegex;            
            r31.Regex = "^(XML|SQL|CQGFW|([A-Z]{0,4}[^A-Z]|[A-Z]{0,3}$)|[^A-Z]*)*$";
            rules.Add(r31);           
            
            NamingConventionRule r4 = new NamingConventionRule();
            r4.Description = "TestFixtures should end with Test.";            
            r4.Matches = new Match[]{new Match(Declaration.Class, AccessLevels.Any, null, "NUnit.Framework.TestFixtureAttribute")};
            r4.MustHaveSuffixes = new string[] {"Test"};
            r4.MustNotHavePrefixes = new string[] {"Test"};
            rules.Add(r4);

            
            NamingConventionRule r5 = new NamingConventionRule();
            r5.Description = "Private fields should be underscore camel.";            
            r5.Matches = new Match[]{new Match(Declaration.Field, AccessLevels.Private) };
            r5.MustHavePrefixes = new string[] {"_"};
            r5.Rule = RuleKind.Camel;
            rules.Add(r5);
            
            NamingConventionRule r6 = new NamingConventionRule();
            r6.Description = "Protected or internal field should be in camel.";          
            r6.Matches = new Match[]{new Match(Declaration.Field, AccessLevels.Protected | AccessLevels.Internal | AccessLevels.ProtectedInternal) };
            r6.MustNotHavePrefixes = new string[]{"_"};
            r6.Rule = RuleKind.Camel;
            rules.Add(r6);

            
            NamingConventionRule r7 = new NamingConventionRule();
            r7.Description = "Public fields should be in pascal.";            
            r7.Matches = new Match[]{new Match(Declaration.Field, AccessLevels.Public) };
            r7.MustNotHavePrefixes = new string[]{"_"};
            r7.Rule = RuleKind.Pascal;
            rules.Add(r7);

            NamingConventionRule r8 = new NamingConventionRule();
            r8.Description = "Public properties should be in pascal.";            
            r8.Matches = new Match[]{new Match(Declaration.Property, AccessLevels.Public) };
            r8.Rule = RuleKind.Pascal;
            rules.Add(r8);
           
            NamingConventionRule r9 = new NamingConventionRule();
            r9.Description = "NonPublic properties should be in camel.";
            r9.Matches = new Match[]{new Match(Declaration.Property, AccessLevels.Any)};
            r9.NotMatches = new Match[]{new Match(Declaration.Property, AccessLevels.Public)};            
            r9.Rule = RuleKind.Camel;
            rules.Add(r9);

            NamingConventionRule r10 = new NamingConventionRule();
            r10.Description = "Function parameters should be in camel.";
            r10.Matches = new Match[] { new Match(Declaration.Parameter) };
            r10.Rule = RuleKind.Camel;
            rules.Add(r10);
            
            
            NamingConventionRule r11 = new NamingConventionRule();
            r11.Description = "Variable should be declared in camel.";
            r11.Matches = new Match[]{new Match(Declaration.Variable)};
            r11.Rule = RuleKind.Camel;
            rules.Add(r11);

            NamingConventionRule r12 = new NamingConventionRule();
            r12.Description = "Private method should be in camel.";
            r12.Matches = new Match[]{new Match(Declaration.Method, AccessLevels.Private)};            
            r12.Rule = RuleKind.Camel;
            rules.Add(r12);

            NamingConventionRule r13 = new NamingConventionRule();
            r13.Description = "Non Private method should be in pascal.";            
            r13.Matches = new Match[]{new Match(Declaration.Method)};
            r13.NotMatches = new Match[]{new Match(Declaration.Method, AccessLevels.Private)};
            r13.Rule = RuleKind.Pascal;
            rules.Add(r13);

            NamingConventionRule r14 = new NamingConventionRule();
            r14.Description = "Test methods should start with Test.";
            r14.Matches = new Match[] {new Match(Declaration.Method, AccessLevels.Any, null, "NUnit.Framework.TestAttribute")};            
            r14.MustHavePrefixes = new string[] {"Test"};
            r14.MustNotHaveSuffixes = new string[] {"Test"};
            rules.Add(r14);
            
            NamingConventionRule r15 = new NamingConventionRule();
            r15.Description = "Constants should be in capital.";
            r15.Matches = new Match[] {new Match(Declaration.Constant)};
            r15.Rule = RuleKind.UpperCase;
            rules.Add(r15);

            NamingConventionRule r16 = new NamingConventionRule();
            r16.Description = "Enumerations should be in Pascal.";
            r16.Matches = new Match[]{new Match(Declaration.Enum)};        
            r16.Rule = RuleKind.Pascal;
            rules.Add(r16);

            NamingConventionRule r17 = new NamingConventionRule();
            r17.Description = "Enumerations should not end with Enum.";
            r17.Matches = new Match[]{new Match(Declaration.Enum)};
            r17.MustNotHaveSuffixes = new string[] {"Enum"};
            rules.Add(r17);

            NamingConventionRule r18 = new NamingConventionRule();
            r18.Description = "Enumeration values should be in pascal.";
            r18.Matches = new Match[]{new Match(Declaration.EnumerationMember)};
            r18.Rule = RuleKind.Pascal;
            rules.Add(r18);

            NamingConventionRule r25 = new NamingConventionRule();
            r25.Description = "Do not name enumerations reserved.";
            r25.Matches = new Match[]{new Match(Declaration.EnumerationMember)};
            r25.Rule = RuleKind.NotMatchesRegex;
            r25.Regex = "(?<remove>(reserved|Reserved))";
            rules.Add(r25);

            NamingConventionRule r26 = new NamingConventionRule();
            r26.Description = "Event should not have Before or After prefix.";
            r26.Matches = new Match[]{new Match(Declaration.Event)};
            r26.MustNotHavePrefixes = new string[]{"Before", "After"};
            rules.Add(r26);
                       
            NamingConventionRule r27 = new NamingConventionRule();
            r27.Description = "Flags enums should have plural names";
            r27.Matches = new Match[]{new Match(Declaration.Enum, AccessLevels.Any, null, "System.FlagsAttribute")  };            
            r27.MustHaveSuffixes = new string[]{"s"};
            rules.Add(r27);
            
            NamingConventionRule r32 = new NamingConventionRule();
            r32.Description = "enums that are not flags should not have plural names";
            r32.Matches = new Match[]{new Match(Declaration.Enum)};
            r32.NotMatches = new Match[] { new Match(Declaration.Enum, AccessLevels.Any, null,  "System.FlagsAttribute") };
            r32.MustNotHaveSuffixes = new string[] { "s" };
            rules.Add(r32);


            NamingConventionRule r29 = new NamingConventionRule();
            r29.Description = "Attribute should end with Attribute.";         
            r29.Matches = new Match[]{new Match(Declaration.Class, AccessLevels.Any, "System.Attribute")};
            r29.MustHaveSuffixes = new string[] { "Attribute" };
            rules.Add(r29);

            NamingConventionRule r40 = new NamingConventionRule();
            r40.Description = "EventArgs should end with EventArgs.";
            r40.Matches = new Match[] { new Match(Declaration.Class, AccessLevels.Any, "System.EventArgs") };
            r40.MustHaveSuffixes = new string[] { "EventArgs" };
            rules.Add(r40);

            NamingConventionRule r41 = new NamingConventionRule();
            r41.Description = "Exceptions should end with Exception.";
            r41.Matches = new Match[] { new Match(Declaration.Class, AccessLevels.Any, "System.Exception") };
            r41.MustHaveSuffixes = new string[] { "Exception" };
            rules.Add(r41);

            NamingConventionRule r42 = new NamingConventionRule();
            r42.Description = "Collections should end with Collection.";
            r42.Matches = new Match[] { new Match(Declaration.Class, AccessLevels.Any, "System.Collections.ICollection"),
                                        new Match(Declaration.Class, AccessLevels.Any, "System.Collections.IEnumerable"),
                                        new Match(Declaration.Class, AccessLevels.Any, "System.Collections.Generic.ICollection")};
            r42.NotMatches = new Match[] { new Match(Declaration.Class, AccessLevels.Any, "System.Collections.Stack"),
                                           new Match(Declaration.Class, AccessLevels.Any, "System.Collections.Queue")};
            r42.MustHaveSuffixes = new string[] { "Collection" };
            rules.Add(r42);

            NamingConventionRule r43 = new NamingConventionRule();
            r43.Description = "Dictionary should end with Dictionary.";
            r43.Matches = new Match[] { new Match(Declaration.Class, AccessLevels.Any, "System.Collections.IDictionary"),
                                        new Match(Declaration.Class, AccessLevels.Any, "System.Collections.Generic.IDictionary")};
            r43.MustHaveSuffixes = new string[] { "Dictionary" };
            rules.Add(r43);

            NamingConventionRule r44 = new NamingConventionRule();
            r44.Description = "Queue should end with Collection or Queue.";
            r44.Matches = new Match[] { new Match(Declaration.Class, AccessLevels.Any, "System.Collections.Queue"),};
            r44.MustHaveSuffixes = new string[] { "Collection", "Queue" };
            rules.Add(r44);

            NamingConventionRule r45 = new NamingConventionRule();
            r45.Description = "Stack should end with Collection or Stack.";
            r45.Matches = new Match[] { new Match(Declaration.Class, AccessLevels.Any, "System.Collections.Stack"), };
            r45.MustHaveSuffixes = new string[] { "Collection", "Stack" };
            rules.Add(r45);
            
            //......................

            NamingConventionRule r19 = new NamingConventionRule();
            r19.Description = "Events should be in pascal.";
            r19.Matches = new Match[]{new Match(Declaration.Event)};
            r19.Rule = RuleKind.Pascal;
            rules.Add(r19);

            NamingConventionRule r20 = new NamingConventionRule();
            r20.Description = "Interfaces should be in Camel and start with I.";
            r20.Matches = new Match[]{new Match(Declaration.Interface)};
            r20.MustHavePrefixes = new string[]{"I"};
            r20.Rule = RuleKind.Pascal;
            rules.Add(r20);

            NamingConventionRule r21 = new NamingConventionRule();
            r21.Description = "Namespaces should be in Pascal.";
            r21.Matches = new Match[]{new Match(Declaration.Namespace)};            
            r21.Rule = RuleKind.Pascal;
            rules.Add(r21);

            settings.Rules = rules.ToArray();
            return settings;
        }
    }
}