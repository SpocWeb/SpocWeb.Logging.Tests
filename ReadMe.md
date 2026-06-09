---
digest:
  local-classes:
    ChangedVariables:
      mtime: "2026-06-09T16:27:52Z"
      digest: "77d93cf72221d129132dc1a0eb8fd8c3f8c8cd462302625a599ce8d7ae60fdb2"
    LogTest:
      mtime: "2026-06-09T16:27:52Z"
      digest: "67164af70d69cc9dfffbfa6beaf1ff455ae7e8d6939c24ba7b0868b95e20ae41"
    Program:
      mtime: "2026-06-09T16:27:52Z"
      digest: "d20518cf5542f1185a0d481d2c8a2f9eaa88e3d91da31a3343ab2d08adf13926"
    SemanticLogTests:
      mtime: "2026-06-09T16:27:52Z"
      digest: "fe3d4a00f7736390501df078e88a8bdfa6bea62e9c6cdca7dee12d0203649943"
  folders: {}
---
# SpocWeb.Logging.Tests
<!-- digest-map
local-classes:
  ChangedVariables: mtime=2026-05-15T20:55:22Z digest=a62af09d34a7fc8fcd1bcf3fc2f287a193777ead6e78c00f1979c3df92fa8316
  LogTest: mtime=2026-05-15T20:55:32Z digest=9868bef24bec4ba8cbc06882f3c61128e618d418dfac4091cc7236c2ded6fa11
  Program: mtime=2025-05-02T17:50:18Z digest=e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855
  SemanticLogTests: mtime=2026-05-15T20:55:56Z digest=0ec98ff147d9aaffa47e6e8f6852c9a5e66f8dce09dcf5c4436516408e357153
folders:
folder_digest: 669916c0c604fe0a5e5bfcf34882f6b5a174645d0707f7575d55e13cd1e8f5b7
folder_mtime: 2026-05-15T20:55:56Z
-->

NUnit tests for the `Logg` structured-logging facade,
verifying positional/named log parsing and Serilog
semantic-log event emission.

## Test Classes

| Class | What it tests |
|---|---|
| `LogTest` | Verifies that `Log.Parse` correctly extracts positional (string-interpolation) and named structured-log tokens. |
| `SemanticLogTests` | Integration tests using the Serilog `TestCorrelator` sink to assert that structured events carry correct property names, levels, exceptions, and context prefixes. |
| `ChangedVariables` | Test-data record carrying named fields used as structured-log values in `LogTest`. |

## Classes

| Class | Responsibility |
|---|---|
| [ChangedVariables](ChangedVariables.cs) | Data container holding field and value names used as structured-log test fixtures. |
| [LogTest](LogTest.cs) | NUnit tests verifying that Parse correctly handles  both positional (string-interpolation) and named structured-log patterns. |
| [Program](Program.cs) | Application entry point for the SpocWeb. |
| [SemanticLogTests](SemanticLogTests.cs) | TODO: LLM |
