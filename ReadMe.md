---
facet-complexity: 1
facet-status: active
facet-layer: infrastructure
concepts:
  - Technology\IT\Software\Logging.md
  - Technology\IT\Software\SW~Programming\Prog~Language\Prog~Paradigm\Prog~Functional\Prog~Rust\Rust~Testing.md
tags:
  - code/nunit_test
  - code/structured_logging
description: "NUnit tests for the `Logg` structured-logging facade, verifying positional/named log parsing and Serilog semantic-log event emission."
digest:
  local-classes:
    ChangedVariables:
      mtime: "2026-08-18T17:17:16Z"
      digest: "77d93cf72221d129132dc1a0eb8fd8c3f8c8cd462302625a599ce8d7ae60fdb2"
    LogTest:
      mtime: "2026-08-18T17:17:16Z"
      digest: "94888f5380c5428a3a4d5addb7594ff85b12a72aa94825922d7085d970429207"
    Program:
      mtime: "2026-08-18T17:17:16Z"
      digest: "517e8a72513e72240f648456893a56215d3fe4197ff08d36aabc26593c964856"
    SemanticLogTests:
      mtime: "2026-08-18T17:17:16Z"
      digest: "7185aea50ffaf2f162f5f170ec028363a28737ee6439205771bca8e412cf66ca"
  folders: {}
related:
  - path: ../_Matthias/Code/NET/_SpocWeb.Root/_std/SpocWeb.IMaths/tensors/tests
    shared-tags: [code/nunit_test]
  - path: ../_Matthias/Code/NET/_root/_projects/db.vm/Coll/Tests
    shared-tags: [code/nunit_test]
dv_has_:
  sub_:
    folders: 0
    files: 16
    units: 4
    facet_:
      layer_:
        infrastructure: 4
      status_:
        active: 3
        partial: 1
      complexity_:
        "1": 2
        "2": 2
    tag_:
      code_:
        nunit_test: 2
        test_fixture: 2
        data_container: 1
        entry_point: 1
        integration_test: 1
    concept_:
      "Technology\\IT\\Software\\SW~Programming\\Prog~Language\\Prog~Paradigm\\Prog~Functional\\Prog~Rust\\Rust~Testing.md": 4
      "Technology\\IT\\Software\\Logging.md": 3
      observability: 1
has_sub_folders: 0
has_sub_files: 16
has_sub_units: 4
has_sub_facet_layer_infrastructure: 4
has_sub_facet_status_active: 3
has_sub_facet_status_partial: 1
has_sub_facet_complexity_1: 2
has_sub_facet_complexity_2: 2
has_sub_tag_code_nunit_test: 2
has_sub_tag_code_test_fixture: 2
has_sub_tag_code_data_container: 1
has_sub_tag_code_entry_point: 1
has_sub_tag_code_integration_test: 1
has_sub_concept_technology_it_software_sw_programming_prog_language_prog_paradigm_prog_functional_prog_rust_rust_testing_md: 4
has_sub_concept_technology_it_software_logging_md: 3
has_sub_concept_observability: 1
---
# SpocWeb.Logging.Tests

NUnit tests for the `Logg` structured-logging facade,
verifying positional/named log parsing and Serilog
semantic-log event emission.

## Dependencies

### Project References

- [SpocWeb.root.logging](../SpocWeb.root.logging/ReadMe.md)

### NuGet Packages

- `coverlet.collector`
- `Microsoft.Extensions.Logging`
- `Microsoft.NET.Test.Sdk`
- `NUnit`
- `NUnit3TestAdapter`
- `Serilog`
- `Serilog.Extensions.Logging`
- `Serilog.Sinks.TestCorrelator`
- `Shouldly`

## Architecture

```mermaid
flowchart TD
  subgraph SpocWeb.Logging.Tests
    LogTest["LogTest (parse tests)"]
    SemanticLogTests["SemanticLogTests (integration tests)"]
    ChangedVariables["ChangedVariables (test-data fixture)"]
    Logg["Logg extension (SUT)"]
    LogParse["Log.Parse (SUT)"]
    Serilog["Serilog TestCorrelator sink"]

    LogTest -->|"calls"| LogParse
    linkStyle 0 opacity:1

    LogTest -->|"uses"| ChangedVariables
    linkStyle 1 opacity:1

    SemanticLogTests -->|"calls"| Logg
    linkStyle 2 opacity:1

    SemanticLogTests -->|"asserts via"| Serilog
    linkStyle 3 opacity:1
  end
```

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
| [Program](Program.cs) | Application entry point for the SpocWeb.Logging.Tests project. |
| [SemanticLogTests](SemanticLogTests.cs) | Semantic log tests. |
