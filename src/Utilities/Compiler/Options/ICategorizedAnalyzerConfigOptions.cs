﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using Microsoft.CodeAnalysis;

namespace Analyzer.Utilities
{
    using static CategorizedAnalyzerConfigOptionsExtensions;

    /// <summary>
    /// Analyzer configuration options that are parsed into general and specific configuration options.
    /// 
    /// .editorconfig format:
    ///  1) General configuration option:
    ///     (a) "dotnet_code_quality.OptionName = OptionValue"
    ///  2) Specific configuration option:
    ///     (a) "dotnet_code_quality.RuleId.OptionName = OptionValue"
    ///     (b) "dotnet_code_quality.RuleCategory.OptionName = OptionValue"
    ///    
    /// .editorconfig examples to configure API surface analyzed by analyzers:
    ///  1) General configuration option:
    ///     (a) "dotnet_code_quality.api_surface = all"
    ///  2) Specific configuration option:
    ///     (a) "dotnet_code_quality.CA1040.api_surface = public, internal"
    ///     (b) "dotnet_code_quality.Naming.api_surface = public"
    ///  See <see cref="SymbolVisibilityGroup"/> for allowed symbol visibility value combinations.
    /// </summary>
    internal interface ICategorizedAnalyzerConfigOptions
    {
        bool IsEmpty { get; }

        [return: MaybeNull]
        T GetOptionValue<T>(string optionName, SyntaxTree tree, DiagnosticDescriptor rule, TryParseValue<T> tryParseValue, [MaybeNull] T defaultValue, OptionKind kind = OptionKind.DotnetCodeQuality);
    }

    internal static class CategorizedAnalyzerConfigOptionsExtensions
    {
        public delegate bool TryParseValue<T>(string value, [MaybeNull, NotNullWhen(returnValue: true)] out T parsedValue);
    }
}